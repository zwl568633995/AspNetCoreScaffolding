using Kay.Framework.Domain.Data;
using Kay.Framework.Domain.Entities;
using Kay.Framework.Extensions;
using Kay.Framework.Utility.Utilities.Snowflake;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Kay.Framework.Domain.EntityFrameworkCore
{
    public class BaseDbContext : DbContext
    {

        private readonly IConfiguration _configuration;

        public BaseDbContext(DbContextOptions options,
            IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        #region 软删除相关配置值

        private const string IsDeletedProperty = nameof(ISoftStatusEntity.Status);
        private const int DeleteValue = 1;

        #endregion

        #region 软删除查询实现相关方法

        private static readonly MethodInfo PropertyMethod =
            typeof(EF).GetMethod(nameof(EF.Property), BindingFlags.Static | BindingFlags.Public)
                ?.MakeGenericMethod(typeof(int));

        private static LambdaExpression GetIsDeletedRestriction(Type type)
        {
            var pram = Expression.Parameter(type, "it");
            var prop = Expression.Call(PropertyMethod, pram, Expression.Constant(IsDeletedProperty));
            var condition = Expression.MakeBinary(ExpressionType.NotEqual, prop, Expression.Constant(DeleteValue));
            var lambda = Expression.Lambda(condition, pram);
            return lambda;
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 软删除查询实现

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(ISoftStatusEntity).IsAssignableFrom(entity.ClrType))
                {
                    modelBuilder
                        .Entity(entity.ClrType)
                        .HasQueryFilter(GetIsDeletedRestriction(entity.ClrType));
                }
            }

            #endregion

            base.OnModelCreating(modelBuilder);
            var dbType = _configuration.GetStringValue("base.dbType").ToLower();
            #region 兼容Oracle 对象全大写
            if (dbType == "oracle")
            {
                foreach (var entity in modelBuilder.Model.GetEntityTypes())
                {
                    entity.Relational().TableName = entity.Relational().TableName.ToUpperInvariant();
                    foreach (var property in entity.GetProperties())
                    {
                        property.Relational().ColumnName = property.Relational().ColumnName.ToUpperInvariant();
                    }

                    foreach (var key in entity.GetKeys())
                    {
                        key.Relational().Name = key.Relational().Name.ToUpperInvariant();
                    }

                    foreach (var key in entity.GetForeignKeys())
                    {
                        key.Relational().Name = key.Relational().Name.ToUpperInvariant();
                    }

                    foreach (var index in entity.GetIndexes())
                    {
                        index.Relational().Name = index.Relational().Name.ToUpperInvariant();
                    }
                }
            }
            #endregion
        }

        /// <summary>
        /// 软删除保存实现
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            var entries = ChangeTracker.Entries().ToList();

            #region markedAsModified

            var markedAsAdded = entries.Where(x => x.State == EntityState.Added);

            foreach (var item in markedAsAdded)
            {
                if (item.Entity is IKeyEntity<long> entity)
                {
                    if (entity.Id == 0)
                    {
                        entity.Id = IdWorker.NewDefaultId;
                    }
                }
            }

            #endregion

            #region markedAsModified

            var markedAsModified = entries.Where(x => x.State == EntityState.Modified);

            foreach (var item in markedAsModified)
            {
                if (item.Entity is IModTimeEntity entity)
                {
                    entity.ModTime = DateTime.Now;
                }
            }

            #endregion

            return base.SaveChanges();
        }
    }
}
