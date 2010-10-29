namespace AutoConfig.Demo.Console.ConfigurationBase
{
    class Configuration : AutoConfig.ConfigurationBase
    {
        public static decimal TestSetting { get { return GetAppSetting<decimal>("TestSetting"); } }
    }
}
