using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Context;
using VehicleRentalAPI.Entities;

namespace VehicleRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController (VehicleRentalContext context) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Rental rental)
        {
            context.Rentals.Add(rental);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRental), new { id = rental.Id }, rental);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetRentals()
        {
            var rentals = await context.Customers.ToListAsync();
            return Ok(rentals);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetRental(int id)
        {
            var rental = await context.Rentals.FirstOrDefaultAsync(rental => rental.Id == id);
            if (rental is null) { return NotFound(); }
            return Ok(rental);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, Rental updatedRental)
        {
            var rentalToUpdate = await context.Rentals.FirstOrDefaultAsync(rental => rental.Id == id);
            if (rentalToUpdate is null) { return NotFound(); }
            rentalToUpdate.CustomerId = updatedRental.CustomerId;
            rentalToUpdate.VehicleId = updatedRental.VehicleId;
            rentalToUpdate.RentalDate = updatedRental.RentalDate;
            rentalToUpdate.ReturnDate = updatedRental.ReturnDate;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var rentalToDelete = await context.Rentals.FirstOrDefaultAsync(rental => rental.Id == id);
            if (rentalToDelete is null) { return NotFound(); }

            context.Rentals.Remove(rentalToDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
