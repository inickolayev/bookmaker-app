using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BookmakerApp.Core
{
    /// <summary>
    ///     Результат выполнения операции
    /// </summary>
    /// <typeparam name="TContract"></typeparam>
    public class OperationResult<TContract>
    {
        /// <summary>
        ///     Результат выполнения операции
        /// </summary>
        public TContract Result { get; set; } = default;
        /// <summary>
        ///     Список ошибок
        /// </summary>
        public IEnumerable<Error> Errors { get; set; } = new List<Error>();
        /// <summary>
        ///     Если ли ошибки
        /// </summary>
        public bool HasErrors => Errors.Any();
    }
}
