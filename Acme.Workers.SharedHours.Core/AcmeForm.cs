using Acme.Workers.SharedHours.Core.Controllers;
using Acme.Workers.SharedHours.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Acme.Workers.SharedHours.Core
{
    public partial class AcmeForm : Form, ISharedHoursView
    {
        #region Controller
        private  SharedHoursController _controller;
        #endregion
        public AcmeForm()
        {
            InitializeComponent();
        }
        #region Interfaces Implemented
        public void ClearGrid()
        {
            // Define columns in grid
            this.SharedHoursGrid.Columns.Clear();
        }

        public void ClearTextBox()
        {
            this.FileNameTextBox.Clear();
        }

        public void SetController(SharedHoursController controller)
        {
            _controller = controller;
        }

        public void AddWorkersToGrid(Dictionary<string, string> sharedHours)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Employees");
            dt.Columns.Add("Shared Hours");
            foreach (KeyValuePair<string,string> day in sharedHours)
            {
                var row = dt.NewRow();
                row[0] = day.Key;
                row[1] = day.Value;
                dt.Rows.Add(row);
            }
            this.SharedHoursGrid.DataSource = dt;
        }


        public string FileTextBox
        {
            get { return this.FileNameTextBox.Text; }
            set { this.FileNameTextBox.Text = value; }
        }
        #endregion
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            this._controller.OpenHoursFile();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.FileNameTextBox.Clear();
            this.SharedHoursGrid.DataSource = null;
            this.SharedHoursGrid.Refresh();
        }
    }
}
