using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FXFinder.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args)
                         .Build();
            //Microsoft.AspNetCore.Hosting.IHostingEnvironment env;
            //Read Configuration from appSettings
            //remove log to console when in production
            //var builder = new ConfigurationBuilder()
            //   .SetBasePath(env.ContentRootPath)
            //   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            //   .AddEnvironmentVariables();
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
            //Initialize Logger
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    //logging initial app state
                    Log.Information("FXFinder started.");
                    host.Run();
                }
                catch (Exception ex)
                {
                    Log.Fatal(ex, "The Application failed to start.");

                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
