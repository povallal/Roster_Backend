using System;

using rosterapi.Authentication;
using rosterapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.SwaggerGen;

using Microsoft.EntityFrameworkCore;

namespace asp_net.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AdminRegisterModel> _adminUserManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AdminRegisterModel> _adminSignInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AuthenticateController> _logger;

        public AuthenticateController(UserManager<AdminRegisterModel> adminUserManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, SignInManager<AdminRegisterModel> adminSignInManager, ApplicationDbContext context, ILogger<AuthenticateController> logger)
        {
            _adminUserManager = adminUserManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _adminSignInManager = adminSignInManager;
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        [Route("register-admin")]
        [ProducesResponseType(typeof(AdminRegisterModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterAdmin([FromBody] AdminRegisterModel model)

        {
            try
            {
                Console.WriteLine("Starting registration of admin user");
                _logger.LogInformation("Starting registration of admin user");
                var userExists = await _adminUserManager.FindByNameAsync(model.Name);
                if (userExists != null)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

                AdminRegisterModel user = new AdminRegisterModel()
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Name = "admin_" + model.Name
                };
                var result = await _adminUserManager.CreateAsync(user, model.PasswordHash);
                if (!result.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check the user details and try again." });

                var roleResult = await _adminUserManager.AddToRoleAsync(user, "Admin");
                if (!roleResult.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Role assignment failed! Please check the user details and try again." });

                _logger.LogInformation("Admin user registration completed successfully");

                return Ok(new Response { Status = "Success", Message = "User created successfully with admin role." });
            }

            catch(Exception ex)
            {

                Console.WriteLine($"An error occurred: {ex}");
                _logger.LogError(ex, "An error occurred during admin registration.");
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Internal Server Error." });
            }
        }



        



    }

}




           