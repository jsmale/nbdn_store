using System;
using System.Collections.Specialized;

namespace nothinbutdotnetstore.infrastructure
{
	public class NameValueMapper<Output> : Mapper<NameValueCollection, Output>
	{
		public Output map_from(NameValueCollection item)
		{
			throw new NotImplementedException();
		}
	}
}