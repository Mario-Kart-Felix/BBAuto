using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Common;

namespace BBAuto
{
    public partial class formTemplateList : Form
    {
        private MainDGV _dgvMain;
        private TemplateList _templateList;

        public formTemplateList()
        {
            InitializeComponent();

            _templateList = TemplateList.getInstance();
        }

        private void TemplateList_Load(object sender, EventArgs e)
        {
            loadData();

            _dgvMain = new MainDGV(_dgvTemplate);

            ResizeDGV();
        }

        private void loadData()
        {
            _dgvTemplate.DataSource = _templateList.ToDataTable();

            if (_dgvTemplate.Columns.Count > 0)
                _dgvTemplate.Columns[0].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            openAddEdit(new Template());
        }

        private void _dgvTemplate_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            openAddEdit(_templateList.getItem(_dgvMain.GetID()));
        }

        private void openAddEdit(Template template)
        {
            TemplateAddEdit templateAE = new TemplateAddEdit(template);
            if (templateAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                loadData();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            _templateList.Delete(_dgvMain.GetID());

            loadData();
        }
        
        private void _dgvTemplate_Resize(object sender, EventArgs e)
        {
            ResizeDGV();
        }

        private void ResizeDGV()
        {
            _dgvTemplate.Columns[1].Width = _dgvTemplate.Width / 2;
            _dgvTemplate.Columns[2].Width = _dgvTemplate.Width / 2;
        }
    }
}
