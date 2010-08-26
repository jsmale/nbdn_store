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
    public class ViewDepartmentChildrenSpecs
    {
        public abstract class concern : Observes<ApplicationCommand,
            ViewDepartmentChildren>
        {
        
        }

        [Subject(typeof(ViewDepartmentChildren))]
        public abstract class when_viewing_the_children_of_a_department : concern
        {
            Establish c = () =>
            {
                request = an<Request>();
                response_engine = the_dependency<ResponseEngine>();
                catalog_tasks = the_dependency<CatalogTasks>();

                department = new Department();
                request.Stub(x => x.map<Department>()).Return(department);
            };

            Because b = () =>
                sut.process(request);

            protected static Request request;
            protected static Department department;
            protected static ResponseEngine response_engine;
            protected static CatalogTasks catalog_tasks;
        }

        [Subject(typeof(ViewDepartmentChildren))]
        public class when_the_department_has_sub_departments : when_viewing_the_children_of_a_department
        {
            Establish c = () =>
            {
                catalog_tasks.Stub(x => x.department_has_sub_departments(department))
                    .Return(true);

                sub_departments = new List<Department>();
                catalog_tasks.Stub(x => x.get_all_sub_departments_in(department))
                    .Return(sub_departments);
            };

            It should_tell_the_response_engine_to_display_the_sub_departments_of_a_department = () =>
                response_engine.was_told_to(x => x.display(sub_departments));

            static IEnumerable<Department> sub_departments;
        }

        [Subject(typeof(ViewDepartmentChildren))]
        public class when_the_department_has_products : when_viewing_the_children_of_a_department
        {
            Establish c = () =>
            {
                products = new List<Product>();
                catalog_tasks.Stub(x => x.get_all_products_in(department))
                    .Return(products);
            };

            It should_tell_the_response_engine_to_display_the_products_of_a_department = () =>
                response_engine.was_told_to(x => x.display(products));

            static IEnumerable<Product> products;
        }
    }
}
