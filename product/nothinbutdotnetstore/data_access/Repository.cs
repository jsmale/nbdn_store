using System.Collections.Generic;

namespace nothinbutdotnetstore.data_access
{
	public interface Repository
	{
		IEnumerable<T> get_all<T>();
	}
}