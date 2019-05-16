using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerAPI.DbModels;

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

        // GET api/customers
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetAll()
        {
            var customers = _customerService.GetAll();
            return Ok(customers);
        }

        [HttpGet("{id:int}")]
        public ActionResult<Customer> GetbyId(int id)
        {
            var customer = _customerService.GetbyId(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        public ActionResult<Customer> Create(Customer customer)
        {
            _customerService.Create(customer);

            return CreatedAtAction(nameof(GetbyId), new { id = customer.Id }, customer);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _customerService.Update(customer);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var customer = _customerService.GetbyId(id);

            if (customer == null)
            {
                return BadRequest();
            }

            _customerService.Delete(id);

            return NoContent();
        }


        [HttpGet("{name}")]
        public ActionResult<IEnumerable<Customer>> Getbyname(string name)
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
