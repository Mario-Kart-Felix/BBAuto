using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Domain;

namespace BBAuto
{
    public partial class Invoice_AddEdit : Form
    {
        private Invoice _invoice;
        private bool _load;

        private WorkWithForm _workWithForm;

        private CheckBox _check;

        public Invoice_AddEdit(Invoice invoice)
        {
            InitializeComponent();

            _invoice = invoice;

            _check = new CheckBox();

            if (_invoice.IsEqualsID(0))
            {
                _check.Location = new System.Drawing.Point(12, 225);
                _check.Width = 250;
                _check.Text = "удалить сдающего из списка Водителей";
                this.Controls.Add(_check);
            }

            lbMoveCar.Text = "Перемещение автомобиля " + _invoice.car.ToString();
        }

        private void Invoice_AddEdit_Load(object sender, EventArgs e)
        {
            loadData();

            this.Text = "Перемещение №" + _invoice.Number;

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.EditModeChanged += EditModeChanged;
            _workWithForm.SetEditMode(_invoice.IsEqualsID(0));
        }

        private void EditModeChanged(Object sender, EditModeEventArgs e)
        {
            if (!_invoice.IsEqualsID(0))
            {
                cbDriverFrom.Enabled = false;
                cbRegionFrom.Enabled = false;
                dtpDate.Enabled = false;
            }
        }

        private void loadData()
        {
            loadDictionary();
            
            lbNumber.Text = "Накладная №" + _invoice.Number;

            cbRegionFrom.SelectedValue = _invoice.RegionFromID;
            cbRegionTo.SelectedValue = _invoice.RegionToID;
            cbDriverFrom.SelectedValue = _invoice.DriverFromID;
            cbDriverTo.SelectedValue = _invoice.DriverToID;

            dtpDate.Value = _invoice.Date;
            mtbDateMove.Text = _invoice.DateMove;

            TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
            tbFile.Text = _invoice.File;
        }

        private void loadDictionary()
        {
            _load = false;

            setDataSourceRegion(cbRegionFrom);
            setDataSourceRegion(cbRegionTo);

            setDataSourceDriver(cbDriverFrom);
            setDataSourceDriver(cbDriverTo);
            
            _load = true;
        }

        private void setDataSourceDriver(ComboBox combo)
        {
            DriverList driverList = DriverList.getInstance();

            combo.DataSource = driverList.ToDataTable(!_invoice.IsEqualsID(0));
            combo.DisplayMember = "ФИО";
            combo.ValueMember = "id";            
        }

        private void setDataSourceRegion(ComboBox combo)
        {
            combo.DataSource = OneStringDictionary.getDataTable("Region");
            combo.DisplayMember = "Название";
            combo.ValueMember = "region_id";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                _invoice.DriverFromID = cbDriverFrom.SelectedValue.ToString();
                _invoice.DriverToID = cbDriverTo.SelectedValue.ToString();
                _invoice.RegionFromID = cbRegionFrom.SelectedValue.ToString();
                _invoice.RegionToID = cbRegionTo.SelectedValue.ToString();
                _invoice.Date = dtpDate.Value;
                _invoice.DateMove = mtbDateMove.Text;

                TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
                _invoice.File = tbFile.Text;

                _invoice.Save();

                if (_check.Checked)
                {
                    DriverList driverList = DriverList.getInstance();
                    Driver driver = driverList.getItem(Convert.ToInt32(cbDriverFrom.SelectedValue.ToString()));
                    driver.IsDriver = false;
                    driver.Save();
                }

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }

        private void cbRegionTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //changeDataSourceDriverTo();
        }

        private void changeDataSourceDriverTo()
        {
            if (isRegionToNotNull())
            {
                Region region = getRegion();

                DriverList driverList = DriverList.getInstance();
                cbDriverTo.DataSource = driverList.ToDataTableByRegion(region, !_invoice.IsEqualsID(0));
                cbDriverTo.DisplayMember = "ФИО";
                cbDriverTo.ValueMember = "id";
            }
        }

        private Region getRegion()
        {
            int idRegion = 0;
            int.TryParse(cbRegionTo.SelectedValue.ToString(), out idRegion);
            RegionList regionList = RegionList.getInstance();
            return regionList.getItem(idRegion);
        }

        private bool isRegionToNotNull()
        {
            return ((_load) && (cbRegionTo.SelectedValue != null));
        }

        private void cbDriverTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DriverList driverList = DriverList.getInstance();

            if (cbDriverTo.SelectedValue == null)
                return;

            int idDriver;
            if (int.TryParse(cbDriverTo.SelectedValue.ToString(), out idDriver))
            {
                Driver driver = driverList.getItem(idDriver);
                cbRegionTo.SelectedValue = driver.Region.ID;
            }
        }
    }
}
