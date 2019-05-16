using Microsoft.EntityFrameworkCore;
using CustomerAPI.DbModels;

namespace CustomerAPI
{
    /// <summary>
    /// Defines CustomerContext object which can be used to query and update customer instances in the repository
    /// </summary>
    public class CustomerContext : DbContext
    {
        /// <summary>
        /// Parameterized Constructor which can be used to specify dbcontextoptions
        /// </summary>
        /// <param name="options">DbContextOptions cor customercontext</param>
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }

        /// <summary>
        /// List of customer objects which will be used to query and update customer details in the entities.
        /// </summary>
        public DbSet<Customer> Customers { get; set; }
    }
}