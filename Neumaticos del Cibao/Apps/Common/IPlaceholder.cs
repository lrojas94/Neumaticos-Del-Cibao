using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neumaticos_del_Cibao.Apps.Common
{
    interface IPlaceholder
    {
        string PlaceholderText
        {
            get;
            set;
        }
        string RealText
        {
            get;
            set;
        }
    }
}
