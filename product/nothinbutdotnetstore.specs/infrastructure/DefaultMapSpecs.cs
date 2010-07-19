 using Machine.Specifications;
 using Machine.Specifications.DevelopWithPassion.Rhino;
 using nothinbutdotnetstore.infrastructure;
 using nothinbutdotnetstore.infrastructure.containers;
 using Rhino.Mocks;

namespace nothinbutdotnetstore.specs.infrastructure
 {   
     public class DefaultMapSpecs
     {
         public abstract class concern : Observes<Map,
                                             DefaultMap>
         {
        
         }

         [Subject(typeof(DefaultMap))]
         public class when_mapping_an_input_to_an_output : concern
         {

             Establish c = () =>
             {
                 container = the_dependency<Container>();
                 the_mapper = the_dependency<Mapper<string,int>>();

                 the_mapper.Stub(x => x.map_from("23")).Return(23);

                 container.Stub(x => x.an_instance_of<Mapper<string, int>>()).Return(the_mapper);
             };

             Because b = () =>
                 result = sut.map<string, int>("23");


             It should_do_the_mapping_by_using_the_mapper_that_can_handle_the_mapping = () =>
                 result.ShouldEqual(23);


             static int result;
             static Container container;
             static Mapper<string, int> the_mapper;
         }
     }
 }
