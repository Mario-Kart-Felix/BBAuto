using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLibraryBBAuto;

namespace BBAuto
{
    public partial class FormMileageFill : Form
    {
        public FormMileageFill()
        {
            InitializeComponent();
        }

        private void FormMileageFill_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Today.AddMonths(-1);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            MileageFill mileageFill = new MileageFill(@"\\bbmru0021\data\aesculap\current reports", dateTimePicker1.Value);
            mileageFill.Begin();

            FormReport formReport = new FormReport(mileageFill.GetMileageReportList());
            formReport.ShowDialog();
        }
    }
}
