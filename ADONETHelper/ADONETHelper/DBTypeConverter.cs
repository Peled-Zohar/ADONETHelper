using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONETHelper
{
    #region enum

    /// <summary>
    /// Represents a common enum of parameters data type for all supported ADO.Net clients.
    /// </summary>
    public enum ADONETType
    {
        /// <summary>
        /// (DbType.Boolean, SqlDbType.Bit, OleDbType.Boolean, OdbcType.Bit)
        /// </summary>
        Boolean,

        /// <summary>
        /// (DbType.Byte, SqlDbType.TinyInt, OleDbType.UnsignedTinyInt , OdbcType.TinyInt)
        /// </summary>
        Byte,

        /// <summary>
        /// (DbType.Binary, SqlDbType.Binary, OleDbType.VarBinary, OdbcType.VarBinary)
        /// </summary>
        Binary,

        /// <summary>
        /// (DbType.AnsiStringFixedLength, SqlDbType.Char, OleDbType.Char, OdbcType.Char)
        /// </summary>
        Char,

        /// <summary>
        /// (DbType.Date, SqlDbType.Date, OleDbType.DBDate, OdbcType.Date)
        /// </summary>
        Date,

        /// <summary>
        /// (DbType.DateTime, SqlDbType.DateTime, OleDbType.DBTimeStamp, OdbcType.DateTime)
        /// </summary>
        DateTime,

        /// <summary>
        /// (DbType.DateTime2, SqlDbType.DateTime2, OleDbType - N/A, OdbcType - N/A),
        /// </summary>
        DateTime2,

        /// <summary>
        /// (DbType.DateTimeOffset, SqlDbType.DateTimeOffset, OleDbType - N/A, OdbcType - N/A)
        /// </summary>
        DateTimeOffset,

        /// <summary>
        /// (DbType.Decimal, SqlDbType.Decimal, OleDbType.Decimal, OdbcType.Decimal)
        /// </summary>
        Decimal,

        /// <summary>
        /// (DbType.Double, SqlDbType.Float , OleDbType.Double, OdbcType.Double)
        /// </summary>
        Double,

        /// <summary>
        /// (DbType.Single, SqlDbType.Real, OleDbType.Single, OdbcType.Real)
        /// </summary>
        Single,

        /// <summary>
        /// (DbType.Guid, SqlDbType.UniqueIdentifier, OleDbType.Guid, OdbcType.UniqueIdentifier)
        /// </summary>
        Guid,

        /// <summary>
        /// (DbType.Int16, SqlDbType.SmallInt, OleDbType.SmallInt, OdbcType.SmallInt)
        /// </summary>
        SmallInt,

        /// <summary>
        /// (DbType.Int32, SqlDbType.Int, OleDbType.Integer, OdbcType.Int)
        /// </summary>
        Int,

        /// <summary>
        /// (DbType.Int64, SqlDbType.BigInt, OleDbType.BigInt, OdbcType.BigInt)
        /// </summary>
        BigInt,

        /// <summary>
        /// (DbType.Object, SqlDbType.Variant, OleDbType.Variant, OdbcType - N/A)
        /// </summary>
        Object,

        /// <summary>
        /// (DbType.StringFixedLength, SqlDbType.NChar, OleDbType.WChar, OdbcType.NChar)
        /// </summary>
        NChar,

        /// <summary>
        /// (DbType.String, SqlDbType.NVarChar, OleDbType.VarWChar, OdbcType.NVarChar)
        /// </summary>
        NVarChar,

        /// <summary>
        /// (DbType.Binary, SqlDbType.VarBinary, OleDbType.VarBinary, OdbcType.VarBinary)
        /// </summary>
        VarBinary,

        /// <summary>
        /// (DbType.AnsiString, SqlDbType.VarChar, OleDbType.VarChar, OdbcType.VarChar)
        /// </summary>
        VarChar,

        /// <summary>
        /// (DbType - N/A, SqlDbType.Structured, OleDbType - N/A, OdbcType - N/A)
        /// </summary>
        Structured,

        /// <summary>
        /// (DbType.Time, SqlDbType.Time, OleDbType.DBTime, OdbcType.Time)
        /// </summary>
        Time,

        /// <summary>
        /// (DbType.UInt16, SqlDbType - N/A, OleDbType.UnsignedSmallInt, OdbcType - N/A)
        /// </summary>
        UInt16,

        /// <summary>
        /// (DbType.UInt32, SqlDbType - N/A, OleDbType.UnsignedInt, OdbcType - N/A)
        /// </summary>
        UInt32,

        /// <summary>
        /// (DbType.UInt64, SqlDbType - N/A, OleDbType.UnsignedBigInt, OdbcType - N/A)
        /// </summary>
        UInt64,

        /// <summary>
        /// (DbType.Xml, SqlDbType.Xml, OleDbType - N/A, OdbcType - N/A)
        /// </summary>
        Xml,

        /// <summary>
        /// (DbType.Currency, SqlDbType.Money, OleDbType.Currency, OdbcType - N/A)
        /// </summary>
        Currency,

        /// <summary>
        /// (DbType - N/A, SqlDbType.Image, OleDbType.LongVarBinary, OdbcType.Image)
        /// </summary>
        Image,

        /// <summary>
        /// (DbType - N/A, SqlDbType.NText, OleDbType.LongVarWChar, OdbcType.NText)
        /// </summary>
        NText,

        /// <summary>
        /// (DbType - N/A, SqlDbType.Text, OleDbType.LongVarChar, OdbcType.Text)
        /// </summary>
        Text,

        /// <summary>
        /// (DbType - N/A, SqlDbType.SmallDateTime, OleDbType.Date, OdbcType.SmallDateTime)
        /// </summary>
        SmallDateTime,

        /// <summary>
        /// (DbType - N/A, SqlDbType.Timestamp, OleDbType - N/A, OdbcType.Timestamp)
        /// </summary>
        Timestamp,

        /// <summary>
        /// (DbType.SByte, SqlDbType - N/A, OleDbType.TinyInt, OdbcType.TinyInt)
        /// </summary>
        SByte, 

        /// <summary>
        /// (DbType - N/A, SqlDbType.SmallMoney, OleDbType - N/A, OdbcType - N/A)
        /// </summary>
        SmallMoney,

        /// <summary>
        /// (DbType - N/A, SqlDbType.Udt, OleDbType - N/A, OdbcType - N/A)
        /// </summary>
        Udt,

        /// <summary>
        /// (DbType.VarNumeric, SqlDbType - N/A, OleDbType.VarNumeric, OdbcType - N/A)
        /// </summary>
        VarNumeric
    }

    #endregion enum

    internal static class DBTypeConverter
    {
        #region private members

        private static List<DbTypeMap> _Map;

        #endregion private members

        #region static constructor

        static DBTypeConverter()
        {
            _Map = new List<DbTypeMap>()
            {
                new DbTypeMap(ADONETType.Boolean, DbType.Boolean, SqlDbType.Bit, OleDbType.Boolean, OdbcType.Bit),
                new DbTypeMap(ADONETType.Byte, DbType.Byte, SqlDbType.TinyInt, OleDbType.UnsignedTinyInt , OdbcType.TinyInt),
                new DbTypeMap(ADONETType.Binary, DbType.Binary, SqlDbType.Binary, OleDbType.Binary, OdbcType.Binary),

                new DbTypeMap(ADONETType.Char, DbType.AnsiStringFixedLength, SqlDbType.Char, OleDbType.Char, OdbcType.Char),
                new DbTypeMap(ADONETType.Currency, DbType.Currency, SqlDbType.Money, OleDbType.Currency, null),

                new DbTypeMap(ADONETType.Date, DbType.Date, SqlDbType.Date, OleDbType.DBDate, OdbcType.Date),
                new DbTypeMap(ADONETType.DateTime, DbType.DateTime, SqlDbType.DateTime, OleDbType.DBTimeStamp, OdbcType.DateTime),
                new DbTypeMap(ADONETType.DateTime2, DbType.DateTime2, SqlDbType.DateTime2, null, null),
                new DbTypeMap(ADONETType.DateTimeOffset, DbType.DateTimeOffset, SqlDbType.DateTimeOffset, null, null),
                new DbTypeMap(ADONETType.Decimal, DbType.Decimal, SqlDbType.Decimal, OleDbType.Decimal, OdbcType.Decimal),
                new DbTypeMap(ADONETType.Double, DbType.Double, SqlDbType.Float , OleDbType.Double, OdbcType.Double),

                new DbTypeMap(ADONETType.Guid, DbType.Guid, SqlDbType.UniqueIdentifier, OleDbType.Guid, OdbcType.UniqueIdentifier),

                new DbTypeMap(ADONETType.Image, null, SqlDbType.Image, OleDbType.LongVarBinary, OdbcType.Image),
                new DbTypeMap(ADONETType.SmallInt, DbType.Int16, SqlDbType.SmallInt, OleDbType.SmallInt, OdbcType.SmallInt),
                new DbTypeMap(ADONETType.Int, DbType.Int32, SqlDbType.Int, OleDbType.Integer, OdbcType.Int),
                new DbTypeMap(ADONETType.BigInt, DbType.Int64, SqlDbType.BigInt, OleDbType.BigInt, OdbcType.BigInt),

                new DbTypeMap(ADONETType.NChar, DbType.StringFixedLength, SqlDbType.NChar, OleDbType.WChar, OdbcType.NChar),
                new DbTypeMap(ADONETType.NVarChar, DbType.String, SqlDbType.NVarChar, OleDbType.VarWChar, OdbcType.NVarChar),
                new DbTypeMap(ADONETType.NText, null, SqlDbType.NText, OleDbType.LongVarWChar, OdbcType.NText),
                
                new DbTypeMap(ADONETType.Object, DbType.Object, SqlDbType.Variant, OleDbType.Variant, null),
                
                new DbTypeMap(ADONETType.SByte, DbType.SByte, null, OleDbType.TinyInt, OdbcType.TinyInt),
                new DbTypeMap(ADONETType.Single, DbType.Single, SqlDbType.Real, OleDbType.Single, OdbcType.Real),
                new DbTypeMap(ADONETType.SmallDateTime, null, SqlDbType.SmallDateTime, OleDbType.Date, OdbcType.SmallDateTime),
                new DbTypeMap(ADONETType.SmallMoney, null, SqlDbType.SmallMoney, null, null),
                new DbTypeMap(ADONETType.Structured, null, SqlDbType.Structured, null, null ),

                new DbTypeMap(ADONETType.VarBinary, DbType.Binary, SqlDbType.VarBinary, OleDbType.VarBinary, OdbcType.VarBinary),
                new DbTypeMap(ADONETType.VarChar, DbType.AnsiString, SqlDbType.VarChar, OleDbType.VarChar, OdbcType.VarChar),
                new DbTypeMap(ADONETType.VarNumeric, DbType.VarNumeric, null, OleDbType.VarNumeric, null),
                
                new DbTypeMap(ADONETType.Text, null, SqlDbType.Text, OleDbType.LongVarChar, OdbcType.Text),
                new DbTypeMap(ADONETType.Time, DbType.Time, SqlDbType.Time, OleDbType.DBTime, OdbcType.Time),
                new DbTypeMap(ADONETType.Timestamp, null, SqlDbType.Timestamp, null, OdbcType.Timestamp),
                
                new DbTypeMap(ADONETType.Udt, null, SqlDbType.Udt, null, null),
                new DbTypeMap(ADONETType.UInt16, DbType.UInt16, null, OleDbType.UnsignedSmallInt, null),
                new DbTypeMap(ADONETType.UInt32, DbType.UInt32, null, OleDbType.UnsignedInt, null),
                new DbTypeMap(ADONETType.UInt64, DbType.UInt64, null, OleDbType.UnsignedBigInt, null),

                new DbTypeMap(ADONETType.Xml, DbType.Xml, SqlDbType.Xml, null, null)
            };
        }

        #endregion static constructor

        #region methods

        internal static DbType ToDbType(this ADONETType type)
        {
            return type.ConvertTo<DbType>();
        }

        internal static SqlDbType ToSqlType(this ADONETType type)
        {
            return type.ConvertTo<SqlDbType>();
        }

        internal static OleDbType ToOleDbType(this ADONETType type)
        {
            return type.ConvertTo<OleDbType>();
        }

        internal static OdbcType ToOdbcType(this ADONETType type)
        {
            return type.ConvertTo<OdbcType>();
        }

        private static dynamic ConvertTo<T>(this ADONETType type)
        {
            var returnValue = _Map.First(m => m.ADONETType == type).GetValueByType(typeof(T));
            if(returnValue != null)
            {
                return returnValue;
            }
            throw new NotSupportedException(string.Format("ADONETType {0} is not supported for {1}", type, typeof(T)));
        }

        #endregion methods

        #region private struct

        private struct DbTypeMap
        {
            #region ctor

            public DbTypeMap(ADONETType adonetType, DbType? dbType, SqlDbType? sqlDbType, OleDbType? oleDbType, OdbcType? odbcType)
                : this()
            {
                ADONETType = adonetType;
                DbType = dbType;
                SqlDbType = sqlDbType;
                OleDbType = oleDbType;
                OdbcType = odbcType;
            }

            #endregion ctor

            #region properties

            internal ADONETType ADONETType { get; private set; }
            internal DbType? DbType { get; private set; }
            internal SqlDbType? SqlDbType { get; private set; }
            internal OleDbType? OleDbType { get; private set; }
            internal OdbcType? OdbcType { get; private set; }

            #endregion properties

            #region methods

            internal dynamic GetValueByType(Type type)
            {
                if (type == typeof(ADONETType))
                {
                    return this.ADONETType;
                }
                if(type == typeof(DbType))
                {
                    return this.DbType;
                }
                if (type == typeof(SqlDbType))
                {
                    return this.SqlDbType;
                }
                if (type == typeof(OleDbType))
                {
                    return this.OleDbType;
                }
                if (type == typeof(OdbcType))
                {
                    return this.OdbcType;
                }
                return null;
            }

            #endregion methods
        }

        #endregion private struct
    }
}


