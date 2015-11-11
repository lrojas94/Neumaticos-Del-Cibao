using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neumaticos_del_Cibao.Database
{
    public partial class Article
    {
        public string Information
        {
            get
            {
                return String.Format("{0} - ({1} {2}) - {3}", Id, Measure, MeasureUnit, Description);
            }
        }


    }
}

