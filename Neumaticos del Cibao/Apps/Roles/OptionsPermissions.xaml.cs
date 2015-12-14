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
        private ObservableCollection<Database.Permission> availablePermissionsList = new ObservableCollection<Database.Permission>();

        private Common.TimedFunction searchFunction;
        private ObservableCollection<Database.Option> optionPermissionsList = new ObservableCollection<Database.Option>(),
                                                      availableOptionsList = new ObservableCollection<Database.Option>();
        private Database.Permission selectedPermission;

        public OptionsPermissions()
        {
            
            InitializeComponent();
            database = new Database.databaseEntities();
            availableOptionsList = new ObservableCollection<Database.Option>(database.Options.ToList());
            options.ItemsSource = availableOptionsList;
            availablePermissionsList = new ObservableCollection<Database.Permission>(database.Permissions.ToList());
            availablePermissions.ItemsSource = availablePermissionsList;
            optionPermissions.ItemsSource = optionPermissionsList;

            Action search = () =>
            {
                availablePermissions.ItemsSource = database.PermissionSearchByName(searchBox.RealText);
            };

            searchFunction = new Common.TimedFunction(search);
            
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchFunction.Run();
        }

        private void searchTimer_Tick(object sender, EventArgs e)
        {
            availablePermissions.ItemsSource = database.PermissionSearchByName(searchBox.RealText);
            searchTimer.Stop();
        }

        private void permissions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = e.AddedItems[0] as Database.Permission;
            selectedPermissionLabel.Content = String.Format("Opciones de {0}", selected.Name);
            selectedPermission = selected;
            optionPermissionsList = new ObservableCollection<Database.Option>(selectedPermission.Options.ToList());
            availableOptionsList = new ObservableCollection<Database.Option>(database.Options.ToList().Except(selectedPermission.Options.ToList()).ToList());
            
            optionPermissions.ItemsSource = optionPermissionsList;
            options.ItemsSource = availableOptionsList;
        }

        private void availablePermission_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(selectedPermission == null)
            {
                MessageBox.Show(
                    "Esta tratando de asignar opciones sin haber selecionado un permiso. Seleccione un permiso e intente de nuevo.", 
                    "Seleccione un permiso."
                    );
                return;
            }

            var selected = (sender as ListBoxItem).Content as Database.Option;
            availableOptionsList.Remove(selected);
            optionPermissionsList.Add(selected);

        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            List<Database.Option> toRemove = new List<Database.Option>(),
                                      toAdd = new List<Database.Option>();
            
            toRemove = selectedPermission.Options.ToList().Except(optionPermissionsList.ToList()).ToList();
            toAdd = optionPermissionsList.ToList().Except(selectedPermission.Options.ToList()).ToList();

            foreach(var permission in toRemove)
            {
                selectedPermission.Options.Remove(permission);
            }
            
            foreach (var permission in toAdd)
            {
                selectedPermission.Options.Add(permission);
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
            var selected = (sender as ListBoxItem).Content as Database.Option;
            availableOptionsList.Add(selected);
            optionPermissionsList.Remove(selected);

        }

        private void availableOptions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
