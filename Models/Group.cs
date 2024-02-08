using System;
using System.Reflection.Metadata;

namespace rosterapi.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }



        public ICollection<MedicalOfficer> MedicalOfficers { get; set; }
    }
}

