using System;
using System.Windows.Forms;
using BBAuto.App.AddEdit;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Presenters;

namespace BBAuto.App.Dictionary
{
  public partial class FormDealerList : Form
  {
    private readonly DealerList _dealerList;

    public FormDealerList()
    {
      InitializeComponent();

      _dealerList = DealerList.getInstance();
    }

    private void DillerList_Load(object sender, EventArgs e)
    {
      loadData();
    }

    private void loadData()
    {
      _dgv.DataSource = _dealerList.ToDataTable();
      _dgv.Columns[0].Visible = false;
      resizeDGV();
    }

    private void _dgvDiller_Resize(object sender, EventArgs e)
    {
      resizeDGV();
    }

    private void resizeDGV()
    {
      int halfSize = _dgv.Width / 2;

      _dgv.Columns[1].Width = halfSize;
      _dgv.Columns[2].Width = halfSize;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      ShowAddEdit(new Dealer());
    }

    private void _dgvDiller_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idDiller;
      int.TryParse(_dgv.Rows[_dgv.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out idDiller);

      DealerList dillerList = DealerList.getInstance();
      Dealer diller = dillerList.getItem(idDiller);

      ShowAddEdit(diller);
    }

    private void ShowAddEdit(Dealer diller)
    {
      Dictionary_AddEdit view = new Dictionary_AddEdit("Карточка \"Дилера\"");
      DictionaryPresenter presenter = new DictionaryPresenter(view, diller);
      if (view.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        loadData();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      int idDiller = Convert.ToInt32(_dgv.Rows[_dgv.SelectedCells[0].RowIndex].Cells[0].Value);
      _dealerList.Delete(idDiller);
    }
  }
}
