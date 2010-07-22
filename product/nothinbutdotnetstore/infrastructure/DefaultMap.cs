using System;
using nothinbutdotnetstore.infrastructure.containers;

namespace nothinbutdotnetstore.infrastructure
{
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