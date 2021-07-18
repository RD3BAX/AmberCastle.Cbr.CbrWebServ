using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmberCastle.Cbr.CbrWebServ
{
    /// <summary>
    /// Данные по размещению бюджетных средств на депозиты коммерческих банков
    /// </summary>
    public class Bauction
    {
        /// <summary>
        /// Дата name="date"
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Срок размещения средств, дней name="Srok"
        /// </summary> 
        public int TermPlacement { get; set; }

        /// <summary>
        /// Средневзвешенная ставка, % годовых name="stav_w"
        /// </summary>
        public double AverageRate { get; set; }

        /// <summary>
        /// Объем размещенных средств, млн. руб. name="vol_sr"
        /// </summary>
        public double VolumeAllocated { get; set; }

        public override string ToString() => 
            $"{Date.ToShortDateString()} : на {TermPlacement} дней под {AverageRate}% в объеме {VolumeAllocated} млн. руб.";
    }

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
