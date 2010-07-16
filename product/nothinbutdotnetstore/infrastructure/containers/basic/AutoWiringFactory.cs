using System;
using System.Collections.Generic;
using System.Reflection;

namespace nothinbutdotnetstore.infrastructure.containers.basic
{
    public class AutoWiringFactory : DependencyFactory
    {
        readonly ConstructorSelectionStrategy constructor_selection_strategy;
        readonly Container container;

        public AutoWiringFactory(ConstructorSelectionStrategy constructor_selection_strategy,Container container)
        {
            this.constructor_selection_strategy = constructor_selection_strategy;
            this.container = container;
            
        }

        public object create()
        {
            var paramlist = new List<object>();
            ConstructorInfo constructor = constructor_selection_strategy.get_applicable_constructor();
            foreach (var parameter_info in constructor.GetParameters())
            {
                paramlist.Add(container.an_instance_of(parameter_info.ParameterType));
            }

            return constructor.Invoke(paramlist.ToArray());
        }
    }
}