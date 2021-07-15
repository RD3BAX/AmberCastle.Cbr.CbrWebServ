using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AmberCastle.Cbr.CbrWebServ
{
    /// <summary>
    /// Сервис для получения ежедневных данных (курсы валют, учетные цены драг. металлов и другие)
    /// </summary>
    public class DailyInfoClient
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

        public DailyInfoClient(HttpClient Client)
        {
            _Client = Client;
        }

        #endregion // Конструктор
    }
}
