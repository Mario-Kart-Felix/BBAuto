using System;
using System.Windows.Forms;
using BBAuto.App.AddEdit;
using BBAuto.Logic.Common;
using BBAuto.Logic.Lists;

namespace BBAuto.App.Dictionary
{
  public partial class formSuppyAddressList : Form
  {
    private readonly SuppyAddressList _suppyAddressList;

    public formSuppyAddressList()
    {
      InitializeComponent();

      _suppyAddressList = SuppyAddressList.getInstance();
    }

    private void formSuppyAddressList_Load(object sender, EventArgs e)
    {
      loadData();
    }

    private void loadData()
    {
      _dgvSuppyAddress.DataSource = _suppyAddressList.ToDataTable();
      _dgvSuppyAddress.Columns[0].Visible = false;
      ResizeDGV();
    }

    private void ResizeDGV()
    {
      _dgvSuppyAddress.Columns[1].Width = 100;
      _dgvSuppyAddress.Columns[2].Width = _dgvSuppyAddress.Width - 100;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      SuppyAddress suppyAddress = new SuppyAddress();

      SuppyAddress_AddEdit aesuppyAddress = new SuppyAddress_AddEdit(suppyAddress);

      if (aesuppyAddress.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        loadData();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      int idSuppyAddress = 0;
      int.TryParse(_dgvSuppyAddress.Rows[_dgvSuppyAddress.CurrentCell.RowIndex].Cells[0].Value.ToString(),
        out idSuppyAddress);

      _suppyAddressList.Delete(idSuppyAddress);

      loadData();
    }

    private void _dgvSuppyAddress_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      int idSuppyAddress = 0;
      int.TryParse(_dgvSuppyAddress.Rows[_dgvSuppyAddress.CurrentCell.RowIndex].Cells[0].Value.ToString(),
        out idSuppyAddress);

      SuppyAddress suppyAddress = _suppyAddressList.getItem(idSuppyAddress);

      SuppyAddress_AddEdit aesuppyAddress = new SuppyAddress_AddEdit(suppyAddress);

      if (aesuppyAddress.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        loadData();
    }

    private void _dgvSuppyAddress_Resize(object sender, EventArgs e)
    {
      ResizeDGV();
    }
  }
}
