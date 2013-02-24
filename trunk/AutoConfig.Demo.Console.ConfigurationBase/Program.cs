using System.Globalization;

namespace AutoConfig.Demo.Console.ConfigurationBase
{
    class Program
    {
        static void Main(string[] args)
        {
			ConfigurationSerializer.Deserialize<Configuration>();

            System.Console.WriteLine(@"The application setting TestSetting is set to ""{0}"".", Configuration.TestSetting);

            System.Console.WriteLine("Press any key to quit.");
            System.Console.ReadKey();
        }
    }
}
