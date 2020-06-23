using Kay.Framework.Domain.Data;
using Kay.Framework.Domain.Entities;
using Kay.Framework.Domain.Specifications;
using Kay.Framework.Exceptions.Common;
using Kay.Framework.Utility.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Z.EntityFramework.Plus;

namespace Kay.Framework.Domain.EntityFrameworkCore.Repositories
{
    public class EfRepository<TEntity, TKey> : IEfRepository<TEntity, TKey>
        where TEntity : class, IKeyEntity<TKey>
    {
        public DbContext DbContext { get; }
        public virtual DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

        private const int DeleteValue = 1;

        public EfRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public TEntity Find(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public TEntity GetById(TKey id)
        {
            var expression = EntityUtility.BuildEqualityExpressionForId<TEntity, TKey>(id);
            var entity = DbSet.FirstOrDefault(expression);
            return entity;
        }

        public IEnumerable<TEntity> ListAll()
        {
            return DbSet.AsEnumerable();
        }

        public TEntity Add(TEntity entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            var exist = GetById(entity.Id);
            if (exist != null)
            {
                DbContext.Entry(exist).CurrentValues.SetValues(entity);
            }
            else
            {
                DbContext.Update(entity);
            }
            //            DbContext.Update(entity);
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="changedPropertyNames"></param>
        public void Update(TEntity entity, string[] changedPropertyNames)
        {
            if (changedPropertyNames == null || changedPropertyNames.Length == 0)
            {
                throw new BaseException("changedPropertyNames is empty!");
            }

            foreach (var propertyName in changedPropertyNames)
            {
                DbContext.Entry(entity).Property(propertyName).IsModified = true;
            }
        }

        public void Delete(TKey id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            if (entity is ISoftStatusEntity softDeletable)
            {
                softDeletable.Status = DeleteValue;
                DbContext.Update(entity);
                return;
            }
            //todo：如果没有Status字段，执行物理删除
            DeleteForced(entity);

            // throw new NalongException($"{typeof(TEntity)} 没有继承自 ISoftDeletable，无法使用Delete方法，请使用 DeleteForced");
        }

        public void Delete(IBaseSpecification<TEntity> spec)
        {
            var list = BuildQuery(spec).ToList();
            foreach (var current in list)
            {
                Delete(current);
            }
        }

        public void DeleteForced(TKey id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                DeleteForced(entity);
            }
        }

        public void DeleteForced(TEntity entity)
        {
            if (entity != null)
            {
                DbContext.Remove(entity);
            }
        }

        public void DeleteForced(IBaseSpecification<TEntity> spec)
        {
            var list = BuildQuery(spec).ToList();
            if (list.IsNotNullOrEmpty())
            {
                DbContext.RemoveRange(list);
            }
        }

        public void DeleteBatch(IBaseSpecification<TEntity> spec)
        {
            BuildQuery(spec).Delete();
        }

        public IEnumerable<TEntity> List(IBaseSpecification<TEntity> spec)
        {
            return BuildQuery(spec).AsEnumerable();
        }

        public TEntity GetSingleBySpec(IBaseSpecification<TEntity> spec)
        {
            return BuildQuery(spec).FirstOrDefault();
        }

        public long Count(IBaseSpecification<TEntity> spec)
        {
            return BuildQuery(spec).Count();
        }

        #region private

        private IQueryable<TEntity> BuildQuery(IBaseSpecification<TEntity> spec)
        {
            var query = DbSet.AsQueryable();
            if (spec.IgnoreSoftDeleteFilter)
            {
                query = query.IgnoreQueryFilters();
            }

            return SpecificationBuilder<TEntity, TKey>.BuildQuery(query, spec);
        }

        #endregion private
    }
}
