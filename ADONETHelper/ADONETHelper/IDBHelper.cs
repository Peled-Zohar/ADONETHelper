using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONETHelper
{
    /// <summary>
    /// Represents the interface of a helper class for working with ADO.Net.
    /// Supported clients: Odbc, OleDb and Sql.
    /// Encapsulates connection, command, parameters and data adapters.
    /// Enables quick and easy execution of major database operation:
    /// ExecuteNonQuery, ExecuteScalar, 
    /// Load data using a DataReader, DataSet or DataTable.
    /// </summary>
    public interface IDBHelper
    {
        /// <summary>
        /// Executes an SQL non-query statement (INSERT/UPDATE/DELETE).
        /// </summary>
        /// <param name="sql">SQL statement to execute.</param>
        /// <param name="commandType">One of the Sql.Data.CommandType values. The default is Text.</param>
        /// <param name="parameters">Parameters of the SQL statement.</param>
        /// <returns>An integer value indication the number of rows effected by the SQL statement.</returns>
        int ExecuteNonQuery(string sql, CommandType commandType, params IDbDataParameter[] parameters);

        /// <summary>
        /// Execute an SQL select statement that returns a single scalar value.
        /// </summary>
        /// <typeparam name="T">Data type of the value to return.</typeparam>
        /// <param name="sql">SQL statement to execute.</param>
        /// <param name="commandType">One of the Sql.Data.CommandType values. The default is Text.</param>
        /// <param name="parameters">Parameters of the SQL statement.</param>
        /// <returns>An instance of T, or it's default.</returns>
        T ExecuteScalar<T>(string sql, CommandType commandType, params IDbDataParameter[] parameters);

        /// <summary>
        /// Executes an SQL Select statement using an instance of a class that's implementing IDataReader.
        /// Recommended use: Populating data objects.
        /// </summary>
        /// <param name="sql">SQL statement to execute.</param>
        /// <param name="commandType">One of the Sql.Data.CommandType values. The default is Text.</param>
        /// <param name="populate">A function to run that accepts an IDataReader and returns a boolean, to do the actuall population of the data object.</param>
        /// <param name="parameters">Parameters of the SQL statement.</param>
        /// <returns>The boolean value returned from the populate argument.</returns>
        bool ExecuteReader(string sql, CommandType commandType, Func<IDataReader, bool> populate, params IDbDataParameter[] parameters);

        /// <summary>
        /// Executes an SQL Select statement and returns it's results using a DataSet.
        /// </summary>
        /// <param name="sql">SQL statement to execute.</param>
        /// <param name="commandType">One of the Sql.Data.CommandType values. The default is Text.</param>
        /// <param name="parameters">Parameters of the SQL statement.</param>
        /// <returns>An instance of the DataSet class with the results of the SQL query.</returns>
        DataSet FillDataSet(string sql, CommandType commandType, params IDbDataParameter[] parameters);

        /// <summary>
        /// Executes an SQL Select statement and returns it's results using a DataTable.
        /// </summary>
        /// <param name="sql">SQL statement to execute.</param>
        /// <param name="commandType">One of the Sql.Data.CommandType values. The default is Text.</param>
        /// <param name="parameters">Parameters of the SQL statement.</param>
        /// <returns>An instance of the DataTable class with the results of the SQL query.</returns>
        DataTable FillDataTable(string sql, CommandType commandType, params IDbDataParameter[] parameters);

        /// <summary>
        /// Executes an SQL statement.
        /// </summary>
        /// <typeparam name="T">Data type of the value to return.</typeparam>
        /// <param name="sql">SQL statement to execute.</param>
        /// <param name="commandType">One of the Sql.Data.CommandType values. The default is Text.</param>
        /// <param name="function">A function to execute with the IDbCommand (i.e. Filling a DataTable).</param>
        /// <param name="parameters">Parameters of the SQL statement.</param>
        /// <returns>The value returned from the function argument.</returns>
        T Execute<T>(string sql, CommandType commandType, Func<IDbCommand, T> function, params IDbDataParameter[] parameters);

        /// <summary>
        /// Creates an output parameter with the specified name, type and size.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="size">The size of the parameter.</param>
        /// <returns>An output parameter with the specified name, type and size.</returns>
        IDbDataParameter CreateOutputParameter(string name, ADONETType type, int size);
        
        /// <summary>
        /// Creates an output parameter with the specified name and type.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <returns>An output parameter with the specified name and type.</returns>
        IDbDataParameter CreateOutputParameter(string name, ADONETType type);

        /// <summary>
        /// Creates an input parameter with the specified name, type and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An input parameter with the specified name, type and value.</returns>
        IDbDataParameter CreateParameter(string name, ADONETType type, object value);

        /// <summary>
        /// Creates an input parameter with the specified name, type, size and value.
        /// </summary>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <param name="size">The size of the parameter.</param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>An input parameter with the specified name, type, size and value.</returns>
        IDbDataParameter CreateParameter(string name, ADONETType type, int size, object value);
        
    }
}
