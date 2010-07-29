 using System;
 using System.Collections.Specialized;
 using Machine.Specifications;
 using Machine.Specifications.DevelopWithPassion.Rhino;
 using nothinbutdotnetstore.infrastructure;

namespace nothinbutdotnetstore.specs.infrastructure
 {   
 	public class NameValueMapperSpecs
 	{
 		public abstract class concern : Observes<Mapper<NameValueCollection,TestClass>,
 			NameValueMapper<TestClass>>
 		{
        
 		}

 		[Subject(typeof(NameValueMapper<TestClass>))]
 		public class when_mapping_from_a_name_value_collection : concern
 		{
        
 			It should_map_the_key_values_to_the_appropriate_properties_on_the_output_class = () =>        
 				
 		}

		public class TestClass
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public DateTime LastUpdated { get; set; }
		}
 	}
 }
