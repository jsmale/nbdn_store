using System.Data;
using System.Linq;
using System.Reflection;
using Machine.Specifications;
using Machine.Specifications.DevelopWithPassion.Rhino;
using nothinbutdotnetstore.infrastructure.containers;
using nothinbutdotnetstore.infrastructure.containers.basic;
using Rhino.Mocks;

namespace nothinbutdotnetstore.specs.infrastructure
{
    public class AutoWiringFactorySpecs
    {
        public abstract class concern : Observes<DependencyFactory,
                                            AutoWiringFactory>
        {
        }

        [Subject(typeof(AutoWiringFactory))]
        public class when_creating_a_dependency : concern
        {
            Establish c = () =>
            {
                the_connection = an<IDbConnection>();
                the_command = an<IDbCommand>();
                constructor_selection_strategy = the_dependency<ConstructorSelectionStrategy>();
                container = the_dependency<Container>();
                container.Stub(x => x.an_instance_of(typeof(IDbConnection))).Return(the_connection);
                container.Stub(x => x.an_instance_of(typeof(IDbCommand))).Return(the_command);

                the_constructor =
                    typeof(OurTypeWithDependencies).GetConstructors().OrderByDescending(x => x.GetParameters().Count()).
                        First();

                constructor_selection_strategy.Stub(x => x.get_applicable_constructor()).Return(the_constructor);

                provide_a_basic_sut_constructor_argument(typeof(OurTypeWithDependencies));
            };

            Because b = () =>
                result = sut.create();

            It should_return_the_dependency_populated_with_all_of_its_container_resolved_dependencies = () =>
            {
                var item = result.ShouldBeAn<OurTypeWithDependencies>();
                item.connection.ShouldEqual(the_connection);
                item.command.ShouldEqual(the_command);
            };

            static object result;
            static IDbConnection the_connection;
            static IDbCommand the_command;
            static Container container;
            static ConstructorSelectionStrategy constructor_selection_strategy;
            static ConstructorInfo the_constructor;
        }
    }

    public class OurTypeWithDependencies
    {
        public IDbConnection connection;

        public IDbCommand command;

        public OurTypeWithDependencies(IDbConnection connection, IDbCommand command)
        {
            this.connection = connection;
            this.command = command;
        }

        public OurTypeWithDependencies(IDbCommand command)
        {
            this.command = command;
        }

        public OurTypeWithDependencies(IDbConnection connection)
        {
            this.connection = connection;
        }
    }
}