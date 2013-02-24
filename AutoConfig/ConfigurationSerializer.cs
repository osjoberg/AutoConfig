using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Globalization;
using System.Reflection;
using AutoConfig.ConfigSection;
using AutoConfig.Converters;

namespace AutoConfig
{
	public static class ConfigurationSerializer
	{
		public static void Deserialize(Type type, CultureInfo configurationCulture = null)
		{
			Deserialize(type, configurationCulture, null, null);
		}

		public static TConfiguration Deserialize<TConfiguration>(CultureInfo configurationCulture = null) where TConfiguration : new()
		{
			return Deserialize<TConfiguration>(configurationCulture, null, null);
		}

		internal static void Deserialize(Type type, CultureInfo configurationCulture, IEnumerable<string> includeKeys, IConfigurationProvider provider)
		{
			ArgumentValidator.IsNotNull("type", type);

			var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Static);
			Deserialize(null, properties, configurationCulture, includeKeys, provider);			
		}

		internal static TConfiguration Deserialize<TConfiguration>(CultureInfo configurationCulture, IEnumerable<string> includeKeys, IConfigurationProvider provider) where TConfiguration : new()
		{
			var configuration = new TConfiguration();
			var properties = typeof(TConfiguration).GetProperties(BindingFlags.Public | BindingFlags.Instance);

			return (TConfiguration)Deserialize(configuration, properties, configurationCulture, includeKeys, provider);
		}

		private static object Deserialize(object configuration, IEnumerable<PropertyInfo> properties, CultureInfo configurationCulture, IEnumerable<string> includeKeys = null, IConfigurationProvider provider = null)
		{
			provider = provider ?? new ConfigurationProvider();

			// If configuration culture is not specified try to get it from the configuration file.
			if (configurationCulture == null)
				configurationCulture = GetConfigurationCulture();

			foreach (var property in properties)
			{
				if (includeKeys != null && !new List<string>(includeKeys).Contains(property.Name))
					continue;

				if (property.GetSetMethod(true) == null)
					continue;
				
				property.SetValue(configuration, DeserializeProperty(property.PropertyType, property.Name, configurationCulture, provider), null);
			}

			return configuration;
		}

		private static CultureInfo GetConfigurationCulture()
		{
			var autoConfigSection = AutoConfigConfigurationSection.GetSection();
			return autoConfigSection == null ? null : autoConfigSection.Culture;
		}

		/// <summary>
		/// Retrieve an property from configuration by it's key/name. If the type is nullable and the setting is set to an empty string, null will be returned.
		/// </summary>
		/// <param name="type">Type of the AppSetting.</param>
		/// <param name="key">AppSetting key.</param>
		/// <param name="configurationCulture">Culture.</param>
		/// <returns>Corresponding value.</returns>
		private static object DeserializeProperty(Type type, string key, CultureInfo configurationCulture, IConfigurationProvider provider)
		{
			if (type == typeof(Factory<DbConnection>))
			{
				var settings = provider.GetConnectionString(key);
				if (settings == null)
					throw new ConfigurationSerializerException(ExceptionMessage.ConnectionStringNameNotFound, key);

				return ConnectionStringConverter.ToDbConnection(settings);
			}

			if (type.IsSubclassOf(typeof(ConfigurationSection)))
			{
				var alternativeName = char.ToLowerInvariant(key[0]) + key.Substring(1);
				var section = provider.GetSection(key) ?? provider.GetSection(alternativeName);
				if (section == null)
					throw new ConfigurationSerializerException(ExceptionMessage.ConfigSectionNotFound, key);

				return section;
			}

			var value = provider.GetAppSetting(key);
			if (value == null)
				throw new ConfigurationSerializerException(ExceptionMessage.AppSettingsKeyNotFound, key);

			return AppSettingConverter.ToType(type, value, configurationCulture);
		}
	}
}
