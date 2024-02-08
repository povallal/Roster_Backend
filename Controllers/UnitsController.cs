using System;
using Microsoft.AspNetCore.Mvc;
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




       
    }
}

