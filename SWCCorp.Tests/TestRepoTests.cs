using NUnit.Framework;
using SWCCorp.BLL;
using SWCCorp.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Tests
{

    [TestFixture]
    public class TestRepoTests
    {
        [Test]
        public void CanLoadTestRepoData()
        {
            OrderManager manager = OrderManagerFactory.Create();

            OrderLookupResponse response = manager.LookupOrder("");

            Assert.IsNotNull(response.Orders);
            Assert.IsTrue(response.Success);
        }

    }

}
