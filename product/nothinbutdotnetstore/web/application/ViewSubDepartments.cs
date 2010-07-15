using nothinbutdotnetstore.model;
using nothinbutdotnetstore.tasks;
using nothinbutdotnetstore.tasks.stubs;
using nothinbutdotnetstore.web.core;

namespace nothinbutdotnetstore.web.application
{
    public class ViewSubDepartments : ApplicationCommand
    {
        ResponseEngine response_engine;
        CatalogTasks catalog;
        Department department;

        public ViewSubDepartments()
            : this(new DefaultResponseEngine(),
                new StubCatalogTasks(),
                new Department())
        {
        }

        public ViewSubDepartments(ResponseEngine response_engine, CatalogTasks catalog, Department department)
        {
            this.response_engine = response_engine;
            this.catalog = catalog;
            this.department = department;
        }

        public void process(Request request)
        {
            response_engine.display(catalog.get_all_sub_departments(department));
        }

    }
}