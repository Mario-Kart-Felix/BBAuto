using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLibraryBBAuto;
using ClassLibraryBBAuto.Loaders;

namespace BBAuto
{
    public partial class FormLoadFuel : Form
    {
        public FormLoadFuel()
        {
            InitializeComponent();
        }

        private void FormLoadFuel_Load(object sender, EventArgs e)
        {
            cbFirm.Items.Add(FuelReport.Петрол);
            cbFirm.Items.Add(FuelReport.Neste);
            cbFirm.SelectedIndex = 0;
            //cbFirm.Items.Add(FuelReport.Чеки);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPath.Text))
            {
                MessageBox.Show("Не выбран файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FuelLoader fuelLoader = new FuelLoader(tbPath.Text, (FuelReport)cbFirm.SelectedItem);
            fuelLoader.Load();
        }
    }
}
