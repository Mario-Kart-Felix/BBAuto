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
