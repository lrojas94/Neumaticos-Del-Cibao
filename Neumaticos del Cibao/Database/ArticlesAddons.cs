using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neumaticos_del_Cibao.Database
{
    public partial class Article
    {
        public string ArticleTemplateSize
        {
            get
            {
                return String.Format("Medida: {0} {1}",  Measure, MeasureUnit);
            }
        }

        public string ArticleTemplateTitle
        {
            get
            {
                return string.Format("[{0}] - {1}",CodeIdentifier,Name);
            }
        }


    }
}

