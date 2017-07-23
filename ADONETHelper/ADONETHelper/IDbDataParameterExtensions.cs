using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONETHelper
{
    /// <summary>
    /// Provides extensions for IDbDataParameter to help get the parameters and values in client code.
    /// </summary>
    public static class IDbDataParameterExtensions
    {
        /// <summary>
        /// Gets the value of the parameter, or default(T) if the parameter's value is null or DBNull.
        /// </summary>
        /// <typeparam name="T">The type of the value of the parameter.</typeparam>
        /// <param name="parameter">The parameter.</param>
        /// <returns>The value of the parameter as T, or default(T) if it's value is null or DBNull.</returns>
        public static T GetValueOrDefault<T>(this IDbDataParameter parameter)
        {
            return parameter.GetValueOrDefault<T>(default(T));
        }

        /// <summary>
        /// Gets the value of the parameter, or defaultValue if the parameters value is null or DBNull.
        /// </summary>
        /// <typeparam name="T">The type of the value of the parameter.</typeparam>
        /// <param name="parameter">The parameter.</param>
        /// <param name="defaultValue">A value of type T that will be returned if the parameter's value is null or DBNull.</param>
        /// <returns>The value of the parameter as T, or defaultValue if it's value is null or DBNull.</returns>
        public static T GetValueOrDefault<T>(this IDbDataParameter parameter, T defaultValue)
        {
            return (parameter.Value == null || Convert.IsDBNull(parameter.Value)) ? defaultValue : (T)parameter.Value;
        }
    }
}
