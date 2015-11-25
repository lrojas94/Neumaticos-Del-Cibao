using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Neumaticos_del_Cibao.Database;
using FluentAssertions;

namespace Neumaticos_del_Cibao_Tests
{
    [TestFixture]
    class CreditShoppingBillsTest
    {
        [Test]
        [TestCase(123324.1231231213D)]
        [TestCase(1.000000200000005D)]
        [TestCase(1.000000000000005D)]
        public void Pay_WithSameAmmountAsOwedMoney_ShouldSetIsDoneToTrue(double ammount)
        {
            var bill = new ShoppingBill();
            bill.CreditShoppingBill = new CreditShoppingBill();
            bill.IsCredit = true;
            bill.CreditShoppingBill.ShoppingBill = bill;
            bill.CreditShoppingBill.Owed = ammount;
            bill.CreditShoppingBill.Pay(ammount);
            bill.CreditShoppingBill.IsDonePaying.ShouldBeEquivalentTo(true);
        }

        [Test]
        public void Pay_WhenDone_PayingShouldThrowException()
        {
            var bill = new ShoppingBill();
            bill.CreditShoppingBill = new CreditShoppingBill();
            bill.CreditShoppingBill.ShoppingBill = bill;
            bill.CreditShoppingBill.Owed = 0D;
            bill.CreditShoppingBill.IsDonePaying = true;

            Action a = () => { bill.CreditShoppingBill.Pay(100D); };
            a.ShouldThrow<InvalidOperationException>("It makes no sense to pay a debt when you're done paying");
        }

        [Test]
        public void Pay_PayingSumWithElements_ShouldSetDoneToTrue()
        {
            var list = new List<double> { 1.0000000002D, 1.0221000009999D, 10.2002D, 129.99809D, 250000000000.00000009D};
            var sum = 0D;
            
            foreach(var num in list)
                sum += num;

            var bill = new ShoppingBill();
            bill.CreditShoppingBill = new CreditShoppingBill();
            bill.CreditShoppingBill.Owed = sum;
            bill.IsCredit = true;
            bill.CreditShoppingBill.ShoppingBill = bill;

            foreach (var num in list)
                bill.CreditShoppingBill.Pay(num);

            Console.Write(bill.CreditShoppingBill.Owed);

            bill.CreditShoppingBill.IsDonePaying.ShouldBeEquivalentTo(true);

        }

        [Test]
        public void Pay_WhenCreditBillsBillIsNull_ShouldReturnException()
        {
            var bill = new ShoppingBill();
            bill.CreditShoppingBill = new CreditShoppingBill();
            bill.CreditShoppingBill.Owed = 100.0D;
            bill.IsCredit = false;

            Action a = () => { bill.CreditShoppingBill.Pay(100.0D); };
            a.ShouldThrow<ArgumentNullException>("This Credit bill should belong to a bill.");

        }

        [Test]
        public void Pay_PayingNotCreditBill_ShouldReturnException()
        {
            var bill = new ShoppingBill();
            bill.CreditShoppingBill = new CreditShoppingBill();
            bill.CreditShoppingBill.Owed = 100.0D;
            bill.IsCredit = false;
            bill.CreditShoppingBill.ShoppingBill = bill;

            Action a = () => { bill.CreditShoppingBill.Pay(100.0D); };
            a.ShouldThrow<InvalidOperationException>("A non-credit bill is not to be payed. It is only payed once, when it was registered initially.");
        }
    }
}
