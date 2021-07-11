using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Extensions.Http;

namespace AmberCastle.Cbr.CbrWebServ.TestConsole
{
    class Program
    {
        private static IHost __Hosting;

        public static IHost Hosting => __Hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Hosting.Services;

        public static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            //.ConfigureAppConfiguration((hostContext, builder) =>
            //{
            //    // Add other providers for JSON, etc.

            //    if (hostContext.HostingEnvironment.IsDevelopment())
            //    {
            //        builder.AddUserSecrets<Program>();
            //    }
            //})
            .ConfigureServices(ConfigureServices);

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            //services.AddHttpClient<CbrClient>(client =>
            //{
            //    var config = host.Configuration.GetSection("CbrWebServ");
            //    client.BaseAddress = new Uri(
            //        $"{config["Schema"]}://" +
            //        $"{config["Address"]}"
            //        );
            //})
            //    .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            //    .AddPolicyHandler(GetRetryPolicy())
            //    ;
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var jitter = new Random();
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(6, retry_attempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retry_attempt)) +
                    TimeSpan.FromMilliseconds(jitter.Next(0, 1000)));
        }

        static async Task Main(string[] args)
        {
            using var host = Hosting;
            await host.StartAsync();

            

            Console.WriteLine("Завершено!");
            Console.ReadLine();
            await host.StopAsync();
        }
    }
}
