using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
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
            services.AddHttpClient<DailyInfoClient>(client =>
            {
                var config = host.Configuration.GetSection("CbrWebServ");
                client.BaseAddress = new Uri(
                    $"{config["Schema"]}://" +
                    $"{config["Address"]}"
                    );
            })
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy())
                ;
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

            var client = Services.GetRequiredService<DailyInfoClient>();

            //Console.WriteLine(client.AllDataInfoXML().Result);

            //var bauctions = client.GetBauction(DateTime.Today.AddYears(-15), DateTime.Today).Result;
            //foreach (var bauction in bauctions)
            //{
            //    Console.WriteLine(bauction);
            //}

            Console.WriteLine(client.GetBiCurBacket(DateTime.Today.AddYears(-1), DateTime.Today).Result);
            var BiCurBackets = client.GetBiCurBacket(DateTime.Today.AddYears(-1), DateTime.Today).Result;


            //var doc = client.GetBauction(DateTime.Today.AddYears(-15), DateTime.Today).Result;
            ////XNamespace myns = "http://web.cbr.ru/";

            //var field = doc.Descendants("BA");

            ////var TheElements1 =
            ////    from
            ////        AnyElement
            ////        in
            ////        doc.Descendants("BC")
            ////    select
            ////        AnyElement;
            //foreach (var xElement in field)
            //{
            //    Console.WriteLine(DateTime.Parse(xElement.Element("date").Value));
            //    Console.WriteLine(int.Parse(xElement.Element("Srok").Value));
            //    Console.WriteLine(double.Parse(xElement.Element("stav_w").Value, CultureInfo.InvariantCulture));
            //    Console.WriteLine(double.Parse(xElement.Element("vol_sr").Value, CultureInfo.InvariantCulture));
            //}



            //using (var witer = new XmlTextWriter(doc))

            ////проходим по каждому элементу в найшей library
            ////(этот элемент сразу доступен через свойство doc.Root)
            //foreach (XElement el in doc.Root.Elements("Body"))
            //{
            //    Console.WriteLine(el);

            //    ////Выводим имя элемента и значение аттрибута id
            //    //Console.WriteLine("{0} {1}", el.Name, el.Attribute("id").Value);
            //    //Console.WriteLine("  Attributes:");
            //    ////выводим в цикле все аттрибуты, заодно смотрим как они себя преобразуют в строку
            //    //foreach (XAttribute attr in el.Attributes())
            //    //    Console.WriteLine("    {0}", attr);
            //    //Console.WriteLine("  Elements:");
            //    ////выводим в цикле названия всех дочерних элементов и их значения
            //    //foreach (XElement element in el.Elements())
            //    //    Console.WriteLine("    {0}: {1}", element.Name, element.Value);
            //}


            Console.WriteLine("Завершено!");
            Console.ReadLine();
            await host.StopAsync();
        }
    }
}
