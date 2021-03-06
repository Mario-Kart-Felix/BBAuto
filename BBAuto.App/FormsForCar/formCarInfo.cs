using System;
using System.Data;
using System.Windows.Forms;
using BBAuto.Logic.Entities;

namespace BBAuto.App.FormsForCar
{
  public partial class formCarInfo : Form
  {
    public formCarInfo(Car car)
    {
      InitializeComponent();

      DataTable dt = car.info.ToDataTable();
      DataTable dt2 = car.info.Grade.ToDataTable();

      foreach (DataRow row in dt2.Rows)
        dt.Rows.Add(row.ItemArray);

      _dgvCarInfo.DataSource = dt;

      ResizeDGV();
    }

    private void formCarInfo_Resize(object sender, EventArgs e)
    {
      ResizeDGV();
    }

    private void ResizeDGV()
    {
      _dgvCarInfo.Columns[0].Width = _dgvCarInfo.Width / 2;
      _dgvCarInfo.Columns[1].Width = _dgvCarInfo.Width / 2;
    }
  }
}
