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
        private Database.databaseEntities database = new Database.databaseEntities();
        private Database.Permission permission;

        public ViewPermission(Database.Permission permission = null)
        {
            if(permission == null)
            {
                newEntry = true;
                this.permission = new Database.Permission();
            }
            else
            {
                this.permission = permission;
            }

            InitializeComponent();
            
            DataContext = permission;
            name.SetBinding(TextBox.TextProperty, "Name");
            description.SetBinding(TextBox.TextProperty, "Description");

        }

        private void BtnCreate_Click(object sender, RoutedEventArgs e)
        {
            if(newEntry == true)
            {
                database.Permissions.Add(this.permission);
                
            }
            database.SaveChanges();


        }
    }
}
