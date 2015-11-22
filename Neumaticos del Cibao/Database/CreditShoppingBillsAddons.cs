using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neumaticos_del_Cibao.Database
{
    public partial class CreditShoppingBill
    {
        public bool IsDonePaying
        {
            get
            {
                return DonePaying == "t";
            }
            set
            {
                if (value)
                    DonePaying = "t";
                else
                    DonePaying = "f";
            }
        }

        public void Pay(double ammount,databaseEntities context = null)
        {
            if (context == null)
                context = new databaseEntities();

            //Create a register:
            this.Owed -= ammount;
            
            var register = new CreditShoppingBillsRegister();
            register.Payed = ammount;
            register.CreditShoppingBill = this;
            register.Owed = this.Owed;
            register.Date = DateTime.Today.ToString();

            this.Payed += ammount;
            
            if(Owed == 0D)
                this.IsDonePaying = true;

            context.CreditShoppingBillsRegisters.Add(register);
            context.SaveChangesAsync();
        }
    }
}
