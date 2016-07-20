using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Domain;

namespace BBAuto
{
    public partial class formViolationList : Form
    {
        Driver driver;
        ViolationList violationList;

        public formViolationList(Driver driver)
        {
            InitializeComponent();

            this.driver = driver;

            violationList = ViolationList.getInstance();
        }

        private void ViolationList_Load(object sender, EventArgs e)
        {
            dgvViolation.DataSource = violationList.ToDataTable(driver);

            if (dgvViolation.DataSource != null)
                formatDGV();
        }

        private void formatDGV()
        {
            dgvViolation.Columns[0].Visible = false;
            dgvViolation.Columns[1].Visible = false;
            dgvViolation.Columns[5].Visible = false;
        }
    }
}
