using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Domain;
using BBAuto.Domain.Common;

namespace BBAuto
{
    public partial class FormMileageFill : Form
    {
        private const string REPORT_PATH = @"\\bbmru0021\data\aesculap\current reports";

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
            try
            {
                MileageFill mileageFill = new MileageFill(REPORT_PATH, dateTimePicker1.Value);
                mileageFill.Begin();

                FormReport formReport = new FormReport(mileageFill.GetMileageReportList());
                formReport.ShowDialog();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Нет доступа к папке", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
