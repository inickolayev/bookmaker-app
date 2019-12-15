using BookmakerApp.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookmakerApp.Core
{
    /// <summary>
    ///     Сервис пользователей
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        ///     Получить список пользователей
        /// </summary>
        Task<IEnumerable<UserInfo>> GetUsersAsync();
    }
}
