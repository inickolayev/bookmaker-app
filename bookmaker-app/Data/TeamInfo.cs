using System;
using System.Collections.Generic;
using System.Text;

namespace BookmakerApp.Data
{
    /// <summary>
    ///     Информация о команде
    /// </summary>
    public abstract class TeamInfo: BaseInfo
    {
        /// <summary>
        ///     Название команды
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        ///     Рейтинг команды
        /// </summary>
        public double Rating { get; set; }
    }
}
