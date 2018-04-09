using BBAuto.Domain.Entities;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using System;
using System.Windows.Forms;

namespace BBAuto
{
  public partial class ListDTP : Form
  {
    private Car car;

    private DTPList dtpList;

    public ListDTP(Car car)
    {
      InitializeComponent();

      this.car = car;

      dtpList = DTPList.getInstance();

      loadDTP();
    }

    private void Add_Click(object sender, EventArgs e)
    {
      DTP_AddEdit aedtp = new DTP_AddEdit(car.createDTP());
      aedtp.ShowDialog();
    }

    private void delete_Click(object sender, EventArgs e)
    {
      int idDTP = Convert.ToInt32(_dgvDTP.Rows[_dgvDTP.SelectedCells[0].RowIndex].Cells[0].Value);

      dtpList.Delete(idDTP);

      loadDTP();
    }

    private void loadDTP()
    {
      _dgvDTP.DataSource = dtpList.ToDataTable(car);
    }

    private void _dgvDTP_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (isCellNoHeader(e.RowIndex))
      {
        int idDTP = Convert.ToInt32(_dgvDTP.Rows[e.RowIndex].Cells[0].Value);

        DTP dtp = dtpList.getItem(idDTP);

        DTP_AddEdit aedtp = new DTP_AddEdit(dtp);
        aedtp.ShowDialog();
      }
    }

    private bool isCellNoHeader(int rowIndex)
    {
      return rowIndex >= 0;
    }
  }
}
