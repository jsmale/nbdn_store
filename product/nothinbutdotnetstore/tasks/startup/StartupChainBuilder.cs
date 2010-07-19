using System;
using System.Collections.Generic;
using nothinbutdotnetstore.infrastructure.extensions;

namespace nothinbutdotnetstore.tasks.startup
{
    public class StartupChainBuilder
    {
        StartupCommandFactory startup_command_factory;
        IList<StartupCommand> all_commands;

        public StartupChainBuilder(StartupCommandFactory startup_command_factory, IList<StartupCommand> all_commands,
                            Type initial_command_type)
        {
            this.startup_command_factory = startup_command_factory;
            this.all_commands = all_commands;
            add_command(initial_command_type);
        }

        public void finish_by<Command>() where Command : StartupCommand
        {
            add_command(typeof(Command));
            all_commands.each(x => x.run());
        }

        void add_command(Type command_type)
        {
            all_commands.Add(startup_command_factory.create_command_from(command_type));
        }

        public StartupChainBuilder followed_by<Command>() where Command : StartupCommand
        {
            add_command(typeof(Command));
            return this;
        }
    }
}