using System;

namespace AmberCastle.Cbr.CbrWebServ.Models
{
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
}