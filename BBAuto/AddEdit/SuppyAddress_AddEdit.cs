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
    public partial class SuppyAddress_AddEdit : Form
    {
        private SuppyAddress _suppyAddress;

        private WorkWithForm _workWithForm;

        public SuppyAddress_AddEdit(SuppyAddress suppyAddress)
        {
            InitializeComponent();

            _suppyAddress = suppyAddress;
        }

        private void aeSuppyAddress_Load(object sender, EventArgs e)
        {
            loadRegions();

            loadData();

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.EditModeChanged += EnableIfAccountWayBill;
            _workWithForm.SetEditMode(_suppyAddress.IsEqualsID(0));
        }

        private void EnableIfAccountWayBill(Object sender, EditModeEventArgs e)
        {
            if (!User.IsFullAccess())
                _workWithForm.SetEnableValue(btnSave, User.GetRole() == RolesList.AccountantWayBill);
        }

        private void loadData()
        {
            cbRegion.SelectedValue = _suppyAddress.ID;
        }

        private void loadRegions()
        {
            Regions regions = Regions.getInstance();

            DataTable dt = regions.ToDataTable();

            cbRegion.DataSource = dt;
            cbRegion.ValueMember = dt.Columns[0].ColumnName;
            cbRegion.DisplayMember = dt.Columns[1].ColumnName;
        }

        private void loadMyPoints()
        {
            if (cbRegion.SelectedValue == null)
                return;

            MyPointList myPointList = MyPointList.getInstance();

            int idRegion;
            int.TryParse(cbRegion.SelectedValue.ToString(), out idRegion);

            DataTable dt = myPointList.ToDataTable(idRegion);

            cbMyPoint.DataSource = dt;
            cbMyPoint.ValueMember = dt.Columns[0].ColumnName;
            cbMyPoint.DisplayMember = dt.Columns[1].ColumnName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                if (cbMyPoint.SelectedValue == null)
                {
                    MessageBox.Show("Выберите адрес подачи", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idPoint;
                int.TryParse(cbMyPoint.SelectedValue.ToString(), out idPoint);
                MyPointList myPointList = MyPointList.getInstance();
                MyPoint point = myPointList.getItem(idPoint);
                _suppyAddress.Point = point;

                _suppyAddress.Save();

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }

        private void cbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadMyPoints();
        }
    }
}
