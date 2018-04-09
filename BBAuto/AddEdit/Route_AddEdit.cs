using System;
using System.Data;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;
using BBAuto.Logic.Tables;

namespace BBAuto.App.AddEdit
{
  public partial class Route_AddEdit : Form
  {
    private Route _route;
    private WorkWithForm _workWithForm;
    private int _idRegion;

    public Route_AddEdit(Route route, int idRegion)
    {
      InitializeComponent();

      _route = route;
      _idRegion = idRegion;

      Regions regions = Regions.getInstance();
      lbRegion.Text = string.Concat("Регион: ", regions.getItem(_idRegion));

      loadPoints();
    }

    private void Route_AddEdit_Load(object sender, EventArgs e)
    {
      FillFields();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.EditModeChanged += SetEnable;
      _workWithForm.SetEditMode(_route.Id == 0);
    }

    private void SetEnable(Object sender, EditModeEventArgs e)
    {
      if (User.GetRole() == RolesList.AccountantWayBill)
      {
        _workWithForm.SetEnableValue(btnSave, true);
      }
    }

    private void FillFields()
    {
      lbMyPoint1.Text = string.Concat("Пункт отправления: ", _route.MyPoint1.Name);
      cbMyPoint2.SelectedValue = _route.MyPoint2.Name;
      tbDistance.Text = _route.Distance.ToString();
    }

    private void loadPoints()
    {
      MyPointList myPointList = MyPointList.getInstance();
      DataTable dt = (_route.MyPoint2 == null)
        ? myPointList.ToDataTableWithoutExists(_idRegion, _route.MyPoint1)
        : myPointList.ToDataTable(_idRegion);

      cbMyPoint2.DataSource = dt;
      cbMyPoint2.ValueMember = dt.Columns[0].ColumnName;
      cbMyPoint2.DisplayMember = dt.Columns[1].ColumnName;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        if (cbMyPoint2.SelectedValue == null)
        {
          MessageBox.Show("Выберите пункт назначения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
        else if (tbDistance.Text == string.Empty)
        {
          MessageBox.Show("Введите дистанцию", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
        }
        else
        {
          MyPointList myPointList = MyPointList.getInstance();
          int idMyPoint2;
          int.TryParse(cbMyPoint2.SelectedValue.ToString(), out idMyPoint2);

          _route.MyPoint2 = myPointList.getItem(idMyPoint2);
          int distance;
          int.TryParse(tbDistance.Text, out distance);
          _route.Distance = distance;
          _route.Save();
        }

        DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
