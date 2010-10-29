using System;

namespace AutoConfig.Demo.Web.Simple
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            AutoConfigManager.Initialize();
        }
   }
}