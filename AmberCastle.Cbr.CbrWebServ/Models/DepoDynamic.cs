using System;

namespace AmberCastle.Cbr.CbrWebServ.Models
{
    /// <summary>
    /// Динамика ставок привлечения средств по депозитным операциям
    /// </summary>
    public class DepoDynamic
    {
        /// <summary>
        /// name="DateDepo"
        /// </summary>
        public DateTime DateDepo { get; set; }

        /// <summary>
        /// name="Overnight"
        /// </summary>
        public double? Overnight { get; set; }

        /// <summary>
        /// name="TomNext"
        /// </summary>
        public double? TomNext { get; set; }

        /// <summary>
        /// name="P1week"
        /// </summary>
        public double? P1week { get; set; }

        /// <summary>
        /// name="P2weeks"
        /// </summary>
        public double? P2weeks { get; set; }

        /// <summary>
        /// name="P1month"
        /// </summary>
        public double? P1month { get; set; }

        /// <summary>
        /// name="P3month"
        /// </summary>
        public double? P3month { get; set; }

        /// <summary>
        /// name="SpotNext"
        /// </summary>
        public double? SpotNext { get; set; }

        /// <summary>
        /// name="SpotWeek"
        /// </summary>
        public double? SpotWeek { get; set; }

        /// <summary>
        /// name="Spot2Weeks"
        /// </summary>
        public double? Spot2Weeks { get; set; }

        /// <summary>
        /// name="CallDeposit"
        /// </summary>
        public double? CallDeposit { get; set; }
    }
}