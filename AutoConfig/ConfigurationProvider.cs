using System.Configuration;

namespace AutoConfig
{
	public class ConfigurationProvider : IConfigurationProvider
	{
		public object GetSection(string name)
		{
			return ConfigurationManager.GetSection(name);
		}

		public ConnectionStringSettings GetConnectionString(string name)
		{
			return ConfigurationManager.ConnectionStrings[name];
		}

		public string GetAppSetting(string key)
		{
			return ConfigurationManager.AppSettings[key];
		}
	}
}
