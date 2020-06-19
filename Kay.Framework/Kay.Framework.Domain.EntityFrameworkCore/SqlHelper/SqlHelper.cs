using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Kay.Framework.Utility.Extensions;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;


namespace Kay.Framework.Domain.EntityFrameworkCore
{
    public static class SqlHelper
    {
        public static int ExecuteSqlNonQuery(
            string dbType,
            DbConnection conn,
            string sql,
            CommandType sqlCommandType,
            params object[] parameters)
        {
            switch (dbType)
            {
                case DbTypeConsts.DbTypeSqlServer:
                    {
                        return SqlServerExecuteSqlNonQuery(conn, sql, sqlCommandType, parameters);
                    }
                case DbTypeConsts.DbTypeOracle:
                    {
                        return OracleExecuteSqlNonQuery(conn, sql, sqlCommandType, parameters);
                    }
                case DbTypeConsts.DbTypeMySql:
                    {
                        return MySqlExecuteSqlNonQuery(conn, sql, sqlCommandType, parameters);
                    }
                default:
                    {
                        return SqlServerExecuteSqlNonQuery(conn, sql, sqlCommandType, parameters);
                    }
            }
        }

        public static IList<TEntity> FromSql<TEntity>(
            string dbType,
            DbConnection conn,
            string sql,
            CommandType sqlCommandType,
            params object[] parameters)
            where TEntity : new()

        {
            switch (dbType)
            {
                case DbTypeConsts.DbTypeSqlServer:
                    {
                        return SqlServerFromSql<TEntity>(conn, sql, sqlCommandType, parameters);
                    }
                case DbTypeConsts.DbTypeOracle:
                    {
                        return OracleFromSql<TEntity>(conn, sql, sqlCommandType, parameters);
                    }
                case DbTypeConsts.DbTypeMySql:
                    {
                        return MySqlFromSql<TEntity>(conn, sql, sqlCommandType, parameters);
                    }
                default:
                    {
                        return SqlServerFromSql<TEntity>(conn, sql, sqlCommandType, parameters);
                    }
            }
        }

        public static int ExecuteSqlCommand(
            string dbType,
            DbConnection conn,
            string sql,
            CommandType sqlCommandType,
            params object[] parameters)
        {
            switch (dbType)
            {
                case DbTypeConsts.DbTypeSqlServer:
                    {
                        return SqlServerExecuteSqlCommand(conn, sql, sqlCommandType, parameters);
                    }
                case DbTypeConsts.DbTypeOracle:
                    {
                        return OracleExecuteSqlCommand(conn, sql, sqlCommandType, parameters);
                    }
                case DbTypeConsts.DbTypeMySql:
                    {
                        return MySqlExecuteSqlCommand(conn, sql, sqlCommandType, parameters);
                    }
                default:
                    {
                        return SqlServerExecuteSqlCommand(conn, sql, sqlCommandType, parameters);
                    }
            }
        }

        #region Oracle

        public static int OracleExecuteSqlNonQuery(
            DbConnection conn,
            string sql,
            CommandType sqlCommandType,
            params object[] parameters)
        {
            OracleCommand command = null;
            try
            {
                conn.Open();
                command = ((OracleConnection)conn).CreateCommand();
                command.CommandText = sql;
                command.CommandType = sqlCommandType;
                if (parameters != null && parameters.Any())
                {
                    foreach (var parameter in parameters)
                    {
                        var p = parameter as SqlParameter;
                        var name = p.ParameterName.TrimStart('@');
                        command.Parameters.Add(new OracleParameter(name, ChangeOracleDbType(p.SqlDbType)));
                        command.Parameters[name].Value = p.Value;
                    }
                }

                command.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                var count = command.ExecuteNonQuery();
                return count;
            }
            finally
            {
                if (command != null)
                {
                    if (command.Parameters.IsNotNull())
                    {
                        command.Parameters.Clear();
                    }
                }

                conn?.Close();
            }
        }

        public static IList<TEntity> OracleFromSql<TEntity>(
            DbConnection conn,
            string sql,
            CommandType sqlCommandType,
            params object[] parameters)
            where TEntity : new()

        {
            OracleCommand command = null;
            try
            {
                conn.Open();
                command = ((OracleConnection)conn).CreateCommand();
                command.CommandText = sql;
                command.CommandType = sqlCommandType;
                if (parameters != null && parameters.Any())
                {
                    foreach (var parameter in parameters)
                    {
                        var p = parameter as SqlParameter;
                        var name = p.ParameterName.TrimStart('@');
                        command.Parameters.Add(new OracleParameter(name, ChangeOracleDbType(p.SqlDbType)));
                        command.Parameters[name].Value = p.Value;
                    }
                }

                command.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                var props = typeof(TEntity).GetProperties();
                var rtnList = new List<TEntity>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var model = new TEntity();
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            var name = reader.GetName(i);
                            var prop = props.FirstOrDefault(a =>
                                string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase));
                            if (prop != null)
                            {
                                var readerVal = reader[name];
                                if (readerVal != DBNull.Value)
                                {
                                    SetValue(model, prop.Name, readerVal);
                                }
                            }
                        }

