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

        #region Private fields

        private readonly CoreGenerator _coreGenerator;

        #endregion Private fields
    }
}
