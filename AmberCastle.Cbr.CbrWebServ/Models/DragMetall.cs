using System;

namespace AmberCastle.Cbr.CbrWebServ.Models
{
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
}