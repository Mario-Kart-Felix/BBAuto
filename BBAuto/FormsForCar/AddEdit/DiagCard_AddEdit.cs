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
    public partial class DiagCard_AddEdit : Form
    {
        private DiagCard _diagCard;

        private WorkWithForm _workWithForm;

        public DiagCard_AddEdit(DiagCard diagCard)
        {
            InitializeComponent();

            _diagCard = diagCard;
        }

        private void DiagCard_AddEdit_Load(object sender, EventArgs e)
        {
            FillFields();

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.SetEditMode(_diagCard.IsEqualsID(0));
        }

        private void FillFields()
        {
            tbNumber.Text = _diagCard.name;
            dtpDate.Value = _diagCard.date;

            TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
            tbFile.Text = _diagCard.file;

            lbCarInfo.Text = _diagCard.GetCar().ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                _diagCard.name = tbNumber.Text;
                _diagCard.date = dtpDate.Value.Date;

                TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
                _diagCard.file = tbFile.Text;

                _diagCard.Save();

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }
    }
}
