using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.Exceptions.Common
{
    public interface IErrorCode
    {
        /// <summary>
        /// 业务异常代码
        /// </summary>
        string ErrorCode { get; set; }
    }

    /// <summary>
    /// 为了兼容老的code message
    /// </summary>
    public interface IErrorNumber
    {
        /// <summary>
        /// 业务异常代码（int）
        /// </summary>
        int ErrorNumber { get; set; }
    }
}
