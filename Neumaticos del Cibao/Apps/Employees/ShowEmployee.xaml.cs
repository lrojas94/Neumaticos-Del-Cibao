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
    /// Interaction logic for ShowEmployee.xaml
    /// </summary>
    public partial class ShowEmployee : Page
    {
        Database.Employee employee;
        public ShowEmployee(Database.Employee employee)
        {
            InitializeComponent();
            this.employee = employee;
            DataContext = this.employee;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEmployee(employee));
        }
    }
}
