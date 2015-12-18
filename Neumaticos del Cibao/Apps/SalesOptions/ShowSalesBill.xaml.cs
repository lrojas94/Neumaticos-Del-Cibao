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

namespace Neumaticos_del_Cibao.Apps.SalesOptions
{
    /// <summary>
    /// Interaction logic for ShowShoppingBill.xaml
    /// </summary>
    public partial class ShowSalesBill : Page
    {
        Database.databaseEntities database;

        public ShowSalesBill(Database.SalesBill salesBill)
        {
            InitializeComponent();
            InitializeComponent();
            database = new databaseEntities();
            reporter.Reset();
            var ta = new SalesBillTableAdapter();
            var source = ta.GetData().Where(row => row.BillId == salesBill.Id);

            var salesDataSource = new ReportDataSource("SalesBillArticles", source);
            reporter.LocalReport.DataSources.Add(salesDataSource);
            reporter.Name = "Factura de Venta";
            reporter.LocalReport.ReportEmbeddedResource = "Neumaticos_del_Cibao.Apps.Reports.SalesBillReport.rdlc";
            reporter.RefreshReport();
        }
    }
}
