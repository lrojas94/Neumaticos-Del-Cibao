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

namespace Neumaticos_del_Cibao.CRUD_Clients
{
    /// <summary>
    /// Interaction logic for ViewAllClients.xaml
    /// </summary>
    public partial class ViewAllClients : Page
    {
        private Database.databaseEntities database = new Database.databaseEntities();
        private Database.Client selectedClient = null;
        private DispatcherTimer searchBoxTimer;
        public ViewAllClients()
        {
            InitializeComponent();

            searchBoxTimer = new DispatcherTimer();
            searchBoxTimer.Tick += searchBox_Tick;
            searchBoxTimer.Interval = new TimeSpan(0, 0, 0, 0, 50);
            clientsListBox.ItemsSource = database.Clients.ToList();
        }

        private void searchBox_Tick(object sender, object e)
        {
            clientsListBox.ItemsSource = database.ClientSearchByName(searchBox.RealText);
            searchBoxTimer.Stop();
        }
        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!searchBoxTimer.IsEnabled)
            {
                searchBoxTimer.Start();
            }
        }
    


    private void clientsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDeleteClient.IsEnabled = true;
            btnModifyClient.IsEnabled = true;
            btnViewClient.IsEnabled = true;
            selectedClient = (sender as ListBox).SelectedItem as Database.Client;
        }

        private void btnAddClient_Click(object sender, RoutedEventArgs e)
        {
            //abrir ventana anadir cliente
        }

        private void btnViewClient_Click(object sender, RoutedEventArgs e)
        {
            //abrir pestana ver cliente
        }

        private void btnModifyClient_Click(object sender, RoutedEventArgs e)
        {
            //abrir ventana modificar
        }

        private void btnDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            database.Clients.Remove(selectedClient);
            database.SaveChangesAsync();
            clientsListBox.ItemsSource = database.EmployeeSearchByName(searchBox.RealText);
        }
    }
}
