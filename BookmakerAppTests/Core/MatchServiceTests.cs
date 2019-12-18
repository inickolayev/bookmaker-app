using BookmakerApp.Core;
using BookmakerApp.Data;
using BookmakerAppTests.Generators;
using BookmakerAppTests.Mocks;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static BookmakerApp.Core.OperationResultHelper;

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

        /// <summary>
        ///     Тест на проверку успешного выполнения идеального сценария
        /// </summary>
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
                Match = match,
                FirstTeamResult = 3,
                SecondTeamResult = 1,
            };

            await _matchService.AddTeam(firstTeam);
            await _matchService.AddTeam(secondTeam);
            await _matchService.AddMatch(match);
            await _matchService.AddMatchResult(expected);

            var opResult = await _matchService.GetMatchResultAsync(match);

            Assert.False(opResult.HasErrors);
            var result = opResult.Result;
            Assert.True(Compare(result, expected));
        }

        [Fact]
        public async Task AddTeam_AlreadyExist_error()
        {
            var teamName = $"team_1";
            var team = _matchServiceGenerator.GenerateTeam(teamName);
            IEnumerable<ErrorInfo> expected = new List<ErrorInfo> { MatchService<TestTeamInfo>.TEAM_ALREADY_EXIST_ERROR(teamName) };

            var firstResult = await _matchService.AddTeam(team);
            Assert.False(firstResult.HasErrors);

            var secondResult = await _matchService.AddTeam(team);
            Assert.True(secondResult.HasErrors);
            var result = secondResult.Errors;
            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task AddMatch_AlreadyExist_error()
        {
            var match = _matchServiceGenerator.GenerateMatch(EMatchStatus.AWAITING);
            IEnumerable<ErrorInfo> expected = new List<ErrorInfo> { MatchService<TestTeamInfo>.MATCH_ALREADY_EXIST_ERROR(match) };

            await _matchService.AddTeam(match.FirstTeam);
            await _matchService.AddTeam(match.SecondTeam);
            var firstResult = await _matchService.AddMatch(match);
            Assert.False(firstResult.HasErrors);

            var secondResult = await _matchService.AddMatch(match);
            Assert.True(secondResult.HasErrors);
            var result = secondResult.Errors;
            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task AddMatch_FirstTeamNotExist_error()
        {
            var match = _matchServiceGenerator.GenerateMatch(EMatchStatus.AWAITING);
            var expected = Error<MatchResultInfo<TestTeamInfo>>(MatchService<TestTeamInfo>.TEAM_DOES_NOT_EXIST_ERROR(match.FirstTeam)).Errors;

            var opResult = await _matchService.AddMatch(match);

            Assert.True(opResult.HasErrors);
            var result = opResult.Errors;
            Assert.Equal(result, expected);
        }

        [Fact]
        public async Task AddMatch_SecondTeamNotExist_error()
        {
            var match = _matchServiceGenerator.GenerateMatch(EMatchStatus.AWAITING);
            var expected = Error<MatchResultInfo<TestTeamInfo>>(MatchService<TestTeamInfo>.TEAM_DOES_NOT_EXIST_ERROR(match.SecondTeam)).Errors;

            await _matchService.AddTeam(match.FirstTeam);
            var opResult = await _matchService.AddMatch(match);

            Assert.True(opResult.HasErrors);
            var result = opResult.Errors;
            Assert.Equal(result, expected);
        }

        #endregion Tests

        #region Private fields

        private readonly IMatchService<TestTeamInfo> _matchService;
        private readonly MatchServiceGenerator _matchServiceGenerator;
        private readonly CoreGenerator _coreGenerator;

        #endregion Private fields

        #region Private methods

        private bool Compare(TeamInfo team1, TeamInfo team2)
            => team1.TeamName == team2.TeamName
            && team1.Rating == team2.Rating;

        private bool Compare<TTeam>(MatchInfo<TTeam> match1, MatchInfo<TTeam> match2)
            where TTeam : TeamInfo
            => Compare(match1.FirstTeam, match2.FirstTeam)
            && Compare(match1.SecondTeam, match2.SecondTeam)
            && match1.Status == match2.Status;

        private bool Compare<TTeam>(MatchResultInfo<TTeam> result1, MatchResultInfo<TTeam> result2)
            where TTeam : TeamInfo
            => Compare(result1.Match, result2.Match)
            && result1.FirstTeamResult == result2.FirstTeamResult
            && result1.SecondTeamResult == result2.SecondTeamResult;

        #endregion Private methods
    }
}
