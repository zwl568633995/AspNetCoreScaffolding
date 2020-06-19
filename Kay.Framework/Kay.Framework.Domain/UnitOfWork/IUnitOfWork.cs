using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Kay.Framework.Domain.UnitOfWork
{
    /// <summary>
    /// 抽象工作单元
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// 持久化
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql, params object[] parameters);

        /// <summary>
        /// 执行非查询sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlNonQuery(string sql, params object[] parameters);

        /// <summary>
        /// 执行sql查询返回特定的实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IList<TEntity> FromSql<TEntity>(string sql, params object[] parameters) where TEntity : new();

        #region 支持自定义sqlCommandType

        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlCommandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlCommand(string sql, CommandType sqlCommandType, params object[] parameters);

        /// <summary>
        /// 执行非查询sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="sqlCommandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteSqlNonQuery(string sql, CommandType sqlCommandType, params object[] parameters);

        /// <summary>
        /// 执行sql查询返回特定的实体
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql"></param>
        /// <param name="sqlCommandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IList<TEntity> FromSql<TEntity>(string sql, CommandType sqlCommandType, params object[] parameters) where TEntity : new();

        #endregion 支持自定义sqlCommandType
    }
}
