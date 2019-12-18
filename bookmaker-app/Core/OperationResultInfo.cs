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
    public class OperationResultInfo<TContract>
    {
        /// <summary>
        ///     Результат выполнения операции
        /// </summary>
        public TContract Result { get; set; } = default;
        /// <summary>
        ///     Список ошибок
        /// </summary>
        public IEnumerable<ErrorInfo> Errors { get; set; } = new List<ErrorInfo>();
        /// <summary>
        ///     Если ли ошибки
        /// </summary>
        public bool HasErrors => Errors.Any();
    }
}
