namespace AmberCastle.Cbr.CbrWebServ.Models
{
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
}