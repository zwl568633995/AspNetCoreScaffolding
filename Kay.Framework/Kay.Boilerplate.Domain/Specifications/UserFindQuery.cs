using Kay.Boilerplate.Domain.Entities;
using Kay.Framework.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Kay.Boilerplate.Domain.Specifications
{
    public class UserFindQuery : BaseSpecification<TbUserEntity>
    {
        public UserFindQuery(string userName) : base(new UserSpec(userName))
        {
        }
    }


    public class UserSpec : Specification<TbUserEntity>
    {
        private string UserName { get; }

        public UserSpec(string userName)
        {
            UserName = userName;
        }

        public override Expression<Func<TbUserEntity, bool>> ToExpression()
        {
            return a => a.UserName == UserName;
        }
    }
}
