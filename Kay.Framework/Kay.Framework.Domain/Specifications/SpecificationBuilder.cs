using Kay.Framework.Domain.Entities;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Kay.Framework.Domain.Specifications
{
    /// <summary>
    /// 通用Specification构造器
    /// </summary>
    /// <typeparam name="TEntity">泛型实体</typeparam>
    /// <typeparam name="TKey">泛型Id</typeparam>
    public class SpecificationBuilder<TEntity, TKey> where TEntity : class, IKeyEntity<TKey>
    {
        public static IQueryable<TEntity> BuildQuery(
            IQueryable<TEntity> inputQuery,
            IBaseSpecification<TEntity> baseSpecification)
        {
            var query = inputQuery;
            if (baseSpecification.Where != null)
            {
                query = query.Where(baseSpecification.Where);
            }

            if (string.IsNullOrEmpty(baseSpecification.Sorting))
            {
                if (baseSpecification.OrderBy != null)
                {
                    query = query.OrderBy(baseSpecification.OrderBy);
                }

                else if (baseSpecification.OrderByDescending != null)
                {
                    query = query.OrderByDescending(baseSpecification.OrderByDescending);
                }
            }
            else
            {
                query = query.OrderBy(baseSpecification.Sorting);
            }

            if (baseSpecification.IsPaging)
            {
                query = query
                    .Skip(baseSpecification.Skip)
                    .Take(baseSpecification.Take);
            }
            return query;
        }
    }
}
