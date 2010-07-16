using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using Machine.Specifications.DevelopWithPassion.Rhino;
using nothinbutdotnetstore.tasks.startup;
using Rhino.Mocks;
using nothinbutdotnetstore.infrastructure.extensions;

namespace nothinbutdotnetstore.specs.tasks
{
    public class StartupChainBuilderSpecs
    {
        public abstract class concern : Observes<StartupChainBuilder>
        {
            Establish c = () =>
            {
                all_commands = new List<StartupCommand>();
                startup_command_factory = the_dependency<StartupCommandFactory>();
                provide_a_basic_sut_constructor_argument(all_commands);
            };

            protected static IList<StartupCommand> all_commands;
            protected static StartupCommandFactory startup_command_factory;
        }

        [Subject(typeof(StartupChainBuilder))]
        public class when_constructed_with_an_initial_startup_command_type : concern
        {
            Establish c = () =>
            {
                first_command = new FirstCommand();
                provide_a_basic_sut_constructor_argument(typeof(FirstCommand));
                startup_command_factory.Stub(x => x.create_command_from(typeof(FirstCommand))).Return(first_command);
            };

            It should_store_the_startup_command_as_a_step_in_the_startup_process = () =>
                all_commands.ShouldContainOnly(first_command);

            static StartupCommand first_command;
        }
        
        [Subject(typeof(StartupChainBuilder))]
        public class when_finishing_the_command_chain : concern
        {
            Establish c = () =>
            {
                number_of_commands_run = 0;
                var command = new DelegateCommand(() => { number_of_commands_run++; });
                Enumerable.Range(1, 9).each(x => all_commands.Add(command));

                provide_a_basic_sut_constructor_argument(typeof(DelegateCommand));
                startup_command_factory
                    .Stub(x => x.create_command_from(typeof(DelegateCommand)))
                    .Return(command);
            };

            Because b = () =>
                sut.finish_by<DelegateCommand>();

            It should_run_all_of_the_commands_in_the_chain = () =>
                number_of_commands_run.ShouldEqual(all_commands.Count);

            static int number_of_commands_run;                    
        }

        [Subject(typeof(StartupChainBuilder))]
        public class when_following_the_command_chain : concern
        {
            Establish c = () =>
            {
                command = new DelegateCommand(() => {});
                Enumerable.Range(1, 9).each(x => all_commands.Add(command));

                provide_a_basic_sut_constructor_argument(typeof(FirstCommand));
                startup_command_factory
                    .Stub(x => x.create_command_from(typeof(DelegateCommand)))
                    .Return(command);
            };

            Because b = () =>
                result = sut.followed_by<DelegateCommand>();

            It should_append_the_command_to_the_command_list = () =>
                all_commands.Last().ShouldEqual(command);

            It should_return_itself = () =>
                result.ShouldEqual(sut);

            static DelegateCommand command;
            static StartupChainBuilder result;
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