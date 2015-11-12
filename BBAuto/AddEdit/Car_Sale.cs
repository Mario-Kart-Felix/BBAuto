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
    public partial class Car_Sale : Form
    {
        CarSale carSale;
        public Car_Sale(CarSale carSale)
        {
            InitializeComponent();

            this.carSale = carSale;
        }

        private void Car_Sale_Load(object sender, EventArgs e)
        {
            if (carSale.Date != "")
            {
                dtpDate.Value = Convert.ToDateTime(carSale.Date);
                chbSale.Checked = true;
            }
            else
                chbSale.Checked = false;

            changeVisible();
            tbComm.Text = carSale.comm;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string date = "";
            if (chbSale.Checked)
                date = dtpDate.Value.Date.ToShortDateString();

            carSale.Date = date;
            carSale.comm = tbComm.Text;

            carSale.Save();
        }

        private void chbSale_CheckedChanged(object sender, EventArgs e)
        {
            changeVisible();
        }

        private void changeVisible()
        {
            dtpDate.Visible = chbSale.Checked;
        }
    }
}