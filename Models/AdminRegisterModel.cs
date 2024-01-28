using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace rosterapi.Models
{
	public class AdminRegisterModel: IdentityUser
    {

        
        

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public override string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public override string PasswordHash { get; set; }


    }
}

        
    
