using System;
using System.Collections.Generic;
using System.Text;

namespace BookmakerApp.Data
{
    /// <summary>
    ///     Статус матча
    /// </summary>
    public enum EMatchStatus
    {
        /// <summary>
        ///     Матч ожидается
        /// </summary>
        AWAITING,
        /// <summary>
        ///     Матч идет
        /// </summary>
        IN_PROCESS,
        /// <summary>
        ///     Матч завершился
        /// </summary>
        FINISHED
    }
}
