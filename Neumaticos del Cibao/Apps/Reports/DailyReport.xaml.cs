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
using Neumaticos_del_Cibao.Database;
using Microsoft.Reporting.WinForms;
namespace Neumaticos_del_Cibao.Apps.Reports
{
    /// <summary>
    /// Interaction logic for DailyReport.xaml
    /// </summary>
    public partial class DailyReport : Page
    {

        Database.databaseEntities database;
        DailyReportDataSet dataset;
        public DailyReport()
        {
            InitializeComponent();
            database = new databaseEntities();
            dataset = new DailyReportDataSet();
            reporter.Reset();
            var shoppingTable = getShoppingData();
            var shoppingDataSource = new ReportDataSource("ShoppingInformation", shoppingTable);
            reporter.LocalReport.DataSources.Add(shoppingDataSource);
            reporter.Name = "Reporte Diario";
            reporter.LocalReport.ReportEmbeddedResource = "Neumaticos_del_Cibao.Apps.Reports.DailyReport.rdlc";
            reporter.RefreshReport();
        }

        private System.Data.DataTable getShoppingData()
        {
            // Get day:
            var today = DateTime.Today.ToString();
            //Get all PAYED bills from today:
            var nonCreditBills = (from bill in database.ShoppingBills
                                  where bill.Date == today && bill.Credit == "f"
                                  select bill).Sum(bill => 
                                       bill.ShoppingBillsArticles.Sum(
                                           a => a.ArticlePrice * a.ArticleCount
                                       )
                                   );
            //Get total payment on credit bills:
            var creditBills = (from bill in database.ShoppingBills
                               where bill.Date == today && bill.Credit == "t"
                               select bill).Sum(
                                bill =>
                                    bill
                                    .CreditShoppingBill
                                    .CreditShoppingBillsRegisters
                                    .Where(register => register.Date == today)
                                    .Sum(register => register.Payed)
                                );


            var totalPayedToday = nonCreditBills + creditBills;

            var dataTable = dataset.Tables["ShoppingInformation"];
            var row = dataTable.NewRow();

            row["Date"] = today;
            row["CreditBillsPayment"] = creditBills == null ? 0 : creditBills;
            row["BillsPayment"] = nonCreditBills;
            row["TotalPayment"] = totalPayedToday;

            dataTable.Rows.Add(row);

            return dataTable;

        }
    }
}
