using System;
using System.Web;
using nothinbutdotnetstore.infrastructure;
using nothinbutdotnetstore.tasks.startup;

namespace nothinbutdotnetstore.web.core
{
	[Singleton]
    public class DefaultRequestFactory : RequestFactory
    {
        Map mapper;

        public DefaultRequestFactory(Map mapper)
        {
            this.mapper = mapper;
        }

        public Request create_from(HttpContext http_context)
        {
            return new DefaultRequest(http_context.Request.RawUrl,
                                      http_context.Request.Params, mapper);
        }
    }
}