 using System.Collections.Generic;
 using Machine.Specifications;
 using Machine.Specifications.DevelopWithPassion.Rhino;
 using nothinbutdotnetstore.data_access;
 using nothinbutdotnetstore.model;
 using nothinbutdotnetstore.tasks;
 using Rhino.Mocks;

namespace nothinbutdotnetstore.specs.tasks
 {   
 	public class CatalogTasksSpecs
 	{
 		public abstract class concern : Observes<CatalogTasks,
 			DefaultCatalogTasks>
 		{
        
 		}

 		[Subject(typeof(DefaultCatalogTasks))]
 		public class when_getting_all_the_main_departments : concern
 		{
 			Establish c = () =>
 			{
 				department1 = new Department();
				department2 = new Department{parentId = 2};
				department3 = new Department();
 				repository = the_dependency<Repository>();

				repository.Stub(x => x.get_all<Department>())
					.Return(new []{department1, department2, department3});
 			};
 			Because b = () =>
 				result = sut.get_all_main_departments();

 			It should_only_return_the_main_departments = () =>
 				result.ShouldContainOnly(department1, department3);

 			static Repository repository;
 			static IEnumerable<Department> result;
 			static Department department1;
 			static Department department2;
 			static Department department3;
 		}

        [Subject(typeof(DefaultCatalogTasks))]
 		public class when_getting_all_the_sub_departments : concern
 		{
 			Establish c = () => {
 			    department1 = new Department{id = 2};
				department2 = new Department{parentId = 2};
 			    department3 = new Department{parentId = 2};
 				repository = the_dependency<Repository>();

				repository.Stub(x => x.get_all<Department>())
					.Return(new []{department1, department2, department3});
 			};
 			Because b = () =>
 				result = sut.get_all_sub_departments_in(department1);

 			It should_only_return_the_sub_departments = () =>
 				result.ShouldContainOnly(department2, department3);

 			static Repository repository;
 			static IEnumerable<Department> result;
 			static Department department1;
 			static Department department2;
 			static Department department3;
 		}
 	}
 }
