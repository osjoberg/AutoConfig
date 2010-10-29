using System.Configuration;

namespace AutoConfig.Test
{
    class TestConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("test")]
        public string Test
        {
            get { return (string)this["test"]; }
        }
    }
}
