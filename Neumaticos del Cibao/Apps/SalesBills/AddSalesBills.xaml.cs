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
using System.Collections.ObjectModel;

namespace Neumaticos_del_Cibao.Apps.SalesBills
{
    /// <summary>
    /// Interaction logic for AddSalesBills.xaml
    /// </summary>
    public partial class AddSalesBills : Page
    {
        private Database.SalesBill salesBill;
        private Database.databaseEntities database;
        private Database.Client selectedClient;
        private Boolean isNewEntry = true;
        
        

        public AddSalesBills(Database.SalesBill salesBill = null, Database.databaseEntities database = null)
        {
            InitializeComponent();

            this.salesBill = salesBill;
            if (this.salesBill == null)
            {
                isNewEntry = false;
                this.salesBill = new Database.SalesBill();
                this.salesBill.SalesBillArticles = new List<Database.SalesBillArticle>();
                this.salesBill.Client = new Database.Client();
            }

            this.database = database;
            if (this.database == null)
                this.database = new Database.databaseEntities();

            DataContext = this.salesBill;
            
            
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

        private void articlesDataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == articleIdTextColumn.Header.ToString())
            {
                
                var articleWindow = new Common.WindowsWithFrame();
                var articleFrame = new Articles.ViewAllArticles(database);

                //articleFrame.searchBox.Text = cell.Text.ToString().ToLower();

                articleWindow.frame.Content = articleFrame;
                articleWindow.ShowDialog();

                var selectedArticle = articleFrame.selectedArticle;
                var item = e.Row.Item as Database.SalesBillArticle;
                item.Article = selectedArticle;
            }
        }

    }
}
