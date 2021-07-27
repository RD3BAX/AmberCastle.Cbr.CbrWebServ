namespace AmberCastle.Cbr.CbrWebServ.Models
{
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
}