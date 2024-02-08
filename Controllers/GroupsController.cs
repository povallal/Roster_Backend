using System;
using Microsoft.AspNetCore.Mvc;
using rosterapi.Data;
using rosterapi.Models;

namespace rosterapi.Controllers
{
	
        [ApiController]
        [Route("api/groups")]
        public class GroupsController : ControllerBase
        {
            private readonly UserAuthDbContext _context;

            public GroupsController(UserAuthDbContext context)
            {
                _context = context;
            }

            [HttpPost("create")]
            public async Task<ActionResult<Response>> CreateGroup([FromBody] GroupCreateModel model)
            {
                // Check if the group name already exists
                if (_context.Groups.Any(g => g.Name == model.Name))
                {
                    return BadRequest(new Response { Status = "Error", Message = "Group with this name already exists." });
                }

                var group = new Group
                {
                    Name = model.Name,
                    // Add other properties as needed
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow,
                };

                _context.Groups.Add(group);
                await _context.SaveChangesAsync();

                return Ok(new Response { Status = "Success", Message = "Group created successfully." });
            }
        }

    
}

