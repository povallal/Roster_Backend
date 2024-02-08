using System;
using System.ComponentModel.DataAnnotations;

namespace rosterapi.Models
{
	public class UnitRegisterModel
	{

        //[Required]
        //public int Id { get; set; } // Include the Id property if needed

        [Required]
        public string Name { get; set; }

       

    }
}

