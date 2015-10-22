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

        private void bindEmployee()
        {
            name.SetBinding(TextBox.TextProperty,employee.Person.Name);
            lastName.SetBinding(TextBox.TextProperty, employee.Person.LastName);
            birthDate.SetBinding(DatePicker.TextProperty, employee.Person.BirthDate);
            email.SetBinding(TextBox.TextProperty,employee.Person.Email);
            phone.SetBinding(TextBox.TextProperty, employee.Person.Phone) ;
            username.SetBinding(TextBox.TextProperty,employee.Username);
            startDate.SetBinding(TextBox.TextProperty,employee.StartDate);

            sex.SelectedIndex = employee.Person.Sex == "Masculino" ? 0 : 1;
            /*
            Here we should set password, but since no decryption/encryption algorithm is ready,
            this won't be taken into account right now.
            */
        }

        private void formToEmployee()
        {
            var database = new Database.databaseEntities();
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
            InitializeComponent();
            /*
                Placeholder class works fine when it's about 
            */
            if(employee == null)
            {
                isNewEntry = true;
                this.employee = new Database.Employee();
            }
            else
            {
                this.employee = employee;
            }

            bindEmployee();
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            formToEmployee();
        }
    }
}
