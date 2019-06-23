using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppInitialize.Infrastructures
{
    public class ResponseEditingMiddleware
    {
        private RequestDelegate nextDelegate;
        public ResponseEditingMiddleware(RequestDelegate next)
        {
            nextDelegate = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            //در هنگام رفت پردازشی ندارد میگه برو بعدی
            await nextDelegate.Invoke(httpContext);
            //بعدی را انجام دارد برگشته حالا این میدل ور پردازش دارد

            if (httpContext.Response.StatusCode == 403)
            {
                await httpContext.Response
                .WriteAsync("Edge not supported", Encoding.UTF8);
            }
            else if (httpContext.Response.StatusCode == 404)
            {
                await httpContext.Response
                .WriteAsync("No content middleware response", Encoding.UTF8);
            }
        }

       
    }
}
