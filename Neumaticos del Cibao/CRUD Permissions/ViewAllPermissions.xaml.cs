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

namespace Neumaticos_del_Cibao.CRUD_Permissions
{
    /// <summary>
    /// Interaction logic for ViewAllPermissions.xaml
    /// </summary>
    public partial class ViewAllPermissions : Page
    {

        public ViewAllPermissions()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Content = new ViewPermission();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //Content = new ViewPermission(Name, Description);
        }
    }
}
