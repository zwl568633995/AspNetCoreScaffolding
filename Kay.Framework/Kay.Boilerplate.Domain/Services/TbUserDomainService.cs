using Kay.Boilerplate.Domain.Entities;
using Kay.Framework.Domain.Repositories;
using Kay.Framework.Domain.Services;
using Kay.Framework.RegisterInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Boilerplate.Domain.Services
{
    public class TbUserDomainService: DomainService
    {
        private readonly IRepository<TbUserEntity, long> _myUserRepository;

        public TbUserDomainService(
            IServiceProvider serviceProvider,
            IRepository<TbUserEntity, long> myUserRepository) : base(serviceProvider)
        {
            _myUserRepository = myUserRepository;
        }

        public TbUserEntity Get(long id)
        {
            var entity = _myUserRepository.GetById(id);
            return entity;
        }

        public bool Create(TbUserEntity entity)
        {
            _myUserRepository.Add(entity);
            return UnitOfWork.SaveChanges() > 0;
        }
    }
}
