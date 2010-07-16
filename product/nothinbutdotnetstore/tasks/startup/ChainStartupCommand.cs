namespace nothinbutdotnetstore.tasks.startup
{
    public class ChainStartupCommand : StartupCommand
    {
        StartupCommand base_command;
        StartupCommand next_command;

        public ChainStartupCommand(StartupCommand startup_command)
        {
            this.base_command = base_command;
            this.next_command = next_command;
        }

        public ChainStartupCommand(StartupCommand base_command, StartupCommand next_command)
        {
            this.base_command = base_command;
            this.next_command = next_command;
        }

        public void run()
        {
            base_command.run();
            next_command.run();
        }
    }
}