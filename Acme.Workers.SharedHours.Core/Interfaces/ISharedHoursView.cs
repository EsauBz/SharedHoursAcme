using Acme.Workers.SharedHours.Core.Controllers;
using System.Collections.Generic;

namespace Acme.Workers.SharedHours.Core.Interfaces
{
    public interface ISharedHoursView
    {
        public void SetController(SharedHoursController controller);
        public void ClearGrid();
        public void ClearTextBox();

        public void AddWorkersToGrid(Dictionary<string, string> sharedHours);


        public string FileTextBox { get; set; }
    }
}
