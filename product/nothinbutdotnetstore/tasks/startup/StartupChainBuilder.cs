using System;
using System.Collections.Generic;
using nothinbutdotnetstore.infrastructure.extensions;

namespace nothinbutdotnetstore.tasks.startup
{
    public class StartupChainBuilder
    {
        StartupCommandFactory startup_command_factory;
        StartupCommand startup_command;

        public StartupChainBuilder(StartupCommandFactory startup_command_factory, Type first_command_type)
        {
            this.startup_command_factory = startup_command_factory;
            startup_command = create_command_from(first_command_type);
        }

        StartupCommand create_command_from(Type first_command_type)
        {
            return startup_command_factory.create_command_from(first_command_type);
        }

        private StartupChainBuilder(StartupCommandFactory startup_command_factory, StartupCommand previous_command, Type command_type)
        {
            this.startup_command_factory = startup_command_factory;
            startup_command = new ChainStartupCommand(previous_command, create_command_from(command_type));
        }

        public void finish_by<Command>() where Command : StartupCommand
        {
            followed_by<Command>().startup_command.run();
        }

        public StartupChainBuilder followed_by<Command>() where Command : StartupCommand
        {
            return new StartupChainBuilder(startup_command_factory, startup_command, typeof(Command));
        }
    }
}