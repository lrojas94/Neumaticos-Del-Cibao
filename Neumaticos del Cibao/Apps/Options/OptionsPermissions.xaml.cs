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

namespace Neumaticos_del_Cibao.Apps.Options
{
    /// <summary>
    /// Interaction logic for OptionsPermissions.xaml
    /// </summary>
    public partial class OptionsPermissions : Page
    {

        private Database.databaseEntities database;
        private DispatcherTimer searchTimer;
        private ObservableCollection<Database.Permission> availablePermissionsList = new ObservableCollection<Database.Permission>(),
                                          optionPermissionsList = new ObservableCollection<Database.Permission>();
        private ObservableCollection<Database.Option> availableOptionsList = new ObservableCollection<Database.Option>();
        private Database.Option selectedOption;

        public OptionsPermissions()
        {
            
            InitializeComponent();
            database = new Database.databaseEntities();
            availableOptionsList = new ObservableCollection<Database.Option>(database.Options.ToList());
            options.ItemsSource = database.Options.ToList();
            availablePermissionsList = new ObservableCollection<Database.Permission>(database.Permissions.ToList());
            availablePermissions.ItemsSource = availablePermissionsList;
            optionPermissions.ItemsSource = optionPermissionsList;
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
            options.ItemsSource = database.OptionSearchByName(searchBox.RealText);
            searchTimer.Stop();
        }

        private void options_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = e.AddedItems[0] as Database.Option;
            selectedOptionLabel.Content = String.Format("Permisos de {0}", selected.OptionTitle);
            selectedOption = selected;
            optionPermissionsList = new ObservableCollection<Database.Permission>(selectedOption.Permissions.ToList());
            availablePermissionsList = new ObservableCollection<Database.Permission>(database.Permissions.ToList().Except(selectedOption.Permissions.ToList()).ToList());
            optionPermissions.ItemsSource = optionPermissionsList;
            availablePermissions.ItemsSource = availablePermissionsList;
        }

        private void availablePermission_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(selectedOption == null)
            {
                //Alert something here as there's no employee selected.
                MessageBox.Show(
                    "Esta tratando de asignar permisos sin haber selecionado una opcion. Seleccione una opcion e intente de nuevo.", 
                    "Seleccione una Opcion!"
                    );
                return;
            }

            var selected = (sender as ListBoxItem).Content as Database.Permission;
            availablePermissionsList.Remove(selected);
            optionPermissionsList.Add(selected);

        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            List<Database.Permission> toRemove = new List<Database.Permission>(),
                                      toAdd = new List<Database.Permission>();
            
            toRemove = selectedOption.Permissions.ToList().Except(optionPermissionsList.ToList()).ToList();
            toAdd = optionPermissionsList.ToList().Except(selectedOption.Permissions.ToList()).ToList();

            foreach(var permission in toRemove)
            {
                selectedOption.Permissions.Remove(permission);
            }
            
            foreach (var permission in toAdd)
            {
                selectedOption.Permissions.Add(permission);
            }

            try
            {
                database.SaveChanges();
                MessageBox.Show("Permisos añadidos correctamente.");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        private void optionPermission_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //Option will never be NULL. 
            var selected = (sender as ListBoxItem).Content as Database.Permission;
            availablePermissionsList.Add(selected);
            optionPermissionsList.Remove(selected);

        }

        private void availablePermissions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
