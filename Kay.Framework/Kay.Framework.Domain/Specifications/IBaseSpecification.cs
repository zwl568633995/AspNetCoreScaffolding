using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Kay.Framework.Domain.Specifications
{
    /// <summary>
    /// Expression 抽象接口
    /// 为了避免出现一大坨各种查询(a=>a.Id == 123 && a.Name == "abc")，build query 要相对谨慎，有现成的就不要新增Specification类
    /// </summary>
    /// <typeparam name="TEntity">泛型实体</typeparam>
    public interface IBaseSpecification<TEntity>
    {
        /// <summary>
        ///<see cref="NL.Framework.Domain.Specifications.SpecificationBuilder"/>
        /// 和OrderBy  OrderByDescending 互斥
        /// <example>
        /// Url 请求 Examples:
        /// "Name"
        /// "Name DESC"
        /// "Name ASC, Id DESC"
        /// </example>
        /// </summary>
        string Sorting { get; }

        Expression<Func<TEntity, bool>> Where { get; }
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescending { get; }

        /// <summary>
        /// 忽略软删除过滤
        /// </summary>
        bool IgnoreSoftDeleteFilter { get; }

        #region 分页相关

        int Take { get; }
        int Skip { get; }
        bool IsPaging { get; }

        #endregion
    }
}
