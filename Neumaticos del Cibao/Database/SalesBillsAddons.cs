using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neumaticos_del_Cibao.Database
{
    [ImplementPropertyChanged]
    public partial class SalesBill
    {
        [DependsOn("SalesBillsArticles")]
        public double FinalPrice
        {
            get
            {
                var sum = 0D;
                foreach (var purchased in SalesBillArticles)
                    sum += purchased.TotalPrice;
                return sum;
            }
        }

        [DependsOn("SalesBillsArticles")]
        public double FinalQuantity
        {
            get
            {
                var sum = 0D;
                foreach (var purchased in SalesBillArticles)
                    sum += purchased.Quantity;
                return sum;
            }
        }

        public string SalesBillTitle
        {
            get
            {
                return string.Format("Factura [{0:D7}] - Vendido a: {1} - {2}", Id, Client.Name, Convert.ToDateTime(Date).ToShortDateString());
            }
        }

        public string SalesBillSubtitle
        {
            get
            {
                return string.Format("Monto Total: RD$ {0:N2} | Articulos Vendidos: {1}", FinalPrice, FinalQuantity);
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
                {
                    if (CreditSalesBill.IsDonePaying)
                        return string.Format("Esta Factura a Credito esta Saldada.", CreditSalesBill.Owed);
                    else
                        return string.Format("Cuentas por cobrar: RD$ {0:N2}", CreditSalesBill.Owed);
                }
                else
                    return "No existen cuentas por cobrar.";
            }
        }
    }
}
