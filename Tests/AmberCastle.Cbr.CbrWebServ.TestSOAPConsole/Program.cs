using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AmberCastle.Cbr.CbrWebServ.TestSOAPConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            XNamespace myns = "http://web.cbr.ru/";

            XElement parameters = 
                new XElement(myns + "OstatDynamic",
                    new XElement(myns + "fromDate", DateTime.Today.AddDays(-10)),
                    new XElement(myns + "ToDate", DateTime.Today)
                );
            
            
            XElement parameters2 = 
                new XElement(myns + "GetLatestDateTime",
                    new XElement(myns + "fromDate", DateTime.Today.AddDays(-10)),
                    new XElement(myns + "ToDate", DateTime.Today)
                );


            var connection_string = $"https://www.cbr.ru/DailyInfoWebServ/DailyInfo.asmx";



            try
            {
                using (var client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip })
                {
                    BaseAddress = new Uri(connection_string)
                    //Timeout = _timeout
                })
                {
                    var cbrClient = new CbrСlient(client);

                    //var result = cbrClient.GetFromCbr(parameters).Result;
                    
                    //Console.WriteLine(result);

                    //var result2 = cbrClient.GetFromCbr(parameters2).Result;

                    //Console.WriteLine(result2);

                    Console.WriteLine(cbrClient.AllDataInfoXML().Result);



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

    public class CbrСlient
    {
        #region Поля

        private readonly HttpClient _Client;

        #endregion // Поля

        #region Методы

        /// <summary>
        /// Получение всей оперативной информации (XmlDocument)
        /// </summary>
        /// <returns></returns>
        public async Task<XDocument> AllDataInfoXML(CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "AllDataInfoXML");

            return await GetFromCbr(parameters, Cancel).ConfigureAwait(false);
        }

        #endregion // Методы

        public async Task<XDocument> GetFromCbr(XElement parameters, CancellationToken Cancel = default)
        {
            //var _apiUrl = $"/DailyInfoWebServ/DailyInfo.asmx";

            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            XNamespace xsd = "http://www.w3.org/2001/XMLSchema";
            XNamespace ns = "http://www.w3.org/2003/05/soap-envelope";

            XDocument soapRequest = new XDocument(
                new XDeclaration("1.0", "UTF-8", "no"),
                new XElement(ns + "Envelope",
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XAttribute(XNamespace.Xmlns + "xsd", xsd),
                    new XAttribute(XNamespace.Xmlns + "soap12", ns),
                    new XElement(ns + "Body", parameters)
                ));


            try
            {

                var request = new HttpRequestMessage()
                {
                    //RequestUri = new Uri(_apiUrl),
                    Method = HttpMethod.Post
                };

                request.Content = new StringContent(soapRequest.ToString(), Encoding.UTF8, "text/xml");

                request.Headers.Clear();
                _Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("text/xml");
                //request.Headers.Add("SOAPAction", $"http://web.cbr.ru/{method}");

                var test = request.ToString();

                HttpResponseMessage response = await _Client.SendAsync(request, Cancel).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    //throw new Exception();
                    return default;
                }

                Task<Stream> streamTask = response.Content.ReadAsStreamAsync(Cancel);
                Stream stream = await streamTask.ConfigureAwait(false);
                var sr = new StreamReader(stream);

                return XDocument.Load(sr);
            }
            catch (AggregateException ex)
            {
                if (ex.InnerException is TaskCanceledException)
                {
                    throw ex.InnerException;
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region Конструктор

        public CbrСlient(HttpClient Client)
        {
            _Client = Client;
        }

        #endregion // Конструктор
    }
}
