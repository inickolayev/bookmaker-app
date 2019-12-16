using System;
using System.Collections.Generic;
using System.Text;

namespace BookmakerApp.Data
{
    /// <summary>
    ///     Информация о прошедшем матче
    /// </summary>
    /// <typeparam name="TTeam">Тип команд матча (футбол, воллейбол и т.д.)</typeparam>
    public class MatchInfo<TTeam>: BaseInfo
        where TTeam : TeamInfo
    {
        /// <summary>
        ///     Первая команда
        /// </summary>
        public TTeam FirstTeam { get; set; }
        /// <summary>
        ///     Вторая команда
        /// </summary>
        public TTeam SecondTeam { get; set; }
        /// <summary>
        ///     Стаутс соревнования
        /// </summary>
        public EMatchStatus Status { get; set; }
    }
}
