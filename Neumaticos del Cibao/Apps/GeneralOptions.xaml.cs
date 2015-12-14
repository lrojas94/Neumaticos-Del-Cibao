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

namespace Neumaticos_del_Cibao.Apps
{
    /// <summary>
    /// Interaction logic for GeneralOptios.xaml
    /// </summary>
    public partial class GeneralOptions : Page
    {
        private Database.databaseEntities database;
        
        public GeneralOptions(Database.Employee loggedEmployee,Database.databaseEntities context = null)
        {
            InitializeComponent();
            database = context;
            if(database == null)
                database = new Database.databaseEntities();
            var options = database.EmployeesOptions(loggedEmployee, "General");
            grid.ParentPage = this;
            grid.BuildOptionGrid(options);
        }
    }
}
