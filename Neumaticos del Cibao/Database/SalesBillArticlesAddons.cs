using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neumaticos_del_Cibao.Database
{
    [ImplementPropertyChanged]
    public partial class SalesBillArticle
    {
        public SalesBillArticle()
        {
            ArticlePrice = 0D;
            Quantity = 0;
        }

        [DependsOn("ArticleCount", "ArticlePrice")]
        public double TotalPrice
        {
            get
            {
                return Quantity * ArticlePrice;
            }
        }
    }
}
