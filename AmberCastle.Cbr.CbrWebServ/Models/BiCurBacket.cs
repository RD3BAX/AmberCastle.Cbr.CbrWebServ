using System;

namespace AmberCastle.Cbr.CbrWebServ.Models
{
    /// <summary>
    /// Структура бивалютной корзины
    /// </summary>
    public class BiCurBacket
    {
        /// <summary>
        /// Дата начала действия name="D0"
        /// </summary>
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// Число единиц иностранной валюты в составе корзины / Доллар США name="USD"
        /// </summary>
        public double NumberOfUnitsUSD { get; set; }

        /// <summary>
        /// Число единиц иностранной валюты в составе корзины / Евро name="EUR"
        /// </summary>
        public double NumberOfUnitsEUR { get; set; }

        public override string ToString() =>
            $"Начало действия {EffectiveDate.ToShortDateString()} USD {NumberOfUnitsUSD}% - EUR {NumberOfUnitsEUR}%";
    }
}