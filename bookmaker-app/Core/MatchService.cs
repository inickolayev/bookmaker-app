using BookmakerApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmakerApp.Core
{
    /// <summary>
    ///     Сервис, отвечающий за хранение и обработку спортивных результатов
    /// </summary>
    public class MatchService<TTeam>: IMatchService<TTeam>
        where TTeam: TeamInfo
    {
        #region .Ctor

        public MatchService()
        {
            _teams = new List<TTeam>();
            _matches = new List<MatchInfo<TTeam>();
            _results = new List<MatchResultInfo<TTeam>();
        }

        #endregion .Ctor

        #region Public methods

        #region IMatchService Implementation

        /// <summary>
        ///     Получить писок команд
        /// </summary>
        public async Task<IEnumerable<TTeam>> GetTeamsAsync()
            => await Task.FromResult(_teams).ConfigureAwait(false);

        /// <summary>
        ///     Получить список всех футбольных матчей
        /// </summary>
        /// <returns>Список матчей</returns>
        public async Task<IEnumerable<MatchInfo<TTeam>>> GetMatchesAsync(int skip = 0, int take = int.MaxValue)
            => await Task.FromResult(_matches.Skip(skip).Take(take)).ConfigureAwait(false);


        /// <summary>
        ///     Получить список футбольных матчей с учетом фильтрации
        /// </summary>
        /// <param name="status">Статус матчей</param>
        /// <param name="skip">Кол-во пропущенных записей в общем списке</param>
        /// <param name="take">Кол-во взятых записей, начиная с первой не пропущенной записи</param>
        /// <returns>Список матчей</returns>
        public async Task<IEnumerable<MatchInfo<TTeam>>> GetMatchesAsync(EMatchStatus status, int skip = 0, int take = int.MaxValue)
            => await Task.FromResult(_matches.Where(m => m.Status == status).Skip(skip).Take(take)).ConfigureAwait(false);

        /// <summary>
        ///     Получить результат матча
        /// </summary>
        /// <param name="match">Матч</param>
        /// <returns>Результат матча</returns>
        public async Task<OperationResult<MatchResultInfo<TTeam>>> GetMatchResultAsync(MatchInfo<TTeam> match)
        {
            var someResult = new MatchResultInfo<TTeam>
            {
                Id = 1,
                Match = match,
                FirstTeamResult = 0,
                SecondTeamResult = 2,
            };
            return await Task.FromResult(OperationResultFactory.Ok(someResult)).ConfigureAwait(false);
        }

        #endregion IMatchService Implementation

        #endregion Public methods

        #region Private fields

        private readonly IList<TTeam> _teams;
        private readonly IList<MatchInfo<TTeam>> _matches;
        private readonly IList<MatchResultInfo<TTeam>> _results;

        #endregion Private fields
    }
}
