using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace rosterapi.Models
{
	public class ChiefConsultantRegistrationModel
	{


        [Key]
        [Required(ErrorMessage = "ID number is required.")]
        [StringLength(20, ErrorMessage = "ID number must not exceed 20 characters.")]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name must not exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [StringLength(15, ErrorMessage = "Phone number must not exceed 15 characters.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be a 10-digit number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email must not exceed 100 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must not exceed 100 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
       

        // Navigation property
        [ForeignKey("UnitId")]
        public Unit Unit { get; set; }

        public int UnitId { get; set; }

    }
}

