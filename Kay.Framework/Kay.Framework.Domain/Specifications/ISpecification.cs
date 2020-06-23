using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Kay.Framework.Domain.Specifications
{
    public interface ISpecification<TEntity>
    {
        bool IsSatisfiedBy(TEntity obj);
        Expression<Func<TEntity, bool>> ToExpression();
    }
}
