using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberCastle.Cbr.CbrWebServ
{
    /// <summary>
    /// Данные по размещению бюджетных средств на депозиты коммерческих банков
    /// </summary>
    public class Bauction
    {
        /// <summary>
        /// Дата name="date"
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Срок размещения средств, дней name="Srok"
        /// </summary> 
        public int TermPlacement { get; set; }

        /// <summary>
        /// Средневзвешенная ставка, % годовых name="stav_w"
        /// </summary>
        public double AverageRate { get; set; }

        /// <summary>
        /// Объем размещенных средств, млн. руб. name="vol_sr"
        /// </summary>
        public double VolumeAllocated { get; set; }

        public override string ToString() => 
            $"{Date.ToShortDateString()} : на {TermPlacement} дней под {AverageRate}% в объеме {VolumeAllocated} млн. руб.";
    }

    /// <summary>
    /// Структура бивалютной корзины
    /// </summary>
    public class BiCurBacket
    {
        /// <summary>
        /// Дата начала действия name="D0"
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Число единиц иностранной валюты в составе корзины / Доллар США name="USD"
        /// </summary>
        public double NumberOfUnitsUSD { get; set; }

        /// <summary>
        /// Число единиц иностранной валюты в составе корзины / Евро name="EUR"
        /// </summary>
        public double NumberOfUnitsEUR { get; set; }

        public override string ToString() =>
            $"Начало действия {EffectiveDate.ToShortDateString()} USD {NumberOfUnitsUSD}% - EUR {NumberOfUnitsEUR}%";
    }

    /// <summary>
    /// Стоимость бивалютной корзины
    /// </summary>
    public class BiCurBase
    {
        /// <summary>
        /// Дата name="date"
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Стоимость, руб. name="VAL"
        /// </summary>
        public double Val { get; set; }

        public override string ToString() =>
            $"{Date.ToShortDateString()} стоимость {Val} руб.";
    }

    /// <summary>
    /// Отпускные цены Банка России на инвестиционные монеты
    /// </summary>
    public class CoinsBase
    {
        /// <summary>
        /// Дата name="date"
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Каталожный номер name="Cat_number"
        /// </summary>
        public string CatalogueNumber { get; set; }

        /// <summary>
        /// Наименование монеты name="name"
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номинал name="nominal"
        /// </summary>
        public double Denomination { get; set; }

        /// <summary>
        /// Тип металла (1-золото, 2-серебро) name="Metall"
        /// </summary>
        public int MetalType { get; set; }

        /// <summary>
        /// Содержание чистого металла, г name="Q"
        /// </summary>
        public double PureMetalContent { get; set; }

        /// <summary>
        /// Отпускная цена Банка России name="PriceBR"
        /// </summary>
        public double PriceBR { get; set; }

        public override string ToString() =>
            $"{Date.ToShortDateString()} монета {Name} цена {PriceBR} руб.";
    }

    /// <summary>
    /// Требования Банка России к кредитным организациям
    /// </summary>
    public class DV
    {
        /// <summary>
        /// Дата name="Date"
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Кредиты овернайт name="VOvern"
        /// </summary>
        public double OvernightLoans { get; set; }

        /// <summary>
        /// Ломбардные кредиты name="VLomb"
        /// </summary>
        public double LombardLoans { get; set; }

        /// <summary>
        /// Внутридневные кредиты name="VIDay"
        /// </summary>
        public double IntradayLoans { get; set; }

        /// <summary>
        /// По другим кредитам name="VOther"
        /// </summary>
        public double OtherLoans { get; set; }

        /// <summary>
        /// Обеспеченные золотом name="Vol_Gold"
        /// </summary>
        public double BackedByGold { get; set; }

        /// <summary>
        /// Дата для данных по Внутридневным кредитам name="VIDate"
        /// </summary>
        public DateTime DateForIntradayLoans { get; set; }
    }

    /// <summary>
    /// Динамика ставок привлечения средств по депозитным операциям
    /// </summary>
    public class DepoDynamic
    {
        /// <summary>
        /// name="DateDepo"
        /// </summary>
        public DateTime DateDepo { get; set; }

        /// <summary>
        /// name="Overnight"
        /// </summary>
        public double? Overnight { get; set; }

        /// <summary>
        /// name="TomNext"
        /// </summary>
        public double? TomNext { get; set; }

        /// <summary>
        /// name="P1week"
        /// </summary>
        public double? P1week { get; set; }

        /// <summary>
        /// name="P2weeks"
        /// </summary>
        public double? P2weeks { get; set; }

        /// <summary>
        /// name="P1month"
        /// </summary>
        public double? P1month { get; set; }

        /// <summary>
        /// name="P3month"
        /// </summary>
        public double? P3month { get; set; }

        /// <summary>
        /// name="SpotNext"
        /// </summary>
        public double? SpotNext { get; set; }

        /// <summary>
        /// name="SpotWeek"
        /// </summary>
        public double? SpotWeek { get; set; }

        /// <summary>
        /// name="Spot2Weeks"
        /// </summary>
        public double? Spot2Weeks { get; set; }

        /// <summary>
        /// name="CallDeposit"
        /// </summary>
        public double? CallDeposit { get; set; }
    }

    /// <summary>
    /// Динамика учетных цен драгоценных металлов
    /// </summary>
    public class DragMetall
    {
        /// <summary>
        /// name="DateMet"
        /// </summary>
        public DateTime DateMet { get; set; }

        /// <summary>
        /// name="CodMet"
        /// </summary>
        public int CodMet { get; set; }

        /// <summary>
        /// name="price"
        /// </summary>
        public double Price { get; set; }
    }

    /// <summary>
    /// Справочник по кодам редких валют от Thomson Reuters
    /// </summary>
    public class EnumRValute
    {
        /// <summary>
        /// Цифровой ISO код валюты name="num_code"
        /// </summary>
        public int DigitalCodeISO { get; set; }

        /// <summary>
        /// Символьный ISO код валюты name="char_code"
        /// </summary>
        public string CharacterCodeISO { get; set; }

        /// <summary>
        /// Название рус. name="Title_ru"
        /// </summary>
        public string NameRU { get; set; }

        /// <summary>
        /// Название анг. name="Title_en"
        /// </summary>
        public string NameEN { get; set; }

        public override string ToString() =>
            $"{NameRU}";
    }

    /// <summary>
    /// Справочник по кодам валют
    /// </summary>
    public class EnumValute
    {
        /// <summary>
        /// name="Vcode"
        /// </summary>
        public string Vcode { get; set; }

        /// <summary>
        /// name="Vname"
        /// </summary>
        public string Vname { get; set; }

        /// <summary>
        /// name="VEngname"
        /// </summary>
        public string VEngname { get; set; }

        /// <summary>
        /// name="Vnom"
        /// </summary>
        public double Vnom { get; set; }

        /// <summary>
        /// name="VcommonCode"
        /// </summary>
        public string VcommonCode { get; set; }

        /// <summary>
        /// name="VnumCode"
        /// </summary>
        public int? VnumCode { get; set; }

        /// <summary>
        /// name="VcharCode"
        /// </summary>
        public string VcharCode { get; set; }

        public override string ToString() =>
            $"{VcharCode}";
    }

    /// <summary>
    /// Фиксинги на драгоценные металлы
    /// </summary>
    public class FixingBase
    {
        /// <summary>
        /// Дата name="Date"
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Золото (вечер) (доллары США / тр. унция) name="Gold"
        /// </summary>
        public double Gold { get; set; }

        /// <summary>
        /// Серебро (доллары США / тр. унция) name="Silver"
        /// </summary>
        public double Silver { get; set; }
    }

    /// <summary>
    /// Динамика ежедневных курсов валюты
    /// </summary>
    public class ValuteCursDynamic
    {
        /// <summary>
        /// name="CursDate"
        /// </summary>
        public DateTime CursDate { get; set; }

        /// <summary>
        /// name="Vcode"
        /// </summary>
        public string Vcode { get; set; }

        /// <summary>
        /// name="Vnom"
        /// </summary>
        public double Vnom { get; set; }

        /// <summary>
        /// name="Vcurs"
        /// </summary>
        public double Vcurs { get; set; }

        public override string ToString() =>
            $"{CursDate} - {Vcode} : {Vcurs}";
    }

    /// <summary>
    /// Ежедневный курс валюты
    /// </summary>
    public class ValuteCursOnDate
    {
        /// <summary>
        /// name="Vname"
        /// </summary>
        public string Vname { get; set; }

        /// <summary>
        /// name="Vnom"
        /// </summary>
        public double Vnom { get; set; }

        /// <summary>
        /// name="Vcurs"
        /// </summary>
        public double Vcurs { get; set; }

        /// <summary>
        /// name="Vcode"
        /// </summary>
        public int Vcode { get; set; }

        /// <summary>
        /// name="VchCode"
        /// </summary>
        public string VchCode { get; set; }

        public override string ToString() =>
            $"{VchCode} {Vcurs}";
    }

    /// <summary>
    /// Динамики ежедневных курсов редкой валюты от Thomson Reuters
    /// </summary>
    public class ReutersValuteData
    {
        /// <summary>
        /// Дата name="DT"
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Котировка name="val"
        /// </summary>
        public double Quotation { get; set; }

        /// <summary>
        /// обратная котировка name="dir
        /// </summary>
        public bool ReverseQuotation { get; set; }
    }
}
