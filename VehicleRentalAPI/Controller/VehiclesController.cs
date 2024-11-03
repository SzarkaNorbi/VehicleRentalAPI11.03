using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRentalAPI.Context;
using VehicleRentalAPI.Entities;

namespace VehicleRentalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController(VehicleRentalContext context) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.Id }, vehicle);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetVehicles()
        {
            var vehicles = await context.Vehicles.ToListAsync();
            return Ok(vehicles);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await context.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == id);
            if (vehicle is null) { return NotFound(); }
            return Ok(vehicle);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, Vehicle updatedVehicle)
        {
            var vehicleToUpdate = await context.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == id);
            if (vehicleToUpdate is null) { return NotFound(); }
            vehicleToUpdate.Model = updatedVehicle.Model;
            vehicleToUpdate.LicensePlate = updatedVehicle.LicensePlate;
            vehicleToUpdate.DailyRate = updatedVehicle.DailyRate;
            vehicleToUpdate.Available = updatedVehicle.Available;
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var vehicleToDelete = await context.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.Id == id);
            if (vehicleToDelete is null) { return NotFound(); }

            context.Vehicles.Remove(vehicleToDelete);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
