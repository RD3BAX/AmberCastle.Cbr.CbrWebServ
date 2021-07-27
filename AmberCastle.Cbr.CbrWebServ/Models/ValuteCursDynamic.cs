using System;

namespace AmberCastle.Cbr.CbrWebServ.Models
{
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
}