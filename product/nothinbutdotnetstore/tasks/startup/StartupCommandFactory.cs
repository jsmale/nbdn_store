using System;
using System.Collections.Generic;
using nothinbutdotnetstore.infrastructure.containers.basic;
using System.Linq;

namespace nothinbutdotnetstore.tasks.startup
{
    public interface StartupCommandFactory
    {
        StartupCommand create_command_from(Type command_type);
    }

    public class DefaultStartupCommandFactory : StartupCommandFactory 
    {
        IDictionary<Type, DependencyFactory> factories;


        public DefaultStartupCommandFactory(IDictionary<Type, DependencyFactory> factories)
        {
            this.factories = factories;
        }

        public StartupCommand create_command_from(Type command_type)
        {
            var factories_type = typeof (IDictionary<Type, DependencyFactory>);
            var constructor = command_type.GetConstructors().First(x =>
                x.GetParameters().Count() == 1
                    && x.GetParameters()[0].ParameterType == factories_type);
            return (StartupCommand) constructor.Invoke(new object[] {factories});
        }
    }
}