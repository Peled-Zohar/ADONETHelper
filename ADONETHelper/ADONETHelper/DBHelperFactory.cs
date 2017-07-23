using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONETHelper
{
    /// <summary>
    /// Used by the GetInstance factory method to return the concrete implementation of IDBHelper.
    /// </summary>
    public enum DataBaseType
    {
        /// <summary>
        /// SQL Server implementation.
        /// </summary>
        SQLServer,

        /// <summary>
        /// OleDb implementation.
        /// </summary>
        OleDb,

        /// <summary>
        /// Odbc implementation.
        /// </summary>
        Odbc
    }

    /// <summary>
    /// A factory class responsible of creating an instance of a class that implements IDBHelper.
    /// </summary>
    public static class DBHelperFactory
    {

        /// <summary>
        /// Creates and returns an instance of a class that implements IDBHelper.
        /// </summary>
        /// <param name="type">A member of the DataBaseType enum.</param>
        /// <param name="connectionString">The connection string used to connect to the database.</param>
        /// <returns>An instance of a class that implements IDBHelper.</returns>
        public static IDBHelper GetInstance(DataBaseType type, string connectionString)
        {
            switch(type)
            {
                case DataBaseType.Odbc:
                    return new OdbcDBHelper(connectionString);
                case DataBaseType.OleDb:
                    return new OleDbDBHelper(connectionString);
                case DataBaseType.SQLServer:
                    return new SQLDBHelper(connectionString);
            }
            return null;
        }
    }
}
