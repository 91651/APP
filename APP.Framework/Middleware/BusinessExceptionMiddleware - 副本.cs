using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.Framework
{
    /// <summary>
    /// 异常中间件配置对象
    /// </summary>
    public class CustomExceptionMiddleWareOption
    {
        public CustomExceptionMiddleWareOption(
            CustomExceptionHandleType handleType = CustomExceptionHandleType.JsonHandle,
            IList<PathString> jsonHandleUrlKeys = null,
            string errorHandingPath = "")
        {
            HandleType = handleType;
            JsonHandleUrlKeys = jsonHandleUrlKeys;
            ErrorHandingPath = errorHandingPath;
        }

        /// <summary>
        /// 异常处理方式
        /// </summary>
        public CustomExceptionHandleType HandleType { get; set; }

        /// <summary>
        /// Json处理方式的Url关键字
        /// <para>仅HandleType=Both时生效</para>
        /// </summary>
        public IList<PathString> JsonHandleUrlKeys { get; set; }

        /// <summary>
        /// 错误跳转页面
        /// </summary>
        public PathString ErrorHandingPath { get; set; }
    }

    /// <summary>
    /// 错误处理方式
    /// </summary>
    public enum CustomExceptionHandleType
    {
        JsonHandle = 0,   //Json形式处理
        PageHandle = 1,   //跳转网页处理
        Both = 2          //根据Url关键字自动处理
    }

    class BusinessExceptionMiddleware
    {
        /// 管道请求委托
        /// </summary>
        private RequestDelegate _next;

        /// <summary>
        /// 配置对象
        /// </summary>
        private CustomExceptionMiddleWareOption _option;

        /// <summary>
        /// 需要处理的状态码字典
        /// </summary>
        private IDictionary<int, string> exceptionStatusCodeDic;

        public BusinessExceptionMiddleware(RequestDelegate next, CustomExceptionMiddleWareOption option)
        {
            _next = next;
            _option = option;
            exceptionStatusCodeDic = new Dictionary<int, string>
            {
                { 401, "未授权的请求" },
                { 404, "找不到该页面" },
                { 403, "访问被拒绝" },
                { 500, "服务器发生意外的错误" }
            };
        }

        public async Task Invoke(HttpContext context)
        {
            
            Exception exception = null;
            try
            {
                await _next(context);   //调用管道执行下一个中间件
            }
            catch (Exception ex)
            {
                //context.Response.Clear();
                //context.Response.StatusCode = 500;   //发生未捕获的异常，手动设置状态码
                exception = ex;
            }
            finally
            {

                //var exceptionHandlerFeature = new ExceptionHandlerFeature()
                //{
                //    Error = new Exception()
                //};
                //context.Features.Set<IExceptionHandlerFeature>(exceptionHandlerFeature);
                //var a = context.Features.Get<IExceptionHandlerPathFeature>();
                var b = context.Features.Get<IExceptionHandlerFeature>();
                var d = context.Features.ToList();
                //var c = b.Error is Exception;


                if (exceptionStatusCodeDic.ContainsKey(context.Response.StatusCode) &&
                    !context.Items.ContainsKey("ExceptionHandled"))  //预处理标记
                {
                    var errorMsg = string.Empty;
                    if (context.Response.StatusCode == 500 && exception != null)
                    {
                        errorMsg = $"{exceptionStatusCodeDic[context.Response.StatusCode]}\r\n{(exception.InnerException != null ? exception.InnerException.Message : exception.Message)}";
                    }
                    else
                    {
                        errorMsg = exceptionStatusCodeDic[context.Response.StatusCode];
                    }
                    exception = new Exception(errorMsg);
                }

                if (exception != null)
                {
                    var handleType = _option.HandleType;
                    if (handleType == CustomExceptionHandleType.Both)   //根据Url关键字决定异常处理方式
                    {
                        var requestPath = context.Request.Path;
                        handleType = _option.JsonHandleUrlKeys != null && _option.JsonHandleUrlKeys.Count(
                            k => requestPath.StartsWithSegments(k, StringComparison.CurrentCultureIgnoreCase)) > 0 ?
                            CustomExceptionHandleType.JsonHandle :
                            CustomExceptionHandleType.PageHandle;
                    }

                    if (handleType == CustomExceptionHandleType.JsonHandle)
                        await JsonHandle(context, exception);
                    else
                        await PageHandle(context, exception, _option.ErrorHandingPath);
                }
            }
        }

        /// <summary>
        /// 处理方式：返回Json格式
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private async Task JsonHandle(HttpContext context, Exception ex)
        {
            var apiResponse = new { json = ex };
            var serialzeStr = JsonConvert.SerializeObject(apiResponse);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(serialzeStr, Encoding.UTF8);
        }

        /// <summary>
        /// 处理方式：跳转网页
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private async Task PageHandle(HttpContext context, Exception ex, PathString path)
        {
            context.Items.Add("Exception", ex);
            var originPath = context.Request.Path;
            context.Request.Path = path;   //设置请求页面为错误跳转页面
            try
            {
                await _next(context);
            }
            catch { }
            finally
            {
                context.Request.Path = originPath;   //恢复原始请求页面
            }
        }
    }
    public static class BusinessExceptionExtensions
    {
        public static IApplicationBuilder UseBusinessException(this IApplicationBuilder app, CustomExceptionMiddleWareOption option)
        {
            return app.UseMiddleware<BusinessExceptionMiddleware>(option);
        }
    }
}
