using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

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
            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            return doc;
        }

        /// <summary>
        /// Данные по размещению бюджетных средств на депозиты коммерческих банков
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Bauction>> GetBauction(DateTime FromDate, DateTime ToDate, CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "Bauction",
                    new XElement(myns + "fromDate", FromDate),
                    new XElement(myns + "ToDate", ToDate)
                );

            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            var field = doc.Descendants("BA");
            var result = new List<Bauction>();

            foreach (var xElement in field)
            {
                result.Add(new Bauction
                {
                    Date = DateTime.Parse(xElement.Element("date").Value),
                    TermPlacement = int.Parse(xElement.Element("Srok").Value),
                    AverageRate = double.Parse(xElement.Element("stav_w").Value, CultureInfo.InvariantCulture),
                    VolumeAllocated = double.Parse(xElement.Element("vol_sr").Value, CultureInfo.InvariantCulture)
                });
            }
            return result;
        }

        /// <summary>
        /// Структура бивалютной корзины
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<BiCurBacket>> GetBiCurBacket(DateTime FromDate, DateTime ToDate, CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "BiCurBacket",
                    new XElement(myns + "fromDate", FromDate),
                    new XElement(myns + "ToDate", ToDate)
                );

            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            var field = doc.Descendants("BC");
            var result = new List<BiCurBacket>();

            foreach (var xElement in field)
            {
                result.Add(new BiCurBacket
                {
                    EffectiveDate = DateTime.Parse(xElement.Element("D0").Value),
                    NumberOfUnitsUSD = double.Parse(xElement.Element("USD").Value, CultureInfo.InvariantCulture),
                    NumberOfUnitsEUR = double.Parse(xElement.Element("EUR").Value, CultureInfo.InvariantCulture)
                });
            }
            return result;
        }

        /// <summary>
        /// Стоимость бивалютной корзины
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<List<BiCurBase>> GetBiCurBase(DateTime FromDate, DateTime ToDate, CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "BiCurBase",
                    new XElement(myns + "fromDate", FromDate),
                    new XElement(myns + "ToDate", ToDate)
                );

            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            var field = doc.Descendants("BCB");
            var result = new List<BiCurBase>();

            foreach (var xElement in field)
            {
                result.Add(new BiCurBase
                {
                    Date = DateTime.Parse(xElement.Element("D0").Value),
                    Val = double.Parse(xElement.Element("VAL").Value, CultureInfo.InvariantCulture),
                });
            }
            return result;
        }

        /// <summary>
        /// Отпускные цены Банка России на инвестиционные монеты
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<List<CoinsBase>> GetCoins_base(DateTime FromDate, DateTime ToDate, CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "Coins_base",
                    new XElement(myns + "fromDate", FromDate),
                    new XElement(myns + "ToDate", ToDate)
                );

            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            var field = doc.Descendants("CB");
            var result = new List<CoinsBase>();

            foreach (var xElement in field)
            {
                result.Add(new CoinsBase
                {
                    Date = DateTime.Parse(xElement.Element("date").Value),
                    CatalogueNumber = xElement.Element("Cat_number").Value.Trim(),
                    Name = xElement.Element("name").Value.Trim(),
                    Denomination = double.Parse(xElement.Element("nominal").Value, CultureInfo.InvariantCulture),
                    MetalType = int.Parse(xElement.Element("Metall").Value, CultureInfo.InvariantCulture),
                    PureMetalContent = double.Parse(xElement.Element("Q").Value, CultureInfo.InvariantCulture),
                    PriceBR = double.Parse(xElement.Element("PriceBR").Value, CultureInfo.InvariantCulture)
                });
            }
            return result;
        }

        /// <summary>
        /// Требования Банка России к кредитным организациям
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<List<DV>> GetDV(DateTime FromDate, DateTime ToDate, CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "DV",
                    new XElement(myns + "fromDate", FromDate),
                    new XElement(myns + "ToDate", ToDate)
                );

            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            var field = doc.Descendants("DV");
            var result = new List<DV>();

            foreach (var xElement in field)
            {
                result.Add(new DV
                {
                    Date = DateTime.Parse(xElement.Element("Date").Value),
                    OvernightLoans = double.Parse(xElement.Element("VOvern").Value, CultureInfo.InvariantCulture),
                    LombardLoans = double.Parse(xElement.Element("VLomb").Value, CultureInfo.InvariantCulture),
                    IntradayLoans = double.Parse(xElement.Element("VIDay").Value, CultureInfo.InvariantCulture),
                    OtherLoans = double.Parse(xElement.Element("VOther").Value, CultureInfo.InvariantCulture),
                    BackedByGold = double.Parse(xElement.Element("Vol_Gold").Value, CultureInfo.InvariantCulture),
                    DateForIntradayLoans = DateTime.Parse(xElement.Element("VIDate").Value)
                });
            }
            return result;
        }

        /// <summary>
        /// Динамика ставок привлечения средств по депозитным операциям
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<List<DepoDynamic>> GetDepoDynamic(DateTime FromDate, DateTime ToDate, CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "DepoDynamic",
                    new XElement(myns + "fromDate", FromDate),
                    new XElement(myns + "ToDate", ToDate)
                );

            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            var field = doc.Descendants("Depo");
            var result = new List<DepoDynamic>();

            foreach (var xElement in field)
            {
                result.Add(new DepoDynamic
                {
                    DateDepo = (DateTime)xElement.Element("DateDepo"),
                    Overnight = (double?)xElement.Element("Overnight"),
                    TomNext = (double?)xElement.Element("TomNext"),
                    P1week = (double?)xElement.Element("P1week"),
                    P2weeks = (double?)xElement.Element("P2weeks"),
                    P1month = (double?)xElement.Element("P1month"),
                    P3month = (double?)xElement.Element("P3month"),
                    SpotNext = (double?)xElement.Element("SpotNext"),
                    SpotWeek = (double?)xElement.Element("SpotNext"),
                    Spot2Weeks = (double?)xElement.Element("Spot2Weeks"),
                    CallDeposit = (double?)xElement.Element("CallDeposit")
                });
            }
            return result;
        }

        /// <summary>
        /// Динамика учетных цен драгоценных металлов
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<List<DragMetall>> GetDragMetDynamic(DateTime FromDate, DateTime ToDate, CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "DragMetDynamic",
                    new XElement(myns + "fromDate", FromDate),
                    new XElement(myns + "ToDate", ToDate)
                );

            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            var field = doc.Descendants("DrgMet");
            var result = new List<DragMetall>();

            foreach (var xElement in field)
            {
                result.Add(new DragMetall
                {
                    DateMet = (DateTime)xElement.Element("DateMet"),
                    CodMet = (int)xElement.Element("CodMet"),
                    Price = (double)xElement.Element("price")
                });
            }
            return result;
        }

        /// <summary>
        /// Справочник по кодам редких валют от Thomson Reuters 
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<List<EnumRValute>> GetEnumReutersValutes(CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "EnumReutersValutes");

            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            var field = doc.Descendants("EnumRValutes");
            var result = new List<EnumRValute>();

            foreach (var xElement in field)
            {
                result.Add(new EnumRValute
                {
                    DigitalCodeISO = (int)xElement.Element("num_code"),
                    CharacterCodeISO = (string)xElement.Element("char_code"),
                    NameRU = (string)xElement.Element("Title_ru"),
                    NameEN = (string)xElement.Element("Title_en")
                });
            }
            return result;
        }

        /// <summary>
        /// Справочник по кодам валют
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<EnumValute>> GetEnumValutes(CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "EnumValutes");

            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            var field = doc.Descendants("EnumValutes");
            var result = new List<EnumValute>();

            foreach (var xElement in field)
            {
                result.Add(new EnumValute
                {
                    Vcode = ((string) xElement.Element("Vcode")).Trim(),
                    Vname = ((string)xElement.Element("Vname")).Trim(),
                    VEngname = ((string)xElement.Element("VEngname")).Trim(),
                    Vnom = (double)xElement.Element("Vnom"),
                    VcommonCode = ((string)xElement.Element("VcommonCode")).Trim(),
                    VnumCode = (int?)xElement.Element("VnumCode"),
                    VcharCode = ((string)xElement.Element("VcharCode"))
                });
            }
            return result;
        }

        /// <summary>
        /// Фиксинги на драгоценные металлы
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<FixingBase>> GetFixingBase(DateTime FromDate, DateTime ToDate, CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "FixingBase",
                    new XElement(myns + "fromDate", FromDate),
                    new XElement(myns + "ToDate", ToDate)
                );

            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            var field = doc.Descendants("FB");
            var result = new List<FixingBase>();

            foreach (var xElement in field)
            {
                result.Add(new FixingBase
                {
                    Date = (DateTime)xElement.Element("date"),
                    Gold = (double)xElement.Element("Gold"),
                    Silver = (double)xElement.Element("Silver")
                });
            }
            return result;
        }

        /// <summary>
        /// Получение динамики ежедневных курсов валюты
        /// Для получения кода волюты использовать GetEnumValutes
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="ValutaCode">Код в классификаторе ЦБРФ</param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<ValuteCursDynamic>> GetCursDynamic(DateTime FromDate, DateTime ToDate, string ValutaCode, CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "GetCursDynamic",
                    new XElement(myns + "FromDate", FromDate),
                    new XElement(myns + "ToDate", ToDate),
                    new XElement(myns + "ValutaCode", ValutaCode)
                );

            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            var field = doc.Descendants("ValuteCursDynamic");
            var result = new List<ValuteCursDynamic>();

            foreach (var xElement in field)
            {
                result.Add(new ValuteCursDynamic
                {
                    CursDate = (DateTime) xElement.Element("CursDate"),
                    Vcode = (string) xElement.Element("Vcode"),
                    Vnom = (double) xElement.Element("Vnom"),
                    Vcurs = (double) xElement.Element("Vcurs")
                });
            }
            return result;
        }

        /// <summary>
        /// Получение ежедневных курсов валют
        /// </summary>
        /// <param name="OnDate"></param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<ValuteCursOnDate>> GetCursOnDate(DateTime OnDate, CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "GetCursOnDate",
                    new XElement(myns + "On_date", OnDate)
                );

            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            var field = doc.Descendants("ValuteCursOnDate");
            var result = new List<ValuteCursOnDate>();

            foreach (var xElement in field)
            {
                result.Add(new ValuteCursOnDate
                {
                    Vname = ((string)xElement.Element("Vname")).Trim(),
                    Vnom = (double)xElement.Element("Vnom"),
                    Vcurs = (double)xElement.Element("Vcurs"),
                    Vcode = (int)xElement.Element("Vcode"),
                    VchCode = ((string)xElement.Element("VchCode"))
                });
            }
            return result;
        }

        /// <summary>
        /// Последняя дата публикации курсов валют(ежемесячные валюты)
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<DateTime> GetLatestDate(CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "GetLatestDate");

            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            var result = doc
                .Element(XName.Get("Envelope", "http://www.w3.org/2003/05/soap-envelope"))
                .Element(XName.Get("Body", "http://www.w3.org/2003/05/soap-envelope"))
                .Element(myns + "GetLatestDateResponse")
                .Element(myns  + "GetLatestDateResult")                
                .Value;

            return DateTime.ParseExact(result, "yyyyMMdd", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Последняя дата публикации курсов валют (ежемесячные валюты)
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<DateTime> GetLatestDateTimeSeld(CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "GetLatestDateTimeSeld");

            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            return (DateTime)doc
                .Element(XName.Get("Envelope", "http://www.w3.org/2003/05/soap-envelope"))
                .Element(XName.Get("Body", "http://www.w3.org/2003/05/soap-envelope"))
                .Element(myns + "GetLatestDateTimeSeldResponse")
                .Element(myns + "GetLatestDateTimeSeldResult");
        }

        /// <summary>
        /// Последняя дата публикации редких валют от Thomson Reuters
        /// </summary>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<DateTime> GetLatestReutersDateTime(CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "GetLatestReutersDateTime");

            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            return (DateTime)doc
                .Element(XName.Get("Envelope", "http://www.w3.org/2003/05/soap-envelope"))
                .Element(XName.Get("Body", "http://www.w3.org/2003/05/soap-envelope"))
                .Element(myns + "GetLatestReutersDateTimeResponse")
                .Element(myns + "GetLatestReutersDateTimeResult");
        }

        /// <summary>
        /// Получение динамики ежедневных курсов редкой валюты от Thomson Reuters
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="NumCode"></param>
        /// <param name="Cancel"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<ReutersValuteData>> GetReutersCursDynamic(DateTime FromDate, DateTime ToDate, int NumCode, CancellationToken Cancel = default)
        {
            XNamespace myns = "http://web.cbr.ru/";
            XElement parameters =
                new XElement(myns + "GetReutersCursDynamic",
                    new XElement(myns + "FromDate", FromDate),
                    new XElement(myns + "ToDate", ToDate),
                    new XElement(myns + "NumCode", NumCode)
                );

            var doc = await GetFromCbr(parameters, Cancel).ConfigureAwait(false);

            var field = doc.Descendants("VCD");
            var result = new List<ReutersValuteData>();

            foreach (var xElement in field)
            {
                result.Add(new ReutersValuteData
                {
                    Date = (DateTime)xElement.Element("DT"),
                    Quotation = (double)xElement.Element("val"),
                    ReverseQuotation = (bool)xElement.Element("dir")
                });
            }
            return result;
        }

        //public async Task<XDocument> BiCurBase(DateTime FromDate, DateTime ToDate, CancellationToken Cancel = default)
        //{
        //    XNamespace myns = "http://web.cbr.ru/";
        //    XElement parameters =
        //        new XElement(myns + "BiCurBase",
        //            new XElement(myns + "fromDate", FromDate),
        //            new XElement(myns + "ToDate", ToDate)
        //        );

        //    return await GetFromCbr(parameters, Cancel).ConfigureAwait(false);
        //}

        //public async Task<XDocument> BiCurBase(DateTime FromDate, DateTime ToDate, CancellationToken Cancel = default)
        //{
        //    XNamespace myns = "http://web.cbr.ru/";
        //    XElement parameters =
        //        new XElement(myns + "BiCurBase",
        //            new XElement(myns + "fromDate", FromDate),
        //            new XElement(myns + "ToDate", ToDate)
        //        );

        //    return await GetFromCbr(parameters, Cancel).ConfigureAwait(false);
        //}

        //public async Task<XDocument> BiCurBase(DateTime FromDate, DateTime ToDate, CancellationToken Cancel = default)
        //{
        //    XNamespace myns = "http://web.cbr.ru/";
        //    XElement parameters =
        //        new XElement(myns + "BiCurBase",
        //            new XElement(myns + "fromDate", FromDate),
        //            new XElement(myns + "ToDate", ToDate)
        //        );

        //    return await GetFromCbr(parameters, Cancel).ConfigureAwait(false);
        //}

        #endregion // Методы

        protected async Task<XDocument> GetFromCbr(XElement parameters, CancellationToken Cancel = default)
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
