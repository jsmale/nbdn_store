using System.Collections.Generic;
using Machine.Specifications;
using Machine.Specifications.DevelopWithPassion.Rhino;
using nothinbutdotnetstore.tasks.startup;
using Rhino.Mocks;

namespace nothinbutdotnetstore.specs.tasks
{
    public class StartupChainBuilderSpecs
    {
        public abstract class concern : Observes<StartupChainBuilder>
        {
        }

        [Subject(typeof(StartupChainBuilder))]
        public class when_constructed_with_an_initial_startup_command_type : concern
        {
            Establish c = () =>
            {
                all_commands = new List<StartupCommand>();
                startup_command_factory = the_dependency<StartupCommandFactory>();
                provide_a_basic_sut_constructor_argument(all_commands);
                first_command = new FirstCommand();


                provide_a_basic_sut_constructor_argument(typeof(FirstCommand));
                startup_command_factory.Stub(x => x.create_command_from(typeof(FirstCommand))).Return(first_command);
            };

            It should_store_the_startup_command_as_a_step_in_the_startup_process = () =>
                all_commands.ShouldContainOnly(first_command);

            static IList<StartupCommand> all_commands;
            static StartupCommand first_command;
            static StartupCommandFactory startup_command_factory;
        }

        class FirstCommand : StartupCommand
        {
            public void run()
            {
            }
        }
    }
}