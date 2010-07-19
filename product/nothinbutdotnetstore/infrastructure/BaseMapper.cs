using System;

namespace nothinbutdotnetstore.infrastructure
{
    public class BaseMapper<Input, Output> : Mapper<Input, Output>
    {
        public Output map_from(Input item)
        {
            return (Output) Convert.ChangeType(item, typeof(Output));
        }
    }
}