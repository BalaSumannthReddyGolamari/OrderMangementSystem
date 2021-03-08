using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderingSystem.Controllers;
using System.Web.Http;
using OrderingSystem.Models;
using System.Web.Http.Results;
using System.Collections.Generic;

namespace OrderingSystemUniTestCases
{
    [TestClass]
    public class TestCustomerController
    {
      
        [TestMethod]
        public void GetCustomer_ShouldReturnProductWithSameID()
        {
            var context = new TestStoreAppContext();

            context.Customer.Add(GetDemoCUstomer());

            var controller = new CustomerController(context);

            var result = controller.GetCustomer(1) as OkNegotiatedContentResult<Customer>;

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Content.customerId);
        }

        private Customer GetDemoCUstomer()
        {
            return new Customer() { customerId = 1, customerName = "Bala", EmailId = "Bala@gmail.com" };
        }

        private Customer PostDemoCustomer()
        {
            return new Customer() { customerName = "Bala", EmailId = "Bala123@gmail.com" };
        }


        [TestMethod]
        public void GetCustomerTest()
        {
            var context = new TestStoreAppContext();
            context.Customer.Add(new OrderingSystem.Models.Customer { customerId = 1, customerName = "Bala", EmailId = "bala@gmail.com" });
            context.Customer.Add(new OrderingSystem.Models.Customer { customerId = 2, customerName = "Hemanth", EmailId = "Hemanth@gmail.com" });
            context.Customer.Add(new OrderingSystem.Models.Customer { customerId = 3, customerName = "Rana", EmailId = "rana@gmail.com" });
            context.Customer.Add(new OrderingSystem.Models.Customer { customerId = 4, customerName = "Rahul", EmailId = "Rahul@gmail.com" });

            var controller = new CustomerController(context);

            var result = controller.GetCustomer() as OkNegotiatedContentResult<IEnumerable<Customer>>;
            //as TestCustomerDbSet;

            //.GetProducts() as TestProductDbSet;

            Assert.IsNotNull(result);
            Assert.AreEqual(context, result.Content);
        }

        [TestMethod]
        public void PostCustomer_ShouldReturnSameCustomer()
        {
            var controller = new CustomerController(new TestStoreAppContext());

            var item = PostDemoCustomer();

            var result = controller.Post(item) as CreatedAtRouteNegotiatedContentResult<Customer>;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteName, "DefaultApi");
            Assert.AreEqual(result.RouteValues["id"], result.Content.customerId);
            Assert.AreEqual(result, "Customer added");
        }
    }
}
