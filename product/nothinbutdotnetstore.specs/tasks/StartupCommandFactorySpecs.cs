using System;
using System.Collections.Generic;
using Machine.Specifications;
using Machine.Specifications.DevelopWithPassion.Rhino;
using nothinbutdotnetstore.infrastructure.containers.basic;
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
        public class when_creating_a_command_and_it_has_the_required_constructor : concern
        {
            Establish c = () =>
                factories = the_dependency<IDictionary<Type, DependencyFactory>>();

            Because b = () =>
                result = sut.create_command_from(typeof(MyCommand));

            It should_create_the_startup_command_and_provide_it_the_dictionary_of_factories = () =>
                result.ShouldBeAn<MyCommand>().factories.ShouldEqual(factories);

            static StartupCommand result;
            static IDictionary<Type, DependencyFactory> factories;
        }

        public class MyCommand : StartupCommand
        {
            public IDictionary<Type, DependencyFactory> factories { get; set; }

            public MyCommand(IDictionary<Type, DependencyFactory> factories)
            {
                this.factories = factories;
            }

            public void run()
            {
            }
        }
    }
}