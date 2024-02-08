using System;
using System.Security.Policy;

namespace rosterapi.Models
{
    public class MedicalOfficer : User
    {

       
       
        public Unit Unit { get; set; }
        public int UnitId { get; set; }   // Foreign key property

     
        public Group Group { get; set; }
        public int GroupId { get; set; }  // Foreign key property
    }

}




