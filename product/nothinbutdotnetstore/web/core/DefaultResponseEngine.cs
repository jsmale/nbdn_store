using System;
using System.Web;
using nothinbutdotnetstore.tasks.startup;

namespace nothinbutdotnetstore.web.core
{
	[Singleton]
    public class DefaultResponseEngine : ResponseEngine
    {
        ViewFactory view_factory;

        public static Context context = delegate
        {
            throw new Exception(
                "You need to provide a context resolver at application startup");
        };


        public DefaultResponseEngine(ViewFactory view_factory)
        {
            this.view_factory = view_factory;
        }

        public void display<ViewModel>(ViewModel view_model)
        {
            view_factory.create_view(view_model).ProcessRequest(context());
        }
    }
}