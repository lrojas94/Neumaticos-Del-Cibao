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
                toSearch = toSearch.ToLower();
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

        public List<Client> ClientSearchByName(string toSearch)
        {
            if (toSearch != "")
            {
                toSearch = toSearch.ToLower();
                return Clients.Where(
                    client => client.Name.ToLower().Contains(toSearch) ||
                    client.ContactName.ToLower().Contains(toSearch)
                    ).ToList();
            }
            else
            {
                return Clients.ToList();
            }
        }

        public List<Article> ArticleSearchByName(string toSearch)
        {
            if (toSearch != "")
            {
                toSearch = toSearch.ToLower();
                return Articles.Where(
                    Article => Article.Name.ToLower().Contains(toSearch) ||
                    Article.CodeIdentifier.ToLower().Contains(toSearch)
                    ).ToList();
            }
            else
            {
                return Articles.ToList();
            }
        }

        public List<ShoppingBill> ShoppingBillSearch(string toSearch)
        {
            if (toSearch != "")
            {
                toSearch = toSearch.ToLower();
                var noLeadingZeros = 0;
                int.TryParse(toSearch,out noLeadingZeros);
                var noLeadingString = noLeadingZeros.ToString();
                return ShoppingBills.Where(
                    bill => bill.Date.Contains(toSearch)
                    || bill.Id.ToString().Contains(noLeadingString)
                    || bill.Client.Name.Contains(toSearch)
                    ).ToList();
            }
            else
                return ShoppingBills.ToList();
        }

        public List<Database.Option> EmployeesOptions(Employee loggedEmployee, String optionType)
        {
            List<Database.Option> options = new List<Option>();
            var employeePermissions = loggedEmployee.Permissions.ToList();
            

            foreach (var permision in employeePermissions)
            {
                foreach (var option in permision.Options.Where(o => o.OptionType == optionType).ToList())
                {
                    options.Add(option);
                }
            }
            return options;
        }

        public List<Option> OptionSearchByTitle(string toSearch)
        //Basic OptionSearch function. For more complex searching use LINQ.
        {
            if (toSearch != "")
            {
                return Options.Where(
                    options => options.OptionTitle.ToLower().Contains(toSearch)).ToList();
            }
            else
            {
                return Options.ToList();
            }
        }
        public List<Permission> PermissionSearchByName(string toSearch)
        //Basic PermissionSearch function. For more complex searching use LINQ.
        {
            if (toSearch != "")
            {

                return Permissions.Where(
                    permissions => permissions.Name.ToLower().Contains(toSearch)).ToList();
            }
            else
            {
                return Permissions.ToList();
            }
        }
    }
}
