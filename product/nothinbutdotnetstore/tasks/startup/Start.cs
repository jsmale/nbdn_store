using System;
using System.Collections.Generic;
using nothinbutdotnetstore.infrastructure.containers.basic;

namespace nothinbutdotnetstore.tasks.startup
{
    public class Start
    {
        public static StartupChainBuilder by<Command>() where Command : StartupCommand
        {
            return new StartupChainBuilder(new DefaultStartupCommandFactory(
                new Dictionary<Type, DependencyFactory>()),
                new List<StartupCommand>(),
                typeof(Command));
        }
    }
}