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
        public async Task GetMatchResultAsync_Success()
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
        public async Task AddTeam_AlreadyExist_Error()
        {
            var teamName = $"team_1";
            var team = _matchServiceGenerator.GenerateTeam(teamName);
            IEnumerable<ErrorInfo> expected = new List<ErrorInfo> { MatchService<TestTeamInfo>.TEAM_ALREADY_EXIST_ERROR(teamName) };

            var firstResult = await _matchService.AddTeam(team);
            Assert.False(firstResult.HasErrors);

            var secondResult = await _matchService.AddTeam(team);
            Assert.True(secondResult.HasErrors);
            var result = secondResult.Errors;
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task AddMatch_AlreadyExist_Error()
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
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task AddMatch_FirstTeamNotExist_Error()
        {
            var match = _matchServiceGenerator.GenerateMatch(EMatchStatus.AWAITING);
            var expected = Error<MatchResultInfo<TestTeamInfo>>(MatchService<TestTeamInfo>.TEAM_DOES_NOT_EXIST_ERROR(match.FirstTeam)).Errors;

            var opResult = await _matchService.AddMatch(match);

            Assert.True(opResult.HasErrors);
            var result = opResult.Errors;
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task AddMatch_SecondTeamNotExist_Error()
        {
            var match = _matchServiceGenerator.GenerateMatch(EMatchStatus.AWAITING);
            var expected = Error<MatchResultInfo<TestTeamInfo>>(MatchService<TestTeamInfo>.TEAM_DOES_NOT_EXIST_ERROR(match.SecondTeam)).Errors;

            await _matchService.AddTeam(match.FirstTeam);
            var opResult = await _matchService.AddMatch(match);

            Assert.True(opResult.HasErrors);
            var result = opResult.Errors;
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task AddMatchResult_MatchNotExist_Error()
        {
            var matchResult = _matchServiceGenerator.GenerateResult();
            var expected = Error<MatchResultInfo<TestTeamInfo>>(MatchService<TestTeamInfo>.MATCH_DOES_NOT_EXIST_ERROR(matchResult.Match)).Errors;

            await _matchService.AddTeam(matchResult.Match.FirstTeam);
            await _matchService.AddTeam(matchResult.Match.SecondTeam);
            var opResult = await _matchService.AddMatchResult(matchResult);

            Assert.True(opResult.HasErrors);
            var result = opResult.Errors;
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task AddMatchResult_MatchHasWrongStatus_Error()
        {
            var matchResult = _matchServiceGenerator.GenerateResult();
            matchResult.Match.Status = EMatchStatus.AWAITING;
            var expected = Error<MatchResultInfo<TestTeamInfo>>(MatchService<TestTeamInfo>.MATCH_WRONG_STATUS_ERROR(matchResult.Match)).Errors;

            await _matchService.AddTeam(matchResult.Match.FirstTeam);
            await _matchService.AddTeam(matchResult.Match.SecondTeam);
            await _matchService.AddMatch(matchResult.Match);
            var opResult = await _matchService.AddMatchResult(matchResult);

            Assert.True(opResult.HasErrors);
            var result = opResult.Errors;
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetMatchResult_MatchNotExist_Error()
        {
            var matchResult = _matchServiceGenerator.GenerateResult();
            var expected = Error<MatchResultInfo<TestTeamInfo>>(MatchService<TestTeamInfo>.MATCH_DOES_NOT_EXIST_ERROR(matchResult.Match)).Errors;

            await _matchService.AddTeam(matchResult.Match.FirstTeam);
            await _matchService.AddTeam(matchResult.Match.SecondTeam);
            var opResult = await _matchService.GetMatchResultAsync(matchResult.Match);

            Assert.True(opResult.HasErrors);
            var result = opResult.Errors;
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetMatchResults_Success()
        {
            var matchResult1 = _matchServiceGenerator.GenerateResult();
            var matchResult2 = _matchServiceGenerator.GenerateResult();
            var expected = new List<MatchResultInfo<TestTeamInfo>>
            {
                matchResult1,
                matchResult2
            };

            await AddWithDependencies(matchResult1);
            await AddWithDependencies(matchResult2);
            var opResult = await _matchService.GetMatchResultsAsync();

            Assert.False(opResult.HasErrors);
            var result = opResult.Result;
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetMatchResults_ForTeam_Success()
        {
            var team = _matchServiceGenerator.GenerateTeam();
            var team1 = _matchServiceGenerator.GenerateTeam();
            var team2 = _matchServiceGenerator.GenerateTeam();
            var match1 = _matchServiceGenerator.GenerateMatch(EMatchStatus.AWAITING, team, team1);
            var match2 = _matchServiceGenerator.GenerateMatch(EMatchStatus.AWAITING, team, team2);
            var match3 = _matchServiceGenerator.GenerateMatch(EMatchStatus.AWAITING, team1, team2);
            var expected = new List<MatchInfo<TestTeamInfo>>
            {
                match1, match2
            };

            await _matchService.AddTeam(team);
            await _matchService.AddTeam(team1);
            await _matchService.AddTeam(team2);
            await _matchService.AddMatch(match1);
            await _matchService.AddMatch(match2);
            await _matchService.AddMatch(match3);
            var opResult = await _matchService.GetMatchesAsync(team);

            Assert.False(opResult.HasErrors);
            var result = opResult.Result;
            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task GetMatches_Success()
        {
            var match1 = _matchServiceGenerator.GenerateMatch(EMatchStatus.AWAITING);
            var match2 = _matchServiceGenerator.GenerateMatch(EMatchStatus.FINISHED);
            var expected = new List<MatchInfo<TestTeamInfo>>
            {
                match1,
                match2
            };

            await AddWithDependencies(match1);
            await AddWithDependencies(match2);
            var opResult = await _matchService.GetMatchesAsync();

            Assert.False(opResult.HasErrors);
            var result = opResult.Result;
            Assert.Equal(expected, result);
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

        private async Task AddWithDependencies(MatchInfo<TestTeamInfo> match)
        {
            await _matchService.AddTeam(match.FirstTeam);
            await _matchService.AddTeam(match.SecondTeam);
            await _matchService.AddMatch(match);
        }

        private async Task AddWithDependencies(MatchResultInfo<TestTeamInfo> result)
        {
            await AddWithDependencies(result.Match);
            await _matchService.AddMatchResult(result);
        }

        #endregion Private methods
    }
}
