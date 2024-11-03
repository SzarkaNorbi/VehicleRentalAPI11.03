using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Context;
using VehicleRentalAPI.Entities;

namespace VehicleRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController(VehicleRentalContext context) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Customer customer)
        {
            context.Customers.Add(customer);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await context.Customers.ToListAsync();
            return Ok(customers);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await context.Customers.FirstOrDefaultAsync(customer => customer.Id == id);
            if (customer is null) { return NotFound(); }
            return Ok(customer);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, Customer updatedCustomer)
        {
            var customerToUpdate = await context.Customers.FirstOrDefaultAsync(customer => customer.Id == id);
            if (customerToUpdate is null) { return NotFound(); }
            customerToUpdate.Name = updatedCustomer.Name;
            customerToUpdate.Email = updatedCustomer.Email;
            customerToUpdate.PhoneNumber = updatedCustomer.PhoneNumber;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var customerToDelete = await context.Customers.FirstOrDefaultAsync(customer => customer.Id == id);
            if (customerToDelete is null) { return NotFound(); }

            context.Customers.Remove(customerToDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
