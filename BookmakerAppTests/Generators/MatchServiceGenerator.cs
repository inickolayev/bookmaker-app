using BookmakerApp.Data;
using BookmakerAppTests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookmakerAppTests.Generators
{
    /// <summary>
    ///     Генератор сущностей для сервиса матчей
    /// </summary>
    public class MatchServiceGenerator
    {
        public MatchServiceGenerator(CoreGenerator coreGenerator)
        {
            _coreGenerator = coreGenerator;
        }

        /// <summary>
        ///     Сгенерировать команду
        /// </summary>
        public TestTeamInfo GenerateTeam(string name)
            => new TestTeamInfo
            {
                Id = _coreGenerator.GenerateInt(1),
                Rating = _coreGenerator.GenerateDouble(0, 100),
                TeamName = name ?? _coreGenerator.GenerateName(),
            };

        /// <summary>
        ///     Сгенерировать матч
        /// </summary>
        public MatchInfo<TestTeamInfo> GenerateMatch(EMatchStatus status, string teamName1 = "team_1", string teamName2 = "team_2")
            => new MatchInfo<TestTeamInfo>
            {
                Id = _coreGenerator.GenerateInt(1),
                FirstTeam = GenerateTeam(teamName1),
                SecondTeam = GenerateTeam(teamName2),
                Status = status
            };


        /// <summary>
        ///     Сгенерировать результат матча
        /// </summary>
        public MatchResultInfo<TestTeamInfo> GenerateResult(int firtTeamResult, int secondTeamResult, string teamName1 = "team_1", string teamName2 = "team_2")
            => new MatchResultInfo<TestTeamInfo>
            {
                Id = _coreGenerator.GenerateInt(1),
                Match = GenerateMatch(EMatchStatus.FINISHED, teamName1, teamName2),
                FirstTeamResult = firtTeamResult,
                SecondTeamResult = secondTeamResult,
            };

        /// <summary>
        ///     Сгенерировать результат матча
        /// </summary>
        public MatchResultInfo<TestTeamInfo> GenerateResult(string teamName1 = "team_1", string teamName2 = "team_2")
            => GenerateResult(_coreGenerator.GenerateInt(0, 10), _coreGenerator.GenerateInt(0, 10), teamName1, teamName2);

        #region Private fields

        private readonly CoreGenerator _coreGenerator;

        #endregion Private fields
    }
}
