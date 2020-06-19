using Kay.Framework.Domain.UnitOfWork;
using Kay.Framework.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Kay.Framework.Domain.EntityFrameworkCore.UnitOfWork
{
    public class EfUnitOfWork : IUnitOfWork
    {
        public DbContext DbContext { get; }

        private readonly bool _miniProfiler = false;
        private readonly string _dbType;

        public EfUnitOfWork(
            DbContext context,
            IConfiguration configuration)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
            _miniProfiler = !configuration.GetBoolValue("base.disable_auditing_MiniProfiler");
            _dbType = configuration.GetStringValue("base.dbType", "SqlServer").ToLower();
        }

        public int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            var result = ExecuteSqlCommand(sql, CommandType.Text, parameters);
            return result;
        }

        public int ExecuteSqlNonQuery(string sql, params object[] parameters)
        {
            var result = ExecuteSqlNonQuery(sql, CommandType.Text, parameters);
            return result;
        }

        public IList<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : new()
        {
            var result = FromSql<TEntity>(sql, CommandType.Text, parameters);
            return result;
        }

        public int ExecuteSqlCommand(string sql, CommandType sqlCommandType, params object[] parameters)
        {
            var conn = DbContext.Database.GetDbConnection();
            return SqlHelper.ExecuteSqlCommand(_dbType, conn, sql, sqlCommandType, parameters);
        }

        public int ExecuteSqlNonQuery(string sql, CommandType sqlCommandType, params object[] parameters)
        {
            var conn = DbContext.Database.GetDbConnection();
            return SqlHelper.ExecuteSqlNonQuery(_dbType, conn, sql, sqlCommandType, parameters);
        }

        public IList<TEntity> FromSql<TEntity>(string sql, CommandType sqlCommandType, params object[] parameters) where TEntity : new()
        {
            var conn = DbContext.Database.GetDbConnection();
            return SqlHelper.FromSql<TEntity>(_dbType, conn, sql, sqlCommandType, parameters);
        }
    }
}
