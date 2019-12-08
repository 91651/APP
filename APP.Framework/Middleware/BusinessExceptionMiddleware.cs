using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace APP.Framework
{
    class BusinessExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BusinessExceptionMiddleware> _logger;
        private readonly BusinessExceptionOption _options;

        public BusinessExceptionMiddleware(RequestDelegate next, ILogger<BusinessExceptionMiddleware> logger, IOptions<BusinessExceptionOption> options)
        {
            _next = next;
            _logger = logger;
            _options = options.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception e)
            {
                if(_options.ResponseType is BusinessExceptionResponseType.Page)
                {
                    throw;
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var message = new ExceptionMessageModel()
                    {
                        Message = "",
                        Exception = e.ToString()
                    };
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(message));
                }
            }
            
        }
    }
    public class BusinessExceptionOption
    {
        public BusinessExceptionResponseType ResponseType { get; set; } = BusinessExceptionResponseType.Page;
    }

    /// <summary>
    /// 异常响应内容格式
    /// </summary>
    public enum BusinessExceptionResponseType
    {
        Json = 0,
        Page = 1,
        WithAccept = 2
    }
    public class ExceptionMessageModel
    {
        public string Message { get; set; }


        public string Exception { get; set; }
    }
    public static class BusinessException
    {
        public static IApplicationBuilder UseBusinessException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BusinessExceptionMiddleware>();
        }
        public static IApplicationBuilder UseBusinessException(this IApplicationBuilder builder, Action<BusinessExceptionOption> options)
        {
            var o = new BusinessExceptionOption();
            options.Invoke(o);
            return builder.UseMiddleware<BusinessExceptionMiddleware>(Options.Create(o));
        }
    }
}
