using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
namespace Neumaticos_del_Cibao.Database
{
    [ImplementPropertyChanged]
    public partial class ShoppingBill
    {   
        [DependsOn("ShoppingBillsArticles")]
        public double FinalPrice
        {
            get
            {
                var sum = 0D;
                foreach (var purchased in ShoppingBillsArticles)
                    sum += purchased.TotalPrice;
                return sum;
            }
        }

        [DependsOn("ShoppingBillsArticles")]
        public double FinalQuantity
        {
            get
            {
                var sum = 0D;
                foreach (var purchased in ShoppingBillsArticles)
                    sum += purchased.ArticleCount;
                return sum;
            }
        }

        public string ShoppingBillTitle
        {
            get
            {
                return string.Format("Factura [{0:D7}] - Comprado a: {1} - {2}",Id,Client.Name,Convert.ToDateTime(Date).ToShortDateString());
            }
        }

        public string ShoppingBillSubtitle
        {
            get
            {
                return string.Format("Monto Total: RD$ {0:N2} | Articulos Comprados: {1}", FinalPrice, FinalQuantity);
            }
        }

        public bool IsCredit
        {
            get
            {
                return Credit == "t";
            }

            set
            {
                if (value)
                    Credit = "t";
                else
                    Credit = "f";
            }
        }

        public bool HasITBIS
        {
            get
            {
                return ITBIS == "t";
            }

            set
            {
                if (value)
                    ITBIS = "t";
                else
                    ITBIS = "f";
            }
        }

        public string Debt
        {
            get
            {
                if (IsCredit)
                    return string.Format("Cuentas por Pagar: RD${0}", CreditShoppingBill.Owed);
                else
                    return "No existen cuentas por pagar.";
            }
        }
    }
}
