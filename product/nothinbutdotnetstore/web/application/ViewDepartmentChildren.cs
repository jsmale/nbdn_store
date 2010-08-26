using System;
using nothinbutdotnetstore.model;
using nothinbutdotnetstore.tasks;
using nothinbutdotnetstore.web.core;

namespace nothinbutdotnetstore.web.application
{
    public class ViewDepartmentChildren : ApplicationCommand
    {
        private readonly ResponseEngine responseEngine;
        private readonly CatalogTasks catalogTasks;

        public ViewDepartmentChildren(ResponseEngine responseEngine, CatalogTasks catalogTasks )
        {
            this.responseEngine = responseEngine;
            this.catalogTasks = catalogTasks;
        }

        public void process(Request request)
        {
            //var hasSubDepartments = catalogTasks.department_has_sub_departments(request.map<Department>());
            //if(hasSubDepartments)
            //{
                responseEngine.display(catalogTasks.get_all_sub_departments_in(request.map<Department>()));
            //}
           
           
        }
    }
}