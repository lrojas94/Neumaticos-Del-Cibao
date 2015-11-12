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
    /// Interaction logic for ShowArticle.xaml
    /// </summary>
    public partial class ShowArticle : Page
    {
        Database.Article article;

        public ShowArticle(Database.Article article)
        {
            InitializeComponent();
            this.article = article;
            DataContext = this.article;
        }

        private void btnModifyArticle_Click(object sender, RoutedEventArgs e)
        {
            Database.databaseEntities db = null;
            NavigationService.Navigate(new AddArticle(db, article));

        }
    }
}
