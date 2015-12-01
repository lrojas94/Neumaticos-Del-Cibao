using Neumaticos_del_Cibao.Apps.Common;
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
            private TimedFunction searchFuntion;

            public ViewAllPermissions()
            {
                InitializeComponent();
                permissions.ItemsSource = database.Permissions.ToList();

                Action search = () =>
                {
                    permissions.ItemsSource = database.PermissionSearch(name.RealText);
                };

                searchFuntion = new TimedFunction(search);
                
            }
        


            private void name_TextChanged(object sender, TextChangedEventArgs e)
            {
                searchFuntion.Run();   
            }

            private void btnAnadir_Click(object sender, RoutedEventArgs e)
            {
                NavigationService.Navigate(new ViewPermission());
            }
        }
    }
