using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Web.Compilation;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.infrastructure.containers.basic;
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
            Container container = new BasicContainer(factories);
            IOC.factory_resolver = () => container;

            var frontControllerFactory =
                new SingletonFactory(new BasicDependencyFactory(() => new DefaultFrontController(
                    new DefaultCommandRegistry(new StubFakeCommandSet())
                    )));
            factories.Add(typeof(FrontController), frontControllerFactory);

            var requestFactory =
                new SingletonFactory(new BasicDependencyFactory(() => new StubRequestFactory()));
            factories.Add(typeof(RequestFactory), requestFactory);

//            var view_assembly_type =
//                Type.GetType("nothinbutdotnetstore.web.ui.views.DepartmentBrowser, nothinbutdotnetstore.web.ui");
            var views = Assembly.GetCallingAssembly().GetTypes().Where(x => x.GetInterface("ViewFor`1") != null);
            var responseEngineFactory =
                new SingletonFactory(new BasicDependencyFactory(() => new DefaultResponseEngine(
                    new DefaultViewFactory(new DefaultViewRegistry(views)))));
            factories.Add(typeof(ResponseEngine), responseEngineFactory);
            DefaultViewFactory.page_factory = BuildManager.CreateInstanceFromVirtualPath;

            var catalogTasksFactory =
                new SingletonFactory(new BasicDependencyFactory(() => new StubCatalogTasks()));
            factories.Add(typeof(CatalogTasks), catalogTasksFactory);

        }
    }
}