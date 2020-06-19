using System;
using System.Data.Common;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Kay.Framework.Domain;
using Kay.Framework.EntityFrameworkCore.MySQL;
using Kay.Framework.EntityFrameworkCore.Oracle;
using Kay.Framework.EntityFrameworkCore.SqlServer;


namespace Kay.Framework.AspNetCore.Mvc.EntityFrameworkCore
{
    public static class CommonDbContextConfigurationContextExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <param name="dbType">SqlServer、Oracle、MySQL</param>
        /// <param name="connectionString"></param>
        /// <param name="dbVersion">数据库版本</param>
        /// <returns></returns>
        public static DbContextOptionsBuilder UseNalongBuilder(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] string dbType,
            [NotNull] string connectionString,
            [CanBeNull] string dbVersion = "")
        {
            return InitBuilder(optionsBuilder, dbType, connectionString, dbVersion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <param name="dbType">SqlServer、Oracle、MySQL</param>
        /// <param name="connection"></param>
        /// <param name="dbVersion">数据库版本</param>
        /// <returns></returns>
        public static DbContextOptionsBuilder UseNalongBuilder(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] string dbType,
            [NotNull] DbConnection connection,
            [CanBeNull]string dbVersion = "")
        {
            return InitBuilder(optionsBuilder, dbType, connection, dbVersion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="optionsBuilder"></param>
        /// <param name="dbType"></param>
        /// <param name="connectionString"></param>
        /// <param name="dbVersion">数据库版本</param>
        /// <returns></returns>
        public static DbContextOptionsBuilder<TContext> UseNalongBuilder<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] string dbType,
            [NotNull] string connectionString,
            [CanBeNull]string dbVersion = "")
            where TContext : DbContext
        {
            return InitBuilder(optionsBuilder, dbType, connectionString, dbVersion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="optionsBuilder"></param>
        /// <param name="dbType"></param>
        /// <param name="connection"></param>
        /// <param name="dbVersion">数据库版本</param>
        /// <returns></returns>
        public static DbContextOptionsBuilder<TContext> UseNalongBuilder<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] string dbType,
            [NotNull] DbConnection connection,
            [CanBeNull]string dbVersion = "")
            where TContext : DbContext
        {
            return InitBuilder(optionsBuilder, dbType, connection, dbVersion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <param name="dbType">SqlServer、Oracle、MySQL</param>
        /// <param name="connection"></param>
        /// <param name="dbVersion">数据库版本</param>
        /// <returns></returns>
        private static DbContextOptionsBuilder InitBuilder(
            [NotNull] DbContextOptionsBuilder optionsBuilder,
            [NotNull] string dbType,
            [NotNull] DbConnection connection,
            [CanBeNull]string dbVersion)
        {
            switch (dbType.ToLower())
            {
                case DbTypeConsts.DbTypeSqlServer:
                    {
                        return optionsBuilder.UseDefaultSqlServer(connection);
                    }
                case DbTypeConsts.DbTypeOracle:
                    {
                        return optionsBuilder.UseDefaultOracle(connection,
                            op =>
                            {
                                if (string.IsNullOrEmpty(dbVersion))
                                {
                                    dbVersion = DbTypeConsts.OracleDefaultVersion;
                                }
                                op.UseOracleSQLCompatibility(dbVersion);
                            });
                    }
                case DbTypeConsts.DbTypeMySql:
                    {
                        return optionsBuilder.UseDefaultMySql(connection);
                    }
                default:
                    {
                        return optionsBuilder.UseDefaultSqlServer(connection);
                    }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <param name="dbType"></param>
        /// <param name="connectionString"></param>
        /// <param name="dbVersion">数据库版本</param>
        /// <returns></returns>
        private static DbContextOptionsBuilder InitBuilder(
            [NotNull] DbContextOptionsBuilder optionsBuilder,
            [NotNull] string dbType,
            [NotNull] string connectionString,
            [CanBeNull]string dbVersion
            )
        {
            switch (dbType.ToLower())
            {
                case DbTypeConsts.DbTypeSqlServer:
                    {
                        return optionsBuilder.UseDefaultSqlServer(connectionString);
                    }

                case DbTypeConsts.DbTypeOracle:
                    {
                        return optionsBuilder.UseDefaultOracle(connectionString,
                            op =>
                            {
                                if (string.IsNullOrEmpty(dbVersion))
                                {
                                    dbVersion = DbTypeConsts.OracleDefaultVersion;
                                }
                                op.UseOracleSQLCompatibility(dbVersion);
                            });
                    }

                case DbTypeConsts.DbTypeMySql:
                    {
                        return optionsBuilder.UseDefaultMySql(connectionString);
                    }
                default:
                    {
                        return optionsBuilder.UseDefaultSqlServer(connectionString);
                    }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <param name="dbType">SqlServer、Oracle、MySQL</param>
        /// <param name="connection"></param>
        /// <param name="dbVersion">数据库版本</param>
        /// <returns></returns>
        private static DbContextOptionsBuilder<TContext> InitBuilder<TContext>(
            [NotNull] DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] string dbType,
            [NotNull] DbConnection connection,
            [CanBeNull]string dbVersion)
            where TContext : DbContext
        {
            switch (dbType.ToLower())
            {
                case DbTypeConsts.DbTypeSqlServer:
                    {
                        return optionsBuilder.UseDefaultSqlServer(connection);
                    }
                case DbTypeConsts.DbTypeOracle:
                    {
                        return optionsBuilder.UseDefaultOracle(connection,
                            op =>
                            {
                                if (string.IsNullOrEmpty(dbVersion))
                                {
                                    dbVersion = DbTypeConsts.OracleDefaultVersion;
                                }
                                op.UseOracleSQLCompatibility(dbVersion);
                            });
                    }
                case DbTypeConsts.DbTypeMySql:
                    {
                        return optionsBuilder.UseDefaultMySql(connection);
                    }
                default:
                    {
                        return optionsBuilder.UseDefaultSqlServer(connection);
                    }
            }
        }

        private static DbContextOptionsBuilder<TContext> InitBuilder<TContext>(
            [NotNull] DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] string dbType,
            [NotNull] string connectionString,
            [CanBeNull]string dbVersion)
            where TContext : DbContext
        {
            switch (dbType.ToLower())
            {
                case DbTypeConsts.DbTypeSqlServer:
                    {
                        return optionsBuilder.UseDefaultSqlServer(connectionString);
                    }
                case DbTypeConsts.DbTypeOracle:
                    {
                        return optionsBuilder.UseDefaultOracle(connectionString,
                            op =>
                            {
                                if (string.IsNullOrEmpty(dbVersion))
                                {
                                    dbVersion = DbTypeConsts.OracleDefaultVersion;
                                }
                                op.UseOracleSQLCompatibility(dbVersion);
                            });
                    }
                case DbTypeConsts.DbTypeMySql:
                    {
                        return optionsBuilder.UseDefaultMySql(connectionString);
                    }
                default:
                    {
                        return optionsBuilder.UseDefaultSqlServer(connectionString);
                    }
            }
        }
    }
}
