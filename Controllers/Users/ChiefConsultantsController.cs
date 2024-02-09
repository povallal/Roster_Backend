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



        // Get all Chief Consultants
        [HttpGet("chief-consultants")]
        public ActionResult<IEnumerable<UserResponse>> GetAllChiefConsultants()
        {
            var chiefConsultants = _context.ChiefConsultants
                .Include(c => c.Unit) // Include the related Unit
                .ToList();

            // Map ChiefConsultant entities to ChiefConsultantResponse objects
            var chiefConsultantResponses = chiefConsultants.Select(c => new UserResponse
            {
                Id = c.Id, // Populate the Id property
                UserName = c.UserName,
                Email = c.Email,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                IsActive = c.IsActive,
                UnitName = c.Unit != null ? c.Unit.Name : null // Check if Unit is not null before accessing its properties
            }).ToList();

            return Ok(chiefConsultantResponses);
        }










    }
}

