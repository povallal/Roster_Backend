using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using rosterapi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace rosterapi.Models
{
    public class Unit
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }


        // Each Unit can have many ChiefConsultants, Consultants, and MedicalOfficers
        public ICollection<ChiefConsultant> ChiefConsultants { get; set; }
        public ICollection<Consultant> Consultants { get; set; }
        public ICollection<MedicalOfficer> MedicalOfficers { get; set; }

    }
}

