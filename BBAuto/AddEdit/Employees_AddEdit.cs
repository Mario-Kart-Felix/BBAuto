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
    public partial class Employees_AddEdit : Form
    {
        private Employees _employees;

        private WorkWithForm _workWithForm;

        public Employees_AddEdit(Employees employees)
        {
            InitializeComponent();

            _employees = employees;
        }

        private void aeEmployees_Load(object sender, EventArgs e)
        {
            loadDictionaries();

            loadData();

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.EditModeChanged += EnableIfAccountWayBill;
            _workWithForm.SetEditMode(_employees.IsEqualsID(0));
        }

        private void EnableIfAccountWayBill(Object sender, EditModeEventArgs e)
        {
            if (!User.IsFullAccess())
                _workWithForm.SetEnableValue(btnSave, User.GetRole() == RolesList.AccountantWayBill);
        }

        private void loadDictionaries()
        {
            Regions regions = Regions.getInstance();
            cbRegion.DataSource = regions.ToDataTable();
            cbRegion.ValueMember = "id";
            cbRegion.DisplayMember = "Название";

            EmployeesNames employeesNames = EmployeesNames.getInstance();
            cbEmployeesName.DataSource = employeesNames.ToDataTable();
            cbEmployeesName.ValueMember = "id";
            cbEmployeesName.DisplayMember = "Название";

            DriverList driverList = DriverList.getInstance();
            cbDriver.DataSource = driverList.ToDataTable();
            cbDriver.ValueMember = "id";
            cbDriver.DisplayMember = "ФИО";
        }

        private void loadData()
        {
            cbRegion.SelectedValue = _employees.ID;
            cbEmployeesName.SelectedValue = _employees.IDEmployeesName;
            cbDriver.SelectedValue = _employees.IDDriver;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                _employees.ID = cbRegion.SelectedValue.ToString();
                _employees.IDEmployeesName = cbEmployeesName.SelectedValue.ToString();
                _employees.IDDriver = cbDriver.SelectedValue.ToString();

                _employees.Save();

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }
    }
}