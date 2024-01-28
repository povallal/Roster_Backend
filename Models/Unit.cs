using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using rosterapi.Models;

namespace rosterapi.Models
{
	public class Unit
	{
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        // Navigation property
        public ICollection<ChiefConsultantRegistrationModel> ChiefConsultants { get; set; }


        // SelectList property for generating the dropdown options
        public SelectList GetUnitSelectList()
        {
            var units = new List<Unit>
            {
                new Unit { Id = 1, Name = "Unit 1" },
                new Unit { Id = 2, Name = "Unit 2" },
                new Unit { Id = 3, Name = "Unit 3" }
            };

            return new SelectList(units, "Id", "Name");
        }
    }
}

