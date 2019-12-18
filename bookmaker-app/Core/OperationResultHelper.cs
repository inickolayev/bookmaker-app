using System;
using System.Collections.Generic;
using System.Text;

namespace BookmakerApp.Core
{
    /// <summary>
    ///     Хелпер для создания результаттов операций
    /// </summary>
    public static class OperationResultHelper
    {
        public static OperationResultInfo<TContract> Ok<TContract>(TContract result)
            => new OperationResultInfo<TContract> { Result = result };

        public static OperationResultInfo<TContract> Error<TContract>(params ErrorInfo[] errors)
            => new OperationResultInfo<TContract> { Errors = errors }; 
    }
}
