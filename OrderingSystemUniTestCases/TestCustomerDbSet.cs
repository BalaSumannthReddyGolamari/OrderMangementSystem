using OrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystemUniTestCases
{
    class TestCustomerDbSet : TestContext<Customer>
    {
        public override Customer Find(params object[] keyValues)
        {
            return this.SingleOrDefault(cust => cust.customerId == (int)keyValues.Single());
        }
    }
}
