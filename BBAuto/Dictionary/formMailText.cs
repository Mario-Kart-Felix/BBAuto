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
using BBAuto.Domain.Presenter;

namespace BBAuto
{
    public partial class formMailText : Form
    {
        private MailTextList mailTextList;

        public formMailText()
        {
            InitializeComponent();

            mailTextList = MailTextList.getInstance();
        }

        private void formMailText_Load(object sender, EventArgs e)
        {
            loadData();
        }

        private void loadData()
        {
            dgv.DataSource = mailTextList.ToDataTable();
            dgv.Columns[0].Visible = false;
            resizeDGV();
        }

        private void dgv_Resize(object sender, EventArgs e)
        {
            resizeDGV();
        }

        private void resizeDGV()
        {
            dgv.Columns[1].Width = dgv.Width;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ShowAddEdit(new MailText());
        }
        
        private void dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int idMailText;
            int.TryParse(dgv.Rows[dgv.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out idMailText);

            MailTextList mailTextList = MailTextList.getInstance();
            MailText mailText = mailTextList.getItem(idMailText);

            ShowAddEdit(mailText);
        }

        private void ShowAddEdit(MailText mailText)
        {
            Dictionary_AddEdit view = new Dictionary_AddEdit("Текст уведомления");
            DictionaryPresenter presenter = new DictionaryPresenter(view, mailText);
            if (view.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                loadData();
        }
    }
}
