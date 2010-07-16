using System;
using System.Collections.Generic;
using System.Threading;
using nothinbutdotnetstore.infrastructure.extensions;

namespace nothinbutdotnetstore.tasks.startup
{
    public class StartupChainBuilder
    {
        StartupCommandFactory startup_command_factory;

        public StartupChainBuilder(StartupCommandFactory startup_command_factory, IList<StartupCommand> startup_commands,
            Type first_command_type)
        {
            this.startup_command_factory = startup_command_factory;
            append_command(first_command_type);
        }

        public void finish_by<Command>() where Command : StartupCommand
        {
            append_command<Command>();
        }

        void append_command<Command>()
        {
            append_command(typeof(Command));
        }

        void append_command(Type command_type)
        {
            var command = startup_command_factory.create_command_from(command_type);
            ThreadPool.QueueUserWorkItem(startup_command_queue_callback, command);

        }

        private void startup_command_queue_callback(object state)
        {
            ((StartupCommand) state).run();
        }

        public StartupChainBuilder followed_by<Command>() where Command : StartupCommand
        {
            append_command<Command>();
            return this;
        }
    }
}