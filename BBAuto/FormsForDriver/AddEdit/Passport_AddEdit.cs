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
    public partial class Passport_AddEdit : Form
    {
        private Passport _passport;

        private WorkWithForm _workWithForm;

        public Passport_AddEdit(Passport passport)
        {
            InitializeComponent();

            _passport = passport;
        }

        private void Passport_AddEdit_Load(object sender, EventArgs e)
        {
            fillFields();

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.SetEditMode(_passport.IsEqualsID(0));
        }

        private void fillFields()
        {
            tbFirstName.Text = _passport.name;
            tbLastName.Text = _passport.lastName;
            tbSecondName.Text = _passport.secondName;
            mtbNumber.Text = _passport.number;
            mtbGiveDate.Text = _passport.GiveDate;
            tbGiveOrg.Text = _passport.giveOrg;
            tbAddress.Text = _passport.address;

            TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
            tbFile.Text = _passport.file;
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                copyFields();
                _passport.Save();
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }

        private void copyFields()
        {
            _passport.name = tbFirstName.Text;
            _passport.lastName = tbLastName.Text;
            _passport.secondName = tbSecondName.Text;
            _passport.number = mtbNumber.Text;
            _passport.GiveDate = mtbGiveDate.Text;
            _passport.giveOrg = tbGiveOrg.Text;
            _passport.address = tbAddress.Text;

            TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
            _passport.file = tbFile.Text;
        }
    }
}
