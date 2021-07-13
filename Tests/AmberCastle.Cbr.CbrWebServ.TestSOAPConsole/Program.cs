﻿using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AmberCastle.Cbr.CbrWebServ.TestSOAPConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var _apiUrl = $"http://www.cbr.ru/DailyInfoWebServ/DailyInfo.asmx";

            XNamespace ns = "http://schemas.xmlsoap.org/soap/envelope/";
            XNamespace myns = "http://web.cbr.ru/";

            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";

            XDocument soapRequest = new XDocument(
                new XDeclaration("1.0", "UTF-8", "no"),
                new XElement(ns + "Envelope",
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XAttribute(XNamespace.Xmlns + "xsd", xsd),
                    new XAttribute(XNamespace.Xmlns + "soap", ns),
                    new XElement(ns + "Body",
                        new XElement(myns + "GetCursOnDateXML",
                            //new XElement(myns + "client",
                            //    new XElement(myns + "Username", $"_apiUsername"),
                            //    new XElement(myns + "Password", $"_apiPassword")),
                            new XElement(myns + "On_date", DateTime.Today)
                        )
                    )
                ));

            try
            {
                using (var client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip }) /*{ Timeout = _timeout }*/)
                {
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(_apiUrl),
                        Method = HttpMethod.Post
                    };

                    request.Content = new StringContent(soapRequest.ToString(), Encoding.UTF8, "text/xml");

                    request.Headers.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
                    request.Headers.Add("SOAPAction", "http://web.cbr.ru/GetCursOnDateXML");

                    var test = request.ToString();

                    HttpResponseMessage response = client.SendAsync(request).Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception();
                    }

                    Task<Stream> streamTask = response.Content.ReadAsStreamAsync();
                    Stream stream = streamTask.Result;
                    var sr = new StreamReader(stream);
                    var soapResponse = XDocument.Load(sr);
                    Console.WriteLine(soapResponse);

                    //var xml = soapResponse.Descendants(myns + "GetStuffResult").FirstOrDefault().ToString();
                    //var purchaseOrderResult = StaticMethods.Deserialize<GetStuffResult>(xml);
                }
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is TaskCanceledException)
                {
                    throw ex.InnerException;
                }
                else
                {
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
