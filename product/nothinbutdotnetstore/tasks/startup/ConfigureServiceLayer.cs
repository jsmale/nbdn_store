using System;
using System.Collections.Generic;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.infrastructure.containers.basic;
using nothinbutdotnetstore.infrastructure.logging;
using nothinbutdotnetstore.infrastructure.logging.simple;
using nothinbutdotnetstore.tasks.stubs;

namespace nothinbutdotnetstore.tasks.startup
{
    public class ConfigureServiceLayer
    {
        IDictionary<Type, DependencyFactory> factories;

        public ConfigureServiceLayer(IDictionary<Type, DependencyFactory> factories)
        {
            this.factories = factories;
        }

        private void add_to_factories<TypeToRegister>(object creation)
        {
            factories.Add(typeof(TypeToRegister), create_factory(creation));
        }
        
        private SingletonFactory create_factory(object dependency)
        {
            return new SingletonFactory(new BasicDependencyFactory(() => dependency));
        }

        public void run()
        {
            add_to_factories<CatalogTasks>(new StubCatalogTasks());
        }
    }
}