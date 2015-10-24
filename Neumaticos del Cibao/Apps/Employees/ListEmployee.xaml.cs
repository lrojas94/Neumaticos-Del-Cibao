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
using System.Windows.Threading;

namespace Neumaticos_del_Cibao.Apps.Employees
{
    /// <summary>
    /// Interaction logic for ListEmployee.xaml
    /// </summary>
    public partial class ListEmployee : Page
    {
        private Database.databaseEntities database = new Database.databaseEntities();
        private Database.Employee selectedEmployee = null;
        private DispatcherTimer searchBoxTimer;

        public ListEmployee()
        {
            InitializeComponent();

            searchBoxTimer = new DispatcherTimer();
            searchBoxTimer.Tick += searchBox_Tick;
            searchBoxTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);

            employeeListBox.ItemsSource = database.Employees.ToList();
        }
        


        private void searchBox_Tick(object sender, object e)
        {
            employeeListBox.ItemsSource = database.EmployeeSearchByName(searchBox.RealText);
            searchBoxTimer.Stop();
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!searchBoxTimer.IsEnabled)
            {
                searchBoxTimer.Start();
            }
        }

        private void employeeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            btnViewDetails.IsEnabled = true;
            btnDeleteEmployee.IsEnabled = true;
            btnModifyEmployee.IsEnabled = true;
            selectedEmployee = (sender as ListBox).SelectedItem as Database.Employee;
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEmployee());
        }

        private void btnModifyEmployee_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddEmployee(database.Employees.Single(emp=> emp.Id == selectedEmployee.Id)));
        }

        private void btnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            database.Employees.Remove(selectedEmployee);
            database.SaveChangesAsync();
            employeeListBox.ItemsSource = database.EmployeeSearchByName(searchBox.RealText);
        }

        private void btnViewDetails_Click(object sender, RoutedEventArgs e)
        {
            //View Employee details here.
        }
    }
}
