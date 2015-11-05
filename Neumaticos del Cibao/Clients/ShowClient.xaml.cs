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

namespace Neumaticos_del_Cibao.Clients
{
    /// <summary>
    /// Interaction logic for ShowClient.xaml
    /// </summary>
    public partial class ShowClient : Page
    {
        Database.Client client;
        public ShowClient(Database.Client client)
        {
            InitializeComponent();
            this.client = client;
            DataContext = this.client;
            
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            Database.databaseEntities db = null;
            NavigationService.Navigate(new AddClient(db, client));
        }
    }
}
