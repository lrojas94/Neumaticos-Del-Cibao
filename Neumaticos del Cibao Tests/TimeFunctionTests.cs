using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FluentAssertions;
using System.Timers;
using Neumaticos_del_Cibao.Apps.Common;

namespace Neumaticos_del_Cibao_Tests
{
    [TestFixture]
    public class TimeFunctionTests
    {
        [Test]
        public void TimedFunction_InitializedWithNullAction_ShouldThrowException()
        {
            Action action = () => { new TimedFunction(null); };
            action.ShouldThrow<ArgumentNullException>();
        }
    }
}
