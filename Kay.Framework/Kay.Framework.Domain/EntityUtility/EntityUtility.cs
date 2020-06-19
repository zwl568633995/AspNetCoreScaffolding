using Kay.Framework.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Kay.Framework.Domain
{
    public static class EntityUtility
    {
        /// <summary>
        /// 组装泛型实体和泛型Id主键的基于Id的查询Expression
        /// </summary>
        /// <typeparam name="TEntity">泛型实体</typeparam>
        /// <typeparam name="TKey">泛型主键</typeparam>
        /// <param name="id">主键值</param>
        /// <returns></returns>
        public static Expression<Func<TEntity, bool>> BuildEqualityExpressionForId<TEntity, TKey>(TKey id)
            where TEntity : IKeyEntity<TKey>
        {
            var parameterExpression = Expression.Parameter(typeof(TEntity));
            var leftExpression = Expression.PropertyOrField(parameterExpression, nameof(IKeyEntity<TKey>.Id));
            Expression<Func<object>> closure = () => id;
            var rightExpression = Expression.Convert(closure.Body, leftExpression.Type);
            var binaryExpression = Expression.Equal(leftExpression, rightExpression);
            return Expression.Lambda<Func<TEntity, bool>>(binaryExpression, parameterExpression);
        }
    }
}
