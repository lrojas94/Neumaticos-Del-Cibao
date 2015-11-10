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
    /// Interaction logic for AddArticle.xaml
    /// </summary>
    public partial class AddArticle : Page
    {
        private Database.Article article;
        private bool isNewEntry = false;
        private Database.databaseEntities database;
        public AddArticle(Database.databaseEntities context = null, Database.Article article = null)
        {
            InitializeComponent();
            database = context;

            if (context == null)
            {
                database = new Database.databaseEntities();
            }

            bindArticle(article);

        }

        private void bindArticle(Database.Article article)
        {
            if (article == null)
            {
                isNewEntry = true;
                this.article = new Database.Article();
            }
            else
            {
                this.article = article;
            }
            DataContext = this.article;

        }

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            if (isNewEntry == true)
            {
                database.Articles.Add(article);
            }
            database.SaveChangesAsync();
            //NavigationService.Navigate(new ViewAllArticles());
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new ViewAllArticles());
        }
    }
}
