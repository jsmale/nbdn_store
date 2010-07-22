using System;
using System.Web;
using nothinbutdotnetstore.infrastructure;

namespace nothinbutdotnetstore.web.core
{
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