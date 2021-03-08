using OrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystemUniTestCases
{
    class TestOrderDbSet : TestContext<Order>
    {
        public override Order Find(params object[] keyValues)
        {
            return this.SingleOrDefault(ord => ord.Customer.customerId == (int)keyValues.Single());
        }
    }
}
