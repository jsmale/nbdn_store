using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using nothinbutdotnetstore.infrastructure;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.infrastructure.containers.basic;
using nothinbutdotnetstore.model;
using nothinbutdotnetstore.web.core;
using nothinbutdotnetstore.infrastructure.extensions;

namespace nothinbutdotnetstore.tasks.startup
{
    public class ConfigureFrontController : StartupCommand
    {
        IDictionary<Type, DependencyFactory> factories;

        public ConfigureFrontController(IDictionary<Type, DependencyFactory> factories)
        {
            this.factories = factories;
        }

        void add_to_factories<TypeToRegister>(object creation)
        {
            factories.Add(typeof(TypeToRegister), create_factory(creation));
        }

        void add_to_factories(Type type)
        {
            factories.Add(type, auto_factory(type));
        }

        SingletonFactory create_factory(object dependency)
        {
            return new SingletonFactory(new BasicDependencyFactory(() => dependency));
        }

        SingletonFactory auto_factory(Type type)
        {
            return new SingletonFactory(new AutoWiringFactory(
                new GreedyConstructorSelectionStrategy(), IOC.get, type));
        }

        public void run()
        {
            var views = Assembly.Load("nothinbutdotnetstore.web.ui").GetTypes()
                .Where(x => x.GetInterface("ViewFor`1") != null);
			   add_to_factories<IEnumerable<Type>>(views);
            var applicationCommandType = typeof (ApplicationCommand);
            var webCommandType = typeof (WebCommand);
            Assembly.GetAssembly(this.GetType()).GetTypes()
                .Where(x => x.IsClass && applicationCommandType.IsAssignableFrom(x)
                    && !webCommandType.IsAssignableFrom(x))
                .each(x => add_to_factories(x));

			  add_to_factories<Mapper<NameValueCollection, Department>>(
				new NameValueMapper<Department>());
			  add_to_factories<Mapper<NameValueCollection, Product>>(
				new NameValueMapper<Product>());

            DefaultResponseEngine.context = () => HttpContext.Current;
            DefaultViewFactory.page_factory = BuildManager.CreateInstanceFromVirtualPath;
        }
    }
}