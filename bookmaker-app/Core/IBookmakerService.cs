using BookmakerApp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookmakerApp.Core
{
    /// <summary>
    ///     Сервис-букмейкер
    /// </summary>
    public interface IBookmakerService
    {
        /// <summary>
        ///     Сделать ставку
        /// </summary>
        /// <typeparam name="TTeam">Тип соревнования</typeparam>
        /// <param name="expectedResult">Ожидаемый результат</param>
        /// <returns></returns>
        Task<OperationResult<MatchBetInfo<TTeam>>> MakeBetAsync<TTeam>(MatchResultInfo<TTeam> expectedResult)
            where TTeam : TeamInfo;

        /// <summary>
        ///     Получить результат по ставке
        /// </summary>
        /// <typeparam name="TTeam">Тип соревнования</typeparam>
        /// <param name="bet">Ставка</param>
        /// <returns>Результат по ставке</returns>
        Task<OperationResult<MatchBetResultInfo<TTeam>>> GetResultAsync<TTeam>(MatchBetInfo<TTeam> bet)
            where TTeam: TeamInfo;
    }
}
