using System;

namespace nothinbutdotnetstore.tasks.startup
{
    public interface StartupCommandFactory
    {
        StartupCommand create_command_from(Type command_type);
    }

    public class DefaultStartupCommandFactory : StartupCommandFactory 
    {
        public StartupCommand create_command_from(Type command_type)
        {
            throw new NotImplementedException();
        }
    }
}