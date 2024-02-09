using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using rosterapi.Models;

using rosterapi.Data;
using System.Text;
using System.Security.Cryptography;
using System.Reflection.Metadata;

namespace rosterapi.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        
        private readonly IConfiguration _configuration;
       
        private readonly UserAuthDbContext _context;

        private readonly RoleManager<IdentityRole<string>> _roleManager;


        public AccountsController(UserManager<User> userManager, SignInManager<User> signInManager, UserAuthDbContext dbContext, IConfiguration configuration, RoleManager<IdentityRole<string>> roleManager, UserAuthDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
           
            _configuration = configuration;
            _roleManager = roleManager;
            _context = context;

        }

        [HttpPost("admin-register")]
        public async Task<ActionResult<Response>> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return BadRequest(new Response { Status = "Error", Message = "User with this email already exists." });
            }

            var admin = new Admin
            {
                UserName = model.UserName,
                Email = model.Email,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
                IsActive = model.IsActive
            };

            var result = await _userManager.CreateAsync(admin, model.Password);
            if (result.Succeeded)
            {
                // Check if the "Admin" role exists, if not, create it
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    var adminRole = new IdentityRole<string>("Admin");
                    var createRoleResult = await _roleManager.CreateAsync(adminRole);
                    if (!createRoleResult.Succeeded)
                    {
                        return BadRequest(new Response { Status = "Error", Message = "Failed to create 'Admin' role.", Errors = createRoleResult.Errors.ToList() });
                    }
                }

                // Add the "Admin" role to the admin user
                var addRoleResult = await _userManager.AddToRoleAsync(admin, "Admin");
                Console.WriteLine("the admin role assigned done to the user");
                if (!addRoleResult.Succeeded)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Adding role to user failed.", Errors = addRoleResult.Errors.ToList() });
                }


                return Ok(new Response { Status = "Success", Message = "Admin registered successfully." });
            }
            else
            {
                return BadRequest(new Response { Status = "Error", Message = "User creation failed.", Errors = result.Errors.ToList() });
            }
        }



        [HttpPost("chief-consultant-register")]
        public async Task<ActionResult<Response>> RegisterChiefConsultant([FromBody] ChiefConsultantRegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return BadRequest(new Response { Status = "Error", Message = "User with this email already exists." });
            }

            var chiefConsultant = new ChiefConsultant
            {
                UserName = model.UserName,
                Email = model.Email,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
                IsActive = model.IsActive,
                UnitId = model.UnitId // Assign the specified unit to the chief consultant
            };

            var result = await _userManager.CreateAsync(chiefConsultant, model.Password);
            if (result.Succeeded)
            {
                // Check if the "ChiefConsultant" role exists, if not, create it
                if (!await _roleManager.RoleExistsAsync("ChiefConsultant"))
                {
                    var chiefConsultantRole = new IdentityRole<string>("ChiefConsultant");
                    var createRoleResult = await _roleManager.CreateAsync(chiefConsultantRole);
                    if (!createRoleResult.Succeeded)
                    {
                        return BadRequest(new Response { Status = "Error", Message = "Failed to create 'ChiefConsultant' role.", Errors = createRoleResult.Errors.ToList() });
                    }
                }

                // Add the "ChiefConsultant" role to the chief consultant user
                var addRoleResult = await _userManager.AddToRoleAsync(chiefConsultant, "ChiefConsultant");
                if (!addRoleResult.Succeeded)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Adding role to user failed.", Errors = addRoleResult.Errors.ToList() });
                }

                return Ok(new Response { Status = "Success", Message = "Chief Consultant registered successfully." });
            }
            else
            {
                return BadRequest(new Response { Status = "Error", Message = "User creation failed.", Errors = result.Errors.ToList() });
            }
        }



        [HttpPost("consultant-register")]
        public async Task<ActionResult<Response>> RegisterConsultant([FromBody] ConsultantRegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return BadRequest(new Response { Status = "Error", Message = "User with this email already exists." });
            }

            var consultant = new Consultant
            {
                UserName = model.UserName,
                Email = model.Email,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
                IsActive = model.IsActive,
                UnitId = model.UnitId // Assign the specified unit to the consultant
            };

        

            var result = await _userManager.CreateAsync(consultant, model.Password);
            if (result.Succeeded)
            {
                // Check if the "Consultant" role exists, if not, create it
                if (!await _roleManager.RoleExistsAsync("Consultant"))
                {
                    var consultantRole = new IdentityRole<string>("Consultant");
                    var createRoleResult = await _roleManager.CreateAsync(consultantRole);
                    if (!createRoleResult.Succeeded)
                    {
                        return BadRequest(new Response { Status = "Error", Message = "Failed to create 'Consultant' role.", Errors = createRoleResult.Errors.ToList() });
                    }
                }

                // Add the "Consultant" role to the consultant user
                var addRoleResult = await _userManager.AddToRoleAsync(consultant, "Consultant");
                Console.WriteLine("The consultant role assigned to the user");
                if (!addRoleResult.Succeeded)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Adding role to user failed.", Errors = addRoleResult.Errors.ToList() });
                }

                return Ok(new Response { Status = "Success", Message = "Consultant registered successfully." });
            }
            else
            {
                return BadRequest(new Response { Status = "Error", Message = "User creation failed.", Errors = result.Errors.ToList() });
            }
        }



        [HttpPost("medical-officer-register")]
        public async Task<ActionResult<Response>> RegisterMedicalOfficer([FromBody] MedicalOfficerRegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return BadRequest(new Response { Status = "Error", Message = "User with this email already exists." });
            }

            var medicalOfficer = new MedicalOfficer
            {
                UserName = model.UserName,
                Email = model.Email,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
                IsActive = model.IsActive,
                UnitId = model.UnitId, // Assign the specified unit to the medical officer
                GroupId = model.GroupId // Assign the specified group to the medical officer
            };
        

             var result = await _userManager.CreateAsync(medicalOfficer, model.Password);
            if (result.Succeeded)
            {
                // Check if the "MedicalOfficer" role exists, if not, create it
                if (!await _roleManager.RoleExistsAsync("MedicalOfficer"))
                {
                    var medicalOfficerRole = new IdentityRole<string>("MedicalOfficer");
                    var createRoleResult = await _roleManager.CreateAsync(medicalOfficerRole);
                    if (!createRoleResult.Succeeded)
                    {
                        return BadRequest(new Response { Status = "Error", Message = "Failed to create 'MedicalOfficer' role.", Errors = createRoleResult.Errors.ToList() });
                    }
                }

                // Add the "MedicalOfficer" role to the medical officer user
                var addRoleResult = await _userManager.AddToRoleAsync(medicalOfficer, "MedicalOfficer");
                Console.WriteLine("The medical officer role assigned to the user");
                if (!addRoleResult.Succeeded)
                {
                    return BadRequest(new Response { Status = "Error", Message = "Adding role to user failed.", Errors = addRoleResult.Errors.ToList() });
                }

                return Ok(new Response { Status = "Success", Message = "Medical Officer registered successfully." });
            }
            else
            {
                return BadRequest(new Response { Status = "Error", Message = "User creation failed.", Errors = result.Errors.ToList() });
            }
        }



        [HttpPost("login")]
        public async Task<ActionResult<Response>> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new Response { Status = "Error", Message = "Invalid User Email." });
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return BadRequest(new Response { Status = "Error", Message = "Invalid Password." });
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var userRole = userRoles.FirstOrDefault();

            Console.WriteLine("The Role for this user", userRole);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, userRoles.FirstOrDefault() ?? "")
            };

            if (!string.IsNullOrEmpty(user.Id))
            {
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            }

            claims.Add(new Claim("roles", string.Join(",", userRoles)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:ValidIssuer"],
                audience: _configuration["Jwt:ValidAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new Response { Status = "Success", Message = jwtToken });
        }




    }
}

