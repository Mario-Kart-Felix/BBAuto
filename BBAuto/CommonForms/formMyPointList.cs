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
    public partial class formMyPointList : Form
    {
        private MainDGV _dgvMain;

        private MyPointList _myPointList;

        public formMyPointList()
        {
            InitializeComponent();

            _myPointList = MyPointList.getInstance();
        }

        private void formMyPointList_Load(object sender, EventArgs e)
        {
            loadData();

            _dgvMain = new MainDGV(dgv);

            ResizeDGV();
        }

        private void loadData()
        {
            dgv.DataSource = _myPointList.ToDataTable();

            if (dgv.Columns.Count > 0)
                dgv.Columns[0].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            openAddEdit(new MyPoint());
        }

        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            openAddEdit(_myPointList.getItem(_dgvMain.GetID()));
        }

        private void openAddEdit(MyPoint myPoint)
        {
            MyPoint_AddEdit myPointAE = new MyPoint_AddEdit(myPoint);
            if (myPointAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                loadData();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            _myPointList.Delete(_dgvMain.GetID());

            loadData();
        }

        private void dgv_Resize(object sender, EventArgs e)
        {
            ResizeDGV();
        }

        private void ResizeDGV()
        {
            dgv.Columns[1].Width = dgv.Width / 2;
            dgv.Columns[2].Width = dgv.Width / 2;
        }
    }
}
