using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Data.Common;

namespace Kay.Framework.EntityFrameworkCore.MySQL
{
    public static class DbContextConfigurationContextMySqlExtensions
    {
        public static DbContextOptionsBuilder UseDefaultMySql(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] string connectionString,
            [CanBeNull] Action<MySqlDbContextOptionsBuilder> optionsAction = null)
        {
            return optionsBuilder.UseMySql(connectionString, optionsAction);
        }

        public static DbContextOptionsBuilder UseDefaultMySql(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] DbConnection connection,
            [CanBeNull] Action<MySqlDbContextOptionsBuilder> optionsAction = null)
        {
            return optionsBuilder.UseMySql(connection, optionsAction);
        }

        public static DbContextOptionsBuilder<TContext> UseDefaultMySql<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] string connectionString,
            [CanBeNull] Action<MySqlDbContextOptionsBuilder> optionsAction = null)
            where TContext : DbContext
        {
            return optionsBuilder.UseMySql(connectionString, optionsAction);
        }

        public static DbContextOptionsBuilder<TContext> UseDefaultMySql<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] DbConnection connection,
            [CanBeNull] Action<MySqlDbContextOptionsBuilder> optionsAction = null)
            where TContext : DbContext
        {
            return optionsBuilder.UseMySql(connection, optionsAction);
        }
    }
}
