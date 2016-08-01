using BBAuto.Domain.Common;
using BBAuto.Domain.Lists;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BBAuto
{
    public partial class formSuppyAddressList : Form
    {
        SuppyAddressList suppyAddressList;

        public formSuppyAddressList()
        {
            InitializeComponent();

            suppyAddressList = SuppyAddressList.getInstance();
        }

        private void formSuppyAddressList_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            _dgvSuppyAddress.DataSource = suppyAddressList.ToDataTable();
            _dgvSuppyAddress.Columns[0].Visible = false;
            ResizeDGV();
        }

        private void ResizeDGV()
        {
            _dgvSuppyAddress.Columns[1].Width = 100;
            _dgvSuppyAddress.Columns[2].Width = _dgvSuppyAddress.Width - 100;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SuppyAddress suppyAddress = new SuppyAddress();

            SuppyAddress_AddEdit aesuppyAddress = new SuppyAddress_AddEdit(suppyAddress);

            if (aesuppyAddress.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                loadData();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int idSuppyAddress = 0;
            int.TryParse(_dgvSuppyAddress.Rows[_dgvSuppyAddress.CurrentCell.RowIndex].Cells[0].Value.ToString(), out idSuppyAddress);

            suppyAddressList.Delete(idSuppyAddress);

            loadData();
        }

        private void _dgvSuppyAddress_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int idSuppyAddress = 0;
            int.TryParse(_dgvSuppyAddress.Rows[_dgvSuppyAddress.CurrentCell.RowIndex].Cells[0].Value.ToString(), out idSuppyAddress);

            SuppyAddress suppyAddress = suppyAddressList.getItem(idSuppyAddress);

            SuppyAddress_AddEdit aesuppyAddress = new SuppyAddress_AddEdit(suppyAddress);

            if (aesuppyAddress.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                loadData();
        }

        private void _dgvSuppyAddress_Resize(object sender, EventArgs e)
        {
            ResizeDGV();
        }
    }
}
