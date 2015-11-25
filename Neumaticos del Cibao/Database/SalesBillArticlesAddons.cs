using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neumaticos_del_Cibao.Database
{

    public partial class SalesBillArticle
    {
        public SalesBillArticle()
        {
            this.Quantity = 0;
            this.ArticlePrice = 0D;
        }

        public double TotalArticle
        {
            get
            {
                //return 500;
                //if (Quantity != 0 && ArticlePrice != 0)
                    return (Quantity * ArticlePrice);

            }
        }
    }
}
