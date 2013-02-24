using System;

namespace AutoConfig
{
	class ConfigurationSerializerException : Exception
	{
		public ConfigurationSerializerException(string message) : base (message)
		{			
		}

		public ConfigurationSerializerException(string message, params object[] @params) : base(string.Format(message, @params))
		{
		}

		public ConfigurationSerializerException(string message, Exception innerException, params object[] @params) : base(string.Format(message, @params), innerException)
		{			
		}
	}
}
