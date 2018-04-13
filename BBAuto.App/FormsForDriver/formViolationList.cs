using System;
using System.Windows.Forms;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;

namespace BBAuto.App.FormsForDriver
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
