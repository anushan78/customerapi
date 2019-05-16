using CustomerAPI.DbModels;
using System.Collections.Generic;

namespace CustomerAPI.Services
{
    /// <summary>
    /// Defines contract for data access and domain logic for manipulating customer data.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Return all customers.
        /// </summary>
        /// <returns>List of all customers</returns>
        IEnumerable<Customer> GetAll();

        /// <summary>
        /// Returns customer for the specified id.
        /// </summary>
        /// <param name="id">id of the customer to be retrieved</param>
        /// <returns>Customer for the specified id.</returns>
        Customer GetbyId(int id);

        /// <summary>
        /// Insert Customer.
        /// </summary>
        /// <param name="customer">Customer to be inserted</param>
        void Create(Customer customer);

        /// <summary>
        /// Updates custome in the repository
        /// </summary>
        /// <param name="customer">Customer object which populated with details to be updated</param>
        void Update(Customer customer);

        /// <summary>
        /// Removes customer.
        /// </summary>
        /// <param name="id">Id of the customer to be removed</param>
        void Delete(int id);

        /// <summary>
        /// Return the customer using the specified name parameter filtered with first name and last name combined.
        /// </summary>
        /// <param name="name">Name to be partially searched</param>
        /// <returns></returns>
        IEnumerable<Customer> GetByName(string name);
    }
}