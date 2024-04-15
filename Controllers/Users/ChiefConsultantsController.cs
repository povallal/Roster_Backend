using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rosterapi.Data;
using rosterapi.Models;

namespace rosterapi.Controllers.Users
{
    [ApiController]
    [Route("api/chief-consultants")]
    public class ChiefConsultantsController : ControllerBase
    {
        private readonly UserAuthDbContext _context;

        public ChiefConsultantsController(UserAuthDbContext context)
        {
            _context = context;
        }



        [HttpPost("assign-chief-to-unit")]
        public async Task<ActionResult<Response>> AssignChiefConsultantToUnit(string chiefConsultantId, int unitId)
        {
            var chiefConsultant = await _context.ChiefConsultants.SingleOrDefaultAsync(cc => cc.Id == chiefConsultantId);
            if (chiefConsultant == null) return NotFound("ChiefConsultant not found");

            // If ChiefConsultant already has a Unit assigned and it's different from the new Unit
            if (chiefConsultant.UnitId != null && chiefConsultant.UnitId != unitId)
            {
                return BadRequest(new Response { Status = "Error", Message = "Chief Consultant is already assigned to a different Unit." });
            }

            var unit = await _context.Units.Include(u => u.ChiefConsultant).SingleOrDefaultAsync(u => u.Id == unitId);
            if (unit == null) return NotFound("Unit not found");

            // If the Unit is already assigned to a different Chief Consultant
            if (unit.ChiefConsultant != null && unit.ChiefConsultant.Id != chiefConsultantId)
            {
                return BadRequest(new Response { Status = "Error", Message = $"This Unit is already assigned to Chief Consultant {unit.ChiefConsultant.UserName}." });
            }

            // Assign the Unit to the ChiefConsultant
            chiefConsultant.UnitId = unitId;
            await _context.SaveChangesAsync();

            return Ok(new Response { Status = "Success", Message = "Chief Consultant assigned to Unit successfully." });
        }





        [HttpGet("all-chief-consultants")]
        public async Task<ActionResult<IEnumerable<ChiefConsultnatResponse>>> GetAllChiefConsultants()
        {
            Console.WriteLine("Get all cheif consultant called");
            var chiefConsultants = await _context.ChiefConsultants.Include(cc => cc.Unit)
                .Select(cc => new ChiefConsultnatResponse
                {
                    Id = cc.Id,
                    UserName = cc.UserName,
                    Email = cc.Email,
                    CreatedAt = cc.CreatedAt,
                    UpdatedAt = cc.UpdatedAt,
                    IsActive = cc.IsActive,
                    UnitName = cc.Unit != null ? cc.Unit.Name : null
                })
                .ToListAsync();

            if (chiefConsultants == null || !chiefConsultants.Any())
            {
                return NotFound("No Chief Consultants found.");
            }

            return Ok(chiefConsultants);
        }











    }
}

