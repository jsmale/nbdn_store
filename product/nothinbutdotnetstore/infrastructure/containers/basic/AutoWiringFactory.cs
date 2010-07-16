using System.Collections.Generic;
using System.Linq;

namespace nothinbutdotnetstore.infrastructure.containers.basic
{
    public class AutoWiringFactory : DependencyFactory
    {
        ConstructorSelectionStrategy constructor_selection_strategy;
        Container container;

        public AutoWiringFactory(ConstructorSelectionStrategy constructor_selection_strategy, Container container)
        {
            this.constructor_selection_strategy = constructor_selection_strategy;
            this.container = container;
        }

        public object create()
        {
            var constructor = constructor_selection_strategy.get_applicable_constructor();
            var arguments = constructor.GetParameters()
                .Select(x => container.an_instance_of(x.ParameterType));


            return constructor.Invoke(arguments.ToArray());
        }
    }
}