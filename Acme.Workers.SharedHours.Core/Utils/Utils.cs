using System;
using System.Collections.Generic;

namespace Acme.Workers.SharedHours.Core.Models
{
    public static class Utils
    {
        public static string AddSharedHours(List<(OfficeHours worker, OfficeHours worker2)> list)
        {
            TimeSpan result = TimeSpan.Zero;

            for(int i = 0; i < list.Count; i++)
            {
                var initHour = list[i].worker.InitalHour < list[i].worker2.InitalHour 
                    ? list[i].worker2.InitalHour : list[i].worker.InitalHour;
                var finalHour = list[i].worker.FinalHour < list[i].worker2.FinalHour 
                    ? list[i].worker.FinalHour : list[i].worker2.FinalHour;
                result += finalHour - initHour;
            }

            return (result.Hours).ToString();
        }
    }
}
