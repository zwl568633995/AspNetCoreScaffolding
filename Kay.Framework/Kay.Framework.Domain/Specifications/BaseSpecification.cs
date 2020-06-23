using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;

namespace Kay.Framework.Domain.Specifications
{
    /// <summary>
    /// 表达式构造基类，通过构造函数传递查询表达式Expression
    /// </summary>
    /// <typeparam name="TEntity">泛型实体</typeparam>
    public abstract class BaseSpecification<TEntity> : IBaseSpecification<TEntity>
    {
        protected BaseSpecification(Expression<Func<TEntity, bool>> whereExpression, bool ignoreSoftDeleteFilter = false)
        {
            Where = whereExpression;
            IgnoreSoftDeleteFilter = ignoreSoftDeleteFilter;

        }
        public string Sorting { get; private set; }
        public Expression<Func<TEntity, bool>> Where { get; }
        public Expression<Func<TEntity, object>> OrderBy { get; private set; }
        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }
        public bool IgnoreSoftDeleteFilter { get; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPaging { get; private set; }

        #region 构造分页、正序、降序

        /// <summary>
        /// 构建分页
        /// </summary>
        /// <param name="pageNumber">从1开始</param>
        /// <param name="pageSize">每页条数</param>
        public virtual void BuildPaging(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }

            Skip = (pageNumber - 1) * pageSize;
            Take = pageSize;
            IsPaging = true;
        }

        /// <summary>
        /// 此方法用于Request传字符串排序 和BuildOrderBy、BuildOrderByDescending 互斥
        /// </summary>
        /// <param name="sorting"></param>
        public virtual void BuildSorting(string sorting)
        {
            if (OrderBy != null || OrderByDescending != null)
            {
                Exception();
            }
            Sorting = sorting;
        }

        /// <summary>
        /// 此方法用于Specification类构造函数中设置 和BuildSorting互斥
        /// </summary>
        /// <param name="orderByExpression"></param>
        protected virtual void BuildOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            if (!string.IsNullOrEmpty(Sorting))
            {
                Exception();
            }
            OrderBy = orderByExpression;
        }

        /// <summary>
        /// 此方法用于Specification类构造函数中设置 和BuildSorting互斥
        /// </summary>
        /// <param name="orderByDescendingExpression"></param>
        protected virtual void BuildOrderByDescending(Expression<Func<TEntity, object>> orderByDescendingExpression)
        {
            if (!string.IsNullOrEmpty(Sorting))
            {
                Exception();
            }
            OrderByDescending = orderByDescendingExpression;
        }

        private void Exception()
        {
            throw new Exception("OrderBy OrderByDescending 和 Sort 不能同时赋值");
        }
        #endregion
    }
}
