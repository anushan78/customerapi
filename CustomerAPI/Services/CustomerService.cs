using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAPI.DbModels;
using Microsoft.EntityFrameworkCore;

namespace CustomerAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerContext _customerContext;

        public CustomerService(CustomerContext context)
        {
            _customerContext = context;
        }

        public void Create(Customer customer)
        {
            _customerContext.Customers.Add(customer);
            _customerContext.SaveChanges();
        }

        public void Delete(int id)
        {
            _customerContext.Customers.Remove(GetbyId(id));
            _customerContext.SaveChanges();
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customerContext.Customers.ToList();
        }

        public Customer GetbyId(int id)
        {
            return _customerContext.Customers
                .Where(customerItem => customerItem.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Customer> GetByName(string name)
        {
            return _customerContext.Customers
                .Where(customerItem => $"{customerItem.FirstName} {customerItem.LastName}".IndexOf(name) >= 0)
                .ToList();
        }

        public void Update(Customer customer)
        {
            _customerContext.Entry(customer).State = EntityState.Modified;
            _customerContext.SaveChanges();
        }
    }
}
