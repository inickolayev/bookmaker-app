using System;
using System.Collections.Generic;
using System.Text;

namespace BookmakerApp.Data
{
    /// <summary>
    ///     Модель пользователя
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        ///     Имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///     Счет
        /// </summary>
        public double Account { get; set; }
    }
}
