using BBAuto.Domain.ForCar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BBAuto
{
    public partial class Model_AddEdit : Form
    {
        private Model _model;

        private WorkWithForm _workWithForm;

        public Model_AddEdit(Model model)
        {
            InitializeComponent();

            _model = model;
        }

        private void aeModel_Load(object sender, EventArgs e)
        {
            fillFields();

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.SetEditMode(_model.ID == 0);
        }

        private void fillFields()
        {
            tbName.Text = _model.Name;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                if (tbName.Text == string.Empty)
                {
                    MessageBox.Show("Введите название", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    _model.Name = tbName.Text;
                    _model.Save();
                }

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }
    }
}
