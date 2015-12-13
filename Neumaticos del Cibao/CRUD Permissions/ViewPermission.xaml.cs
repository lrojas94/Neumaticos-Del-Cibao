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



namespace Neumaticos_del_Cibao.CRUD_Permissions
{
    /// <summary>
    /// Interaction logic for ViewPermission.xaml
    /// </summary>
    
    public partial class ViewPermission : Page
    {

        private bool newEntry = false;
        private Database.databaseEntities database;
        private Database.Permission permission;

        public ViewPermission(Database.Permission permission = null, Database.databaseEntities database = null)
        {
            this.permission = permission;
            if(this.permission == null)
            {
                newEntry = true;
                this.permission = new Database.Permission();
            }

            this.database = database;
            if (this.database == null)
                this.database = new Database.databaseEntities();


            InitializeComponent();
            
            DataContext = this.permission;

        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if(newEntry == true)
            {
                database.Permissions.Add(this.permission);
                Console.WriteLine(this.permission.Name);    
            }
            database.SaveChangesAsync();

            NavigationService.Navigate(new ViewAllPermissions(database));
            


        }
    }
}
