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

namespace Neumaticos_del_Cibao.Apps.ShoppingOptions
{
    /// <summary>
    /// Interaction logic for BasicBills.xaml
    /// </summary>
    public partial class BasicBills : Page
    {
        private Database.Client selectedSupplier = null;
        private DataGridRow selectedRow = null;

        public BasicBills()
        {
            InitializeComponent();
            date.RealText = DateTime.Today.ToString();

            var test = new Database.ShoppingBillsArticle();
            test.Article = new Database.Article();
            var list = new List<Database.ShoppingBillsArticle>();
            articles.ItemsSource = list;
        }

        private void searchSupplier_Click(object sender, RoutedEventArgs e)
        {
            var suppliersWindow = createWindowWithFrame();
            var suppliersFrame = new Clients.ViewAllClients();
            suppliersWindow.Item2.Content = suppliersFrame;
            suppliersFrame.searchBox.Text = supplier.RealText;

            MessageBox.Show("Puede seleccionar un suplidor dando doble click o, con este seleccionado, presionando enter.", "Mensaje de ayuda");

            suppliersWindow.Item1.ShowDialog();
            selectedSupplier = suppliersFrame.SelectedClient;
            if(selectedSupplier != null)
                supplier.Text = selectedSupplier.Name;

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
                //var articlesFrame = new Apps.Articles.AllArticles();
                //var selectedArticle = articlesFrame.SelectedArticle;
                articlesWindow.Item1.ShowDialog();

            }
        }
    }
}
