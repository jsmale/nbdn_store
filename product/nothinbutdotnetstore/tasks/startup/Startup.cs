using System;
using System.Collections.Generic;
using nothinbutdotnetstore.infrastructure.containers.basic;

namespace nothinbutdotnetstore.tasks.startup
{
    public class Startup
    {
        static Dictionary<Type, DependencyFactory> factories = new Dictionary<Type, DependencyFactory>();

        public static void run()
        {
//            Start.by<ConfiguringCoreServices>()
//                .followed_by<ConfigureFronController>()
//                .finish_by<ConfigureServiceLayer>();
        }
    }
}