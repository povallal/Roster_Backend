using System;
using System.ComponentModel.DataAnnotations;

namespace rosterapi.Models
{
	public class GroupCreateModel
	{
        [Required]
        public string Name { get; set; }

        public int UnitId { get; set; }  // Foreign key property


    }
}

