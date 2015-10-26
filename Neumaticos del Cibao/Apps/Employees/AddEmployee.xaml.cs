using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Neumaticos_del_Cibao.Apps.Employees
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Page
    {
        private Database.Employee employee;
        private bool isNewEntry = false;
        private Database.databaseEntities database;

        private void bindEmployee()
        {
            /*
                Most part of binding takes place in XAML, but some stuff is to be done
            */
            if(employee.Person != null && sex != null)
            {
                sex.SelectedIndex = employee.Person.Sex == "Masculino" ? 0 : 1;
                DataContext = employee;
            }
            /*
                Here we should set password, but since no decryption/encryption algorithm is ready,
                this won't be taken into account right now.
            */
        }

        private void formToEmployee()
        {
            employee.Person.Sex = (sex.SelectedItem as ComboBoxItem).Content.ToString() ;
            employee.Password = password.Password.ToString(); 

            if (isNewEntry)
            {
                database.Employees.Add(employee);
            }

            try
            {
                database.SaveChangesAsync();
                NavigationService.Navigate(new ListEmployee());
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException e)
            {
                Console.WriteLine(e.StackTrace);
                foreach(var error in e.EntityValidationErrors)
                {
                    foreach(var valError in error.ValidationErrors)
                    {
                        Console.WriteLine(valError.ErrorMessage);
                    }
                }
            }

        }

        private bool validate()
        {
            return true;
        }

        public AddEmployee(Database.Employee employee = null)
        {
            this.employee = employee;
            if (employee == null)
            {
                isNewEntry = true;
                this.employee = new Database.Employee();
                this.employee.Person = new Database.Person();
            }
            else
            {
                bindEmployee();
            }
            

        }

        public AddEmployee(Database.databaseEntities context = null,Database.Employee employee = null) : this(employee)
        {
            /*
                Something really important learned here:
                When using databaseEntities, creating a new instance when another one is already active,
                will create different context. This means that saving data in one of them, does NOT affect
                the real data, but the data in the new context (Which is not the db context itself). If passing data
                from other places, remember to pass the database context like done here.
            */
            InitializeComponent();
            /*
                Placeholder class works fine when it's about 
            */

            database = context;

            if (context == null)
            {
                database = new Database.databaseEntities();
            }

            bindEmployee();


        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            formToEmployee();
        }
    }
}
