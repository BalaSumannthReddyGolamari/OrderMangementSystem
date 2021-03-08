using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderingSystem.Models;
using OrderingSystem.Controllers;
using System.Web.Http.Results;

namespace OrderingSystemUniTestCases
{
    [TestClass]
    public class TestOrderController
    {
        [TestMethod]
        public void GteOrder_ShouldReturnProductWithCustomerId()
        {
            var context = new TestStoreAppContext();

            context.order.Add(GetDemoOrder());

            var controller = new OrderController(context);

            var result = controller.GetOrder(3) as OkNegotiatedContentResult<Order>;

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Content.Customer.customerId);
        }

        private Order GetDemoOrder()
        {
            return new Order() { OrderId = 2, CustomerId = 3, productId = 1, quantity = 1, amount = 10000 };
        }
    }
}
