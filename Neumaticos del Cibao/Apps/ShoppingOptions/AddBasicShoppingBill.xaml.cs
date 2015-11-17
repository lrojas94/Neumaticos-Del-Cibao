using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Neumaticos_del_Cibao.Apps.Common;
using System.Data.Entity.Validation;

namespace Neumaticos_del_Cibao.Apps.ShoppingOptions
{
    /// <summary>
    /// Interaction logic for BasicBills.xaml
    /// </summary>
    public partial class AddBasicShoppingBill : Page
    {
        private Database.ShoppingBill bill;
        private Database.Client selectedSupplier = null;
        private DataGridRow selectedRow = null;
        private bool isNewEntry = false;
        private Database.databaseEntities database;

        public AddBasicShoppingBill(Database.ShoppingBill bill = null)
        {
            InitializeComponent();
            
            this.bill = bill;

            if (this.bill == null)
            {
                this.bill = new Database.ShoppingBill();
                this.bill.ShoppingBillsArticles = new List<Database.ShoppingBillsArticle>();
                this.bill.Date = DateTime.Today.ToString();
                isNewEntry = true;
            }

            DataContext = this.bill;

        }

        public AddBasicShoppingBill(Database.databaseEntities context = null, Database.ShoppingBill bill = null) : this(bill)
        {

            database = context;
            if (context == null)
                database = new Database.databaseEntities();

            DataContext = this.bill;
        }

        private void searchSupplier_Click(object sender, RoutedEventArgs e)
        {
            var suppliersWindow = createWindowWithFrame();
            var suppliersFrame = new Clients.ViewAllClients(database);
            suppliersWindow.Item2.Content = suppliersFrame;
            suppliersFrame.searchBox.Text = supplier.RealText;

            MessageBox.Show("Puede seleccionar un suplidor dando doble click o, con este seleccionado, presionando enter.", "Mensaje de ayuda");

            suppliersWindow.Item1.ShowDialog();
            selectedSupplier = suppliersFrame.SelectedClient;
            if(selectedSupplier != null)
                supplier.Text = selectedSupplier.Name;

            bill.Client = selectedSupplier;

        }

        private Tuple<Window,Frame> createWindowWithFrame()
        {
            var window = new Window();
            var frame = new Frame();
            frame.Template = FindResource("BaseFrame") as ControlTemplate;
            window.Content = frame;
            return new Tuple<Window, Frame>(window, frame);
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

            if(e.Column.DisplayIndex == 0)
            {
                var articlesWindow = createWindowWithFrame();
                var articlesFrame = new Articles.ViewAllArticles(database);

                articlesFrame.searchBox.Text = cell.Text;
                articlesWindow.Item2.Content = articlesFrame;
                articlesWindow.Item1.ShowDialog();

                var selectedArticle = articlesFrame.SelectedArticle;

                var item = e.Row.Item as Database.ShoppingBillsArticle;
                item.Article = selectedArticle;
            }
            
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //Save the data
            
            try
            {
                if (isNewEntry)
                    database.ShoppingBills.Add(bill);
                database.SaveChangesAsync();
                NavigationService.Navigate(new ShowShoppingBills());
            }
            catch(DbEntityValidationException errors)
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
            finalPrice.Content = string.Format("Precio Final: RD$ {0:N2}", bill.FinalPrice);
            finalQuantity.Content = string.Format("Cantidad De Articulos: {0:N0}", bill.FinalQuantity);
        }
    }
}
