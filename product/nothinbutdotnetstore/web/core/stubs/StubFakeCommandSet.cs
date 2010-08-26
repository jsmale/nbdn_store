using System;
using System.Collections;
using System.Collections.Generic;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.tasks;
using nothinbutdotnetstore.tasks.startup;
using nothinbutdotnetstore.web.application;

namespace nothinbutdotnetstore.web.core.stubs
{
	public class StubFakeCommandSet : IEnumerable<WebCommand>
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<WebCommand> GetEnumerator()
        {
            yield return new DefaultWebCommand(x => x.raw_command.Contains(typeof(ViewMainDepartments).Name + ".store"),
                                               new ViewMainDepartments(
                                                   IOC.get.an_instance_of<ResponseEngine>(),
                                                   IOC.get.an_instance_of<CatalogTasks>()));
            yield return new DefaultWebCommand(x => x.raw_command.Contains(typeof(ViewDepartmentChildren).Name + ".store"),
                                               new ViewDepartmentChildren(
                                                   IOC.get.an_instance_of<ResponseEngine>(),
                                                   IOC.get.an_instance_of<CatalogTasks>()));
        }
    }
}