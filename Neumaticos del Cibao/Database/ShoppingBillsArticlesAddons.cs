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
    public partial class ShoppingBillsArticle
    {
        public ShoppingBillsArticle()
        {
            ArticlePrice = 0D;
            ArticleCount = 0D;
        }

        [DependsOn("ArticleCount","ArticlePrice")]
        public double TotalPrice
        {
            get
            {
                return ArticleCount * ArticlePrice;
            }
        }
    }
}
