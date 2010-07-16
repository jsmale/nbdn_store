using System;
using System.Collections.Generic;
using nothinbutdotnetstore.infrastructure.containers.basic;
using nothinbutdotnetstore.tasks.stubs;

namespace nothinbutdotnetstore.tasks.startup
{
    public class ConfigureServiceLayer : StartupCommand
    {
        IDictionary<Type, DependencyFactory> factories;

        public ConfigureServiceLayer(IDictionary<Type, DependencyFactory> factories)
        {
            this.factories = factories;
        }

        void add_to_factories<TypeToRegister>(object creation)
        {
            factories.Add(typeof(TypeToRegister), create_factory(creation));
        }

        SingletonFactory create_factory(object dependency)
        {
            return new SingletonFactory(new BasicDependencyFactory(() => dependency));
        }

        public void run()
        {
            add_to_factories<CatalogTasks>(new StubCatalogTasks());
        }
    }
}