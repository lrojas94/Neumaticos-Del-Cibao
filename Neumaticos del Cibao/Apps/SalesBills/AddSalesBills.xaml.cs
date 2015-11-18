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
using Microsoft.Win32;


namespace Neumaticos_del_Cibao.Apps.SalesBills
{
    /// <summary>
    /// Interaction logic for AddSalesBills.xaml
    /// </summary>
    public partial class AddSalesBills : Page
    {
        Database.databaseEntities database = new Database.databaseEntities();
        Database.Client selectedClient;
        
        
        public AddSalesBills()
        {
            InitializeComponent();

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //abrir ventana de seleccion de cliente
            var clientWindow = new Common.WindowsWithFrame();
            var clientFrame = new Clients.ViewAllClients(database);


            clientWindow.frame.Content = clientFrame;
            clientFrame.searchBox.Text = clientName.Text.ToString().ToLower();
            
            clientWindow.ShowDialog();
            selectedClient = clientFrame.SelectedClient;

            if (selectedClient != null)
                clientName.Text = selectedClient.Name;

            
        }


    }
}
