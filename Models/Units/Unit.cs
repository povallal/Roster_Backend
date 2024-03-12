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



        // Define a navigation property for a one-to-one relationship
        public ChiefConsultant ChiefConsultant { get; set; }
    }
}

