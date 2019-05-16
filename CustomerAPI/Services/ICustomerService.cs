using System.Collections.Generic;
using CustomerAPI.DbModels;

namespace CustomerAPI.Services
{

    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        Customer GetbyId(int id);
        void Create(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
        IEnumerable<Customer> GetByName(string name);
    }
}