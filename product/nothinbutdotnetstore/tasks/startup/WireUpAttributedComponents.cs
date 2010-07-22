using System;
using System.Collections.Generic;
using System.Reflection;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.infrastructure.containers.basic;
using System.Linq;
using nothinbutdotnetstore.infrastructure.extensions;

namespace nothinbutdotnetstore.tasks.startup
{
    public class WireUpAttributedComponents : StartupCommand
    {
        IDictionary<Type, DependencyFactory> factories;

        public WireUpAttributedComponents(IDictionary<Type, DependencyFactory> factories)
        {
            this.factories = factories;
        }

        public void run()
        {
            wire_up_singletons();
            wire_up_transients();
        }

        void wire_up_transients()
        {
            wire_up_components<TransientAttribute>((x, y, z) => new AutoWiringFactory(x, y, z));
        }

        void wire_up_singletons()
        {
            wire_up_components<SingletonAttribute>((x, y, z) => new SingletonFactory(new AutoWiringFactory(x, y, z)));
        }

        void wire_up_components<AttributeType>(Func<ConstructorSelectionStrategy, Container, Type, DependencyFactory> factory) where AttributeType : Attribute
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.GetCustomAttributes(typeof(AttributeType), false).Length > 0);
            types.each(type =>
            {
            	var attribute = (IoCAttribute)type.GetCustomAttributes(typeof (AttributeType), false).First();					
                var interfaces = type.GetInterfaces();
					 var contract = attribute.ContractType ?? ((interfaces.Length > 0) ? interfaces[0] : type);
                factories.Add(contract, factory(new GreedyConstructorSelectionStrategy(), IOC.get, type));
            });
        }
    }
}