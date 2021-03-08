using OrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystemUniTestCases
{
    public class TestStoreAppContext : IOrderRepository
    {
        public TestStoreAppContext()
        {
            this.Customer = new TestCustomerDbSet();
            this.order = new TestOrderDbSet();
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Order> order { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Product item) { }
        public void Dispose() { }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void AddCustomer(Customer customer)
        {
            Customer.Add(customer);
            SaveChanges();
        }

        public IEnumerable<Customer> GetAllCustomer()
        {
            return Customer.ToList();

        }

        public Customer GetCustomer(int id)
        {
            return Customer.FirstOrDefault(a => a.customerId == id);
        }

        public bool checkEmailId(string emailId)
        {
            if (Customer.Any(x => x.EmailId == emailId))
            {
                return false;
            }
            return false;
        }

        public IEnumerable<Product> GetAllProduct()
        {
            throw new NotImplementedException();
        }

        public Product GetProduct(string Name)
        {
            throw new NotImplementedException();
        }

        public void AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAllOrder()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrderByCust(int id)
        {
            return order.Where(a => a.Customer.customerId == id).ToList();
        }
    }
}
