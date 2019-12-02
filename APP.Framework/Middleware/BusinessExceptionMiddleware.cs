using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace APP.Framework
{
    class BusinessExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<BusinessExceptionMiddleware> _logger;

        public BusinessExceptionMiddleware(RequestDelegate next, ILogger<BusinessExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    public static class BusinessException
    {
        public static IApplicationBuilder UseBusinessException(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<BusinessExceptionMiddleware>();
            return builder;
        }
    }
}
