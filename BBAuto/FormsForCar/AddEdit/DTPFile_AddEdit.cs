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
    public partial class DTPFile_AddEdit : Form
    {
        private DTPFile _dtpFile;

        private WorkWithForm _workWithForm;

        public DTPFile_AddEdit(DTPFile dtpFile)
        {
            InitializeComponent();

            _dtpFile = dtpFile;
        }

        private void DTPFile_AddEdit_Load(object sender, EventArgs e)
        {
            FillFields();

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.SetEditMode(_dtpFile.IsEqualsID(0));
        }

        private void FillFields()
        {
            tbName.Text = _dtpFile.name;
            TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
            tbFile.Text = _dtpFile.file;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                _dtpFile.name = tbName.Text;
                TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
                _dtpFile.file = tbFile.Text;

                _dtpFile.Save();

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }
    }
}