                        rtnList.Add(model);
                    }
                }

                return rtnList;
            }
            finally
            {
                if (command != null)
                {
                    if (command.Parameters.IsNotNull())
                    {
                        command.Parameters.Clear();
                    }
                }

                conn?.Close();
            }
        }

        public static int OracleExecuteSqlCommand(
            DbConnection conn,
            string sql,
            CommandType sqlCommandType,
            params object[] parameters)
        {
            OracleCommand command = null;
            try
            {
                conn.Open();
                command = ((OracleConnection)conn).CreateCommand();
                command.CommandText = sql;
                command.CommandType = sqlCommandType;
                if (parameters != null && parameters.Any())
                {
                    foreach (var parameter in parameters)
                    {
                        var p = parameter as SqlParameter;
                        var name = p.ParameterName.TrimStart('@');
                        command.Parameters.Add(new OracleParameter(name, ChangeOracleDbType(p.SqlDbType),
                            ParameterDirection.Input));
                        command.Parameters[name].Value = p.Value;
                    }
                }

                command.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                var val = command.ExecuteScalar();
                int.TryParse(val.ToString(), out var count);
                return count;
            }
            finally
            {
                if (command != null)
                {
                    if (command.Parameters.IsNotNull())
                    {
                        command.Parameters.Clear();
                    }
                }

                conn?.Close();
            }
        }

        #endregion

        #region SqlServer

        public static int SqlServerExecuteSqlNonQuery(
            DbConnection conn,
            string sql,
            CommandType sqlCommandType,
            params object[] parameters)
        {
            DbCommand command = null;
            try
            {
                conn.Open();
                command = conn.CreateCommand();
                command.CommandText = sql;
                command.CommandType = sqlCommandType;
                if (parameters != null && parameters.Any())
                {
                    command.Parameters.AddRange(parameters);
                }

                var count = command.ExecuteNonQuery();
                return count;
            }
            finally
            {
                if (command != null)
                {
                    if (command.Parameters.IsNotNull())
                    {
                        command.Parameters.Clear();
                    }
                }

                conn?.Close();
            }
        }

        public static IList<TEntity> SqlServerFromSql<TEntity>(
            DbConnection conn,
            string sql,
            CommandType sqlCommandType,
            params object[] parameters)
            where TEntity : new()

        {
            DbCommand command = null;
            try
            {
                conn.Open();
                command = conn.CreateCommand();
                command.CommandText = sql;
                command.CommandType = sqlCommandType;
                if (parameters != null && parameters.Any())
                {
                    command.Parameters.AddRange(parameters);
                }

                var props = typeof(TEntity).GetProperties();
                var rtnList = new List<TEntity>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var model = new TEntity();
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            var name = reader.GetName(i);
                            var prop = props.FirstOrDefault(a =>
                                string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase));
                            if (prop != null)
                            {
                                var readerVal = reader[name];
                                prop.SetValue(model,
                                    readerVal == DBNull.Value ? GetDefault(prop.PropertyType) : readerVal);
                            }
                        }

                        rtnList.Add(model);
                    }
                }

                return rtnList;
            }
            finally
            {
                if (command != null)
                {
                    if (command.Parameters.IsNotNull())
                    {
                        command.Parameters.Clear();
                    }
                }

                conn?.Close();
            }
        }

        public static int SqlServerExecuteSqlCommand(
            DbConnection conn,
            string sql,
            CommandType sqlCommandType,
            params object[] parameters)
        {
            DbCommand command = null;
            try
            {
                conn.Open();
                command = conn.CreateCommand();
                command.CommandText = sql;
                command.CommandType = sqlCommandType;
                if (parameters != null && parameters.Any())
                {
                    command.Parameters.AddRange(parameters);
                }

                var count = (int)command.ExecuteScalar();
                return count;
            }
            finally
            {
                if (command != null)
                {
                    if (command.Parameters.IsNotNull())
                    {
                        command.Parameters.Clear();
                    }
                }

                conn?.Close();
            }
        }

        #endregion

        #region MySQL

        public static int MySqlExecuteSqlNonQuery(
            DbConnection conn,
            string sql,
            CommandType sqlCommandType,
            params object[] parameters)
        {
            MySqlCommand command = null;
            try
            {
                conn.Open();
                command = ((MySqlConnection)conn).CreateCommand();
                command.CommandText = sql;
                command.CommandType = sqlCommandType;
                if (parameters != null && parameters.Any())
                {
                    command.Parameters.AddRange(parameters);
                }
                var count = command.ExecuteNonQuery();
                return count;
            }
            finally
            {
                if (command != null)
                {
                    if (command.Parameters.IsNotNull())
                    {
                        command.Parameters.Clear();
                    }
                }

                conn?.Close();
            }
        }

        public static IList<TEntity> MySqlFromSql<TEntity>(
            DbConnection conn,
            string sql,
            CommandType sqlCommandType,
            params object[] parameters)
            where TEntity : new()
        {
            MySqlCommand command = null;
            try
            {
                conn.Open();
                command = ((MySqlConnection)conn).CreateCommand();
                command.CommandText = sql;
                command.CommandType = sqlCommandType;
                if (parameters != null && parameters.Any())
                {
                    foreach (var parameter in parameters)
                    {
                        var p = parameter as SqlParameter;
                        var mysqlPar = new MySqlParameter(p.ParameterName, ChangeMySqlDbType(p.SqlDbType))
                        {
                            Value = p.Value,
                            Direction = ParameterDirection.Input
                        };

                        command.Parameters.Add(mysqlPar);
                    }
                }

                //command.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                var props = typeof(TEntity).GetProperties();
                var rtnList = new List<TEntity>();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var model = new TEntity();
                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            var name = reader.GetName(i);
                            var prop = props.FirstOrDefault(a =>
                                string.Equals(a.Name, name, StringComparison.OrdinalIgnoreCase));
                            if (prop != null)
                            {
                                var readerVal = reader[name];
                                if (readerVal != DBNull.Value)
                                {
                                    SetValue(model, prop.Name, readerVal);
                                }
                            }
                        }

                        rtnList.Add(model);
                    }
                }

                return rtnList;
            }
            finally
            {
                if (command != null)
                {
                    if (command.Parameters.IsNotNull())
                    {
                        command.Parameters.Clear();
                    }
                }

                conn?.Close();
            }
        }

        public static int MySqlExecuteSqlCommand(
            DbConnection conn,
            string sql,
            CommandType sqlCommandType,
            params object[] parameters)
        {
            MySqlCommand command = null;
            try
            {
                conn.Open();
                command = ((MySqlConnection)conn).CreateCommand();
                command.CommandText = sql;
                command.CommandType = sqlCommandType;
                if (parameters != null && parameters.Any())
                {
                    foreach (var parameter in parameters)
                    {
                        var p = parameter as SqlParameter;
                        var mysqlPar = new MySqlParameter(p.ParameterName, ChangeMySqlDbType(p.SqlDbType))
                        {
                            Value = p.Value,
                            Direction = ParameterDirection.Input
                        };

                        command.Parameters.Add(mysqlPar);
                    }
                }

                //command.Parameters.Add("p_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                var val = command.ExecuteScalar();
                int.TryParse(val.ToString(), out var count);
                return count;
            }
            finally
            {
                if (command != null)
                {
                    if (command.Parameters.IsNotNull())
                    {
                        command.Parameters.Clear();
                    }
                }

                conn?.Close();
            }
        }

        #endregion

        #region 获取Type默认值

        public static void SetValue(object inputObject, string propertyName, object propertyVal)
        {
            var type = inputObject.GetType();
            var propertyInfo = type.GetProperty(propertyName);
            var propertyType = propertyInfo.PropertyType;
            var targetType = IsNullableType(propertyType) ? Nullable.GetUnderlyingType(propertyType) : propertyType;
            propertyVal = Convert.ChangeType(propertyVal, targetType);
            propertyInfo.SetValue(inputObject, propertyVal, null);
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        //https://stackoverflow.com/questions/325426/programmatic-equivalent-of-defaulttype/4027869#4027869
        private static object GetDefault(Type type)
        {
            object output = null;
            if (type.IsValueType)
            {
                output = Activator.CreateInstance(type);
            }

            return output;
        }

        #endregion 获取Type默认值

        #region ChangDbType

        /// <summary>
        /// SqlDbType to OracleDbType
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        private static OracleDbType ChangeOracleDbType(SqlDbType dbType)
        {
            switch (dbType)
            {
                case SqlDbType.VarChar:
                case SqlDbType.NVarChar:
                    {
                        return OracleDbType.Varchar2;
                    }
                case SqlDbType.Int:
                    {
                        return OracleDbType.Int32;
                    }
                case SqlDbType.BigInt:
                    {
                        return OracleDbType.Int64;
                    }
                case SqlDbType.DateTime:
                    {
                        return OracleDbType.Date;
                    }
                default:
                    {
                        return OracleDbType.Varchar2;
                    }
            }
        }

        /// <summary>
        /// SqlDbType to MySqlDbType
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        private static MySqlDbType ChangeMySqlDbType(SqlDbType dbType)
        {
            switch (dbType)
            {
                case SqlDbType.VarChar:
                case SqlDbType.NVarChar:
                    {
                        return MySqlDbType.VarString;
                    }
                case SqlDbType.Int:
                    {
                        return MySqlDbType.Int32;
                    }
                case SqlDbType.BigInt:
                    {
                        return MySqlDbType.Int64;
                    }
                case SqlDbType.DateTime:
                    {
                        return MySqlDbType.DateTime;
                    }
                default:
                    {
                        return MySqlDbType.VarString;
                    }
            }
        }

        #endregion
    }
}