using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kay.Boilerplate.ApplicationService.Dto;
using Kay.Boilerplate.ApplicationService.IAppService;
using Kay.Framework.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Kay.Boilerplate.Application.Service.Http.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getbyusername")]
        public UserDto GetByUserName(string userName)
        {
            return _userAppService.GetByUserName(userName);
        }
    }
}
