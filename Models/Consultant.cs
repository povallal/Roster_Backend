using System;
namespace rosterapi.Models
{
    public class Consultant : User
    {


        public Unit Unit { get; set; }

        public int UnitId { get; set; }  // Foreign key property
      
    }
}

