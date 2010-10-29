using System.Configuration;

namespace AutoConfig.Demo.Web.Simple
{
    public partial class _Default : System.Web.UI.Page
    {
        public string EnvironmentName { get { return AutoConfigManager.Environment.Name; } }

        public string TestSettingValue { get { return ConfigurationManager.AppSettings["TestSetting"]; } }
    }
}
