namespace AmberCastle.Cbr.CbrWebServ.Models
{
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
}