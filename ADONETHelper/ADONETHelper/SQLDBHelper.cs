using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ADONETHelper
{
    /// <summary>
    /// An implementation of IDBHelper to be used with SQL Server database.
    /// </summary>
    internal class SQLDBHelper : DBHelper<SqlConnection, SqlCommand, SqlParameter, SqlDataAdapter>
    {

        #region ctor

        /// <summary>
        /// Initializes the connection string needed to connect to the database.
        /// Inheritors must call this constractor with the proper connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        internal SQLDBHelper(string connectionString)
            : base(connectionString)
        {
        }

        #endregion ctor

        #region protected methods

        /// <summary>
        /// Fills a DataTable with the results of an SQL query.
        /// </summary>
        /// <param name="sql">SQL query to execute.</param>
        /// <param name="commandType">One of the Sql.Data.CommandType values. The default is Text.</param>
        /// <param name="parameters">Parameters of the SQL statement.</param>
        /// <returns></returns>
        protected DataTable FillDataTable(string sql, CommandType commandType, params SqlParameter[] parameters)
        {
            return Execute<DataTable>(sql, commandType, command => 
                {
                    var dataTable = new DataTable();
                    using (var adapter = new SqlDataAdapter((SqlCommand)command))
                    {
                        adapter.Fill(dataTable);
                    }
                    return dataTable;
                }, parameters);
        }

        /// <summary>
        /// Creates a new instance of the SqlParameter class.
        /// </summary>
        /// <param name="name">The name of the paramenter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <returns>A new instance of SqlParameter with the specified name and type.</returns>
        protected override IDbDataParameter CreateParameter(string name, ADONETType type)
        {
            return new SqlParameter(name, type.ToSqlType());
        }
        
        #endregion protected methods
        
    }
}
