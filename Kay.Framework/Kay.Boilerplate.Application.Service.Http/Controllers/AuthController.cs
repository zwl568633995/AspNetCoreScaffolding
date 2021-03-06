﻿using Kay.Framework.AspNetCore.Mvc.Controllers;
using Kay.Framework.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kay.Framework.Utility.Extensions;
using Kay.Framework.Redis;
using Kay.Boilerplate.ApplicationService.IAppService;
using Kay.Framework.AspNetCore.Mvc.Attributes;
using Kay.Boilerplate.ApplicationService.Dto;

namespace Kay.Boilerplate.Application.Service.Http.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthAppService _authAppService;
        private readonly IUserAppService _userAppService;
        public AuthController(IAuthAppService authAppService, IUserAppService userAppService)
        {
            _authAppService = authAppService;
            _userAppService = userAppService;
        }


        /// <summary>
        /// 登录授权
        /// </summary>
        /// <param name="userDto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public string Login(UserDto userDto)
        {
            //1.CheckAccount 可获取相应的角色 存入token
            return _authAppService.GetToken(userDto.UserName, userDto.Password);
        }


        /// <summary>
        /// 测试验证token
        /// </summary>
        /// <returns></returns>
        [NeedAuthorize]
        [HttpGet]
        [Route("test")]
        public string Get()
        {
            return "test";
        }
    }
}
