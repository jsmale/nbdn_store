using System;
using System.Collections.Generic;
using nothinbutdotnetstore.infrastructure.extensions;

namespace nothinbutdotnetstore.tasks.startup
{
    public class StartupChainBuilder
    {
        readonly StartupCommandFactory startup_command_factory;
        readonly IList<StartupCommand> startup_commands;

        public StartupChainBuilder(StartupCommandFactory startup_command_factory, IList<StartupCommand> startup_commands,
            Type first_command_type)
        {
            this.startup_command_factory = startup_command_factory;
            this.startup_commands = startup_commands;
            append_command(first_command_type);
        }

        public void finish_by<Command>() where Command : StartupCommand
        {
            append_command<Command>();
            startup_commands.each(x => x.run());
        }

        void append_command<Command>()
        {
            append_command(typeof(Command));
        }

        void append_command(Type command_type)
        {
            startup_commands.Add(startup_command_factory.create_command_from(command_type));
        }

        public StartupChainBuilder followed_by<Command>() where Command : StartupCommand
        {
            append_command<Command>();
            return this;
        }
    }
}