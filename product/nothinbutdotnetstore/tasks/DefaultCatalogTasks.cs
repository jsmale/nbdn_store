using System;
using System.Collections.Generic;
using nothinbutdotnetstore.data_access;
using nothinbutdotnetstore.model;
using System.Linq;
using nothinbutdotnetstore.tasks.startup;

namespace nothinbutdotnetstore.tasks
{
	[Singleton]
	public class DefaultCatalogTasks : CatalogTasks 
	{
		readonly Repository repository;

		public DefaultCatalogTasks(Repository repository)
		{
			this.repository = repository;
		}
		
		public IEnumerable<Department> get_all_main_departments()
		{
			return repository.get_all<Department>().Where(x => x.parentId == null);
		}

		public IEnumerable<Department> get_all_sub_departments_in(Department department)
		{
			return repository.get_all<Department>().Where(x => x.parentId == department.id);
		}

		public IEnumerable<Product> get_all_products_in(Department department)
		{
			return repository.get_all<Product>().Where(x => x.departmentId == department.id);
		}

	    public bool department_has_sub_departments(Department department)
	    {
	        throw new NotImplementedException();
	    }
	}
}