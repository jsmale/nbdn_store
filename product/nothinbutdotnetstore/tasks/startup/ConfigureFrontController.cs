using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using nothinbutdotnetstore.infrastructure;
using nothinbutdotnetstore.infrastructure.containers.basic;
using nothinbutdotnetstore.model;
using nothinbutdotnetstore.web.core;
using nothinbutdotnetstore.web.core.stubs;

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

        SingletonFactory create_factory(object dependency)
        {
            return new SingletonFactory(new BasicDependencyFactory(() => dependency));
        }

        public void run()
        {
            var views = Assembly.Load("nothinbutdotnetstore.web.ui").GetTypes()
                .Where(x => x.GetInterface("ViewFor`1") != null);
			   add_to_factories<IEnumerable<Type>>(views);

			  add_to_factories<Mapper<NameValueCollection, Department>>(
				new NameValueMapper<Department>());
			  add_to_factories<Mapper<NameValueCollection, Product>>(
				new NameValueMapper<Product>());

            DefaultResponseEngine.context = () => HttpContext.Current;
            DefaultViewFactory.page_factory = BuildManager.CreateInstanceFromVirtualPath;
        }
    }
}