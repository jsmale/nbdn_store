using System;
using System.Linq;

namespace nothinbutdotnetstore.infrastructure.containers.basic
{
    public class AutoWiringFactory : DependencyFactory
    {
        ConstructorSelectionStrategy constructor_selection_strategy;
        Container container;
        Type type_to_create;

        public AutoWiringFactory(ConstructorSelectionStrategy constructor_selection_strategy, Container container, Type type_to_create)
        {
            this.constructor_selection_strategy = constructor_selection_strategy;
            this.type_to_create = type_to_create;
            this.container = container;
        }

        public object create()
        {
            var constructor = constructor_selection_strategy.get_applicable_constructor_on(type_to_create);
            var arguments = constructor.GetParameters()
                .Select(x => container.an_instance_of(x.ParameterType));

            return constructor.Invoke(arguments.ToArray());
        }
    }
}