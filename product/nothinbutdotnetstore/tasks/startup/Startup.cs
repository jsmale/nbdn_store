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
		static Dictionary<Type, DependencyFactory> factories  = new Dictionary<Type, DependencyFactory>();
		public static void run()
		{
			configure_core_services(factories);
			configure_front_controller(factories);
			configure_service_layer(factories);
		}
		
		private static void addToDict<TypeToRegister>(object creation)
		{
			factories.Add(typeof(TypeToRegister), create_factory(creation));
		}

		private static void configure_service_layer(Dictionary<Type, DependencyFactory> factories)
		{
			addToDict<CatalogTasks>(new StubCatalogTasks());
//			factories.Add(typeof (CatalogTasks), create_factory());
		}

		private static void configure_front_controller(Dictionary<Type, DependencyFactory> factories)
		{
			addToDict<FrontController>(new DefaultFrontController(new DefaultCommandRegistry(new StubFakeCommandSet())));
			addToDict<RequestFactory>(new StubRequestFactory());
			addToDict<ResponseEngine>(new DefaultResponseEngine(new DefaultViewFactory(new DefaultViewRegistry(null))));

			DefaultViewFactory.page_factory = BuildManager.CreateInstanceFromVirtualPath;
		}

		private static void configure_core_services(Dictionary<Type, DependencyFactory> factories)
		{
			Container container = new BasicContainer(factories);
			factories.Add(typeof(LoggerFactory), new BasicDependencyFactory(() => new TextWriterLoggerFactory()));
			IOC.factory_resolver = () => container;
		}

		private static SingletonFactory create_factory(object dependency)
		{
			return new SingletonFactory(new BasicDependencyFactory(() => dependency));
		}
	}
}