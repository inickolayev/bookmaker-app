using System;
using System.Collections.Generic;
using System.Text;

namespace BookmakerApp.Core
{
    /// <summary>
    ///     Ошибка
    /// </summary>
    public class ErrorInfo
    {
        public ErrorInfo(string mess)
        {
            Message = mess;
        }

        /// <summary>
        ///     Сообщение об ошибке
        /// </summary>
        public string Message { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ErrorInfo err)
            {
                return err.Message == Message;
            }
            return false;
        }
    }
}
