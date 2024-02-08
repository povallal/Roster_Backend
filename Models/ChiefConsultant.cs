using System;
namespace rosterapi.Models
{
    public class ChiefConsultant : User
    {

       
        public Unit Unit { get; set; }

        public int UnitId { get; set; }  // Foreign key property

    }
}

