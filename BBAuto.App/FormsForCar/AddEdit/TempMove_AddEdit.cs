using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public partial class TempMove_AddEdit : Form
  {
    private readonly TempMove _tempMove;

    private WorkWithForm _workWithForm;

    public TempMove_AddEdit(TempMove tempMove)
    {
      InitializeComponent();

      _tempMove = tempMove;
    }

    private void TempMove_AddEdit_Load(object sender, EventArgs e)
    {
      loadData();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_tempMove.Id == 0);
    }

    private void loadData()
    {
      loadDictionary();

      if (_tempMove.Driver != null)
        cbDriver.SelectedValue = _tempMove.Driver.Id;

      cbCar.SelectedValue = _tempMove.Car.Id;
      dtpDateBegin.Value = _tempMove.DateBegin;
      dtpDateEnd.Value = _tempMove.DateEnd;
    }

    private void loadDictionary()
    {
      DriverList driverList = DriverList.getInstance();

      cbDriver.DataSource = driverList.ToDataTable();
      cbDriver.DisplayMember = "ФИО";
      cbDriver.ValueMember = "id";

      CarList carList = CarList.getInstance();
      cbCar.DataSource = carList.ToDataTable(Status.Actual);
      cbCar.DisplayMember = "Регистрационный знак";
      cbCar.ValueMember = "id";
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        _tempMove.Driver = DriverList.getInstance().getItem(Convert.ToInt32(cbDriver.SelectedValue));
        _tempMove.Car = CarList.getInstance().getItem(Convert.ToInt32(cbCar.SelectedValue));
        _tempMove.DateBegin = Convert.ToDateTime(dtpDateBegin.Value);
        _tempMove.DateEnd = Convert.ToDateTime(dtpDateEnd.Value);

        _tempMove.Save();

        DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
