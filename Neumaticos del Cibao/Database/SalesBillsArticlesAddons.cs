using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using PropertyChanged;

namespace Neumaticos_del_Cibao.Database
{
    [ImplementPropertyChanged]
    public partial class ShoppingBillArticle
    {
        public ShoppingBillArticle()
        {
            ArticlePrice = 0;
            Quantity = 0;
        }

        [DependsOn("ArticleCount", "ArticlePrice")]
        public double TotalPrice
        {
            get
            {
                return ArticleCount * ArticlePrice;
            }
        }
    }
}
