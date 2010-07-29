 using System;
 using System.Collections.Specialized;
 using Machine.Specifications;
 using Machine.Specifications.DevelopWithPassion.Rhino;
 using nothinbutdotnetstore.infrastructure;

namespace nothinbutdotnetstore.specs.infrastructure
 {   
 	public class NameValueMapperSpecs
 	{
 		public abstract class concern : Observes<Mapper<NameValueCollection, TestClass>,
 			NameValueMapper<TestClass>>
 		{
        
 		}

 		[Subject(typeof(NameValueMapper<TestClass>))]
 		public class when_mapping_from_a_name_value_collection : concern
 		{
 			Establish e = () =>
			{
				item = new TestClass()
				{
					Id = 5,
					LastUpdated = new DateTime(1950, 2, 14),
					Name = "Chewbacca"
				}; 
				nvc = new NameValueCollection()
				{
					{"Id", "5"},
					{"LastUpdated", "02/14/1950"},
					{"Name", "Chewbacca"}
				};
				
			};
        
			Because b = () =>
			{
				result = sut.map_from(nvc);
			};
			            
 			It should_map_the_key_values_to_the_appropriate_properties_on_the_output_class = () =>
			{
				result.Id.ShouldEqual(item.Id);
				result.LastUpdated.ShouldEqual(item.LastUpdated);
				result.Name.ShouldEqual(item.Name);
			};     
 				
			private static NameValueCollection nvc;
			private static TestClass item;
			private static TestClass result;
 		}

		[Subject(typeof(NameValueMapper<TestClass>))]
		public class when_mapping_from_a_name_value_collection_missing_a_property : concern
		{
			Establish e = () =>
			{
				nvc = new NameValueCollection()
				{
					{"Id", "5"},
					{"Name", "Chewbacca"}
				};

			};

			Because b = () =>
			{
				result = sut.map_from(nvc);
			};

			It should_leave_the_property_as_the_default_value = () =>
				result.LastUpdated.ShouldEqual(DateTime.MinValue);

			private static NameValueCollection nvc;
			private static TestClass result;
		}

		public class TestClass
		{
			public int Id { get; set; }
			public string Name { get; set; }
			public DateTime LastUpdated { get; set; }
		}
 	}
 }
