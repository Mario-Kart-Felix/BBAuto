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
    public partial class TempMove_AddEdit : Form
    {
        private TempMove _tempMove;

        private WorkWithForm _workWithForm;

        public TempMove_AddEdit(TempMove tempMove)
        {
            InitializeComponent();

            _tempMove = tempMove;
        }

        private void TempMove_AddEdit_Load(object sender, EventArgs e)
        {
            loadData();

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.SetEditMode(_tempMove.IsEqualsID(0));
        }

        private void loadData()
        {
            loadDictionary();
            
            cbDriver.SelectedValue = _tempMove.IDDriver;
            cbCar.SelectedValue = _tempMove.IDCar;
            dtpDateBegin.Value = _tempMove.dateBegin;
            dtpDateEnd.Value = _tempMove.dateEnd;
        }

        private void loadDictionary()
        {
            DriverList driverList = DriverList.getInstance();

            cbDriver.DataSource = driverList.ToDataTable();
            cbDriver.DisplayMember = "ФИО";
            cbDriver.ValueMember = "id";

            CarList carList = CarList.getInstance();
            cbCar.DataSource = carList.ToDataTable(Status.Actual);
            cbCar.DisplayMember = "Регистрационный знак";
            cbCar.ValueMember = "id";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                _tempMove.IDDriver = cbDriver.SelectedValue.ToString();
                _tempMove.IDCar = cbCar.SelectedValue.ToString();
                _tempMove.dateBegin = Convert.ToDateTime(dtpDateBegin.Value);
                _tempMove.dateEnd = Convert.ToDateTime(dtpDateEnd.Value);

                _tempMove.Save();

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }
    }
}
