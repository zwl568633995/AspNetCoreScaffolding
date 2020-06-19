using Kay.Framework.Domain.Data;
using Kay.Framework.Domain.Entities;
using Kay.Framework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kay.Framework.Domain.EntityFrameworkCore.Repositories
{
    public class EfRepository<TEntity, TKey> : IEfRepository<TEntity, TKey>
        where TEntity : class, IKeyEntity<TKey>
    {
        public DbContext DbContext{get;}

        private const int DeleteValue = 1;

        public DbSet<TEntity> DbSet => DbContext.Set<TEntity>();

        public EfRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public TEntity Add(TEntity entity)
        {
            DbSet.Add(entity);
            return entity;
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

        public TEntity Find(params object[] keyValues)
        {
            return DbSet.Find(keyValues);
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
          
            return entity;
        }

        public void Update(TEntity entity, params string[] changedPropertyNames)
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
    }
}
