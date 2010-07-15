using System.Collections.Generic;
using Machine.Specifications;
using Machine.Specifications.DevelopWithPassion.Rhino;
using nothinbutdotnetstore.model;
using nothinbutdotnetstore.tasks;
using nothinbutdotnetstore.web.application;
using nothinbutdotnetstore.web.core;
using Rhino.Mocks;

namespace nothinbutdotnetstore.specs.web
{
    public class ViewSubDepartmentsSpecs
    {
        public abstract class concern : Observes<ApplicationCommand,
                                            ViewSubDepartments>
        {
        }

        [Subject(typeof(ViewSubDepartments))]
        public class when_vieing_the_list_of_sub_departments_in_a_department : concern
        {
            Establish c = () =>
            {
                catalog_tasks = the_dependency<CatalogTasks>();
                response_engine = the_dependency<ResponseEngine>();
                request = an<Request>();

                sub_departments = new List<Department>();
                
                catalog_tasks.Stub(x => x.get_all_sub_departments(new Department() 
                    { department_id = 1, name = "MD1"})).Return(sub_departments);
            };

            Because b = () =>
                sut.process(request);

            It should_tell_the_response_engine_to_display_the_sub_departments_of_a_department = () =>
                response_engine.received(x => x.display(sub_departments));

            static ResponseEngine response_engine;
            static IEnumerable<Department> sub_departments;
            static Request request;
            static CatalogTasks catalog_tasks;
        }
    }
}