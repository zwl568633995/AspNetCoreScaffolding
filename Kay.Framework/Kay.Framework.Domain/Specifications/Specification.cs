using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Kay.Framework.Domain.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public virtual bool IsSatisfiedBy(T obj)
        {
            return ToExpression().Compile()(obj);
        }

        public abstract Expression<Func<T, bool>> ToExpression();

        public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
        {
            return specification.ToExpression();
        }
    }
}
