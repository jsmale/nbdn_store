using System;
using nothinbutdotnetstore.tasks.startup;

namespace nothinbutdotnetstore.web.ui
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            Startup.run();
        }
    }
}