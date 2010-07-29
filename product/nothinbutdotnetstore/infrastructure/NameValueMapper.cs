using System;
using System.Collections.Specialized;
using System.Reflection;

namespace nothinbutdotnetstore.infrastructure
{
	public class NameValueMapper<Output> : Mapper<NameValueCollection, Output>
	{
		public Output map_from(NameValueCollection item)
		{
			var results = Activator.CreateInstance<Output>();
			var props = typeof(Output).GetProperties();
			foreach(var prop in props)
			{
				if (item[prop.Name] != null) {
					prop.SetValue(results, Convert.ChangeType(item[prop.Name], prop.PropertyType), null);
				}
			}
			return results;
		}
	}
}