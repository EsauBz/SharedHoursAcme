using Acme.Workers.SharedHours.Core.Interfaces;
using Acme.Workers.SharedHours.Core.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Acme.Workers.SharedHours.Core.Controllers
{
    public class SharedHoursController
    {
        private readonly ISharedHoursView _view;
        public SharedHoursController(ISharedHoursView view)
        {
            _view = view;
            view.SetController(this);
        }

        public void LoadView()
        {
            _view.ClearGrid();
            _view.ClearTextBox();
        }

        public void OpenHoursFile()
        {
            var workersList = BrowseFileDialog();
            if (workersList.Count != 0)
            {
                var sharedHours = CheckSharedHours(workersList);
                _view.AddWorkersToGrid(sharedHours);
            }
        }

        public List<Employee> BrowseFileDialog()
        {
            List<Employee> employeesWork = new List<Employee>();
            try
            {
                using (OpenFileDialog winExplorer = new OpenFileDialog())
                {
                    // Available file extensions
                    winExplorer.Filter = "All files(*.*)|*.*";
                    // OpenFileDialog title
                    winExplorer.Title = "Open Share Hours Input";
                    // Show OpenFileDialog box
                    if (winExplorer.ShowDialog() == DialogResult.OK)
                    {
                        _view.FileTextBox = winExplorer.SafeFileName;
                        // Create new StreamReader
                        StreamReader sr = new StreamReader(winExplorer.FileName, Encoding.Default);
                        // Get all text from the file
                        string input = sr.ReadToEnd();
                        // Close the StreamReader
                        sr.Close();
                        // Create the Model from the input file
                        var employees = input.Split('\n');
                        foreach (string employee in employees)
                        {
                            employeesWork.Add(CreateWorkerFromInput(employee));
                        }
                        return employeesWork;
                    }
                }
                return new List<Employee>();
            }
            catch (Exception errorMsg)
            {
                MessageBox.Show(errorMsg.Message);
                return new List<Employee>();
            }

        }

        public Employee CreateWorkerFromInput(string input)
        {
            try
            {
                Employee currentEmployee = new Employee();
                currentEmployee.TimeWorked = new List<OfficeHours>();
                OfficeHours dayModel = new OfficeHours();

                var workerInfo = input.Split('=');
                currentEmployee.Name = workerInfo.FirstOrDefault();
                workerInfo = workerInfo[1].Split(',');
                if (workerInfo.FirstOrDefault() != "")
                {
                    foreach (string day in workerInfo)
                    {
                        dayModel.Day = day.Substring(0, 2);
                        dayModel.InitalHour = System.DateTime.ParseExact(day.Substring(2, 5), "HH:mm", CultureInfo.InvariantCulture);
                        dayModel.FinalHour = System.DateTime.ParseExact(day.Substring(8, 5), "HH:mm", CultureInfo.InvariantCulture);

                        currentEmployee.TimeWorked.Add(dayModel);
                        dayModel = new OfficeHours();
                    }
                }
                return currentEmployee;
            }
            catch (Exception errorMsg)
            {
                MessageBox.Show("The input data may have an incorrect format: " + errorMsg.Message);
                return new Employee();
            }
        }

        public Dictionary<string, string> CheckSharedHours(List<Employee> workersList)
        {
            var sharedHours = new Dictionary<string, string>();
            string name = string.Empty;
            string hours = string.Empty;

            for (int i = 0; i < workersList.Count - 1; i++)
            {
                for (int j = i + 1; j < workersList.Count; j++)
                {
                    name = workersList[i].Name + "-" + workersList[j].Name;
                    hours = GetSharedHours(workersList[i].TimeWorked, workersList[j].TimeWorked);
                    sharedHours.Add(name, hours);
                }
            }

            return sharedHours;
        }

        public string GetSharedHours(List<OfficeHours> workerOne, List<OfficeHours> workerTwo)
        {
            var queryWorkers = from worker in workerOne
                               join worker2 in workerTwo
                               on worker.Day equals worker2.Day
                               where (worker.InitalHour >= worker2.InitalHour
                               && worker.InitalHour.Hour <= worker2.FinalHour.Hour) ||
                               (worker2.InitalHour >= worker.InitalHour
                               && worker2.InitalHour.Hour <= worker.FinalHour.Hour)
                               select (worker, worker2);

            return Utils.AddSharedHours(queryWorkers.ToList());
        }
    }
}
