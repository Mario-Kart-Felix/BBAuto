using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.ForDriver;

namespace BBAuto.App.FormsForDriver.AddEdit
{
  public partial class MedicalCert_AddEdit : Form
  {
    private MedicalCert _medicalCert;

    private WorkWithForm _workWithForm;

    public MedicalCert_AddEdit(MedicalCert medicalCert)
    {
      InitializeComponent();

      _medicalCert = medicalCert;
    }

    private void MedicalCert_AddEdit_Load(object sender, EventArgs e)
    {
      fillFields();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_medicalCert.Id == 0);
    }

    private void fillFields()
    {
      tbNumber.Text = _medicalCert.Number;
      dtpDateBegin.Value = Convert.ToDateTime(_medicalCert.DateBegin);
      dtpDateEnd.Value = Convert.ToDateTime(_medicalCert.DateEnd);

      if (dtpDateBegin.Value == dtpDateEnd.Value)
        SetDateEnd();

      TextBox tbFile = (TextBox) ucFile.Controls["tbFile"];
      tbFile.Text = _medicalCert.File;
    }

    private void save_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        _medicalCert.Number = tbNumber.Text;

        _medicalCert.DateBegin = dtpDateBegin.Value.Date;
        _medicalCert.DateEnd = dtpDateEnd.Value.Date;

        TextBox tbFile = (TextBox) ucFile.Controls["tbFile"];
        _medicalCert.File = tbFile.Text;

        _medicalCert.Save();

        DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }

    private void dtpDateBegin_ValueChanged(object sender, EventArgs e)
    {
      SetDateEnd();
    }

    private void SetDateEnd()
    {
      dtpDateEnd.Value = dtpDateBegin.Value;
      dtpDateEnd.Value = dtpDateEnd.Value.AddYears(2);
    }
  }
}
