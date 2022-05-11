using Acme.Workers.SharedHours.Core.Controllers;
using System;
using System.Windows.Forms;

namespace Acme.Workers.SharedHours.Core
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AcmeForm view = new AcmeForm();
            view.Visible = false;

            SharedHoursController controller = new SharedHoursController(view);
            controller.LoadView();
            view.ShowDialog();
        }
    }
}
