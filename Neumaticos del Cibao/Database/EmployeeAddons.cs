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
            return string.Format("{0} - ({1} {2})", Username, Person.Name, Person.LastName);
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
                return string.Format("Nombre de Usuario: {0}",Username);
            }
        }

        public void SetPassword(string password,Apps.Common.Encryption encoder = null)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty");

            if (encoder == null)
                encoder = new Apps.Common.Encryption();

            Password = encoder.EncryptSHA256(password);
        }

        public bool Login(string password, Apps.Common.Encryption encoder = null)
        {
            if (string.IsNullOrEmpty(Password))
                throw new InvalidOperationException("Password has not been set.");

            if (encoder == null)
                encoder = new Apps.Common.Encryption();
            password = encoder.EncryptSHA256(password);

            return password == Password;
        }
    }
}
