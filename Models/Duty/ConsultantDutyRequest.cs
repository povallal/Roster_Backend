using System;
namespace rosterapi.Models.Duty
{

    // BaseModel.cs
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public static class DutyRequestStatus
    {
        public const string Pending = "Pending";
        public const string Accepted = "Accepted";
        public const string Rejected = "Rejected";
    }

    public class ConsultantDutyRequest : BaseModel
    {
        public string ConsultantId { get; set; } // FK for Consultant User
        public int UnitId { get; set; } // FK for Unit
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        private string _status = DutyRequestStatus.Pending;
        public string Status
        {
            get => _status;
            set
            {
                if (value == DutyRequestStatus.Pending || value == DutyRequestStatus.Accepted || value == DutyRequestStatus.Rejected)
                {
                    _status = value;
                }
                else
                {
                    throw new ArgumentException("Invalid status value");
                }
            }
        }

        public Consultant Consultant { get; set; } // Navigation property for EF Core
        public Unit Unit { get; set; } // Navigation property for EF Core
    }
}

