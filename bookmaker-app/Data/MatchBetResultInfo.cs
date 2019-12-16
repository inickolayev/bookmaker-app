using System;
using System.Collections.Generic;
using System.Text;

namespace BookmakerApp.Data
{
    /// <summary>
    ///     Результат ставки на матч
    /// </summary>
    public class MatchBetResultInfo<TTeam>: BaseInfo
        where TTeam: TeamInfo
    {
        /// <summary>
        ///     Ставка
        /// </summary>
        public MatchBetInfo<TTeam> Bet { get; set; }
        /// <summary>
        ///     Результат соревнования
        /// </summary>
        public MatchResultInfo<TTeam> MatchResult { get; set; }
        /// <summary>
        ///     Награда за ставку
        /// </summary>
        public double Reward { get; set; }
    }
}
