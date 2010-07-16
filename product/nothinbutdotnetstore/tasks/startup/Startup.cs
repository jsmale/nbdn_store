using System;
using System.Collections.Generic;
using System.Web.Compilation;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.infrastructure.containers.basic;
using nothinbutdotnetstore.infrastructure.logging;
using nothinbutdotnetstore.infrastructure.logging.simple;
using nothinbutdotnetstore.tasks.stubs;
using nothinbutdotnetstore.web.core;
using nothinbutdotnetstore.web.core.stubs;

namespace nothinbutdotnetstore.tasks.startup
{
    public class Startup
    {
        static Dictionary<Type, DependencyFactory> factories = new Dictionary<Type, DependencyFactory>();

        public static void run()
        {
            new ConfigureCoreServices(factories).run();
            new ConfigureFrontController(factories).run();
            new ConfigureServiceLayer(factories).run();
        }
    }
}