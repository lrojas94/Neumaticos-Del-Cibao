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
    /// Interaction logic for ViewClient.xaml
    /// </summary>
    public partial class AddClient : Page
    {
        private Database.Client client;
        private bool isNewEntry = false;
        private Database.databaseEntities database;

  
        public AddClient(Database.databaseEntities context = null, Database.Client client = null)
        {
            /*
                Something really important learned here:
                When using databaseEntities, creating a new instance when another one is already active,
                will create different context. This means that saving data in one of them, does NOT affect
                the real data, but the data in the new context (Which is not the db context itself). If passing data
                from other places, remember to pass the database context like done here.
            */
            InitializeComponent();
            /*
                Placeholder class works fine when it's about 
            */

            database = context;

            if (context == null)
            {
                database = new Database.databaseEntities();
            }

            bindClient(client);

        }

        private void bindClient( Database.Client client)
        {
            if (client == null)
            {
                isNewEntry = true;
                this.client = new Database.Client();
            }
            else
            {
                this.client = client;
            }
            if(isNewEntry==false && client.CreditLimit != null)
            {
                creditCheckBox.IsChecked = true;
            }
            DataContext = this.client;

        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if(creditCheckBox.IsChecked == false)
            {
                client.CreditLimit = null;
            }
            if(isNewEntry == true)
            {
                database.Clients.Add(client);
            }
            database.SaveChanges();
            NavigationService.Navigate(new ViewAllClients(database));
        }

        private void creditCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            maxCredit.Visibility = Visibility.Visible;
        }
        private void creditCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            maxCredit.Visibility = Visibility.Hidden;

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ViewAllClients());
        }
    }
}
