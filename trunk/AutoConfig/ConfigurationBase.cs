using System;
using System.Data.Common;
using System.Configuration;
using System.Globalization;
using System.Collections.Generic;

namespace AutoConfig
{
    /// <summary>
    /// Abstract base class used to provide a type-safe access to configuration settings.
    /// </summary>
    public abstract class ConfigurationBase
    {
        /// <summary>
        /// Culture used when parsing data in configuration files.
        /// </summary>
        private static CultureInfo configurationCulture;

        /// <summary>
        /// Initialize system with a custom configuration.
        /// </summary>
        /// <param name="configurationCulture"></param>
        public static void Initialize(CultureInfo configurationCulture)
        {
            ConfigurationBase.configurationCulture = configurationCulture;
        }

        /// <summary>
        /// Get an AppSetting by it's key, throws an exception if the key could not be found.
        /// </summary>
        /// <param name="key">AppSetting key.</param>
        /// <returns>Corresponding value.</returns>
        private static string GetAppSettingString(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (value == null)
                throw new KeyNotFoundException();

            return value;
        }

        /// <summary>
        /// Convert a string to a specified type.
        /// </summary>
        /// <typeparam name="TType">Type to convert to.</typeparam>
        /// <param name="value">Value to convert.</param>
        /// <returns>Instance of the specified type.</returns>
        private static TType ConvertFromString<TType>(string value)
        {
            return (TType)Convert.ChangeType(value, typeof(TType), configurationCulture);
        }

        /// <summary>
        /// Retrieve an AppSetting by it's key.
        /// </summary>
        /// <typeparam name="TType">Type of the AppSetting.</typeparam>
        /// <param name="key">AppSetting key.</param>
        /// <returns>Corresponding value.</returns>
        protected static TType GetAppSetting<TType>(string key)
        {
            string value = GetAppSettingString(key);
            return ConvertFromString<TType>(value);
        }

        /// <summary>
        /// Retrieve an AppSetting by it's key. If the setting is set to an empty string, null will be returned.
        /// </summary>
        /// <typeparam name="TType">Type of the AppSetting.</typeparam>
        /// <param name="key">AppSetting key.</param>
        /// <returns>Corresponding value.</returns>
        protected static TType? GetAppSettingOrNull<TType>(string key) where TType: struct
        {
            string value = GetAppSettingString(key);

            if (value.Length ==  0)
                return null;

            return ConvertFromString<TType>(value);
        }

        /// <summary>
        /// Retrieve connection string settings by it's key.
        /// </summary>
        /// <param name="key">ConnectionStrings key.</param>
        /// <returns>Connection string information.</returns>
        protected static ConnectionStringSettings GetConnectionStringSetting(string key)
        {
            return ConfigurationManager.ConnectionStrings[key];
        }

        /// <summary>
        /// Retrieve a connection instance by it's key.
        /// </summary>
        /// <param name="key">ConnectionStrings key.</param>
        /// <returns>Connection instance.</returns>
        protected static DbConnection CreateConnection(string key)
        {
            ConnectionStringSettings settings = GetConnectionStringSetting(key);

            DbProviderFactory factory = DbProviderFactories.GetFactory(settings.ProviderName);
            DbConnection connection = factory.CreateConnection();
            connection.ConnectionString = settings.ConnectionString;

            return connection;
        }
    }
}
