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
            loadDictionary();

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
            tbAddress.Text = _suppyAddress.name;
        }

        private void loadDictionary()
        {
            Regions regions = Regions.getInstance();

            cbRegion.DataSource = regions.ToDataTable();
            cbRegion.DisplayMember = "Название";
            cbRegion.ValueMember = "id";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                _suppyAddress.ID = cbRegion.SelectedValue.ToString();
                _suppyAddress.name = tbAddress.Text;

                _suppyAddress.Save();

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }
    }
}
