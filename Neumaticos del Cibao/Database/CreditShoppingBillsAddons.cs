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
            if (ShoppingBill == null)
                throw new ArgumentNullException("Base bill is null. This credit bill should not exist.");

            if (IsDonePaying)
                throw new InvalidOperationException("This bill is already payed for.");

            if (!ShoppingBill.IsCredit)
                throw new InvalidOperationException("This is not a credit bill.");

            //Create a register:
            this.Owed -= ammount;

            var register = new CreditShoppingBillsRegister();
            register.Payed = ammount;
            register.CreditShoppingBill = this;
            register.Owed = this.Owed;
            register.Date = DateTime.Today.ToString();

            this.Payed += ammount;
            this.Payed = Math.Round(Payed.GetValueOrDefault(), 3);
            this.Owed = Math.Round(Owed.GetValueOrDefault(), 3);

            if(Owed == 0D)
                this.IsDonePaying = true;
            if(context != null)
                context.CreditShoppingBillsRegisters.Add(register);
            
        }
    }
}
