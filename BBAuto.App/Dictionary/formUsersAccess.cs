using System;
using System.Windows.Forms;
using BBAuto.App.AddEdit;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Lists;

namespace BBAuto.App.Dictionary
{
  public partial class formUsersAccess : Form
  {
    private readonly UserAccessList _userAccessList;

    public formUsersAccess()
    {
      InitializeComponent();

      _userAccessList = UserAccessList.getInstance();
    }

    private void formUsersAccess_Load(object sender, EventArgs e)
    {
      loadData();
    }

    private void loadData()
    {
      _dgvUserAccess.DataSource = _userAccessList.ToDataTable();
      _dgvUserAccess.Columns[0].Visible = false;
      resizeDGV();
    }

    private void resizeDGV()
    {
      int colSize = _dgvUserAccess.Width / 3;

      _dgvUserAccess.Columns[1].Width = colSize;
      _dgvUserAccess.Columns[2].Width = colSize;
      _dgvUserAccess.Columns[3].Width = colSize;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      UserAccess userAccess = new UserAccess();
      UserAccess_AddEdit dAE = new UserAccess_AddEdit(userAccess);
      if (dAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        _userAccessList.Add(userAccess);
        loadData();
      }
    }

    private void _dgvUserAccess_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idDiller = Convert.ToInt32(_dgvUserAccess.Rows[_dgvUserAccess.SelectedCells[0].RowIndex].Cells[0].Value);

      UserAccess userAccess = _userAccessList.getItem(idDiller);

      UserAccess_AddEdit dAE = new UserAccess_AddEdit(userAccess);
      if (dAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        loadData();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      int idUserAccess = Convert.ToInt32(_dgvUserAccess.Rows[_dgvUserAccess.SelectedCells[0].RowIndex].Cells[0].Value);
      _userAccessList.Delete(idUserAccess);
    }
  }
}
