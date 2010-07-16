using System;
using System.Collections.Generic;

namespace nothinbutdotnetstore.tasks.startup
{
    public class StartupChainBuilder
    {
        readonly IList<StartupCommand> startup_commands;
        readonly StartupCommandFactory startup_command_factory;
        readonly Type type;

        public StartupChainBuilder(IList<StartupCommand> startup_commands, StartupCommandFactory startup_command_factory, Type type)
        {
            this.startup_commands = startup_commands;
            this.startup_command_factory = startup_command_factory;
            this.type = type;

            startup_commands.Add(startup_command_factory.create_command_from(type));

        }
    }
}