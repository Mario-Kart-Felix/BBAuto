using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Domain.Common;

namespace BBAuto
{
    public partial class FormReport : Form
    {
        MileageReportList _mileageReportList;

        public FormReport(MileageReportList mileageReportList)
        {
            InitializeComponent();

            _mileageReportList = mileageReportList;
        }

        private void FormReport_Load(object sender, EventArgs e)
        {
            tbReport.Text = _mileageReportList.GetReportMessage();
        }

        private void btnShowReport_Click(object sender, EventArgs e)
        {
            MileageReportExcel mileageReportExcel = new MileageReportExcel(_mileageReportList);
            mileageReportExcel.Create();
        }
    }
}
