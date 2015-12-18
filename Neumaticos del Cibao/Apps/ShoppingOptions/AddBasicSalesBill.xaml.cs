using Neumaticos_del_Cibao.Apps.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
    /// Interaction logic for AddBasicSalesBill.xaml
    /// </summary>
    public partial class AddBasicSalesBill : Page
    {
        private Database.SalesBill bill;
        private Database.Client selectedSupplier = null;
        private DataGridRow selectedRow = null;
        private bool isNewEntry = false;
        private Database.databaseEntities database;

        public AddBasicSalesBill(Database.SalesBill bill = null)
        {
            InitializeComponent();

            this.bill = bill;

            if (this.bill == null)
            {
                this.bill = new Database.SalesBill();
                this.bill.SalesBillArticles = new List<Database.SalesBillArticle>();
                this.bill.Date = DateTime.Today.ToString();
                isNewEntry = true;
            }
            else
            {
                
                //We can't Pay ahead again
                payedAhead.Visibility = Visibility.Hidden;
                creditBill.Visibility = Visibility.Hidden;
            }
            
            DataContext = this.bill;

        }

        public AddBasicSalesBill(Database.databaseEntities context = null, Database.SalesBill bill = null) : this(bill)
        {

            database = context;
            if (context == null)
                database = new Database.databaseEntities();

            DataContext = this.bill;
        }

        private void searchSupplier_Click(object sender, RoutedEventArgs e)
        {
            var suppliersWindow = ExtensionMethods.CreateWindowWithFrame();
            var suppliersFrame = new Clients.ViewAllClients(database);
            suppliersWindow.Item2.Content = suppliersFrame;
            suppliersFrame.searchBox.Text = supplier.RealText;

            MessageBox.Show("Puede seleccionar un suplidor dando doble click o, con este seleccionado, presionando enter.", "Mensaje de ayuda");

            suppliersWindow.Item1.ShowDialog();
            selectedSupplier = suppliersFrame.SelectedClient;
            if (selectedSupplier != null)
                supplier.Text = selectedSupplier.Name;

            bill.Client = selectedSupplier;

        }



        private void supplier_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                searchSupplier.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));

        }

        private void articles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRow = articles.SelectedItem as DataGridRow;
        }

        private void articles_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var cell = e.EditingElement as TextBox;

            if (e.Column.DisplayIndex == 0)
            {
                var articlesWindow = ExtensionMethods.CreateWindowWithFrame();
                var articlesFrame = new Articles.ViewAllArticles(database);

                articlesFrame.searchBox.Text = cell.Text;
                articlesWindow.Item2.Content = articlesFrame;
                articlesWindow.Item1.ShowDialog();

                var selectedArticle = articlesFrame.SelectedArticle;

                var item = e.Row.Item as Database.SalesBillArticle;
                item.Article = selectedArticle;
            }

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Save the data

            try
            {
                if (isNewEntry)
                    database.SalesBills.Add(bill);
                if (bill.IsCredit && bill.CreditSalesBill == null)
                {
                    //Do something
                    var creditBill = new Database.CreditSalesBill();
                    creditBill.SalesBill = bill;
                    creditBill.Owed = bill.FinalPrice;
                    creditBill.Payed = 0D;
                    var owed = 0D;

                    if (double.TryParse(payedAhead.RealValue, out owed))
                        creditBill.Pay(owed, database);
                }

                database.SaveChangesAsync();
                NavigationService.Navigate(new ShowSalesBills());
            }
            catch (DbEntityValidationException errors)
            {
                foreach (var eve in errors.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private void articles_LayoutUpdated(object sender, EventArgs e)
        {
            
            if (bill.HasITBIS)
            {
                var itbisPrice = bill.ITBISPercent / 100.00 * bill.FinalPrice;
                ITBISPrice.Content = string.Format("Valor de ITBIS: RD$ {0:N2}", itbisPrice);
            }
            finalPrice.Content = string.Format("Precio Final: RD$ {0:N2}", bill.FinalPrice);
            finalQuantity.Content = string.Format("Cantidad De Articulos: {0:N0}", bill.FinalQuantity);
        }
    }
}
