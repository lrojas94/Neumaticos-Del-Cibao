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
using Neumaticos_del_Cibao.Apps.Common;

namespace Neumaticos_del_Cibao.Apps.SalesOptions
{
    /// <summary>
    /// Interaction logic for PaySalesCreditBill.xaml
    /// </summary>
    public partial class PaySalesCreditBill : Page
    {
        private Database.databaseEntities database;

        private Database.SalesBill selectedBill;

        public Database.SalesBill SelectedBill
        { 
            get
            {
                return selectedBill;
            }
            set
            {
                selectedBill = value;
                DataContext = selectedBill;
            }
        }

        public PaySalesCreditBill(Database.databaseEntities context = null)
        {
            database = context;
            if (database == null)
                database = new Database.databaseEntities();

            DataContext = selectedBill;

            InitializeComponent();

        }

        public PaySalesCreditBill() : this(null) { }

        private void billNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
                selectBill();
        }

        private void selectBill()
        {
            //Select the bill here.
            var billWindow = ExtensionMethods.CreateWindowWithFrame();
            var billFrame = new ShowSalesBills(database);

            billFrame.SelectCreditBillsOnly = true;
            billFrame.searchBox.Text = billNumber.Text;

            billWindow.Item2.Content = billFrame;
            billWindow.Item1.ShowDialog();

            selectedBill = billFrame.SelectedBill;
            DataContext = selectedBill;
            btnPay.IsEnabled = true;
        }

        private void searchBill_Click(object sender, RoutedEventArgs e)
        {
            selectBill();
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            if (toPay.NumericValue <= 0D)
            {
                MessageBox.Show("No puede ingresar un valor menor o igual a $0.00 en el monto a pagar.");
                return;
            }

            //We can pay now o/
            MessageBoxResult willingToPay = MessageBoxResult.OK;
            if (toPay.NumericValue > selectedBill.CreditSalesBill.Owed)
                willingToPay = MessageBox.Show("Usted esta pagando mas de lo endeudado. Esta seguro que desea hacer esto?", "Aviso Importante", MessageBoxButton.YesNo);
            if (willingToPay == MessageBoxResult.OK || willingToPay == MessageBoxResult.Yes)
            {
                selectedBill.CreditSalesBill.Pay(toPay.NumericValue, database);
                database.SaveChangesAsync();
                NavigationService.GoBack();
            }
        }
    }
}
