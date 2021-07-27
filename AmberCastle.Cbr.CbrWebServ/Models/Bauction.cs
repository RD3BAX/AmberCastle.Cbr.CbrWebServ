using System;

namespace AmberCastle.Cbr.CbrWebServ.Models
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
}
