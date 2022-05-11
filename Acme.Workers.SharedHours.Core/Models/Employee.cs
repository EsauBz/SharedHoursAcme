using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Workers.SharedHours.Core.Models
{
    public class Employee
    {
        public string Name { get; set; }
        public List<OfficeHours> TimeWorked { get; set; }
    }
}
