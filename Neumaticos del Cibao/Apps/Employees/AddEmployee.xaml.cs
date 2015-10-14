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
        private Database.Person person;

        private void readFromEmployee()
        {
        }

        public AddEmployee(Database.Employee employee = null)
        {
            InitializeComponent();
            Common.Placeholder namePlaceholder = new Common.Placeholder("Ej. Miguel Jose", name);
            Common.Placeholder lastNamePlaceholder = new Common.Placeholder("Ej. Perez Rodriguez", lastName);
            if(employee != null)
            {
                //Fill fields with employee data.
                this.employee = employee;
                readFromEmployee();
            }
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
