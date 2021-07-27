using System;

namespace AmberCastle.Cbr.CbrWebServ.Models
{
    /// <summary>
    /// Фиксинги на драгоценные металлы
    /// </summary>
    public class FixingBase
    {
        /// <summary>
        /// Дата name="Date"
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Золото (вечер) (доллары США / тр. унция) name="Gold"
        /// </summary>
        public double Gold { get; set; }

        /// <summary>
        /// Серебро (доллары США / тр. унция) name="Silver"
        /// </summary>
        public double Silver { get; set; }
    }
}