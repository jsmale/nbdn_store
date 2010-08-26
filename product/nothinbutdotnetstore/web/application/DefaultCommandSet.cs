using System;
using System.Collections;
using System.Collections.Generic;
using nothinbutdotnetstore.tasks.startup;
using nothinbutdotnetstore.web.core;

namespace nothinbutdotnetstore.web.application
{
    [Singleton(ContractType = typeof(IEnumerable<WebCommand>))]
    public class DefaultCommandSet : CommandSet
    {
        public IEnumerator<WebCommand> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}