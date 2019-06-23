using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace AppInitialize.Infrastructures
{
    public class ShortCircuitingMiddleware
    {
        private readonly RequestDelegate _next;

        public ShortCircuitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //if (httpContext.Request.Headers["User-Agent"].Any(x => x.ToLower().Contains("edge")))
            if(httpContext.Items["EdgeBrowser"].ToString().ToLower() == "true") //آیتم جدید در درخواست
            {
                //اجازه ادامه ندارد
                httpContext.Response.StatusCode = 403;
            }
            else
            {
                //می تواند ادامه دهد
                await _next.Invoke(httpContext);
            }
        }
    }

}
