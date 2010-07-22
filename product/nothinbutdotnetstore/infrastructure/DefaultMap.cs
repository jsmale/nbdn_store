using System;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.tasks.startup;

namespace nothinbutdotnetstore.infrastructure
{
	[Singleton]
    public class DefaultMap : Map
    {
        Container container;

        public DefaultMap(Container container)
        {
            this.container = container;
        }

        public Output map<Input, Output>(Input item)
        {
            return container.an_instance_of<Mapper<Input, Output>>().map_from(item);
        }
    }
}