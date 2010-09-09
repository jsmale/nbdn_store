using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.tasks.startup;
using nothinbutdotnetstore.web.core;
using System.Linq;

namespace nothinbutdotnetstore.web.application
{
    [Singleton(ContractType = typeof(IEnumerable<WebCommand>))]
    public class DefaultCommandSet : CommandSet
    {
        private readonly Container Container;
        private string nameSpace;
        private Type[] assembly;


        public DefaultCommandSet(Container container)
        {
            Container = container;
            nameSpace = GetType().Namespace;
            assembly = Assembly.GetAssembly(GetType()).GetTypes();
        }

        public IEnumerator<WebCommand> GetEnumerator()
        {
            foreach (var type in assembly)
            {
                if (type.Namespace == nameSpace)
                {
                    //if (type.IsAssignableFrom(typeof (ApplicationCommand)))
                    if(typeof(ApplicationCommand).IsAssignableFrom(type))
                    {
                        yield return
                            new DefaultWebCommand(x => x.raw_command.Contains(type.Name + ".store"),
                                                  (ApplicationCommand) Container.an_instance_of(type));
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}