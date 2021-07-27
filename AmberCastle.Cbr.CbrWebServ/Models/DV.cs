using System;

namespace AmberCastle.Cbr.CbrWebServ.Models
{
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
}