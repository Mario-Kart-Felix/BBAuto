using System;
using System.Windows.Forms;
using BBAuto.App.AddEdit;
using BBAuto.Logic.Common;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Presenters;

namespace BBAuto.App.Dictionary
{
  public partial class formMailText : Form
  {
    private readonly MailTextList _mailTextList;

    public formMailText()
    {
      InitializeComponent();

      _mailTextList = MailTextList.getInstance();
    }

    private void formMailText_Load(object sender, EventArgs e)
    {
      loadData();
    }

    private void loadData()
    {
      dgv.DataSource = _mailTextList.ToDataTable();
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
