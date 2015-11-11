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

namespace Neumaticos_del_Cibao.Apps.Articles
{
    /// <summary>
    /// Interaction logic for ViewAllArticles.xaml
    /// </summary>
    public partial class ViewAllArticles : Page
    {
        private Database.Article selectedArticle;
        private Database.databaseEntities database = new Database.databaseEntities();

        public ViewAllArticles()
        {
            InitializeComponent();
            articlesListBox.ItemsSource = database.Articles.ToList();
        }

        private void btnAddArticle_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddArticle(database));
        }

        private void btnViewArticle_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new ShowArticle(selectedArticle));
        }

        private void btnModifyArticle_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddArticle(database, selectedArticle));
        }

        private void btnDeleteArticle_Click(object sender, RoutedEventArgs e)
        {
            database.Articles.Remove(selectedArticle);
            database.SaveChangesAsync();
            articlesListBox.ItemsSource = database.ClientSearchByName(searchBox.RealText);
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDeleteArticle.IsEnabled = true;
            btnModifyArticle.IsEnabled = true;
            btnViewArticle.IsEnabled = true;
            selectedArticle = (sender as ListBox).SelectedItem as Database.Article;
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
