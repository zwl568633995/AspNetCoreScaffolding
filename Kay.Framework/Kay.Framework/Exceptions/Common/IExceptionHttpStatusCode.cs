using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Kay.Framework.Exceptions.Common
{
    public interface IExceptionHttpStatusCode
    {
        HttpStatusCode HttpStatusCode { get; set; }
    }
}
