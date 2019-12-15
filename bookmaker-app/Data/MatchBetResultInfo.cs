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
        public MatchBetInfo<TTeam> Bet { get; set; }
        public 
    }
}
