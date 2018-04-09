using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;
using BBAuto.Logic.Tables;

namespace BBAuto.App.AddEdit
{
  public partial class Grade_AddEdit : Form
  {
    private Grade _grade;

    private WorkWithForm _workWithForm;

    public Grade_AddEdit(Grade grade)
    {
      InitializeComponent();

      _grade = grade;
    }

    private void aeGrade_Load(object sender, EventArgs e)
    {
      loadTypeEngine();

      fillFields();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_grade.ID == 0);
    }

    private void loadTypeEngine()
    {
      cbEngineType.DataSource = OneStringDictionary.getDataTable("EngineType");
      cbEngineType.DisplayMember = "Название";
      cbEngineType.ValueMember = "engineType_id";
    }

    private void fillFields()
    {
      tbName.Text = _grade.Name;
      tbEPower.Text = _grade.EPower;
      tbEVol.Text = _grade.EVol;
      tbMaxLoad.Text = _grade.MaxLoad;
      tbNoLoad.Text = _grade.NoLoad;
      cbEngineType.SelectedValue = _grade.EngineType.ID;
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        if (!isFill())
          return;

        _grade.Name = tbName.Text;
        _grade.EPower = tbEPower.Text;
        _grade.EVol = tbEVol.Text;
        _grade.MaxLoad = tbMaxLoad.Text;
        _grade.NoLoad = tbNoLoad.Text;

        int idEngineType;
        int.TryParse(cbEngineType.SelectedValue.ToString(), out idEngineType);
        EngineTypeList engineTypeList = EngineTypeList.getInstance();
        EngineType engineType = engineTypeList.getItem(idEngineType);
        _grade.EngineType = engineType;

        _grade.Save();

        DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }

    private bool isFill()
    {
      if (tbName.Text == String.Empty)
      {
        MessageBox.Show("Введите название", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
      }
      if (tbEPower.Text == String.Empty)
      {
        MessageBox.Show("Введите мощность двигателя", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
      }
      if (tbEVol.Text == String.Empty)
      {
        MessageBox.Show("Введите объём двигателя", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
      }
      if (tbMaxLoad.Text == String.Empty)
      {
        MessageBox.Show("Введите разрешенную максимальную массу", "Предупреждение", MessageBoxButtons.OK,
          MessageBoxIcon.Warning);
        return false;
      }
      if (tbNoLoad.Text == String.Empty)
      {
        MessageBox.Show("Введите массу без нагрузки", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return false;
      }

      return true;
    }

    private void isNumber(KeyPressEventArgs e)
    {
      if (!(Char.IsDigit(e.KeyChar)))
      {
        if (e.KeyChar != (char) Keys.Back)
        {
          e.Handled = true;
        }
      }
    }

    private void tbEVol_KeyPress(object sender, KeyPressEventArgs e)
    {
      isNumber(e);
    }

    private void tbEPower_KeyPress(object sender, KeyPressEventArgs e)
    {
      isNumber(e);
    }

    private void tbMaxLoad_KeyPress(object sender, KeyPressEventArgs e)
    {
      isNumber(e);
    }

    private void tbNoLoad_KeyPress(object sender, KeyPressEventArgs e)
    {
      isNumber(e);
    }
  }
}
