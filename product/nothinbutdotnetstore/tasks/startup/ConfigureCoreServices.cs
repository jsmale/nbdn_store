using System;
using System.Collections.Generic;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.infrastructure.containers.basic;
using nothinbutdotnetstore.infrastructure.logging;
using nothinbutdotnetstore.infrastructure.logging.simple;

namespace nothinbutdotnetstore.tasks.startup
{
    public class ConfigureCoreServices
    {
        IDictionary<Type, DependencyFactory> factories;

        public ConfigureCoreServices(IDictionary<Type, DependencyFactory> factories)
        {
            this.factories = factories;
        }

        public void run()
        {
            Container container = new BasicContainer(factories);
            factories.Add(typeof(LoggerFactory), new BasicDependencyFactory(() => new TextWriterLoggerFactory()));
            IOC.factory_resolver = () => container;
        }
    }
}