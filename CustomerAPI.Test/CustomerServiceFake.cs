using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomerAPI.DbModels;
using CustomerAPI.Services;

namespace CustomerAPI.Test
{
    public class CustomerServiceFake : ICustomerService
    {
        private readonly List<Customer> _customers;

        public CustomerServiceFake()
        {
            _customers = new List<Customer>()
            {
                new Customer() { Id = 1, FirstName = "Andy", LastName = "Ten", DateOfBirth = Convert.ToDateTime("01/03/1983") },
                new Customer() { Id = 2, FirstName = "Tina", LastName = "Lee", DateOfBirth = Convert.ToDateTime("01/05/1986") },
                new Customer() { Id = 3, FirstName = "Jake", LastName = "Wood", DateOfBirth = Convert.ToDateTime("10/04/1993") }
            };
        }

        public void Create(Customer customer)
        {
            _customers.Add(customer);
        }

        public void Delete(int id)
        {

            var existingCustomer = _customers.First(customerItem => customerItem.Id == id);
            _customers.Remove(existingCustomer); ;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customers;
        }

        public Customer GetbyId(int id)
        {
            return _customers.Where(customerItem => customerItem.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Customer> GetByName(string name)
        {
            return _customers
                .Where(customerItem => $"{customerItem.FirstName} {customerItem.LastName}".IndexOf(name) >= 0)
                .ToList();
        }

        public void Update(Customer customer)
        {
            var existingCustomer = _customers.Where(customerEntry => customerEntry.Id == customer.Id).FirstOrDefault();

            if (existingCustomer != null)
            {
                existingCustomer.FirstName = customer.FirstName;
                existingCustomer.LastName = customer.LastName;
                existingCustomer.DateOfBirth = customer.DateOfBirth;
            }
        }
    }
}
