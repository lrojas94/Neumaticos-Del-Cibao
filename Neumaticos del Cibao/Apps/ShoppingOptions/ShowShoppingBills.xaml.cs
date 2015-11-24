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

namespace Neumaticos_del_Cibao.Apps.ShoppingOptions
{
    /// <summary>
    /// Interaction logic for ShowShoppingBills.xaml
    /// </summary>
    public partial class ShowShoppingBills : Page
    {
        private Database.ShoppingBill selectedBill;
        private Database.databaseEntities database;
        private TimedFunction searchFunction;

        public bool SelectCreditBillsOnly { get; set; }
        public Database.ShoppingBill SelectedBill
        {
            get
            {
                return selectedBill;
            }
        }

        public ShowShoppingBills(Database.databaseEntities context = null)
        {
            InitializeComponent();

            database = context;
            if (database == null)
                database = new Database.databaseEntities();

            billsListBox.ItemsSource = database.ShoppingBills.ToList();

            Action search = () =>
            {
                billsListBox.ItemsSource = database.ShoppingBillSearch(searchBox.RealText);
            };

            searchFunction = new TimedFunction(search);

        }

        public ShowShoppingBills() : this(null) { }

        private void billsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            selectedBill = (sender as ListBox).SelectedItem as Database.ShoppingBill;

            if (ExtensionMethods.IsModal(Application.Current.MainWindow))
            {
                if (SelectCreditBillsOnly && selectedBill.IsCredit && !selectedBill.CreditShoppingBill.IsDonePaying)
                    Application.Current.MainWindow.Close();
                else
                {
                    MessageBox.Show("Usted no puede seleccionar una factura que no sea a credito o ya este saldada.", "Error en seleccion");
                    selectedBill = null;
                    return;
                }
            }

            btnDeleteBill.IsEnabled = true;
            btnModifyBill.IsEnabled = true;
            btnViewBill.IsEnabled = true;
        }

        private void btnAddBill_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddBasicShoppingBill(database));
        }

        private void btnModifyBill_Click(object sender, RoutedEventArgs e)
        {
            selectedBill.ShoppingBillsArticles = selectedBill.ShoppingBillsArticles.ToList();
            NavigationService.Navigate(new AddBasicShoppingBill(database,selectedBill));
        }

        private void btnDeleteBill_Click(object sender, RoutedEventArgs e)
        {
            database.ShoppingBills.Remove(selectedBill);
            selectedBill = null;
            btnDeleteBill.IsEnabled = false;
            btnModifyBill.IsEnabled = false;
            btnViewBill.IsEnabled = false;
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchFunction.Run();
        }
    }
}
