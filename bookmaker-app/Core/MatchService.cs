using BookmakerApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BookmakerApp.Core.OperationResultHelper;

namespace BookmakerApp.Core
{
    /// <summary>
    ///     Сервис, отвечающий за хранение и обработку спортивных результатов
    /// </summary>
    public class MatchService<TTeam> : IMatchService<TTeam>
        where TTeam : TeamInfo
    {
        #region .Ctor

        public MatchService()
        {
            _teams = new List<TTeam>();
            _matches = new List<MatchInfo<TTeam>>();
            _results = new List<MatchResultInfo<TTeam>>();
        }

        #endregion .Ctor

        #region Public methods

        #region IMatchService Implementation

        #region Teams

        /// <summary>
        ///     Добавить команду
        /// </summary>
        /// <param name="team">Команда для добавления</param>
        /// <returns>Результат выполнения операции добавления</returns>
        public async Task<OperationResultInfo<TTeam>> AddTeam(TTeam team)
        {
            if (_teams.Contains(team))
            {
                return Error<TTeam>(TEAM_ALREADY_EXIST_ERROR(team.TeamName));
            }
            _teams.Add(team);
            return Ok(team);
        }

        /// <summary>
        ///     Получить писок команд
        /// </summary>
        public async Task<IEnumerable<TTeam>> GetTeamsAsync()
            => throw new NotImplementedException();

        #endregion Teams

        #region Matches

        /// <summary>
        ///     Добавить матч
        /// </summary>
        /// <param name="match">Матч</param>
        /// <returns>Результат выполнения операции добавления</returns>
        public async Task<OperationResultInfo<MatchInfo<TTeam>>> AddMatch(MatchInfo<TTeam> match)
        {
            if (!_teams.Contains(match.FirstTeam))
                return Error<MatchInfo<TTeam>>(TEAM_DOES_NOT_EXIST_ERROR(match.FirstTeam));
            if (!_teams.Contains(match.SecondTeam))
                return Error<MatchInfo<TTeam>>(TEAM_DOES_NOT_EXIST_ERROR(match.SecondTeam));
            if (_matches.Contains(match))
                return Error<MatchInfo<TTeam>>(MATCH_ALREADY_EXIST_ERROR(match));
            _matches.Add(match);
            return Ok(match);
        }

        /// <summary>
        ///     Получить список всех футбольных матчей
        /// </summary>
        /// <returns>Список матчей</returns>
        public async Task<IEnumerable<MatchInfo<TTeam>>> GetMatchesAsync()
            => throw new NotImplementedException();

        /// <summary>
        ///     Получить список футбольных матчей с учетом фильтрации
        /// </summary>
        /// <param name="status">Статус матчей</param>
        /// <returns>Список матчей</returns>
        public async Task<IEnumerable<MatchInfo<TTeam>>> GetMatchesAsync(EMatchStatus status)
            => throw new NotImplementedException();

        #endregion Matches

        #region MatchResults

        /// <summary>
        ///     Добавить результат матча
        /// </summary>
        /// <param name="result">Результат матча</param>
        /// <returns>Результат выполнения операции добавления</returns>
        public async Task<OperationResultInfo<MatchResultInfo<TTeam>>> AddMatchResult(MatchResultInfo<TTeam> result)
        {
            if (!_matches.Contains(result.Match))
            {
                return Error<MatchResultInfo<TTeam>>(MATCH_DOES_NOT_EXIST_ERROR(result.Match));
            }
            _results.Add(result);
            return Ok(result);
        }

        /// <summary>
        ///     Получить результаты матчей
        /// </summary>
        /// <returns>Результаты матчей</returns>
        public async Task<OperationResultInfo<IEnumerable<MatchResultInfo<TTeam>>>> GetMatchResultsAsync()
            => throw new NotImplementedException();

        /// <summary>
        ///     Получить результаты матчей для команды
        /// </summary>
        /// <param name="team">Команда, по которой происходит запрос</param>
        /// <returns>Результаты матчей</returns>
        public async Task<OperationResultInfo<IEnumerable<MatchResultInfo<TTeam>>>> GetMatchResultsAsync(TTeam team)
            => throw new NotImplementedException();

        /// <summary>
        ///     Получить результаты матчей для команды с заданым статусом
        /// </summary>
        /// <param name="team">Команда, по которой происходит запрос</param>
        /// <param name="status">Статусы данных матчей</param>
        /// <returns>Результаты матчей</returns>
        public async Task<OperationResultInfo<IEnumerable<MatchResultInfo<TTeam>>>> GetMatchResultsAsync(TTeam team, EMatchStatus status)
            => throw new NotImplementedException();

        /// <summary>
        ///     Получить результат матча
        /// </summary>
        /// <param name="match">Матч</param>
        /// <returns>Результат матча</returns>
        public async Task<OperationResultInfo<MatchResultInfo<TTeam>>> GetMatchResultAsync(MatchInfo<TTeam> match)
        {
            if (!_matches.Contains(match))
            {
                return Error<MatchResultInfo<TTeam>>(MATCH_DOES_NOT_EXIST_ERROR(match));
            }
            var result = _results.First(_ => _.Match == match);
            return Ok(result);
        }

        #endregion MatchResults

        #endregion IMatchService Implementation

        #endregion Public methods

        #region Errors

        public static ErrorInfo TEAM_ALREADY_EXIST_ERROR(string teamName)
            => new ErrorInfo($"Команда \"{teamName}\" уже существует");
        public static ErrorInfo TEAM_DOES_NOT_EXIST_ERROR(TTeam team)
            => new ErrorInfo($"Команды \"{team.TeamName}\" не существует");
        public static ErrorInfo MATCH_ALREADY_EXIST_ERROR(MatchInfo<TTeam> match)
            => new ErrorInfo($"Матч между \"{match.FirstTeam.TeamName}\" и \"{match.SecondTeam.TeamName}\" уже существует");
        public static ErrorInfo MATCH_DOES_NOT_EXIST_ERROR(MatchInfo<TTeam> match)
            => new ErrorInfo($"Матч между \"{match.FirstTeam.TeamName}\" и \"{match.SecondTeam.TeamName}\" не существует");

        #endregion Errors

        #region Private fields

        private readonly IList<TTeam> _teams;
        private readonly IList<MatchInfo<TTeam>> _matches;
        private readonly IList<MatchResultInfo<TTeam>> _results;

        #endregion Private fields
    }
}
