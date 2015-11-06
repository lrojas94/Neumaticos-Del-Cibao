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

    namespace Neumaticos_del_Cibao.CRUD_Permissions
    {
        /// <summary>
        /// Interaction logic for ViewAllPermissions.xaml
        /// </summary>
        public partial class ViewAllPermissions : Page
        {

            private Database.databaseEntities database = new Database.databaseEntities();
            private DispatcherTimer searchTimer;

            public ViewAllPermissions()
            {
                InitializeComponent();
                permissions.ItemsSource = database.Permissions.ToList();
                searchTimer = new DispatcherTimer();
                searchTimer.Tick += searchTimer_Tick;
                searchTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            
            }
        
            private void searchTimer_Tick(object sender, EventArgs e)
            {
                // var searcher = name.RealText.ToLower(); 
                var searcher = name.Text;
                if(searcher != "")
                {
                    var ListPermissions = database.Permissions.Where(
                        perm => permissions.Name.ToLower().Contains(searcher)).ToList();
                    permissions.ItemsSource = searcher;
                }
                else
                {
                    permissions.ItemsSource = database.Permissions.ToList();
                }
                searchTimer.Stop();
            }

            private void name_TextChanged(object sender, TextChangedEventArgs e)
            {
                if(!searchTimer.IsEnabled)
                {
                    searchTimer.Start();
                }
            }

            private void btnAnadir_Click(object sender, RoutedEventArgs e)
            {
                NavigationService.Navigate(new ViewPermission());
            }
        }
    }
