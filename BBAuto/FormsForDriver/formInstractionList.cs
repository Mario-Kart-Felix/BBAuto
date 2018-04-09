using System;
using System.Windows.Forms;
using BBAuto.App.FormsForDriver.AddEdit;
using BBAuto.App.GUI;
using BBAuto.Logic.Common;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.App.FormsForDriver
{
  public partial class formInstractionList : Form
  {
    private Driver _driver;
    private InstractionList instractionList;

    public formInstractionList(Driver driver)
    {
      InitializeComponent();

      _driver = driver;

      instractionList = InstractionList.getInstance();
    }

    private void InstractionList_Load(object sender, EventArgs e)
    {
      LoadData();
      SetEnable();
    }

    private void LoadData()
    {
      dgvInstractions.DataSource = instractionList.ToDataTable(_driver);
      dgvInstractions.Columns[0].Visible = false;

      foreach (DataGridViewRow row in dgvInstractions.Rows)
      {
        int id = 0;
        int.TryParse(row.Cells[0].Value.ToString(), out id);

        Instraction instraction = instractionList.getItem(id);

        if (instraction.File != string.Empty)
          row.DefaultCellStyle.BackColor = BBColors.bbGreen3;
      }
    }

    private void SetEnable()
    {
      btnAdd.Enabled = User.IsFullAccess();
      btnDelete.Enabled = User.IsFullAccess();
    }

    private void add_Click(object sender, EventArgs e)
    {
      Instraction_AddEdit instAE = new Instraction_AddEdit(_driver.createInstraction());
      if (instAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        LoadData();
    }

    private void delete_Click(object sender, EventArgs e)
    {
      int idInstraction =
        Convert.ToInt32(dgvInstractions.Rows[dgvInstractions.SelectedCells[0].RowIndex].Cells[0].Value);
      instractionList.Delete(idInstraction);

      LoadData();
    }

    private void dgvInstractions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      if (isCellNoHeader(e.RowIndex))
      {
        int idInstraction = Convert.ToInt32(dgvInstractions.Rows[e.RowIndex].Cells[0].Value);
        Instraction instraction = instractionList.getItem(idInstraction);

        if ((dgvInstractions.Columns[e.ColumnIndex].HeaderText == "Номер") && (instraction.File != string.Empty))
        {
          WorkWithFiles.OpenFile(instraction.File);
        }
        else
        {
          Instraction_AddEdit instAE = new Instraction_AddEdit(instraction);
          if (instAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            LoadData();
        }
      }
    }

    private bool isCellNoHeader(int rowIndex)
    {
      return rowIndex >= 0;
    }
  }
}
