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
    /// Interaction logic for EmployeePermissions.xaml
    /// </summary>
    public partial class EmployeePermissions : Page
    {

        Database.databaseEntities database;
        DispatcherTimer searchTimer;

        public EmployeePermissions()
        {
            InitializeComponent();
            database = new Database.databaseEntities();
            employees.ItemsSource = database.Employees.ToList();
            searchTimer = new DispatcherTimer();
            searchTimer.Tick += searchTimer_Tick;
            searchTimer.Interval = new TimeSpan(0,0,0,0,50);
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!searchTimer.IsEnabled)
            {
                searchTimer.Start();
            }
        }

        private void searchTimer_Tick(object sender, EventArgs e)
        {
            var toSearch = searchBox.RealText.ToLower();
            if (toSearch != "")
            {
                var searchEmployees = database.Employees.Where(
                    employee => employee.Username.ToLower().Contains(toSearch)
                    || employee.Person.Name.ToLower().Contains(toSearch)
                    || employee.Person.LastName.ToLower().Contains(toSearch)
                ).ToList();
                employees.ItemsSource = searchEmployees;
            }
            else
            {
                employees.ItemsSource = database.Employees.ToList();
            }

            searchTimer.Stop();

            
        }

        private void employees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = e.AddedItems[0] as Database.Employee;
        }
    }
}
