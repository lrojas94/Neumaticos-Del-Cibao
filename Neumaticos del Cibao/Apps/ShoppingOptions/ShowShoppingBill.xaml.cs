using Microsoft.Reporting.WinForms;
using Neumaticos_del_Cibao.Apps.Reports;
using Neumaticos_del_Cibao.Apps.Reports.DailyReportDataSetTableAdapters;
using Neumaticos_del_Cibao.Database;
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
    /// Interaction logic for ShowShoppingBill.xaml
    /// </summary>
    public partial class ShowShoppingBill : Page
    {
        Database.databaseEntities database;
        public ShowShoppingBill(Database.ShoppingBill shoppingBill)
        {
            InitializeComponent();
            InitializeComponent();
            database = new databaseEntities();
            reporter.Reset();
            var ta = new ShoppingBillTableAdapter();
            var source = ta.GetData().Where(row => row.BillId == shoppingBill.Id);

            var shoppingDataSource = new ReportDataSource("ShoppingBillArticles", source);
            reporter.LocalReport.DataSources.Add(shoppingDataSource);
            reporter.Name = "Factura de Compra";
            reporter.LocalReport.ReportEmbeddedResource = "Neumaticos_del_Cibao.Apps.Reports.ShoppingBillReport.rdlc";
            reporter.RefreshReport();
        }
    }
}
