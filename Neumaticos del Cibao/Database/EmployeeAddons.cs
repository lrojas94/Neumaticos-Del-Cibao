using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neumaticos_del_Cibao.Database
{
    public partial class Employee
    {
        public override string ToString()
        {
            return String.Format("{0} - ({1} {2})", Username, Person.Name, Person.LastName);
        }

        public string FullName
        {
            get
            {
                return Person.Name + " " + Person.LastName;
            }
        }

        public string UsernameInfo
        {
            get
            {
                return String.Format("Nombre de Usuario: {0}",Username);
            }
        }
    }
}
