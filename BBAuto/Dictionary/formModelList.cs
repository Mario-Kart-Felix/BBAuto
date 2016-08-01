using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
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
    public partial class formModelList : Form
    {
        private bool _load;

        public formModelList()
        {
            InitializeComponent();

            loadMark();
            loadModel();
        }

        private void loadMark()
        {
            _load = false;
            cbMark.DataSource = OneStringDictionary.getDataTable("Mark");
            cbMark.DisplayMember = "Название";
            cbMark.ValueMember = "mark_id";
            _load = true;
        }

        private void loadModel()
        {
            if (_load)
            {
                int idMark = 0;
                int.TryParse(cbMark.SelectedValue.ToString(), out idMark);
                
                ModelList models = ModelList.getInstance();

                _dgv.DataSource = models.ToDataTable(idMark);

                _dgv.Columns[0].Visible = false;
                _dgv.Columns[1].Width = _dgv.Width;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int markID = Convert.ToInt32(cbMark.SelectedValue);

            Model model = new Model(markID);
            Model_AddEdit modelAddEdit = new Model_AddEdit(model);

            if (modelAddEdit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ModelList models = ModelList.getInstance();
                models.Add(model);

                loadModel();
            }
        }

        private void cbMark_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_load)
            {
                loadModel();
            }
        }

        private void _dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((e.ColumnIndex < 0) || (e.RowIndex < 0))
                return;

            int idModel = Convert.ToInt32(_dgv.Rows[e.RowIndex].Cells[0].Value);

            ModelList models = ModelList.getInstance();
            
            Model_AddEdit aeM = new Model_AddEdit(models.getItem(idModel));
            if (aeM.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                loadModel();
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            ModelList models = ModelList.getInstance();

            foreach (DataGridViewCell cell in _dgv.SelectedCells)
            {
                int idModel = 0;
                int.TryParse(_dgv.Rows[cell.RowIndex].Cells[0].Value.ToString(), out idModel);

                models.Delete(idModel);
            }

            loadModel();
        }
    }
}
