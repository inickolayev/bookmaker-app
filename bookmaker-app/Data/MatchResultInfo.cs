using System;
using System.Collections.Generic;
using System.Text;

namespace BookmakerApp.Data
{
    /// <summary>
    ///     Результат матча
    /// </summary>
    /// <typeparam name="TTeam">Тип команд матча (футбол, воллейбол и т.д.)</typeparam>
    public class MatchResultInfo<TTeam>: BaseInfo
        where TTeam: TeamInfo
    {
        /// <summary>
        ///     Матч
        /// </summary>
        public MatchInfo<TTeam> Match { get; set; }
        /// <summary>
        ///     Результат первой команды
        /// </summary>
        public int FirstTeamResult { get; set; }
        /// <summary>
        ///     Результат второй команды
        /// </summary>
        public int SecondTeamResult { get; set; }
    }
}
