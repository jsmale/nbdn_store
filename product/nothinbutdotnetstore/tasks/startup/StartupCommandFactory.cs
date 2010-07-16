using System;

namespace nothinbutdotnetstore.tasks.startup
{
    public interface StartupCommandFactory
    {
        StartupCommand create_command_from(Type command_type);
    }
}