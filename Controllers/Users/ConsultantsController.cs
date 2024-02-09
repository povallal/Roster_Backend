using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rosterapi.Data;
using rosterapi.Models;

namespace rosterapi.Controllers.Users


{
    [ApiController]
    [Route("api/consultants")]
    public class ConsultantsController : ControllerBase
    {
        private readonly UserAuthDbContext _context;

        public ConsultantsController(UserAuthDbContext context)
        {
            _context = context;
        }


        // Get all Chief Consultants
        [HttpGet("consultants")]
        public ActionResult<IEnumerable<UserResponse>> GetAllConsultants()
        {
            var Consultants = _context.Consultants
                .Include(c => c.Unit) // Include the related Unit
                .ToList();

            // Map ChiefConsultant entities to ChiefConsultantResponse objects
            var ConsultantResponses = Consultants.Select(c => new UserResponse
            {
                Id = c.Id, // Populate the Id property
                UserName = c.UserName,
                Email = c.Email,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                IsActive = c.IsActive,
                UnitName = c.Unit != null ? c.Unit.Name : null // Check if Unit is not null before accessing its properties
            }).ToList();

            return Ok(ConsultantResponses);
        }

    }
}

