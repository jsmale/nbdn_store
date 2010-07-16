using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Machine.Specifications.DevelopWithPassion.Extensions;
using Machine.Specifications.DevelopWithPassion.Rhino;
using nothinbutdotnetstore.infrastructure.extensions;
using nothinbutdotnetstore.tasks.startup;
using Rhino.Mocks;

namespace nothinbutdotnetstore.specs.tasks
{
    public class StartupChainBuilderSpecs
    {
        public abstract class concern : Observes<StartupChainBuilder>
        {
            Establish c = () =>
            {
                all_commands = new List<StartupCommand>();
                first_command = new FirstCommand();
                startup_command_factory = the_dependency<StartupCommandFactory>();
                provide_a_basic_sut_constructor_argument(all_commands);
                provide_a_basic_sut_constructor_argument(typeof(FirstCommand));
                startup_command_factory.Stub(x => x.create_command_from(typeof(FirstCommand)))
                    .Return(first_command);
            };

            protected static IList<StartupCommand> all_commands;
            protected static StartupCommandFactory startup_command_factory;
            protected static StartupCommand first_command;
        }

        [Subject(typeof(StartupChainBuilder))]
        public class when_constructed_with_an_initial_startup_command_type : concern
        {
            It should_store_the_startup_command_as_a_step_in_the_startup_process = () =>
                all_commands.ShouldContainOnly(first_command);
        }

        [Subject(typeof(StartupChainBuilder))]
        public class when_following_the_command_chain : concern
        {
            Establish c = () =>
            {
                command = new DelegateCommand(() => { });

                startup_command_factory
                    .Stub(x => x.create_command_from(typeof(DelegateCommand)))
                    .Return(command);
            };

            Because b = () =>
                result = sut.followed_by<DelegateCommand>();

            It should_append_the_command_to_the_command_list =
                () => { all_commands.ShouldContainOnlyInOrder(first_command, command); };

            It should_return_itself = () =>
                result.ShouldEqual(sut);

            static DelegateCommand command;
            static StartupChainBuilder result;
        }

        [Subject(typeof(StartupChainBuilder))]
        public class when_finishing_the_command_chain : concern
        {
            Establish c = () =>
            {
                number_of_commands_run = 0;
                var command = new DelegateCommand(() => { number_of_commands_run++; });
                EnumerableExtensions.each(Enumerable.Range(1, 9), x => all_commands.Add(command));

                startup_command_factory
                    .Stub(x => x.create_command_from(typeof(DelegateCommand)))
                    .Return(command);
            };

            Because b = () =>
                sut.finish_by<DelegateCommand>();

            It should_run_all_of_the_commands_in_the_chain = () =>
                number_of_commands_run.ShouldEqual(all_commands.Count -1);

            static int number_of_commands_run;
        }

        public class DelegateCommand : StartupCommand
        {
            readonly Action action;

            public DelegateCommand(Action action)
            {
                this.action = action;
            }

            public void run()
            {
                action();
            }
        }

        class FirstCommand : StartupCommand
        {
            public void run()
            {
            }
        }
    }
}