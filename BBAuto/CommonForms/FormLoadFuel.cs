using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Domain;
using BBAuto.Domain.Loaders;

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
            List<string> list = fuelLoader.Load();
            if (list.Count == 0)
            {
                MessageBox.Show("Загрузка завершена без ошибок", "Загрузка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Загрузка завершена с ошибками", "Загрузка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                foreach (var item in list)
                {
                    MessageBox.Show(item, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "(Excel files)|*.xls";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbPath.Text = ofd.FileName;
            }
        }
    }
}
