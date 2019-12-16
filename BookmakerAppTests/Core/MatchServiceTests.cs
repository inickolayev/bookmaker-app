using BookmakerApp.Core;
using BookmakerApp.Data;
using BookmakerAppTests.Generators;
using BookmakerAppTests.Mocks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BookmakerAppTests.Core
{
    public class MatchServiceTests
    {
        #region .Ctor

        public MatchServiceTests()
        {
            _matchService = new MatchService<TestTeamInfo>();
            _coreGenerator = new CoreGenerator();
            _matchServiceGenerator = new MatchServiceGenerator(_coreGenerator);
        }

        #endregion .Ctor

        #region Tests

        [Fact]
        public async Task GetMatchResultAsync_success()
        {
            var firstTeam = _matchServiceGenerator.GenerateTeam($"team_1");
            var secondTeam = _matchServiceGenerator.GenerateTeam($"team_2");
            var match = new MatchInfo<TestTeamInfo>
            {
                Id = 1,
                FirstTeam = firstTeam,
                SecondTeam = secondTeam,
                Status = EMatchStatus.FINISHED
            };

            var expected = new MatchResultInfo<TestTeamInfo>
            {
                Id = 1,
                FirstTeamResult = 3,
                SecondTeamResult = 5,
                Match = match,
            };
            var opResult = await _matchService.GetMatchResultAsync(match);

            Assert.False(opResult.HasErrors);
            var result = opResult.Result;
            Assert.True(result.FirstTeamResult == expected.FirstTeamResult
                && result.SecondTeamResult == expected.SecondTeamResult
                && result.Match == expected.Match);
        }

        #endregion Tests


        #region Private fields

        private readonly IMatchService<TestTeamInfo> _matchService;
        private readonly MatchServiceGenerator _matchServiceGenerator;
        private readonly CoreGenerator _coreGenerator;

        #endregion Private fields
    }
}
