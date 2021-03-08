using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Models
{
    public interface IOrderRepository
    {
        // General 
        Task<bool> SaveChangesAsync();

        // Customer
        void AddCustomer(Customer customer);
        IEnumerable<Customer> GetAllCustomer();
        Customer GetCustomer(int id);
        bool checkEmailId(string emailId);
        int SaveChanges();


        //Product
        IEnumerable<Product> GetAllProduct();
        Product GetProduct(string Name);

        //Order
        void AddOrder(Order order);
        IEnumerable<Order> GetAllOrder();
        List<Order> GetOrderByCust(int id);

    }
}
