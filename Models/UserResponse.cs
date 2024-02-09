using System;
namespace rosterapi.Models
{
	public class UserResponse
	{
        public string Id { get; set; } // Add the Id property
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        public string UnitName { get; set; }

    }



    public class MedicalOfficerResponse : UserResponse
    {

        //public string GroupName { get; set; }
     //   public int GroupId { get; set; }


    }
}

