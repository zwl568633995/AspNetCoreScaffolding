using System;
using System.Collections.Generic;
using System.Text;
using Kay.Boilerplate.ApplicationService.Dto;
using Kay.Framework.RegisterInterfaces;

namespace Kay.Boilerplate.ApplicationService.IAppService
{
    public interface IUserAppService: Kay.Framework.RegisterInterfaces.IAppService
    {
        UserDto GetByUserName(string userName);
        bool CheckAccount(string username, string password);
    }
}
