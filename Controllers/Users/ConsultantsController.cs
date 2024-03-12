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



        [HttpGet("all-consultants")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllConsultants()
        {
            var consultants = await _context.Consultants
                .Select(c => new UserResponse
                {
                    Id = c.Id,
                    UserName = c.UserName,
                    Email = c.Email,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    IsActive = c.IsActive
                })
                .ToListAsync();

            if (!consultants.Any())
            {
                return NotFound("No Consultants found.");
            }

            return Ok(consultants);
        }


    }
}

