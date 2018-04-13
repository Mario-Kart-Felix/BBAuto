using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Lists;

namespace BBAuto.App.AddEdit
{
  public partial class FuelCard_AddEdit : Form
  {
    private FuelCard _fuelCard;
    private FuelCardDriverList fuelCardDriverList;

    private WorkWithForm _workWithForm;

    public FuelCard_AddEdit(FuelCard fuelCard)
    {
      InitializeComponent();

      fuelCardDriverList = FuelCardDriverList.getInstance();

      _fuelCard = fuelCard;
    }

    private void FuelCard_AddEdit_Load(object sender, EventArgs e)
    {
      loadDictionaries();

      LoadData();

      loadDriverList();

      EnterTextBox();
      LeaveTextBox();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_fuelCard.Id == 0);
    }

    private void loadDictionaries()
    {
      Regions regions = Regions.getInstance();
      cbRegion.DataSource = regions.ToDataTable();
      cbRegion.ValueMember = "id";
      cbRegion.DisplayMember = "Название";

      FuelCardTypes fuelCardTypes = FuelCardTypes.getInstance();
      cbFuelCardType.DataSource = fuelCardTypes.ToDataTable();
      cbFuelCardType.ValueMember = "id";
      cbFuelCardType.DisplayMember = "Название";
    }

    private void LoadData()
    {
      tbNumber.Text = _fuelCard.Number;
      cbRegion.SelectedValue = _fuelCard.RegionID;
      tbPin.Text = _fuelCard.Pin;
      cbFuelCardType.SelectedValue = _fuelCard.FuelCardTypeID;

      chbNoEnd.Checked = _fuelCard.IsNoEnd;

      if (!chbNoEnd.Checked)
        dtpDateEnd.Value = Convert.ToDateTime(_fuelCard.DateEnd);

      chbLost.Checked = _fuelCard.IsLost;

      tbComment.Text = _fuelCard.Comment;
    }

    private void loadDriverList()
    {
      _dgv.DataSource = fuelCardDriverList.ToDataTable(_fuelCard);

      _dgv.Columns[0].Visible = false;
      _dgv.Columns[1].Visible = false;
      _dgv.Columns["Номер"].Visible = false;
      _dgv.Columns["Регион"].Visible = false;
      _dgv.Columns["Срок действия"].Visible = false;
      _dgv.Columns["Фирма"].Visible = false;
    }

    private void chbNoEnd_CheckedChanged(object sender, EventArgs e)
    {
      label5.Visible = !chbNoEnd.Checked;
      dtpDateEnd.Visible = !chbNoEnd.Checked;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        Save();

        _fuelCard.AddEmptyDriver();

        DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }

    private void Save()
    {
      try
      {
        CopyFields();

        _fuelCard.Save();
      }
      catch (NullReferenceException)
      {
        MessageBox.Show("Не удалось сохранить топливную карту. Не выбран элемент из списка", "Ошибка",
          MessageBoxButtons.OK, MessageBoxIcon.Error);
        this.DialogResult = System.Windows.Forms.DialogResult.None;
      }
    }

    private void CopyFields()
    {
      _fuelCard.Number = tbNumber.Text;
      _fuelCard.RegionID = cbRegion.SelectedValue.ToString();
      _fuelCard.Pin = tbPin.Text;
      _fuelCard.FuelCardTypeID = cbFuelCardType.SelectedValue.ToString();

      _fuelCard.IsNoEnd = chbNoEnd.Checked;
      if (!chbNoEnd.Checked)
        _fuelCard.DateEnd = dtpDateEnd.Value;

      _fuelCard.IsLost = chbLost.Checked;
      _fuelCard.Comment = tbComment.Text;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      Save();

      try
      {
        ShowAddEditFuelCardDriver(_fuelCard.CreateFuelCardDriver());
      }
      catch (NullReferenceException)
      {
        MessageBox.Show("Ошибка", "Топливная карта не сохранена", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void _dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idFuelCardDriver = GetFuelCardDriverID();

      FuelCardDriver fuelCardDriver = fuelCardDriverList.getItem(idFuelCardDriver);

      ShowAddEditFuelCardDriver(fuelCardDriver);
    }

    private void ShowAddEditFuelCardDriver(FuelCardDriver fuelCardDriver)
    {
      FuelCardDriver_AddEdit fuelCardDriverAddEdit = new FuelCardDriver_AddEdit(fuelCardDriver);
      if (fuelCardDriverAddEdit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        loadDriverList();
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show("Удалить запись о водителе?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
          System.Windows.Forms.DialogResult.Yes)
      {
        int idFuelCardDriver = GetFuelCardDriverID();

        fuelCardDriverList.Delete(idFuelCardDriver);

        loadDriverList();
      }
    }

    private int GetFuelCardDriverID()
    {
      int idFuelCardDriver = 0;
      int.TryParse(_dgv.Rows[_dgv.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out idFuelCardDriver);

      return idFuelCardDriver;
    }

    private void tbNumber_Enter(object sender, EventArgs e)
    {
      EnterTextBox();
    }

    private void tbNumber_Leave(object sender, EventArgs e)
    {
      LeaveTextBox();
    }

    private void EnterTextBox()
    {
      tbNumber.Text = tbNumber.Text.Replace(" ", "");
    }

    private void LeaveTextBox()
    {
      if ((string.IsNullOrEmpty(tbNumber.Text)) || (cbFuelCardType.SelectedValue == null))
        return;

      tbNumber.Text = (cbFuelCardType.SelectedValue.ToString() == "1")
        ? tbNumber.Text.Insert(1, " ").Insert(5, " ").Insert(9, " ")
        : tbNumber.Text.Insert(6, " ").Insert(14, " ");
    }
  }
}
