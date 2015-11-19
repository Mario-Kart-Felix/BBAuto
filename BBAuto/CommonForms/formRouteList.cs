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
    public partial class formRouteList : Form
    {
        private MainDGV _dgvMain;

        private RouteList _routeList;

        public formRouteList()
        {
            InitializeComponent();

            _routeList = RouteList.getInstance();
        }

        private void formRouteList_Load(object sender, EventArgs e)
        {
            loadData();

            _dgvMain = new MainDGV(dgv);

            ResizeDGV();
        }

        private void dgv_Resize(object sender, EventArgs e)
        {
            ResizeDGV();
        }

        private void ResizeDGV()
        {
            dgv.Columns[1].Width = dgv.Width / 3;
            dgv.Columns[2].Width = dgv.Width / 3;
            dgv.Columns[3].Width = dgv.Width / 3;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            openAddEdit(new Route());
        }
        
        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            openAddEdit(_routeList.getItem(_dgvMain.GetID()));
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            _routeList.Delete(_dgvMain.GetID());

            loadData();
        }

        private void openAddEdit(Route route)
        {
            Route_AddEdit routeAE = new Route_AddEdit(route);
            if (routeAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                loadData();
        }

        private void loadData()
        {
            dgv.DataSource = _routeList.ToDataTable();

            if (dgv.Columns.Count > 0)
                dgv.Columns[0].Visible = false;
        }
    }
}
