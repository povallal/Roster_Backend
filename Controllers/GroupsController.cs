using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

                    if (_context.Groups.Any(g => g.Name == model.Name && g.UnitId == model.UnitId))
                    {
                        return BadRequest(new Response { Status = "Error", Message = "Group with this name and unit ID already exists." });
                    }


            var group = new Group
                {
                       Name = model.Name,
                    // Add other properties as needed
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow,
                    UnitId = model.UnitId
                };

                _context.Groups.Add(group);
                await _context.SaveChangesAsync();

                return Ok(new Response { Status = "Success", Message = "Group created successfully." });
            }


            [HttpGet]
            public async Task<ActionResult<IEnumerable<GroupResponse>>> GetGroups()
            {
                // Retrieve all groups from the database
                var groups = await _context.Groups
                             .Select(g => new GroupResponse
                             {
                                 Id = g.Id,
                                 Name = g.Name,
                                 CreatedAt = g.CreatedAt,
                                 UpdatedAt = g.UpdatedAt,
                                 // Add any additional properties as needed
                                 UnitName = g.Unit != null ? g.Unit.Name : null
                             })
                             .ToListAsync();

                // Check if groups exist
                if (groups == null || groups.Count == 0)
                {
                    return NotFound(new Response { Status = "Error", Message = "No groups found." });
                }

                return Ok(groups);
            }





    }


}

