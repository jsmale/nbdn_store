using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.infrastructure.containers.basic;
using nothinbutdotnetstore.infrastructure.logging;
using nothinbutdotnetstore.infrastructure.logging.simple;
using nothinbutdotnetstore.tasks.stubs;
using nothinbutdotnetstore.web.core;
using nothinbutdotnetstore.web.core.stubs;
using System.Linq;

namespace nothinbutdotnetstore.tasks.startup
{
    public class Startup
    {
        public static void run()
        {
            var factories = new Dictionary<Type, DependencyFactory>();
            configure_core_services(factories);
            configure_front_controller(factories);
            configure_service_layer(factories);
        }

        static DependencyFactory singleton(Func<object> factory)
        {
            return new SingletonFactory(new BasicDependencyFactory(factory));
        }

        static void configure_service_layer(IDictionary<Type, DependencyFactory> factories)
        {
            factories.Add(typeof(CatalogTasks), singleton(() => new StubCatalogTasks()));
        }

        static void configure_front_controller(IDictionary<Type, DependencyFactory> factories)
        {
            var front_controller_factory =
                singleton(() => new DefaultFrontController(new DefaultCommandRegistry(new StubFakeCommandSet())));
            factories.Add(typeof(FrontController), front_controller_factory);

            factories.Add(typeof(RequestFactory), singleton(() => new StubRequestFactory()));

            var views = Assembly.Load("nothinbutdotnetstore.web.ui").GetTypes().Where(x => x.GetInterface("ViewFor`1") != null);
            var response_engine_factory =
                singleton(() => new DefaultResponseEngine(new DefaultViewFactory(new DefaultViewRegistry(views))));
            factories.Add(typeof(ResponseEngine), response_engine_factory);
            DefaultViewFactory.page_factory = BuildManager.CreateInstanceFromVirtualPath;
            DefaultResponseEngine.context = () => HttpContext.Current;
        }

        static void configure_core_services(IDictionary<Type, DependencyFactory> factories)
        {
            Container container = new BasicContainer(factories);
            factories.Add(typeof(LoggerFactory), singleton(() => new TextWriterLoggerFactory()));
            IOC.factory_resolver = () => container;
        }
    }
}