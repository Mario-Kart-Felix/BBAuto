using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Static;
using BBAuto.Logic.Tables;

namespace BBAuto.App.AddEdit
{
  public partial class MyPoint_AddEdit : Form
  {
    private MyPoint _mypoint;

    private WorkWithForm _workWithForm;

    public MyPoint_AddEdit(MyPoint mypoint)
    {
      InitializeComponent();

      _mypoint = mypoint;
    }

    private void Point_AddEdit_Load(object sender, EventArgs e)
    {
      Regions regions = Regions.getInstance();

      lbRegion.Text = string.Concat("Регион: ", regions.getItem(_mypoint.RegionID));
      tbName.Text = _mypoint.Name;

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.EditModeChanged += SetEnable;
      _workWithForm.SetEditMode(_mypoint.ID == 0);
    }

    private void SetEnable(Object sender, EditModeEventArgs e)
    {
      if (User.GetRole() == RolesList.AccountantWayBill)
      {
        _workWithForm.SetEnableValue(btnSave, true);
      }
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        if (tbName.Text == string.Empty)
        {
          MessageBox.Show("Введите название", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
        else
        {
          _mypoint.Name = tbName.Text;
          _mypoint.Save();
        }

        DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
