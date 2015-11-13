﻿using System;
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
    public partial class FuelCardDriver_AddEdit : Form
    {
        private FuelCardDriver _fuelCardDriver;

        private WorkWithForm _workWithForm;

        public FuelCardDriver_AddEdit(FuelCardDriver fuelCardDriver)
        {
            InitializeComponent();

            _fuelCardDriver = fuelCardDriver;
        }

        private void FuelCardDriver_AddEdit_Load(object sender, EventArgs e)
        {
            LoadDictionary();

            LoadData();

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.SetEditMode(_fuelCardDriver.IsEqualsID(0));
        }

        private void LoadDictionary()
        {
            DriverList driverList = DriverList.getInstance();
            cbDriver.DataSource = driverList.ToDataTable(_fuelCardDriver.DriverID != "0");
            cbDriver.DisplayMember = "ФИО";
            cbDriver.ValueMember = "id";
        }

        private void LoadData()
        {
            cbDriver.SelectedValue = _fuelCardDriver.DriverID;
            dtpDateBegin.Value = _fuelCardDriver.DateBegin;

            chbNotUse.Checked = _fuelCardDriver.IsNotUse;

            if (chbNotUse.Checked)
                dtpDateEnd.Value = _fuelCardDriver.DateEnd;
        }

        private void chbNotUse_CheckedChanged(object sender, EventArgs e)
        {
            label3.Visible = chbNotUse.Checked;
            dtpDateEnd.Visible = chbNotUse.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                _fuelCardDriver.DriverID = cbDriver.SelectedValue.ToString();
                _fuelCardDriver.DateBegin = dtpDateBegin.Value;

                if (chbNotUse.Checked)
                {
                    if ((!_fuelCardDriver.IsNotUse) && (chbNotUse.Checked))
                    {
                        FuelCard fuelCard = _fuelCardDriver.fuelCard;
                        FuelCardDriver fuelCardDriver = fuelCard.CreateFuelCardDriver();
                        fuelCardDriver.Save();
                    }

                    _fuelCardDriver.DateEnd = dtpDateEnd.Value.Date;
                }
                else
                    _fuelCardDriver.IsNotUse = false;
                                    
                _fuelCardDriver.Save();

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }
    }
}