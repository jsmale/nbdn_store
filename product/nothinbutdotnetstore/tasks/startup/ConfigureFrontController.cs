using System;
using System.Collections.Generic;
using System.Web.Compilation;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.infrastructure.containers.basic;
using nothinbutdotnetstore.infrastructure.logging;
using nothinbutdotnetstore.infrastructure.logging.simple;
using nothinbutdotnetstore.web.core;
using nothinbutdotnetstore.web.core.stubs;

namespace nothinbutdotnetstore.tasks.startup
{
    public class ConfigureFrontController
    {
        IDictionary<Type, DependencyFactory> factories;

        public ConfigureFrontController(IDictionary<Type, DependencyFactory> factories)
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
            add_to_factories<FrontController>(
                new DefaultFrontController(new DefaultCommandRegistry(new StubFakeCommandSet())));
            add_to_factories<RequestFactory>(new StubRequestFactory());
            add_to_factories<ResponseEngine>(
                new DefaultResponseEngine(new DefaultViewFactory(new DefaultViewRegistry(null))));

            DefaultViewFactory.page_factory = BuildManager.CreateInstanceFromVirtualPath;
        }
    }
}