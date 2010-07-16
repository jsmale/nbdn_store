using nothinbutdotnetstore.infrastructure;

namespace nothinbutdotnetstore.tasks.startup
{
    public static class CommandExtensions
    {
        public static Command followed_by(this Command first, Command second)
        {
            return new ChainedCommand(first, second);
        } 
    }
}