using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rosterapi.Data;
using rosterapi.Models;


namespace rosterapi.Controllers.Users
{
    [ApiController]
    [Route("api/medical-officers")]
    public class MedicalOfficersController : ControllerBase
    {

        private readonly UserAuthDbContext _context;

        public MedicalOfficersController(UserAuthDbContext context)
        {
            _context = context;
        }



        [HttpGet("all-medical-officers")]
        public async Task<ActionResult<IEnumerable<MedicalOfficerResponse>>> GetAllMedicalOfficers()
        {
            var medicalOfficers = await _context.MedicalOfficers
                .Include(mo => mo.Group)
                .Select(mo => new MedicalOfficerResponse
                {
                    Id = mo.Id,
                    UserName = mo.UserName,
                    Email = mo.Email,
                    CreatedAt = mo.CreatedAt,
                    UpdatedAt = mo.UpdatedAt,
                    IsActive = mo.IsActive,
                    GroupName = mo.Group.Name, // Assuming the Group entity has a Name property
                })
                .ToListAsync();

            if (!medicalOfficers.Any())
            {
                return NotFound("No Medical Officers found.");
            }

            return Ok(medicalOfficers);
        }


    }
}

