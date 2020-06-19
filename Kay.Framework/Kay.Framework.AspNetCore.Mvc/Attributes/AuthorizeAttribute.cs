using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.AspNetCore.Mvc.Attributes
{
    /// <summary>
    /// 授权标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorizeAttribute : Attribute
    {

    }
}
