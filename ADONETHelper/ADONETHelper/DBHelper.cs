using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONETHelper
{
    /// <summary>
    /// A base class for ADO.Net based data access layer, 
    /// that encapsulates connection, command, and data adapters.
    /// Enables quick and easy execution of major database operation:
    /// ExecuteNonQuery, ExecuteScalar, 
    /// Load data using a DataReader, DataSet or DataTable.
    /// Also provides a shorten way to create and initialize parameters.
    /// </summary>
    internal abstract class DBHelper<TConnection, TCommand, TParameter, TAdapter> : IDBHelper
        where TConnection : IDbConnection, new()
        where TCommand : IDbCommand, new()
        where TParameter : IDbDataParameter, new()
        where TAdapter : IDbDataAdapter, IDisposable, new()
    {
        #region private memberes

        private string _ConnectionString;

        #endregion  private memberes

        #region ctor

        /// <summary>
        /// Initializes the connection string needed to connect to the database.
        /// Inheritors must call this constractor with the proper connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        public DBHelper(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        #endregion ctor

        #region public methods

        /// <summary>
        /// Executes an SQL non-query statement (INSERT/UPDATE/DELETE).
        /// </summary>
        /// <param name="sql">SQL statement to execute.</param>
        /// <param name="commandType">One of the Sql.Data.CommandType values. The default is Text.</param>
        /// <param name="parameters">Parameters of the SQL statement.</param>
        /// <returns>An integer value indication the number of rows effected by the SQL statement.</returns>
        public int ExecuteNonQuery(string sql, CommandType commandType, params IDbDataParameter[] parameters)
        {
            return Execute<int>(sql, commandType, c => c.ExecuteNonQuery(), parameters);
        }

        /// <summary>
        /// Execute an SQL select statement that returns a single scalar value.
        /// </summary>
        /// <typeparam name="T">Data type of the value to return.</typeparam>
        /// <param name="sql">SQL statement to execute.</param>
        /// <param name="commandType">One of the Sql.Data.CommandType values. The default is Text.</param>
        /// <param name="parameters">Parameters of the SQL statement.</param>
        /// <returns>An instance of T, or it's default.</returns>
        public T ExecuteScalar<T>(string sql, CommandType commandType, params IDbDataParameter[] parameters)
        {
            return Execute<T>(sql, commandType, c =>
            {
                var returnValue = c.ExecuteScalar();
                return (returnValue != null && returnValue != DBNull.Value && returnValue is T)
                 ? (T)returnValue
                 : default(T);
            }, parameters);
        }

        /// <summary>
        /// Executes an SQL Select statement using an instance of a class that's implementing IDataReader.
        /// Recommended use: Populating data objects.
        /// </summary>
        /// <param name="sql">SQL statement to execute.</param>
        /// <param name="commandType">One of the Sql.Data.CommandType values. The default is Text.</param>
        /// <param name="populate">A function to run that accepts an IDataReader and returns a boolean, to do the actuall population of the data object.</param>
        /// <param name="parameters">Parameters of the SQL statement.</param>
        /// <returns>The boolean value returned from the populate argument.</returns>
        public bool ExecuteReader(string sql, CommandType commandType, Func<IDataReader, bool> populate, params IDbDataParameter[] parameters)
        {
            return Execute<bool>(sql, commandType, c => populate(c.ExecuteReader()), parameters);
        }

        /// <summary>
        /// Executes an SQL Select statement and returns it's results using a DataSet.
        /// </summary>
        /// <param name="sql">SQL statement to execute.</param>
        /// <param name="commandType">One of the Sql.Data.CommandType values. The default is Text.</param>
        /// <param name="parameters">Parameters of the SQL statement.</param>
        /// <returns>An instance of the DataSet class with the results of the SQL query.</returns>
        public DataSet FillDataSet(string sql, CommandType commandType, params IDbDataParameter[] parameters)
        {
            return Execute<DataSet>(sql, commandType, c => FillDataSet(c), parameters);
        }

        /// <summary>
        /// Executes an SQL Select statement and returns it's results using a DataTable.
        /// </summary>
        /// <param name="sql">SQL statement to execute.</param>
        /// <param name="commandType">One of the Sql.Data.CommandType values. The default is Text.</param>
        /// <param name="parameters">Parameters of the SQL statement.</param>
        /// <returns>An instance of the DataTable class with the results of the SQL query.</returns>
        /// <remarks>
        /// Note to inhreritors: Concrete DataAdapter might have an overload of the Fill method 
        /// that works directly with a data table. You might want to use it instead of this method.
        /// </remarks>
        public virtual DataTable FillDataTable(string sql, CommandType commandType, params IDbDataParameter[] parameters)
        {
            var dataset = FillDataSet(sql, commandType, parameters);
            return (dataset.Tables.Count > 0) ? dataset.Tables[0] : null;
        }

        /// <summary>
        /// Executes an SQL statement.
        /// </summary>
        /// <typeparam name="T">Data type of the value to return.</typeparam>
        /// <param name="sql">SQL statement to execute.</param>
        /// <param name="commandType">One of the Sql.Data.CommandType values. The default is Text.</param>
        /// <param name="function">A function to execute with the IDbCommand (i.e. Filling a DataTable).</param>
        /// <param name="parameters">Parameters of the SQL statement.</param>
        /// <returns>The value returned from the function argument.</returns>
        public T Execute<T>(string sql, CommandType commandType, Func<IDbCommand, T> function, params IDbDataParameter[] parameters)
        {
            using (var con = new TConnection())
            {
                con.ConnectionString = _ConnectionString;
                using (var cmd = new TCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Connection = con;
                    cmd.CommandType = commandType;
                    if (parameters.Length > 0)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    con.Open();
                    return function(cmd);
                }
            }
        }
               
        /// <summary>
        /// Creates an output parameter with the specified name, type and size.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="size">The size of the parameter.</param>
        /// <returns>An output parameter with the specified name, type and size.</returns>
        public IDbDataParameter CreateOutputParameter(string name, ADONETType type, int size)
        {
            var param = CreateOutputParameter(name, type);
            param.Size = size;
            return param;
        }

        /// <summary>
        /// Creates an output parameter with the specified name and type.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <returns>An output parameter with the specified name and type.</returns>
        public IDbDataParameter CreateOutputParameter(string name, ADONETType type)
        {
            var param = CreateParameter(name, type);
            param.Direction = ParameterDirection.Output;
            return param;
        }

        /// <summary>
        /// Creates an input parameter with the specified name, type and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An input parameter with the specified name, type and value</returns>
        public IDbDataParameter CreateParameter(string name, ADONETType type, object value)
        {
            var param = CreateParameter(name, type);
            param.Value = value ?? DBNull.Value;
            return param;
        }

        /// <summary>
        /// Creates an input parameter with the specified name, type, size and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="size">The size of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns></returns>
        public IDbDataParameter CreateParameter(string name, ADONETType type, int size, object value)
        {
            var param = CreateParameter(name, type, value);
            param.Size = size;
            return param;
        }

        #endregion public methods

        #region private methods
        
        private DataSet FillDataSet(IDbCommand command)
        {
            var dataSet = new DataSet();
            using (var adapter = new TAdapter())
            {
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
            }
            return dataSet;
        }

        #endregion private methods

        #region protected methods

        /// <summary>
        /// Creates an instance of TParameter with the specified name and type.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <returns>An instance of TParameter with the specified name and type.</returns>
        protected virtual IDbDataParameter CreateParameter(string name, ADONETType type)
        {
            return new TParameter()
            {
                ParameterName = name,
                DbType = type.ToDbType()
            };
        }

        #endregion protected methods

    }
}
