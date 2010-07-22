using System.Collections.Generic;
using System.Linq;
using nothinbutdotnetstore.tasks.startup;

namespace nothinbutdotnetstore.web.core
{
	[Singleton]
    public class DefaultCommandRegistry : CommandRegistry
    {
        IEnumerable<WebCommand> commands;

        public DefaultCommandRegistry(IEnumerable<WebCommand> commands)
        {
            this.commands = commands;
        }

        public WebCommand get_command_that_can_handle(Request request)
        {
            return commands.FirstOrDefault(x => x.can_handle(request)) ??
                new MissingWebCommand();
        }
    }
}