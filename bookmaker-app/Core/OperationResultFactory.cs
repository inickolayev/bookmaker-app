using System;
using System.Collections.Generic;
using System.Text;

namespace BookmakerApp.Core
{
    /// <summary>
    ///     Фабрика результатов
    /// </summary>
    public class OperationResultFactory
    {
        public OperationResultFactory()
        { }

        public OperationResult<TContract> Ok<TContract>(TContract result)
            => new OperationResult<TContract> { Result = result };

        public OperationResult<TContract> Error<TContract>(params Error[] errors)
            => new OperationResult<TContract> { Errors = errors }; 
    }
}
