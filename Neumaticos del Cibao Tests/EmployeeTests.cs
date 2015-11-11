using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neumaticos_del_Cibao.Database;
using Neumaticos_del_Cibao.Apps.Common;
using NUnit;
using FluentAssertions;
using NUnit.Framework;

namespace Neumaticos_del_Cibao_Tests
{
    [TestFixture]
    class EmployeeTests
    {
        [Test]
        [TestCase("A_Single_Weird_Password_123")]
        [TestCase("A Password With Spaces")]
        [TestCase("A")] //Single character password.
        public void SetPassword_PasswordHashedUsingEncryptionClass_ShouldBeTheSame(string password)
        {
            var employee = new Employee();
            var encoder = new Encryption();

            employee.SetPassword(password);

            employee.Password.ShouldBeEquivalentTo(encoder.EncryptSHA256(password));
        }

        [Test]
        [TestCase("A_Single_Weird_Password_123")]
        [TestCase("A Password With Spaces")]
        [TestCase("A")] //Single character password.
        public void Login_LoginWithSetPassword_ShouldReturnTrue(string password)
        {
            var employee = new Employee();
            employee.SetPassword(password);
            employee.Login(password).ShouldBeEquivalentTo(true);
        }

        [Test]
        public void Login_WithEmptyPassword_ShouldReturnFalse()
        {
            var employee = new Employee();

            //Set password as it needs to be set before comparing.
            employee.SetPassword("Password");

            employee.Login("").ShouldBeEquivalentTo(false);

        }

        [Test]
        public void Login_WithUnsetPassword_ShouldThrowException()
        {
            var employee = new Employee();

            Action a = () =>
            {
                employee.Login("");
            };

            a.ShouldThrow<InvalidOperationException>();
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void SetPassword_EmptyOrNullPassword_ShouldReturnException(string password)
        {
            var employee = new Employee();

            Action a = () =>
            {
                employee.SetPassword(password);
            };

            a.ShouldThrow<ArgumentException>();
        }
    }
}
