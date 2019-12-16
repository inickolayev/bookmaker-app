using System;
using System.Collections.Generic;
using System.Text;

namespace BookmakerApp.Data
{
    /// <summary>
    ///     Базовый класс модели
    /// </summary>
    public abstract class BaseInfo
    {
        /// <summary>
        ///     Идентификатор
        /// </summary>
        public int Id { get; set; }
    }
}
