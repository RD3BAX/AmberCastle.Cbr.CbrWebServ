using System;

namespace AmberCastle.Cbr.CbrWebServ.Models
{
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
}