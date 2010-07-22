 using Machine.Specifications;
 using Machine.Specifications.DevelopWithPassion.Rhino;
 using nothinbutdotnetstore.data_access;
 using nothinbutdotnetstore.model;
 using nothinbutdotnetstore.tasks;

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
 				repository = the_dependency<Repository>();
 			};
 			Because b = () =>
 				sut.get_all_main_departments();

 			It should_get_the_departments_from_the_repository = () =>
 				repository.received(x => x.get_all<Department>());

 			static Repository repository;
 		}
 	}
 }
