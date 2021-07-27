using System;

namespace AmberCastle.Cbr.CbrWebServ.Models
{
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