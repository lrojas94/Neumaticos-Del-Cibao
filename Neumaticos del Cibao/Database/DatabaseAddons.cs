using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Neumaticos_del_Cibao.Database
{
    public partial class databaseEntities : DbContext
    {
       public List<Employee> EmployeeSearchByName(string toSearch) 
            //Basic EmployeeSearch function. For more complex searching use LINQ.
       {
            if (toSearch != "")
            {
                return Employees.Where(
                    employee => employee.Username.ToLower().Contains(toSearch)
                    || employee.Person.Name.ToLower().Contains(toSearch)
                    || employee.Person.LastName.ToLower().Contains(toSearch)
                ).ToList();
            }
            else
            {
                return Employees.ToList();
            }
        }
    }
}
