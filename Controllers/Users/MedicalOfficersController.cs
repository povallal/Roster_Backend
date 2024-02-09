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


        // Get all Chief Consultants
        [HttpGet("medical-officers")]
        public ActionResult<IEnumerable<MedicalOfficerResponse>> GetAllMedicalOfficers()
        {
            var Medicalofficers = _context.MedicalOfficers
                .Include(c => c.Unit) // Include the related Unit
                .ToList();

            // Map ChiefConsultant entities to ChiefConsultantResponse objects
            var MedicalOfficerResponses = Medicalofficers.Select(c => new MedicalOfficerResponse
            {
                Id = c.Id, // Populate the Id property
                UserName = c.UserName,
                Email = c.Email,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                IsActive = c.IsActive,
                UnitName = c.Unit != null ? c.Unit.Name : null,  // Check if Unit is not null before accessing its properties
                //GroupId = (int)(c.Unit != null ? (c.Unit.Groups.FirstOrDefault()?.Id) : -1)
            }).ToList();

            return Ok(MedicalOfficerResponses);
        }

    }
}

