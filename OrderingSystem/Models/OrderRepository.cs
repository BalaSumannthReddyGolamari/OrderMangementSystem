using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrderingSystem.Models
{
    public class OrderRepository : IOrderRepository
    {
        private OrderSystemEntities _entities;

        public OrderRepository(OrderSystemEntities entities)
        {
            _entities = entities;
        }

        public void AddCustomer(Customer customer)
        {
            _entities.Customers.Add(customer);
            _entities.SaveChanges();
        }

        public bool checkEmailId(string emailId)
        {
            if (_entities.Customers.Any(x => x.EmailId == emailId))
            {
                return true;
            }
            return false;
        }
        
        public IEnumerable<Customer> GetAllCustomer()
        {
            return _entities.Customers.AsEnumerable();
        }

        public Customer GetCustomer(int id)
        {
            return _entities.Customers.FirstOrDefault(a => a.customerId == id);
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return _entities.Products.AsEnumerable();
        }

        public Product GetProduct(string Name)
        {
            return _entities.Products.FirstOrDefault(a => a.productName== Name);
        }

        public int SaveChanges()
        {
            return _entities.SaveChanges();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _entities.SaveChangesAsync()) > 0;
        }

        public void AddOrder(Order order)
        {
            _entities.Orders.Add(order);

            _entities.SaveChanges();
        }

        public IEnumerable<Order> GetAllOrder()
        {
            return _entities.Orders.AsEnumerable();
        }

        public List<Order> GetOrderByCust(int id)
        {
            return _entities.Orders.Where(a => a.Customer.customerId == id).ToList();
        }

        //public Customer updateCustomer(Customer result)
        //{
        //    //result.customerName = _entities.Customers.Select(a => a.customerName);
        //    //var existingStudent = _entities.Customers.Where(s => s.customerName == result.customerName);

        //    //var result1 = _entities.Customers.FirstOrDefault(a => a.customerName == result.customerName && a.EmailId == result.EmailId);
        //    //_entities.Customers.Any(a =>a.customerId)
        //    //_entities.SaveChanges();

        //    //_entities.Customers.ElementAtOrDefault()
        //    //return result1;
        //}
    }
}