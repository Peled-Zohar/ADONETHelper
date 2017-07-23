using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONETHelper
{
    /// <summary>
    /// An implementation of IDBHelper to be used with Odbc databases.
    /// </summary>
    internal class OdbcDBHelper : DBHelper<OdbcConnection, OdbcCommand, OdbcParameter, OdbcDataAdapter>
    {
        /// <summary>
        /// Initializes the connection string needed to connect to the database.
        /// Inheritors must call this constractor with the proper connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to the database.</param>
        internal OdbcDBHelper(string connectionString)
            : base(connectionString)
        {

        }

        /// <summary>
        /// Creates a new instance of the OdbcParameter class.
        /// </summary>
        /// <param name="name">The name of the paramenter.</param>
        /// <param name="type">The type of the parameter.</param>
        /// <returns>A new instance of OdbcParameter with the specified name and type.</returns>
        protected override System.Data.IDbDataParameter CreateParameter(string name, ADONETType type)
        {
            return new OdbcParameter(name, type.ToOdbcType());
        }
    }
}
