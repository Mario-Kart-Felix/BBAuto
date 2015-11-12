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
    public partial class Grade_AddEdit : Form
    {
        private Grade _grade;

        private WorkWithForm _workWithForm;
        
        public Grade_AddEdit(Grade grade)
        {
            InitializeComponent();

            _grade = grade;
        }

        private void aeGrade_Load(object sender, EventArgs e)
        {
            loadTypeEngine();

            fillFields();

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.SetEditMode(_grade.IsEqualsID(0));
        }

        private void loadTypeEngine()
        {
            cbEngineType.DataSource = OneStringDictionary.getDataTable("EngineType");
            cbEngineType.DisplayMember = "Название";
            cbEngineType.ValueMember = "engineType_id";
        }

        private void fillFields()
        {
            tbName.Text = _grade.name;
            tbEPower.Text = _grade.ePower;
            tbEVol.Text = _grade.eVol;
            tbMaxLoad.Text = _grade.maxLoad;
            tbNoLoad.Text = _grade.noLoad;
            cbEngineType.SelectedValue = _grade.IDEngineType;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                if (!isFill())
                    return;

                _grade.name = tbName.Text;
                _grade.ePower = tbEPower.Text;
                _grade.eVol = tbEVol.Text;
                _grade.maxLoad = tbMaxLoad.Text;
                _grade.noLoad = tbNoLoad.Text;
                _grade.IDEngineType = cbEngineType.SelectedValue.ToString();

                _grade.Save();

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }

        private bool isFill()
        {
            if (tbName.Text == String.Empty)
            {
                MessageBox.Show("Введите название", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tbEPower.Text == String.Empty)
            {
                MessageBox.Show("Введите мощность двигателя", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tbEVol.Text == String.Empty)
            {
                MessageBox.Show("Введите объём двигателя", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tbMaxLoad.Text == String.Empty)
            {
                MessageBox.Show("Введите разрешенную максимальную массу", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (tbNoLoad.Text == String.Empty)
            {
                MessageBox.Show("Введите массу без нагрузки", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void isNumber(KeyPressEventArgs e)
        {
            if (!(Char.IsDigit(e.KeyChar)))
            {
                if (e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private void tbEVol_KeyPress(object sender, KeyPressEventArgs e)
        {
            isNumber(e);
        }

        private void tbEPower_KeyPress(object sender, KeyPressEventArgs e)
        {
            isNumber(e);
        }

        private void tbMaxLoad_KeyPress(object sender, KeyPressEventArgs e)
        {
            isNumber(e);
        }

        private void tbNoLoad_KeyPress(object sender, KeyPressEventArgs e)
        {
            isNumber(e);
        }
    }
}
