using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using rosterapi.Data;
using rosterapi.Models;
using rosterapi.Models.Duty;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using Azure.Core;

namespace rosterapi.Controllers

{
    [ApiController]
    [Route("api/[controller]")] 
    public class ConsultantDutyController : ControllerBase
    {
        private readonly UserAuthDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ConsultantDutyController> _logger;

        public ConsultantDutyController(UserAuthDbContext context, UserManager<User> userManager, ILogger<ConsultantDutyController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        //[Authorize(Roles = "Consultant")] // Make sure only authorized consultants can make a request
        [HttpPost("create-request")]
        public async Task<ActionResult> CreateDutyRequest([FromBody] ConsultantDutyRequestDto dutyRequestDto)
        {

            try {
               
                // Here you would typically have validation for the dutyRequestDto

                // Assuming we have a method that validates the dutyRequestDto
                // var validationResult = ValidateDutyRequest(dutyRequestDto);
                // if (!validationResult.IsValid)
                // {
                //     return BadRequest(new Response { Status = "Error", Message = "Validation failed.", Errors = validationResult.Errors });
                // }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
              

                // Find the user from the UserManager using the ConsultantId from the duty request
                var user = await _userManager.FindByIdAsync(dutyRequestDto.ConsultantId);
                if (user == null)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Invalid consultant ID." });
                }

               

                // Create a new ConsultantDutyRequest object from the DTO
                var dutyRequest = new ConsultantDutyRequest

                {

                    ConsultantId = dutyRequestDto.ConsultantId,
                    UnitId = dutyRequestDto.UnitId,
                    StartDate = dutyRequestDto.StartDate,
                    EndDate = dutyRequestDto.EndDate,
                    // Status is already set to "Pending" by default
                };
                Console.WriteLine($"Duty Object: ConsultantId: {dutyRequest.ConsultantId}, UnitId: {dutyRequest.UnitId}, StartDate: {dutyRequest.StartDate}, EndDate: {dutyRequest.EndDate}, Status: {dutyRequest.Status}");

                _logger.LogInformation($"Creating duty request: {dutyRequest}");

                // Add the duty request to the DbContext
                _context.ConsultantDutyRequests.Add(dutyRequest);
                // Save changes asynchronously
                await _context.SaveChangesAsync();

                // Return a 201 Created response with the created duty request
                return CreatedAtAction(nameof(CreateDutyRequest), new { id = dutyRequest.Id }, new Response { Status = "Success", Message = "Duty request created successfully." });
            }

            catch (Exception ex)
            {
                // Log the exception details here using your logging framework
                return StatusCode(500, new Response { Status = "Error", Message = "An error occurred while creating the duty request.", Errors = new List<IdentityError> { new IdentityError { Description = ex.Message } } });
            }
        }


        // Add other methods as needed, for example, for updating duty request status, etc




        // Example method for updating duty request status
        //[Authorize(Roles = "Admin")] // Only accessible by admin for approval/rejection
        [HttpPost("update-request-status/{requestId}")]
        public async Task<ActionResult> UpdateDutyRequestStatus(int requestId, [FromBody] UpdateDutyRequestStatusDto statusDto)
        {
            var dutyRequest = await _context.ConsultantDutyRequests.FindAsync(requestId);
            if (dutyRequest == null)
            {
                return NotFound(new { message = "Duty request not found" });
            }

            dutyRequest.Status = statusDto.Status; // Your validation logic should ensure this is an allowed status
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Duty request status updated to {statusDto.Status}." });
        }


        //[Authorize(Roles = "Admin")]
        [HttpGet("fetch-requests")]
        public async Task<IActionResult> FetchDutyRequests([FromQuery] List<string> statuses)
        {
            IQueryable<ConsultantDutyRequest> query = _context.ConsultantDutyRequests;

            // If specific statuses are provided, filter by those statuses
            if (statuses != null && statuses.Count > 0)
            {
                query = query.Where(r => statuses.Contains(r.Status));
            }

            var requests = await query.ToListAsync();
            return Ok(requests);
        }

        [HttpGet("fetch-request")]
        public async Task<IActionResult> FetchDutyRequests([FromQuery] List<string> statuses, [FromQuery] string consultantId)
        {
            IQueryable<ConsultantDutyRequest> query = _context.ConsultantDutyRequests;

            // Filter by specific statuses if provided
            if (statuses != null && statuses.Count > 0)
            {
                query = query.Where(r => statuses.Contains(r.Status));
            }

            // Filter by consultant ID if provided
            if (!string.IsNullOrEmpty(consultantId))
            {
                query = query.Where(r => r.ConsultantId == consultantId);
            }

            var requests = await query.ToListAsync();
            return Ok(requests);
        }









    }



}

