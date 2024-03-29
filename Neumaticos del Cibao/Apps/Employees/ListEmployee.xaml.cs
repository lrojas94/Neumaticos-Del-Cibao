﻿using System;
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
        private Common.TimedFunction searchBoxFilter;

        public ListEmployee()
        {
            InitializeComponent();

            Action search = () =>
            {
                employeeListBox.ItemsSource = database.EmployeeSearchByName(searchBox.RealText);
            };

            searchBoxFilter = new Common.TimedFunction(search);

            employeeListBox.ItemsSource = database.Employees.ToList();
        }
        
        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchBoxFilter.Run();
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
            NavigationService.Navigate(new AddEmployee(database));
        }

        private void btnModifyEmployee_Click(object sender, RoutedEventArgs e)
        {
            var targetView = new AddEmployee(database, selectedEmployee);
            targetView.title.Content = string.Format("Editando a {0}", selectedEmployee.FullName);
            NavigationService.Navigate(targetView);
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
            NavigationService.Navigate(new ShowEmployee(selectedEmployee));
        }
    }
}
