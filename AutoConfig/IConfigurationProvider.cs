using System.Configuration;

namespace AutoConfig
{
	interface IConfigurationProvider
	{
		object GetSection(string name);
		ConnectionStringSettings GetConnectionString(string name);
		string GetAppSetting(string key);
	}
}
