using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONETHelper
{
    /// <summary>
    /// An implementation of IDBHelper to be used with OleDb databases.
    /// </summary>
    internal class OleDbDBHelper : DBHelper<OleDbConnection, OleDbCommand, OleDbParameter, OleDbDataAdapter>
    {
        /// <summary>
        /// Initializes the connection string needed to connect to the database.
        /// Inheritors must call this constractor with the proper connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        internal OleDbDBHelper(string connectionString)
            : base(connectionString)
        {

        }

        /// <summary>
        /// Creates a new instance of the OleDbParameter class.
        /// </summary>
        /// <param name="name">The name of the paramenter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <returns>A new instance of OleDbParameter with the specified name and type.</returns>
        protected override IDbDataParameter CreateParameter(string name, ADONETType type)
        {
            return new OleDbParameter(name, type.ToOleDbType());
        }
    }
}