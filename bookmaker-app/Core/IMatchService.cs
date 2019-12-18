using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BookmakerApp.Data;

namespace BookmakerApp.Core
{
    /// <summary>
    ///     Сервис, отвечающий за хранение и обработку спортивных результатов
    /// </summary>
    public interface IMatchService<TTeam>
            where TTeam : TeamInfo
    {
        #region Teams

        /// <summary>
        ///     Добавить команду
        /// </summary>
        /// <param name="team">Команда для добавления</param>
        /// <returns>Результат выполнения операции добавления</returns>
        Task<OperationResultInfo<TTeam>> AddTeam(TTeam team);

        /// <summary>
        ///     Получить писок команд
        /// </summary>
        Task<IEnumerable<TTeam>> GetTeamsAsync();

        #endregion Teams

        #region Matches

        /// <summary>
        ///     Добавить матч
        /// </summary>
        /// <param name="match">Матч</param>
        /// <returns>Результат выполнения операции добавления</returns>
        Task<OperationResultInfo<MatchInfo<TTeam>>> AddMatch(MatchInfo<TTeam>  match);

        /// <summary>
        ///     Получить список всех футбольных матчей
        /// </summary>
        /// <returns>Список матчей</returns>
        Task<IEnumerable<MatchInfo<TTeam>>> GetMatchesAsync();

        /// <summary>
        ///     Получить список футбольных матчей с учетом фильтрации
        /// </summary>
        /// <param name="status">Статус матчей</param>
        /// <returns>Список матчей</returns>
        Task<IEnumerable<MatchInfo<TTeam>>> GetMatchesAsync(EMatchStatus status);

        #endregion Matches

        #region MatchResults

        /// <summary>
        ///     Добавить результат матча
        /// </summary>
        /// <param name="result">Результат матча</param>
        /// <returns>Результат выполнения операции добавления</returns>
        Task<OperationResultInfo<MatchResultInfo<TTeam>>> AddMatchResult(MatchResultInfo<TTeam> result);

        /// <summary>
        ///     Получить результаты матчей
        /// </summary>
        /// <returns>Результаты матчей</returns>
        Task<OperationResultInfo<IEnumerable<MatchResultInfo<TTeam>>>> GetMatchResultsAsync();

        /// <summary>
        ///     Получить результаты матчей для команды
        /// </summary>
        /// <param name="team">Команда, по которой происходит запрос</param>
        /// <returns>Результаты матчей</returns>
        Task<OperationResultInfo<IEnumerable<MatchResultInfo<TTeam>>>> GetMatchResultsAsync(TTeam team);

        /// <summary>
        ///     Получить результаты матчей для команды с заданым статусом
        /// </summary>
        /// <param name="team">Команда, по которой происходит запрос</param>
        /// <param name="status">Статусы данных матчей</param>
        /// <returns>Результаты матчей</returns>
        Task<OperationResultInfo<IEnumerable<MatchResultInfo<TTeam>>>> GetMatchResultsAsync(TTeam team, EMatchStatus status);

        /// <summary>
        ///     Получить результат матча
        /// </summary>
        /// <param name="match">Матч</param>
        /// <returns>Результат матча</returns>
        Task<OperationResultInfo<MatchResultInfo<TTeam>>> GetMatchResultAsync(MatchInfo<TTeam> match);

        #endregion MatchResults
    }
}
