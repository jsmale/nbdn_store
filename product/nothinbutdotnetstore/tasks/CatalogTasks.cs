using System;
using System.Collections.Generic;
using nothinbutdotnetstore.model;

namespace nothinbutdotnetstore.tasks
{
    public interface CatalogTasks
    {
        IEnumerable<Department> get_all_main_departments();
        IEnumerable<Department> get_all_sub_departments_in(Department department);
        IEnumerable<Product> get_all_products_in(Department department);
    }

	public class DefaultCatalogTasks : CatalogTasks 
	{
		public IEnumerable<Department> get_all_main_departments()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Department> get_all_sub_departments_in(Department department)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Product> get_all_products_in(Department department)
		{
			throw new NotImplementedException();
		}
	}
}