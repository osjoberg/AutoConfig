using System.Configuration;

namespace AutoConfig.Demo.Console.Simple
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialize the AutoConfig system.
            AutoConfigManager.Initialize();

            // Write environment name.
            System.Console.WriteLine("This demo is running in the {0} environment.", AutoConfigManager.Environment.Name);

            // Write system setting.
            System.Console.WriteLine(@"The application setting TestSetting is set to ""{0}"".", ConfigurationManager.AppSettings["TestSetting"]);

            System.Console.WriteLine("Press any key to quit.");
            System.Console.ReadKey();
        }
    }
}
