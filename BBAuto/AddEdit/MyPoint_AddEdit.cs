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
    public partial class MyPoint_AddEdit : Form
    {
        private MyPoint _mypoint;

        private WorkWithForm _workWithForm;

        public MyPoint_AddEdit(MyPoint mypoint)
        {
            InitializeComponent();

            _mypoint = mypoint;
        }

        private void Point_AddEdit_Load(object sender, EventArgs e)
        {
            loadDictionary();

            FillFields();

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.SetEditMode(_mypoint.IsEqualsID(0));
        }

        private void loadDictionary()
        {
            Regions regions = Regions.getInstance();

            DataTable dt = regions.ToDataTable();

            cbRegion.DataSource = dt;
            cbRegion.ValueMember = dt.Columns[0].ColumnName;
            cbRegion.DisplayMember = dt.Columns[1].ColumnName;
        }
        
        private void FillFields()
        {
            cbRegion.SelectedValue = _mypoint.RegionID;
            tbName.Text = _mypoint.Name;
        }

        private void btnSave_Click(object sender, EventArgs e)
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
                    _mypoint.Name = tbName.Text;
                    _mypoint.RegionID = Convert.ToInt32(cbRegion.SelectedValue);
                    _mypoint.Save();
                }

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            else
                _workWithForm.SetEditMode(true);
        }
    }
}
