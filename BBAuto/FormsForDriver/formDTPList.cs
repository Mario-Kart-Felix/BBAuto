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
    public partial class formDTPList : Form
    {
        Driver driver;

        public formDTPList(Driver driver)
        {
            InitializeComponent();
            this.driver = driver;
        }

        private void formDTPList_Load(object sender, EventArgs e)
        {
            DTPList dtpList = DTPList.getInstance();
            dgvDTP.DataSource = dtpList.ToDataTable(driver);

            if (dgvDTP.DataSource != null)
                formatDGV();
        }

        private void formatDGV()
        {
            dgvDTP.Columns[0].Visible = false;
            dgvDTP.Columns[1].Visible = false;
        }
    }
}
