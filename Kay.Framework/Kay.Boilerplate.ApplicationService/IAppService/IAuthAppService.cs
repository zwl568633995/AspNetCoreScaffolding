using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Boilerplate.ApplicationService.IAppService
{
    public interface IAuthAppService :Kay.Framework.RegisterInterfaces.IAppService
    {
        string GetToken(string username,string pwd);
    }
}
