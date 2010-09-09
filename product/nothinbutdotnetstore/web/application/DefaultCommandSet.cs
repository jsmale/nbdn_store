using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.tasks.startup;
using nothinbutdotnetstore.web.core;
using System.Linq;
using nothinbutdotnetstore.infrastructure.extensions;

namespace nothinbutdotnetstore.web.application
{
    [Singleton(ContractType = typeof(IEnumerable<WebCommand>))]
    public class DefaultCommandSet : CommandSet
    {
        private readonly Container Container;
        private IEnumerable<WebCommand> webCommands;


        public DefaultCommandSet(Container container)
        {
            Container = container;
        }

        public IEnumerator<WebCommand> GetEnumerator()
        {
            if (webCommands == null)
            {
                var nameSpace = GetType().Namespace;
                var applicationCommandType = typeof(ApplicationCommand);
                webCommands = Assembly.GetAssembly(GetType()).GetTypes()
                    .Where(type => type.Namespace == nameSpace
                        && applicationCommandType.IsAssignableFrom(type))
                    .Select(type =>
                        new DefaultWebCommand(x => x.raw_command.Contains(type.Name + ".store"),
                            (ApplicationCommand)Container.an_instance_of(type)))
                    .ToArray();
            }
            return webCommands.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}