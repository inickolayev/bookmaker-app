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
        public IEnumerable<Error> Errors { get; private set; } = new List<Error>();
        /// <summary>
        ///     Если ли ошибки
        /// </summary>
        public bool HasErrors => Errors.Any();

        public static OperationResult<TContract> Ok(TContract result)
            => new OperationResult<TContract> { Result = result };

        public static OperationResult<TContract> Error(params Error[] errors)
            => new OperationResult<TContract> { Errors = errors };
    }
}
