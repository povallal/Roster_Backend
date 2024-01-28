using System;
using System.ComponentModel.DataAnnotations;

namespace rosterapi.Models
{
	public class Sample
	{
        [Key]
        public int SampleId { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}

