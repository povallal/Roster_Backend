using System;
namespace rosterapi.Models
{
	public class UnitResponse
    {
		
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public string? ChiefConsultantName { get; set; }
    }



}


