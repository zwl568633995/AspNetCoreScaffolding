using Kay.Boilerplate.ApplicationService.Dto;
using Kay.Boilerplate.ApplicationService.IAppService;
using Kay.Boilerplate.Domain.Entities;
using Kay.Boilerplate.Domain.Services;
using Kay.Boilerplate.Domain.Specifications;
using Kay.Framework.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Kay.Boilerplate.ApplicationService.AppService
{
    public class UserAppService : Kay.Framework.Application.Application.Services.AppService, IUserAppService
    {
        private readonly TbUserDomainService _tbUserDomainService;
        private readonly IRepository<TbUserEntity, long> _userRepository;

        public UserAppService(
           IServiceProvider serviceProvider,
           IRepository<TbUserEntity, long> userRepository,
           TbUserDomainService tbUserDomainService)
           : base(serviceProvider)
        {
            _userRepository = userRepository;
            _tbUserDomainService = tbUserDomainService;
        }

        public bool CheckAccount(string username, string password)
        {
            if (string.IsNullOrEmpty(username)||string.IsNullOrEmpty(password))
            {
                throw new ValidationException("用户名和密码不能为空");
            }

            TbUserEntity tbUserEntity = _userRepository.GetSingleBySpec(new UserFindQuery(username));
            if (tbUserEntity==null)
            {
                throw new ValidationException("当前用户不存在");
            }

            throw new NotImplementedException();
        }

        public UserDto GetByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ValidationException("用户名不能为空");
            }

            TbUserEntity tbUserEntity = _userRepository.GetSingleBySpec(new UserFindQuery(userName));
            Mapper.Bind<TbUserEntity, UserDto>();
            return Mapper.Map<UserDto>(tbUserEntity);
        }
    }
}
