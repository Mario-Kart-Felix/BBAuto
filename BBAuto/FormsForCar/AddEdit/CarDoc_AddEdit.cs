using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.ForCar;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public partial class CarDoc_AddEdit : Form
  {
    private readonly CarDoc _carDoc;

    private WorkWithForm _workWithForm;

    public CarDoc_AddEdit(CarDoc carDoc)
    {
      InitializeComponent();

      _carDoc = carDoc;
    }

    private void CarDoc_AddEdit_Load(object sender, EventArgs e)
    {
      fillFields();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_carDoc.ID == 0);
    }

    private void fillFields()
    {
      tbName.Text = _carDoc.Name;
      TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
      tbFile.Text = _carDoc.File;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        _carDoc.Name = tbName.Text;
        TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
        _carDoc.File = tbFile.Text;

        _carDoc.Save();

        DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
