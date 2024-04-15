using System;
namespace rosterapi.Models.Duty
{
	public class ConsultantDutyRequestDto
	{
        public string ConsultantId { get; set; }
        public int UnitId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

