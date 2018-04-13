using System;
using System.Windows.Forms;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;

namespace BBAuto.App.FormsForDriver
{
  public partial class formDriverCar : Form
  {
    private Driver _driver;

    public formDriverCar(Driver driver)
    {
      InitializeComponent();

      _driver = driver;
    }

    private void DriverCar_Load(object sender, EventArgs e)
    {
      DriverCarList driverCarList = DriverCarList.getInstance();

      dgvDriverCar.DataSource = driverCarList.ToDataTableCar(_driver);
      dgvDriverCar.Columns[0].Visible = false;
      dgvDriverCar.Columns[1].Visible = false;
      dgvDriverCar.Columns[8].Visible = false;
      dgvDriverCar.Columns[9].Visible = false;
    }
  }
}
