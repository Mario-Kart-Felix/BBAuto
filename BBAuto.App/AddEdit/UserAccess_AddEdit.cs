using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Lists;

namespace BBAuto.App.AddEdit
{
  public partial class UserAccess_AddEdit : Form
  {
    private UserAccess _userAccess;

    private WorkWithForm _workWithForm;

    public UserAccess_AddEdit(UserAccess userAccess)
    {
      InitializeComponent();
      _userAccess = userAccess;
    }

    private void aeUserAccess_Load(object sender, EventArgs e)
    {
      loadDictionary();

      if (_userAccess.Driver != null)
        cbDriver.SelectedValue = _userAccess.Driver.Id;
      cbRole.SelectedValue = _userAccess.RoleId;

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_userAccess.Id == 0);
    }

    private void loadDictionary()
    {
      DriverList driverList = DriverList.getInstance();
      cbDriver.SelectedIndexChanged -= cbDriver_SelectedIndexChanged;
      cbDriver.DataSource = driverList.ToDataTable(true);
      cbDriver.DisplayMember = "ФИО";
      cbDriver.ValueMember = "id";
      cbDriver.SelectedIndexChanged += cbDriver_SelectedIndexChanged;

      Roles roles = Roles.getInstance();
      cbRole.DataSource = roles.ToDataTable();
      cbRole.DisplayMember = "Название";
      cbRole.ValueMember = "id";
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        _userAccess.Driver = DriverList.getInstance().getItem(Convert.ToInt32(cbDriver.SelectedValue));
        _userAccess.RoleId = Convert.ToInt32(cbRole.SelectedValue);
        _userAccess.Save();

        DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }

    private void cbDriver_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (cbDriver.SelectedValue == null)
        lbLogin.Text = string.Empty;
      else
      {
        DriverList driverList = DriverList.getInstance();

        int idDriver;
        int.TryParse(cbDriver.SelectedValue.ToString(), out idDriver);

        Driver driver = driverList.getItem(idDriver);
        lbLogin.Text = driver.Login;
      }
    }
  }
}
