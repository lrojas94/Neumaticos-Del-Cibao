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

        private void employeeToForm()
        {
            name.Text = employee.Person.Name;
            lastName.Text = employee.Person.LastName;
            sex.SelectedIndex = employee.Person.Sex == "Masculino" ? 0 : 1;
            birthDate.Text = employee.Person.BirthDate;
            email.Text = employee.Person.Email;
            phone.Text = employee.Person.Phone;
            username.Text = employee.Username;
            startDate.Text = employee.StartDate;
            /*
            Here we should set password, but since no decryption/encryption algorithm is ready,
            this won't be taken into account right now.
            */
        }

        private void formToEmployee()
        {
            var newEntry = employee == null ? true : false;
            var database = new Database.databaseEntities();
            if(employee == null)
            {
                employee = new Database.Employee();
                employee.Person = new Database.Person();
            }

            employee.Person.Name = name.Text;
            employee.Person.LastName = lastName.Text;
            employee.Person.Sex = (sex.SelectedItem as ComboBoxItem).Content.ToString() ;
            employee.Person.BirthDate = birthDate.RealText;
            employee.Person.Email = email.Text;
            employee.Person.Phone = phone.Text;
            employee.Username = username.Text;
            employee.StartDate = startDate.Text;
            /*
                Once again, here we're supposed to encrypt password.
                We're saving it as the database will complain when password
                field is left empty/null. 
            */
            employee.Password = password.Password.ToString(); 
            if (newEntry)
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
            InitializeComponent();
            /*
                Placeholder class works fine when it's about 
            */
            if(employee != null)
            {
                //Fill fields with employee data.
                this.employee = employee;
                employeeToForm();
            }
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            formToEmployee();
        }
    }
}
