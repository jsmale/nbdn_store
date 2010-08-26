using System;
using System.Collections;
using System.Collections.Generic;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.tasks;
using nothinbutdotnetstore.tasks.startup;
using nothinbutdotnetstore.web.application;

namespace nothinbutdotnetstore.web.core.stubs
{
	[Singleton(ContractType = typeof(IEnumerable<WebCommand>))]
    public class StubFakeCommandSet : IEnumerable<WebCommand>
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<WebCommand> GetEnumerator()
        {           
            yield return new DefaultWebCommand(x => !x.raw_command.Contains("?"),
                                               new ViewMainDepartments(
                                                   IOC.get.an_instance_of<ResponseEngine>(),
                                                   IOC.get.an_instance_of<CatalogTasks>()));
            var random = new Random().Next(0, 2);
            yield return new DefaultWebCommand(x => x.raw_command.Contains("?") && random == 0,
                                               new ViewSubDepartments(
                                                   IOC.get.an_instance_of<ResponseEngine>(),
                                                   IOC.get.an_instance_of<CatalogTasks>()));
            yield return new DefaultWebCommand(x => x.raw_command.Contains("?"),
                                               new ViewProducts(
                                                   IOC.get.an_instance_of<ResponseEngine>(),
                                                   IOC.get.an_instance_of<CatalogTasks>()));
        }
    }
}