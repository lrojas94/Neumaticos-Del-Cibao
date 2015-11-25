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

namespace Neumaticos_del_Cibao.Apps.ShoppingOptions
{
    /// <summary>
    /// Interaction logic for PayShoppingCreditBill.xaml
    /// </summary>
    public partial class PayShoppingCreditBill : Page
    {
        private Database.databaseEntities database;

        private Database.ShoppingBill selectedBill;

        public Database.ShoppingBill SelectedBill
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

        public PayShoppingCreditBill(Database.databaseEntities context = null)
        {
            database = context;
            if (database == null)
                database = new Database.databaseEntities();

            DataContext = selectedBill;

            InitializeComponent();

        }

        public PayShoppingCreditBill() : this(null) { }

        private void billNumber_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter || e.Key == Key.Tab)
                selectBill();
        }

        private void selectBill()
        {
            //Select the bill here.
            var billWindow = ExtensionMethods.CreateWindowWithFrame();
            var billFrame = new ShowShoppingBills(database);

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
            if(toPay.NumericValue <= 0D)
            {
                MessageBox.Show("No puede ingresar un valor menor o igual a $0.00 en el monto a pagar.");
                return;
            }

            //We can pay now o/
            MessageBoxResult willingToPay = MessageBoxResult.OK;
            if(toPay.NumericValue > selectedBill.CreditShoppingBill.Owed)
                willingToPay = MessageBox.Show("Usted esta pagando mas de lo endeudado. Esta seguro que desea hacer esto?", "Aviso Importante", MessageBoxButton.YesNo);
            if(willingToPay == MessageBoxResult.OK || willingToPay == MessageBoxResult.Yes)
            {
                selectedBill.CreditShoppingBill.Pay(toPay.NumericValue,database);
                database.SaveChangesAsync();
                NavigationService.GoBack();
            }
        }
    }
}
