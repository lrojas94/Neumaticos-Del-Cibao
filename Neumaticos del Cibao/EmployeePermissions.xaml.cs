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
using System.Collections.ObjectModel;

namespace Neumaticos_del_Cibao.Apps.Employees
{
    /// <summary>
    /// Interaction logic for EmployeePermissions.xaml
    /// </summary>
    public partial class EmployeePermissions : Page
    {

        private Database.databaseEntities database;
        private DispatcherTimer searchTimer;
        private ObservableCollection<Database.Permission> availablePermissionsList = new ObservableCollection<Database.Permission>(),
                                          userPermissionsList = new ObservableCollection<Database.Permission>();
        private Database.Employee selectedEmployee;

        public EmployeePermissions()
        {
            InitializeComponent();
            database = new Database.databaseEntities();
            employees.ItemsSource = database.Employees.ToList();
            availablePermissionsList = new ObservableCollection<Database.Permission>(database.Permissions.ToList());
            availablePermissions.ItemsSource = availablePermissionsList;
            userPermissions.ItemsSource = userPermissionsList;
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
            employees.ItemsSource = database.EmployeeSearchByName(searchBox.RealText);
            searchTimer.Stop();
        }

        private void employees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = e.AddedItems[0] as Database.Employee;
            selectedEmployeeLabel.Content = String.Format("Permisos de {0}", selected.FullName);
            selectedEmployee = selected;
            userPermissionsList = new ObservableCollection<Database.Permission>(selectedEmployee.Permissions.ToList());
            availablePermissionsList = new ObservableCollection<Database.Permission>(database.Permissions.ToList().Except(selectedEmployee.Permissions.ToList()).ToList());
            userPermissions.ItemsSource = userPermissionsList;
            availablePermissions.ItemsSource = availablePermissionsList;
        }

        private void availablePermission_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(selectedEmployee == null)
            {
                //Alert something here as there's no employee selected.
                MessageBox.Show(
                    "Esta tratando de asignar permisos sin haber selecionado un empleado. Seleccione un empleado e intente de nuevo.", 
                    "Seleccione un Empleado!"
                    );
                return;
            }

            var selected = (sender as ListBoxItem).Content as Database.Permission;
            availablePermissionsList.Remove(selected);
            userPermissionsList.Add(selected);

        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            List<Database.Permission> toRemove = new List<Database.Permission>(),
                                      toAdd = new List<Database.Permission>();
            
            toRemove = selectedEmployee.Permissions.ToList().Except(userPermissionsList.ToList()).ToList();
            toAdd = userPermissionsList.ToList().Except(selectedEmployee.Permissions.ToList()).ToList();

            foreach(var permission in toRemove)
            {
                selectedEmployee.Permissions.Remove(permission);
            }

            foreach (var permission in toAdd)
            {
                selectedEmployee.Permissions.Add(permission);
            }

            try
            {
                database.SaveChanges();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void userPermission_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Employee will never be NULL. 
            var selected = (sender as ListBoxItem).Content as Database.Permission;
            availablePermissionsList.Add(selected);
            userPermissionsList.Remove(selected);

        }
    }
}
