using nothinbutdotnetstore.infrastructure.containers;

namespace nothinbutdotnetstore.tasks.startup
{
    public class Start
    {
        public static StartupChainBuilder by<Command>() where Command : StartupCommand
        {
            return new StartupChainBuilder(IOC.get.an_instance_of<StartupCommandFactory>() 
                                           , typeof(Command));
        }
    }
}