using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rosterapi.Data;
using rosterapi.Models;

namespace rosterapi.Controllers
{
    [ApiController]
    [Route("api/units")]
    public class UnitsController : ControllerBase
    {
        private readonly UserAuthDbContext _context;

        public UnitsController(UserAuthDbContext context)
        {
            _context = context;
        }



        [HttpPost("create")]
        public async Task<ActionResult<Response>> CreateUnit([FromBody] UnitRegisterModel model)
        {
            // Check if the unit name already exists
            if (_context.Units.Any(u => u.Name == model.Name))
            {
                return BadRequest(new Response { Status = "Error", Message = "Unit with this name already exists." });
            }

            var unit = new Unit
            {
               
                Name = model.Name,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,

                // Add other properties as needed
            };

            _context.Units.Add(unit);
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Unit created successfully." });
        }






        [HttpGet("all-units")]
        public async Task<ActionResult<IEnumerable<UnitResponse>>> GetAllUnits()
        {
            var units = await _context.Units
                .Include(u => u.ChiefConsultant)
                .Select(u => new UnitResponse
                {
                    Id = u.Id,
                    Name = u.Name,
                    CreatedAt = u.CreatedAt,
                    UpdatedAt = u.UpdatedAt,
                    ChiefConsultantName = u.ChiefConsultant != null ? u.ChiefConsultant.UserName : null
                })
                .ToListAsync();

            if (!units.Any())
            {
                return NotFound("No Units found.");
            }

            return Ok(units);
        }







    }
}

