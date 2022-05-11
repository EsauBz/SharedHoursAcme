using System;

namespace Acme.Workers.SharedHours.Core.Models
{
    public class OfficeHours
    {
        public string Day { get; set; }
        public DateTime InitalHour { get; set; }
        public DateTime FinalHour { get; set; }
    }
}
