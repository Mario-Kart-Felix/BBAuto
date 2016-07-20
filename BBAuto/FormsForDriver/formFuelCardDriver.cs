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
    public partial class formFuelCardDriver : Form
    {
        private Driver _driver;

        public formFuelCardDriver(Driver driver)
        {
            InitializeComponent();

            _driver = driver;
        }

        private void formFuelCardDriver_Load(object sender, EventArgs e)
        {
            FuelCardDriverList fuelCardDriverList = FuelCardDriverList.getInstance();

            dgvDriverCar.DataSource = fuelCardDriverList.ToDataTable(_driver);
            dgvDriverCar.Columns[0].Visible = false;
            dgvDriverCar.Columns[1].Visible = false;
            dgvDriverCar.Columns[3].Visible = false;
        }
    }
}
