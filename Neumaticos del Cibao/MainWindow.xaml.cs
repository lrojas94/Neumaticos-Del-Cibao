using Neumaticos_del_Cibao.CRUD_Permissions;
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

namespace Neumaticos_del_Cibao
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            shoppingFrame.Content = new Apps.ShoppingOptions.ShoppingOptions();
            salesFrame.Content = new Apps.SalesOptions.SalesOptions();
            employeesFrame.Content = new Apps.Employees.ListEmployee();
            //clientsFrame.Content = new Clients.ViewAllClients();
            clientsFrame.Content = new Apps.SalesBills.AddSalesBills();

        }


    }
}
