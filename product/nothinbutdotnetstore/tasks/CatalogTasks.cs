using System;
using System.Collections.Generic;
using nothinbutdotnetstore.data_access;
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
	    readonly Repository repository;

	    public DefaultCatalogTasks(Repository repository)
	    {
	        this.repository = repository;
	    }

	    public IEnumerable<Department> get_all_main_departments()
	    {
	        return repository.get_all<Department>();
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