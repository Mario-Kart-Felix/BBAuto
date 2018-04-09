using System;
using System.Windows.Forms;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;

namespace BBAuto.App.FormsForDriver
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
