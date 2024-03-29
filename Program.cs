
using Serilog;
using Serilog.Events;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace LHDTV
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                .Enrich.FromLogContext()

                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] Request:{RequestId} {Message:lj}{NewLine}{Exception}")
                .WriteTo.File("./Logs/log.log", rollingInterval: RollingInterval.Day, outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] Request:{RequestId} {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
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
