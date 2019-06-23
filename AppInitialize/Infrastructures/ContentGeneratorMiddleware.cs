using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInitialize.Infrastructures
{
    public class ContentGeneratorMiddleware
    {
        private RequestDelegate nextDelegate; // میدل ور بعدی

        private MyUptimeMeasure _myUptimeMeasure;
        public ContentGeneratorMiddleware(RequestDelegate next, MyUptimeMeasure myUptimeMeasure)
        {
            nextDelegate = next;
            _myUptimeMeasure = myUptimeMeasure;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.ToString().ToLower() == "/middleware")
            {
                await httpContext.Response.WriteAsync(
                $"This is from the content middleware {_myUptimeMeasure.GetUptime()}", Encoding.UTF8);
            }
            else
            {
                await nextDelegate.Invoke(httpContext); // کار تحویل میدل ور بعدی می شود
            }
        }
    }

    public static class ContentGeneratorMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ResponseEditingMiddleware>();
            builder.UseMiddleware<RequestEditingMiddleware>();
            builder.UseMiddleware<ShortCircuitingMiddleware>();
            builder.UseMiddleware<ContentGeneratorMiddleware>();
            builder.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
            return builder;
        }
    }
}
