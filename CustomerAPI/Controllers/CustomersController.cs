using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerAPI.DbModels;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomerContext _customerContext;

        public CustomersController(CustomerContext context)
        {
            _customerContext = context;
        }

        // GET api/customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAll()
        {
            return await _customerContext.Customers.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetbyId(int id)
        {
            var customer = await _customerContext.Customers.Where(customerItem => customerItem.Id == id).FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            _customerContext.Customers.Add(customer);
            await _customerContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetbyId), new { id = customer.Id }, customer);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _customerContext.Entry(customer).State = EntityState.Modified;
            await _customerContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerContext.Customers.FindAsync(id);

            if (customer == null)
            {
                return BadRequest();
            }

            _customerContext.Remove(customer);
            await _customerContext.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("{name}")]
        public async Task<ActionResult<Customer>> Getbyname(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest();
            }

            var customer = await _customerContext.Customers
                .Where(customerItem => $"{customerItem.FirstName} {customerItem.LastName}".IndexOf(name) >= 0).FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }
    }
}
