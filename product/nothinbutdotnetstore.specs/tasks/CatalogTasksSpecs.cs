using System.Collections.Generic;
using Machine.Specifications;
using Machine.Specifications.DevelopWithPassion.Rhino;
using nothinbutdotnetstore.data_access;
using nothinbutdotnetstore.model;
using nothinbutdotnetstore.tasks;
using Rhino.Mocks;
using System.Linq;

namespace nothinbutdotnetstore.specs.tasks
{
	public class CatalogTasksSpecs
	{
		public abstract class concern : Observes<CatalogTasks,
			DefaultCatalogTasks>
		{
		    Establish c = () =>
		    {
		        departments = new List<Department>();
                repository = the_dependency<Repository>();
                repository.Stub(x => x.get_all<Department>())
                    .Return(departments);
		    };
            protected static Repository repository;
		    protected static List<Department> departments;
		}

        [Subject(typeof (DefaultCatalogTasks))]
		public abstract class when_getting_departments : concern
        {
            Establish c = () =>
            {
                department1 = new Department { id = 2 };
                department2 = new Department { parentId = 2 };
                department3 = new Department { parentId = 2 };
                department4 = new Department { parentId = 3 };
                departments.AddRange(new[]{department1, department2, department3, department4});
            };

            protected static Department department1;
            protected static Department department2;
            protected static Department department3;
            protected static Department department4;
        }

		[Subject(typeof (DefaultCatalogTasks))]
		public class when_getting_all_the_main_departments : when_getting_departments
		{
		    Because b = () =>
		        result = sut.get_all_main_departments();

			It should_only_return_the_main_departments = () =>
				result.ShouldContainOnly(department1);

		    static IEnumerable<Department> result;
		}

		[Subject(typeof (DefaultCatalogTasks))]
		public class when_getting_all_the_sub_departments : when_getting_departments
		{
			Because b = () =>
				result = sut.get_all_sub_departments_in(department1);

			It should_only_return_the_sub_departments = () =>
				result.ShouldContainOnly(department2, department3);

		    static IEnumerable<Department> result;
		}

		[Subject(typeof (DefaultCatalogTasks))]
		public class when_getting_all_the_products_in_a_department : concern
		{
			Establish c = () =>
			{
				department = new Department {id = 1};
				product1 = new Product {departmentId = 1};
				product2 = new Product {departmentId = 1};
				product3 = new Product {departmentId = 2};

				repository = the_dependency<Repository>();

				repository.Stub(x => x.get_all<Product>())
					.Return(new[] {product1, product2, product3});
			};

			Because b = () =>
				products = sut.get_all_products_in(department);

			It should_only_return_the_products_in_a_department = () =>
				products.ShouldContainOnly(product1, product2);

			static Product product1;
			static Product product2;
			static Product product3;
		    static Department department;
		    static IEnumerable<Product> products;
		}

        [Subject(typeof(DefaultCatalogTasks))]
        public abstract class when_checking_if_department_has_subdepartments : concern
        {
            Establish c = () =>
            {
                department = new Department { id = 1 };
                departments.AddRange(new []
                {
                    new Department{parentId = 2},
                    new Department{parentId = 5},
                    new Department{parentId = 3},
                    new Department{parentId = 4},
                });
            };

            Because b = () =>
                result = sut.department_has_sub_departments(department);

            protected static bool result;
            protected static Department department;
        }

        [Subject(typeof(DefaultCatalogTasks))]
        public class when_department_has_subdepartments : when_checking_if_department_has_subdepartments
        {
            Establish c = () =>
            {
                departments.ElementAt(2).parentId = department.id;
            };

            It should_return_true_if_any_department_has_the_department_as_its_parent = () =>
                result.ShouldBeTrue();            
        }

        [Subject(typeof(DefaultCatalogTasks))]
        public class when_department_does_not_have_subdepartments : when_checking_if_department_has_subdepartments
        {
            It should_return_false_if_no_departments_have_the_department_as_its_parent = () =>
                result.ShouldBeFalse();
        }
	}
}