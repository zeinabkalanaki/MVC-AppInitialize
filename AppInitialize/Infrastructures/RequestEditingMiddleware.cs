using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppInitialize.Infrastructures
{
    public class RequestEditingMiddleware
    {
        private RequestDelegate nextDelegate;
        public RequestEditingMiddleware(RequestDelegate next) => nextDelegate = next;
        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Items["EdgeBrowser"] = 
                httpContext.Request.Headers["User-Agent"].Any(v => v.ToLower().Contains("edge"));

            await nextDelegate.Invoke(httpContext);
        }
    }
}
