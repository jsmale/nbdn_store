 using Machine.Specifications;
 using Machine.Specifications.DevelopWithPassion.Rhino;
 using nothinbutdotnetstore.web.application;
 using System.Linq;
 using nothinbutdotnetstore.web.core;

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
            Because b = () =>
                result = sut.ToArray();

            It should_contain_application_commands_in_same_namespace = () =>
                result.Cast<dynamic>()
                    .Any(x => x.application_command.GetType() == typeof (ViewDepartmentChildren))
                    .ShouldBeTrue();

            static WebCommand[] result;                        
        }
    }
}
