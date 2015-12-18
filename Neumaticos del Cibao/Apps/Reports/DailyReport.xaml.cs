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
using System.Data;

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
            var salesDataTable = dataset.SalesInformation;
            var shoppingDataTable = dataset.ShoppingInformation;
            var date = DateTime.Today;
            var from = date.AddDays(-5);

            salesDataTable.Rows.Add(getSalesData());
            shoppingDataTable.Rows.Add(getShoppingData());

            var shoppingDataSource = new ReportDataSource("ShoppingInformation", shoppingDataTable as DataTable);
            var salesDataSource = new ReportDataSource("SalesInformation", salesDataTable as DataTable);

            reporter.LocalReport.DataSources.Add(shoppingDataSource);
            reporter.LocalReport.DataSources.Add(salesDataSource);
            reporter.Name = "Reporte Diario";
            reporter.LocalReport.ReportEmbeddedResource = "Neumaticos_del_Cibao.Apps.Reports.DailyReport.rdlc";
            reporter.RefreshReport();
        }

        private System.Data.DataRow getShoppingData(string date = null)
        {
            // Get day:
            date = date == null ? DateTime.Today.ToString() : date;
            //Get all PAYED bills from today:
            var nonCreditBills = (from bill in database.ShoppingBills
                                  where bill.Date == date && bill.Credit == "f"
                                  select bill).Sum(bill => 
                                       bill.ShoppingBillsArticles.Sum(
                                           a => (double?)a.ArticlePrice * (double?)a.ArticleCount
                                       )
                                   );
            nonCreditBills = nonCreditBills == null ? 0 : nonCreditBills;
            //Get total payment on credit bills:
            var creditBills = (from bill in database.ShoppingBills
                               where bill.Date == date && bill.Credit == "t"
                               select bill).Sum(
                                bill =>
                                    bill
                                    .CreditShoppingBill
                                    .CreditShoppingBillsRegisters
                                    .Where(register => register.Date == date)
                                    .Sum(register => register.Payed)
                                );
            creditBills = creditBills == null ? 0 : creditBills;

            var totalPayedToday = nonCreditBills + creditBills;

            var dataTable = dataset.ShoppingInformation;
            var row = dataTable.NewRow();

            row["Date"] = date;
            row["CreditBillsPayment"] = creditBills;
            row["BillsPayment"] = nonCreditBills;
            row["TotalPayment"] = totalPayedToday;
            
            return row;

        }

        private System.Data.DataRow getSalesData(string date = null)
        {
            // Get day:
            date = date == null ? DateTime.Today.ToString() : date;

            //Get all PAYED bills from today:
            var nonCreditBills = (from bill in database.SalesBills
                                  where bill.Date == date && bill.Credit == "f"
                                  select bill).Sum(bill =>
                                       bill.SalesBillArticles.Sum(
                                           a => (double?)a.ArticlePrice * a.Quantity
                                       )
                                   );
            nonCreditBills = nonCreditBills == null ? 0 : nonCreditBills;
            //Get total payment on credit bills:
            var creditBills = (from bill in database.SalesBills
                               where bill.Date == date && bill.Credit == "t"
                               select bill).Sum(
                                bill =>
                                    bill
                                    .CreditSalesBill
                                    .CreditSalesBillsRegisters
                                    .Where(register => register.Date == date)
                                    .Sum(register => register.Payed)
                                );
            creditBills = creditBills == null ? 0 : creditBills;

            var totalPayedToday = nonCreditBills + creditBills;

            var dataTable = dataset.SalesInformation;
            var row = dataTable.NewRow();

            row["Date"] = date;
            row["CreditBillsPayment"] = creditBills;
            row["BillsPayment"] = nonCreditBills;
            row["TotalPayment"] = totalPayedToday;

            return row;

        }
    }
}
