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
        ///     Сгенерировать команду
        /// </summary>
        public MatchInfo<TestTeamInfo> GenerateMatch(EMatchStatus status, string teamName1 = "team_1", string teamName2 = "team_2")
            => new MatchInfo<TestTeamInfo>
            {
                Id = _coreGenerator.GenerateInt(1),
                FirstTeam = GenerateTeam(teamName1),
                SecondTeam = GenerateTeam(teamName2),
                Status = status
            };

        #region Private fields

        private readonly CoreGenerator _coreGenerator;

        #endregion Private fields
    }
}
