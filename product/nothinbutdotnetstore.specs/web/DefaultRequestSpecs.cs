using System.Collections.Specialized;
using Machine.Specifications;
using Machine.Specifications.DevelopWithPassion.Rhino;
using nothinbutdotnetstore.infrastructure;
using nothinbutdotnetstore.web.core;
using Rhino.Mocks;

namespace nothinbutdotnetstore.specs.web
{
    public class DefaultRequestSpecs
    {
        public abstract class concern : Observes<Request,
                                            DefaultRequest>
        {
            Establish c = () =>
            {
                provide_a_basic_sut_constructor_argument("");

            };
        }

        [Subject(typeof(DefaultRequest))]
        public class when_mapping_an_input_model : concern
        {
            Establish c = () =>
            {
                payload = new NameValueCollection();
                provide_a_basic_sut_constructor_argument(payload);
                map = the_dependency<Map>();

                map.Stub(x => x.map<NameValueCollection, Person>(payload)).Return(new Person());
            };

            Because b = () =>
                result = sut.map<Person>();


            It should_return_the_model_mapped_from_the_payload = () =>
                result.ShouldBeAn<Person>();


            static Person result;
            static Map map;
            static NameValueCollection payload;
        }
        class Person
        {
        }
    }
}