 using Machine.Specifications;
 using Machine.Specifications.DevelopWithPassion.Rhino;
 using nothinbutdotnetstore.infrastructure.containers;
 using nothinbutdotnetstore.specs.utility;
 using nothinbutdotnetstore.web.application;
 using System.Linq;
 using nothinbutdotnetstore.web.core;
 using Rhino.Mocks;
 using Machine.Specifications.Utility;

namespace nothinbutdotnetstore.specs.web
{   
    public class CommandSetSpecs
    {
        public abstract class concern : Observes<CommandSet,
            DefaultCommandSet>
        {
        
        }

        [Subject(typeof(DefaultCommandSet))]
        public class when_retrieving_command_set : concern
        {
            Establish c = () =>
            {
                var commands = new ApplicationCommand[]
                {
                    new ViewDepartmentChildren(null, null),
                    new ViewMainDepartments(null, null)
                };
                container = the_dependency<Container>();
                commands.Each(command =>
                    container.Stub(x => x.an_instance_of(command.GetType()))
                        .Return(command));
            };

            Because b = () =>
                result = sut.ToArray();

            It should_contain_application_commands_in_same_namespace = () =>
                result.Select(x => new AccessPrivateWrapper(x)).Cast<dynamic>()
                    .Any(x => x.application_command.GetType() == typeof (ViewDepartmentChildren))
                    .ShouldBeTrue();

            static WebCommand[] result;
            static Container container;
        }
    }
}
