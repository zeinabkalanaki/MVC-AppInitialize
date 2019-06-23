using AppInitialize.Infrastructures;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppInitialize
{
    //Request life cycle
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = 
                new ConfigurationBuilder()
                    .SetBasePath(env.WebRootPath)
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{env.EnvironmentName}.json"); //EnvironmentName in project prop debug tab in environment variable

            //Note 
            //environment variable in three level difiled project iis od that can over rided by same key
        }
       
        public void ConfigureServices(IServiceCollection services)
        {
            //اضافه کردن کلاس های مورد نیاز به دیآی کانتینر برای استفاده در کانستراکتور
            services.AddSingleton<MyUptimeMeasure>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) //یک بار اجرا شده و یک پایپ لاینی می سازد که درخواست ها از آن رفت و آمد می کنند
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Exeption"); // مدیریت خطا
            }

            // با استفاده از میدل ور ها
            // که یک کانتکس درارند و یک بعدی
            //ترتیب مهم است 

            ////Response Editing middleware : for response editing
            //app.UseMiddleware<ResponseEditingMiddleware>(); //زمانی که جواب آماده است

            ////Request Editing middleware : for request editing
            //app.UseMiddleware<RequestEditingMiddleware>(); // افزودن آیتم به هدر

            ////Shourt Circuiting middelware : for response editing --> کنسل کردن ادامه پردازش
            //app.UseMiddleware<ShortCircuitingMiddleware>(); // عدم سرویس به browser edge

            ////Content generator middleware : for response editing
            //app.UseMiddleware<ContentGeneratorMiddleware>();

            ////میدل وری که بعدی نداشته باشد می شود ترمینال میدل ور
            ////مث میدل ور زیر
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            //با استفاده از مپ برای هر قسمت از می توان میدل ور های جدا نوشت
            //app.Map("/admin", c =>
            //{
            //    c.UseMiddleware<Middleware1>();
            //    c.UseMiddleware<Middleware2>();
            //});

            //app.MapWhen(context => {
            //    محاسبات کانتکس 
            //    context.Request.Path
            //    return true;
            //}, appbuilder => { });

            app.UseStaticFiles();
            
            //به جای کد های بالا میشه از اکتنشن متد استفاده کرد
            app.UseMyMiddleware();

            app.UseMvcWithDefaultRoute();
            
        }
    }
}
