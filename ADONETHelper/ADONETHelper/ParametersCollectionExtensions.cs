using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADONETHelper
{
    /// <summary>
    /// Provides extensions for IEnumerable<IDbDataParameter> to help get the parameters and values in client code.
    /// </summary>
    public static class ParametersCollectionExtensions
    {
        /// <summary>
        /// Returns the instance of IDbDataParameter with the specified name.
        /// </summary>
        /// <param name="parameters">The parameters collection to search.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The instance of IDbDataParameter with the specified name.</returns>
        public static IDbDataParameter GetByName(this IEnumerable<IDbDataParameter> parameters, string name)
        {
            return parameters.First(p => p.ParameterName == name);
        }

        /// <summary>
        /// Gets the value of the parameter with the specified name.
        /// </summary>
        /// <param name="parameters">The parameters collection to search.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>An instance of the Object class that is the value of the parameter.</returns>
        public static object GetValue(this IEnumerable<IDbDataParameter> parameters, string name)
        {
            return parameters.GetByName(name).Value;
        }

        /// <summary>
        /// Gets the value of the parameter with the specified name, or default(T) if the parameter is not found or it's value is null or DBNull.
        /// </summary>
        /// <typeparam name="T">The type of the value of the parameter.</typeparam>
        /// <param name="parameters">The parameters collection to search.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <returns>The value of the parameter as T, or default(T) if not found or it's value is null or DBNull.</returns>
        public static T GetValueOrDefault<T>(this IEnumerable<IDbDataParameter> parameters, string name)
        {
            return parameters.GetValueOrDefault<T>(name, default(T)); 
        }

        /// <summary>
        /// Gets the value of the parameter with the specified name, or defaultValue if the parameter is not found or it's value is null or DBNull.
        /// </summary>
        /// <typeparam name="T">The type of the value of the parameter.</typeparam>
        /// <param name="parameters">The parameters collection to search.</param>
        /// <param name="name">The name of the parameter.</param>
        /// <param name="defaultValue">A value of type T that will be returned if the parameter is not found or it's value is null or DBNull.</param>
        /// <returns>The value of the parameter as T, or defaultValue if not found or it's value is null or DBNull.</returns>
        public static T GetValueOrDefault<T>(this IEnumerable<IDbDataParameter> parameters, string name, T defaultValue)
        {
            var parameter = parameters.GetByName(name);
            return (parameter == null) ? defaultValue : (T)parameter.GetValueOrDefault<T>(defaultValue);
        }
    }
}
