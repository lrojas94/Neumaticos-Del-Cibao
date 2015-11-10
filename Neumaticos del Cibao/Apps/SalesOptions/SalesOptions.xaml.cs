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

namespace Neumaticos_del_Cibao.Apps.SalesOptions
{
    /// <summary>
    /// Interaction logic for SalesOptions.xaml
    /// </summary>
    public partial class SalesOptions : Page
    {
        Database.databaseEntities database;
        public SalesOptions()
        {
            InitializeComponent();
            database = new Database.databaseEntities();
            var options = database.Options.Where(o => o.OptionType == "Sales").ToList();
            grid.ParentPage = this;
            grid.BuildOptionGrid(options);
        }
    }
}
