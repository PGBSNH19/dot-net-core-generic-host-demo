using GenericHost.DatabaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace GenericHost
{
    class Program
    {
        static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddDbContext<OrderContext>(options => options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection")));
                services.AddHostedService<MainService>();
            })
            .UseSerilog(new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("C:\\log\\log.txt")
                .CreateLogger());
    }
}
