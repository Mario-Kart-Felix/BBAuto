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
    public partial class CarDoc_AddEdit : Form
    {
        private CarDoc _carDoc;

        private WorkWithForm _workWithForm;

        public CarDoc_AddEdit(CarDoc carDoc)
        {
            InitializeComponent();

            _carDoc = carDoc;
        }

        private void CarDoc_AddEdit_Load(object sender, EventArgs e)
        {
            fillFields();

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.SetEditMode(_carDoc.IsEqualsID(0));
        }

        private void fillFields()
        {
            tbName.Text = _carDoc.name;
            TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
            tbFile.Text = _carDoc.file;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                _carDoc.name = tbName.Text;
                TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
                _carDoc.file = tbFile.Text;

                _carDoc.Save();

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }
    }
}