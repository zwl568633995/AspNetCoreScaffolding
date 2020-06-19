using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Data.Common;

namespace Kay.Framework.EntityFrameworkCore.SqlServer
{
    public static class DbContextConfigurationContextSqlServerExtensions
    {
        public static DbContextOptionsBuilder UseDefaultSqlServer(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] string connectionString,
            [CanBeNull] Action<SqlServerDbContextOptionsBuilder> optionsAction = null)
        {
            return optionsBuilder.UseSqlServer(connectionString, optionsAction);
        }

        public static DbContextOptionsBuilder UseDefaultSqlServer(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] DbConnection connection,
            [CanBeNull] Action<SqlServerDbContextOptionsBuilder> optionsAction = null)
        {
            return optionsBuilder.UseSqlServer(connection, optionsAction);
        }

        public static DbContextOptionsBuilder<TContext> UseDefaultSqlServer<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] string connectionString,
            [CanBeNull] Action<SqlServerDbContextOptionsBuilder> optionsAction = null)
            where TContext : DbContext
        {
            return optionsBuilder.UseSqlServer(connectionString, optionsAction);
        }

        public static DbContextOptionsBuilder<TContext> UseDefaultSqlServer<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] DbConnection connection,
            [CanBeNull] Action<SqlServerDbContextOptionsBuilder> optionsAction = null)
            where TContext : DbContext
        {
            return optionsBuilder.UseSqlServer(connection, optionsAction);
        }
    }
}
