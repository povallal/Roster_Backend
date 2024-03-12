using System;
using System.ComponentModel.DataAnnotations;

namespace rosterapi.Models
{

    public class RegisterModel

    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        
        [Display(Name ="Is Active")]
        public bool IsActive { get; set; }


    }

    public class ConsultantRegisterModel : RegisterModel
    {
        
    }

    public class ChiefConsultantRegisterModel : RegisterModel
    {
      
    }

    public class MedicalOfficerRegisterModel : RegisterModel
    {
        
         public int GroupId { get; set; }
    }

}

