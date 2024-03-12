using System;
namespace rosterapi.Models


{


	public class GroupResponse
	{
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
      
    }
}

