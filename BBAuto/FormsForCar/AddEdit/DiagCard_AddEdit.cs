using BBAuto.Domain.ForCar;
using System;
using System.Windows.Forms;

namespace BBAuto
{
  public partial class DiagCard_AddEdit : Form
  {
    private DiagCard _diagCard;

    private WorkWithForm _workWithForm;

    public DiagCard_AddEdit(DiagCard diagCard)
    {
      InitializeComponent();

      _diagCard = diagCard;
    }

    private void DiagCard_AddEdit_Load(object sender, EventArgs e)
    {
      FillFields();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_diagCard.ID == 0);
    }

    private void FillFields()
    {
      tbNumber.Text = _diagCard.Number;
      dtpDate.Value = _diagCard.Date;

      TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
      tbFile.Text = _diagCard.File;

      lbCarInfo.Text = _diagCard.Car.ToString();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        _diagCard.Number = tbNumber.Text;
        _diagCard.Date = dtpDate.Value.Date;

        TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
        _diagCard.File = tbFile.Text;

        _diagCard.Save();

        DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
