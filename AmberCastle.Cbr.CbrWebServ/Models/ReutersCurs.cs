namespace AmberCastle.Cbr.CbrWebServ.Models
{
    /// <summary>
    /// Ежедневный курс редких валют от Thomson Reuters
    /// </summary>
    public class ReutersCurs
    {
        /// <summary>
        /// Цифровой ISO код валюты name="num_code"
        /// </summary>
        public int NumCode { get; set; }

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