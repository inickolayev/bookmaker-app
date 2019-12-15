using System;
using System.Collections.Generic;
using System.Text;
using BookmakerApp.Data;

namespace BookmakerApp.Core
{
    /// <summary>
    ///     Сервис, отвечающий за хранение спортивных результатов
    /// </summary>
    public interface IDataKeeperService
    {
        #region Public fields

        /// <summary>
        ///     Футбольные команды
        /// </summary>
        IEnumerable<FootballTeamInfo> FootballTeams { get; }

        #endregion Public fields

        #region Public methods

        /// <summary>
        ///     Получить список всех футбольных матчей
        /// </summary>
        /// <returns>Список матчей</returns>
        IEnumerable<MatchInfo<FootballTeamInfo>> GetFootballMatches();

        /// <summary>
        ///     Получить список футбольных матчей с учетом фильтрации
        /// </summary>
        /// <param name="status">Статус матчей</param>
        /// <param name="skip">Кол-во пропущенных записей в общем списке</param>
        /// <param name="take">Кол-во взятых записей, начиная с первой не пропущенной записи</param>
        /// <returns>Список матчей</returns>
        IEnumerable<MatchInfo<FootballTeamInfo>> GetFootballMatches(EMatchStatus status, int skip = 0, int take = int.MaxValue);

        #endregion Public methods
    }
}
