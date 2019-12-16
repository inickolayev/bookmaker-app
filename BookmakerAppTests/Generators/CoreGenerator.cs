using System;
using System.Collections.Generic;
using System.Text;

namespace BookmakerAppTests.Generators
{
    /// <summary>
    ///     Генератор различных примитивов
    /// </summary>
    class CoreGenerator
    {
        #region .Ctor

        public CoreGenerator()
        {
            _rnd = new Random();
        }

        #endregion .Ctor

        #region Public fields

        /// <summary>
        ///     Сгенерировать целое число
        /// </summary>
        public int GenerateInt(int min = int.MinValue, int max = int.MaxValue)
            => new Random().Next(min, max);

        public double GenerateDouble(double min = int.MinValue, double max = int.MaxValue)
            => GenerateInt((int)min, (int)max - 1) + _rnd.NextDouble();

        public string GenerateName()
            => Guid.NewGuid().ToString();

        #region Private fields

        private readonly Random _rnd;

        #endregion Private fields
    }
}
