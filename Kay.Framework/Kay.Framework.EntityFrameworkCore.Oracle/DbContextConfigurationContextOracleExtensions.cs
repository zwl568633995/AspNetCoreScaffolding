using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Oracle.EntityFrameworkCore.Infrastructure;
using System;
using System.Data.Common;

namespace Kay.Framework.EntityFrameworkCore.Oracle
{
    public static class DbContextConfigurationContextOracleExtensions
    {
        public static DbContextOptionsBuilder UseDefaultOracle(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] string connectionString,
            [CanBeNull] Action<OracleDbContextOptionsBuilder> optionsAction = null)
        {
            return optionsBuilder.UseOracle(
                connectionString,
                b => b.UseOracleSQLCompatibility("11"));
        }

        public static DbContextOptionsBuilder UseDefaultOracle(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] DbConnection connection,
            [CanBeNull] Action<OracleDbContextOptionsBuilder> optionsAction = null)
        {
            return optionsBuilder.UseOracle(connection,
                b => b.UseOracleSQLCompatibility("11"));
        }

        public static DbContextOptionsBuilder<TContext> UseDefaultOracle<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] string connectionString,
            [CanBeNull] Action<OracleDbContextOptionsBuilder> optionsAction = null)
            where TContext : DbContext
        {
            return optionsBuilder.UseOracle(connectionString,
                b => b.UseOracleSQLCompatibility("11"));
        }

        public static DbContextOptionsBuilder<TContext> UseDefaultOracle<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] DbConnection connection,
            [CanBeNull] Action<OracleDbContextOptionsBuilder> optionsAction = null)
            where TContext : DbContext
        {
            return optionsBuilder.UseOracle(connection,
                b => b.UseOracleSQLCompatibility("11"));
        }
    }
}
