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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitResponse>>> GetUnits()
        {
            // Retrieve all units from the database
            var units = await _context.Units
                     .Select(u => new UnitResponse
                     {
                         Id = u.Id,
                         Name = u.Name,
                         CreatedAt = u.CreatedAt,
                         UpdatedAt = u.UpdatedAt,
                         ChiefConsultantName = u.ChiefConsultants != null && u.ChiefConsultants.Any() ? u.ChiefConsultants.First().UserName : null
                     })
                     .ToListAsync();


            // Check if units exist
            if (units == null || units.Count == 0)
            {
                return NotFound(new Response { Status = "Error", Message = "No units found." });
            }

            return Ok(units);
        }






    }
}

