using System.Configuration;

namespace AutoConfig.Test
{
	class TestConfigurationProvider : IConfigurationProvider
	{
		private readonly object _value;

		public TestConfigurationProvider(object value)
		{
			_value = value;
		}

		public object GetSection(string name)
		{
			return _value;
		}

		public ConnectionStringSettings GetConnectionString(string name)
		{
			return (ConnectionStringSettings)_value;
		}

		public string GetAppSetting(string key)
		{
			return (string) _value;
		}
	}
}
