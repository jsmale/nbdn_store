 using Machine.Specifications;
 using Machine.Specifications.DevelopWithPassion.Rhino;
 using nothinbutdotnetstore.tasks.startup;

namespace nothinbutdotnetstore.specs.tasks
 {   
     public class StartupCommandFactorySpecs
     {
         public abstract class concern : Observes<StartupCommandFactory,
             DefaultStartupCommandFactory>
         {
        
         }

         [Subject(typeof(DefaultStartupCommandFactory))]
         public class when_creating_a_command_from_a_type : concern
         {
        
             It should_use_the_constructor_with_a_dictionary_of_factories = () =>        
                 
         }
     }
 }
