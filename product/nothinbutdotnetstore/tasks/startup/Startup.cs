namespace nothinbutdotnetstore.tasks.startup
{
    public class Startup
    {
        public static void run()
        {
            Start.by<ConfigureCoreServices>()
                .followed_by<ConfigureFrontController>()
                .finish_by<ConfigureServiceLayer>();
        }
    }
}