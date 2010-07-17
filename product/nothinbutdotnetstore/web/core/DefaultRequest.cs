using System.Collections.Specialized;
using nothinbutdotnetstore.infrastructure;

namespace nothinbutdotnetstore.web.core
{
    public class DefaultRequest : Request
    {
        string raw_url;
        NameValueCollection payload;
        Map mapper;

        public DefaultRequest(string raw_url, NameValueCollection payload, Map mapper)
        {
            this.raw_url = raw_url;
            this.payload = payload;
            this.mapper = mapper;
        }

        public string raw_command
        {
            get { return raw_url; }
        }

        public InputModel map<InputModel>()
        {
            return mapper.map<NameValueCollection, InputModel>(payload);
        }
    }
}