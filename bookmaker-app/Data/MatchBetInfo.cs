using System;
using System.Collections.Generic;
using System.Text;

namespace BookmakerApp.Data
{
    /// <summary>
    ///     Информация о ставке на результат матча
    /// </summary>
    public class MatchBetInfo<TTeam>: BaseInfo
        where TTeam: TeamInfo
    {
        /// <summary>
        ///     Матч
        /// </summary>
        public MatchInfo<TTeam> Match { get; set; }
        /// <summary>
        ///     Ожидаемый результат
        /// </summary>
        public MatchResultInfo<TTeam> ExpectedResult { get; set; }
        /// <summary>
        ///     Ставка (в усл. едн.)
        /// </summary>
        public double Cash { get; set; }
    }
}
