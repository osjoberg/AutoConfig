using System;
using System.Configuration;
using System.Globalization;
using System.Reflection;

namespace AutoConfig.Converters
{
	static class AppSettingConverter
	{
		/// <summary>
		/// Convert a an appSettings string to a specified type.
		/// </summary>
		/// <param name="type">Type to convert to.</param>
		/// <param name="value">Value to convert.</param>
		/// <param name="configurationCulture">Culture.</param>
		/// <returns>Instance of the specified type.</returns>
		internal static object ToType(Type type, string value, CultureInfo configurationCulture)
		{
			if (value.Length == 0 && (type.IsClass || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))) && type != typeof(string))
				return null;

			type = Nullable.GetUnderlyingType(type) ?? type;

			if (type.IsEnum)
				return ToEnum(type, value);
			if (typeof(IConvertible).IsAssignableFrom(type))
				return ToConvertible(type, value, configurationCulture);
			if (type == typeof(Type))
				return ToType(value);
			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Factory<>))
				return ToFactoryInstance(type, value);

			return ToInstance(type, value);
		}

		private static object ToEnum(Type type, string value)
		{
			int intValue;

			foreach (var name in Enum.GetNames(type))
				if (name.ToLowerInvariant() == value.ToLowerInvariant())
					return Enum.Parse(type, value, true);

			if (!int.TryParse(value, out intValue) || !Enum.IsDefined(type, intValue))
				throw new ConfigurationSerializerException(ExceptionMessage.ValueIsNotDefinedInEnumOfType, value, type);

			return Enum.ToObject(type, intValue);
		}

		private static object ToConvertible(Type type, string value, CultureInfo configurationCulture)
		{
			try
			{
				return value == null ? null : Convert.ChangeType(value, type, configurationCulture);
			}
			catch (FormatException exception)
			{
				throw new ConfigurationSerializerException(ExceptionMessage.ValueCouldNotBeConvertedToType, exception, value, type);
			}
		}

		private static Type ToType(string typeName)
		{
			foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				var type = assembly.GetType(typeName);
				if (type != null)
					return type;
			}

			throw new ConfigurationSerializerException(ExceptionMessage.CouldNotFindTypeInAnyLoadedAssembly, typeName);
		}

		private static object ToInstance(Type destinationType, string typeName)
		{
			var type = ToType(typeName);

			if (!destinationType.IsAssignableFrom(type))
				throw new ConfigurationSerializerException(ExceptionMessage.CouldNotCastObjectOfTypeToType, destinationType, type);

			return Activator.CreateInstance(type);
		}

		private static object ToFactoryInstance(Type destinationType, string typeName)
		{
			var type = ToType(typeName);

			if (!destinationType.GetGenericArguments()[0].IsAssignableFrom(type))
				throw new ConfigurationSerializerException(ExceptionMessage.CouldNotCastObjectOfTypeToType, destinationType, type);

			return Activator.CreateInstance(destinationType, BindingFlags.NonPublic | BindingFlags.Instance, null, new object[] { type }, null);
		}
	}
}
