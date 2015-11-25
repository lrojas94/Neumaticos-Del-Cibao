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

namespace Neumaticos_del_Cibao.Apps.Articles
{
    /// <summary>
    /// Interaction logic for ViewAllArticles.xaml
    /// </summary>
    public partial class ViewAllArticles : Page
    {
        public Database.Article selectedArticle;
        private Database.databaseEntities database;
        private TimedFunction searchFuntion;

        public ViewAllArticles(Database.databaseEntities database = null)
        {
            InitializeComponent();
            articlesListBox.ItemsSource = database.Articles.ToList();

            this.database = database;
            if(this.database == null)
            {
                this.database = new Database.databaseEntities();
            }

            Action search = () =>
            {
                articlesListBox.ItemsSource = database.ArticleSearchByName(searchBox.RealText);
            };

            searchFuntion = new Common.TimedFunction(search);


        }

        private void btnAddArticle_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddArticle(database));
        }


        private void btnModifyArticle_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddArticle(database, selectedArticle));
        }

        private void btnDeleteArticle_Click(object sender, RoutedEventArgs e)
        {
            database.Articles.Remove(selectedArticle);
            database.SaveChangesAsync();
            articlesListBox.ItemsSource = database.ArticleSearchByName(searchBox.RealText);
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnDeleteArticle.IsEnabled = true;
            btnModifyArticle.IsEnabled = true;
            selectedArticle = (sender as ListBox).SelectedItem as Database.Article;
        }

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchFuntion.Run();
        }
    }
}
