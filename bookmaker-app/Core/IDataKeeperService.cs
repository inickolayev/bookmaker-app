using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookmakerApp.Data;

namespace BookmakerApp.Core
{
    /// <summary>
    ///     Сервис, отвечающий за хранение спортивных результатов
    /// </summary>
    public interface IDataKeeperService
    {
        #region Public methods

        /// <summary>
        ///     Футбольные команды
        /// </summary>
        Task<IEnumerable<FootballTeamInfo>> GetFootballTeamsAsync();

        /// <summary>
        ///     Получить список всех футбольных матчей
        /// </summary>
        /// <returns>Список матчей</returns>
        Task<IEnumerable<MatchInfo<FootballTeamInfo>>> GetFootballMatchesAsync();

        /// <summary>
        ///     Получить список футбольных матчей с учетом фильтрации
        /// </summary>
        /// <param name="status">Статус матчей</param>
        /// <param name="skip">Кол-во пропущенных записей в общем списке</param>
        /// <param name="take">Кол-во взятых записей, начиная с первой не пропущенной записи</param>
        /// <returns>Список матчей</returns>
        Task<IEnumerable<MatchInfo<FootballTeamInfo>>> GetFootballMatchesAsync(EMatchStatus status, int skip = 0, int take = int.MaxValue);

        #endregion Public methods
    }
}
