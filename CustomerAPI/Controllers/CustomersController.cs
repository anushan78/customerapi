using CustomerAPI.DbModels;
using CustomerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CustomerAPI.Controllers
{
    /// <summary>
    /// This defines API endpoints to manipulate customer details.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        /// <summary>
        ///  Constructor to setup customet context
        /// </summary>
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Lists all customers. 
        /// </summary>
        /// <returns>IEnumerable List of Customers.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAll()
        {
            var customers = _customerService.GetAll();
            return Ok(customers);
        }

        /// <summary>
        /// Returns customer for the specified id.
        /// </summary>
        /// <param name="id">The Customer's id</param>
        /// <returns>If exists the custome object; Response with 404 not found response code otherwise</returns>
        [HttpGet("{id:int}")]
        public ActionResult<Customer> GetbyId(int id)
        {
            var customer = _customerService.GetbyId(id);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        /// <summary>
        /// Inserts new customer entry.
        /// </summary>
        /// <param name="customer">The customer object populated with required properties</param>
        /// <returns>New customer which inserted</returns>
        [HttpPost]
        public ActionResult<Customer> Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _customerService.Create(customer);

            return CreatedAtAction(nameof(GetbyId), new { id = customer.Id }, customer);
        }

        /// <summary>
        /// Updates customer entry for the specified id.
        /// </summary>
        /// <param name="id">The id of the customer to be updated</param>
        /// <param name="customer">Customer entry populated with the required details to be updated</param>
        /// <returns>Response with status code 400- bad request for invalid object; Response with 204 no content status code otherwise.</returns>
        [HttpPut("{id:int}")]
        public ActionResult Update(int id, Customer customer)
        {
            if (!ModelState.IsValid || id != customer.Id)
            {
                return BadRequest();
            }

            _customerService.Update(customer);

            return NoContent();
        }

        /// <summary>
        /// Delete customer entry for the specified id.
        /// </summary>
        /// <param name="id">Customer's id to be removed</param>
        /// <returns>Response with status code 400- bad request for invalid object; Response with 204 no content status code otherwise.</returns>
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var customer = _customerService.GetbyId(id);

            if (customer == null)
            {
                return BadRequest();
            }

            _customerService.Delete(id);

            return NoContent();
        }

        /// <summary>
        /// Returns list of customers by matching with the full name (first name and last name combined) with the specified name parameter.
        /// </summary>
        /// <param name="name">Name use to find the customers by first name and last name.</param>
        /// <returns>400 badrequest response or 404 notfonund response or 200 ok response.</returns>
        [HttpGet("{name}")]
        public ActionResult<IEnumerable<Customer>> GetbyName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }

            var customer = _customerService.GetByName(name);

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }
    }
}
