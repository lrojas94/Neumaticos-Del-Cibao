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

namespace Neumaticos_del_Cibao.Apps.ShoppingOptions
{
    /// <summary>
    /// Interaction logic for ShoppingOptions.xaml
    /// </summary>
    ///

    public interface IBillDelegate
    {

    }
            
    public partial class ShoppingOptions : Page
    {
        Database.databaseEntities database;
        public ShoppingOptions()
        {
            InitializeComponent();
            database = new Database.databaseEntities();
            var options = database.Options.Where(o => o.OptionType == "Shopping").ToList();
            grid.ParentPage = this;
            grid.BuildOptionGrid(options);
        }
        
    }
}
