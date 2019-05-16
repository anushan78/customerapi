using CustomerAPI.DbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CustomerAPI.Services
{
    /// <summary>
    /// Defines data access and domain logic for manipulating customer data.
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly CustomerContext _customerContext;

        /// <summary>
        /// CustomerService Constructor
        /// </summary>
        /// <param name="context">Customer context used to persist customer data into the undelying repository</param>
        public CustomerService(CustomerContext context)
        {
            _customerContext = context;
        }

        /// <summary>
        /// Insert Customer.
        /// </summary>
        /// <param name="customer">Customer to be inserted</param>
        public void Create(Customer customer)
        {
            _customerContext.Customers.Add(customer);
            _customerContext.SaveChanges();
        }

        /// <summary>
        /// Removes customer.
        /// </summary>
        /// <param name="id">Id of the customer to be removed</param>
        public void Delete(int id)
        {
            _customerContext.Customers.Remove(GetbyId(id));
            _customerContext.SaveChanges();
        }

        /// <summary>
        /// Return all customers.
        /// </summary>
        /// <returns>List of all customers</returns>
        public IEnumerable<Customer> GetAll()
        {
            return _customerContext.Customers.ToList();
        }

        /// <summary>
        /// Returns customer for the specified id.
        /// </summary>
        /// <param name="id">id of the customer to be retrieved</param>
        /// <returns>Customer for the specified id.</returns>
        public Customer GetbyId(int id)
        {
            return _customerContext.Customers
                .Where(customerItem => customerItem.Id == id)
                .FirstOrDefault();
        }

        /// <summary>
        /// Return the customer using the specified name parameter filtered with first name and last name combined.
        /// </summary>
        /// <param name="name">Name to be partially searched</param>
        /// <returns></returns>
        public IEnumerable<Customer> GetByName(string name)
        {
            return _customerContext.Customers
                .Where(customerItem => $"{customerItem.FirstName} {customerItem.LastName}".IndexOf(name) >= 0)
                .ToList();
        }

        /// <summary>
        /// Updates custome in the repository
        /// </summary>
        /// <param name="customer">Customer object which populated with details to be updated</param>
        public void Update(Customer customer)
        {
            _customerContext.Entry(customer).State = EntityState.Modified;
            _customerContext.SaveChanges();
        }
    }
}
