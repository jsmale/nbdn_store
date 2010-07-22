using System;
using System.Collections.Generic;
using nothinbutdotnetstore.model;
using System.Linq;
using nothinbutdotnetstore.tasks.startup;

namespace nothinbutdotnetstore.data_access
{
	public interface Repository
	{
		IEnumerable<T> get_all<T>();
	}

	[Singleton(ContractType = typeof(Repository))]
	public class InMemoryRepository : Repository
	{
		IDictionary<Type, IList<object>> dictionary;

		public InMemoryRepository()
		{
			dictionary = new Dictionary<Type, IList<object>>
			{
				{
					typeof(Department), new List<object>
					{
						new Department{id=1,name = "Produce"},
						new Department{id=2,name = "Meat"},
						new Department{id=3,name = "Vegetables",parentId = 1},
						new Department{id=4,name = "Fruit",parentId = 1},						
						new Department{id=5,name = "Beef",parentId = 2},
						new Department{id=6,name = "Fish",parentId = 2},
					}
				}, 
				{
					typeof(Product), new List<object>
					{
						new Product{departmentId = 3,name = "Lettuce"},
						new Product{departmentId = 3,name = "Carrot"},
						new Product{departmentId = 4,name = "Apple"},
						new Product{departmentId = 4,name = "Orange"},
						new Product{departmentId = 5,name = "Prime Rib"},
						new Product{departmentId = 5,name = "Sirloin"},
						new Product{departmentId = 6,name = "Bass"},
						new Product{departmentId = 6,name = "Salmon"},
					}
				}
			};
		}

		public IEnumerable<T> get_all<T>()
		{
			return dictionary[typeof (T)].Cast<T>();
		}
	}
}