using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;

namespace AppInitialize
{
    public class Program
    {
        //App life cycle
        //آماده سازی و تنظیمات هاست
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Run();
        }

        //public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        //    WebHost.CreateDefaultBuilder(args)
        //        .UseStartup<Startup>();

        public static IWebHost CreateWebHostBuilder(string[] args)
        {
            return new WebHostBuilder()
                .UseKestrel() //وب سرور اختصاصی دات نت کر
                .UseContentRoot(Directory.GetCurrentDirectory()) // برای عکس و کانفیگ
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json",
                                        optional: true,
                                        reloadOnChange: true)
                           .AddJsonFile($"appsettings.{env.EnvironmentName}.json",
                                         optional: true, reloadOnChange: true);

                    if (env.IsDevelopment())
                    {
                        var appAssembly =
                        Assembly.Load(new AssemblyName(env.ApplicationName));
                        if (appAssembly != null)
                        {
                            config.AddUserSecrets(appAssembly, optional: true);
                        }
                    }

                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })

                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                })

                .UseDefaultServiceProvider((context, options) =>
                {
                    options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
                })

                .UseIISIntegration() // ریورس پراکسی برای حل مشکل کسترل در ویرچیوآل هاستینگ
                .UseStartup("AppInitialize")
                .Build(); // زیرساخت را بساز
        }
    }
}
