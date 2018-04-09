using BBAuto.Domain.ForDriver;
using System;
using System.Windows.Forms;

namespace BBAuto
{
  public partial class License_AddEdit : Form
  {
    private DriverLicense _license;

    private WorkWithForm _workWithForm;

    public License_AddEdit(DriverLicense license)
    {
      InitializeComponent();

      _license = license;
    }

    private void License_AddEdit_Load(object sender, EventArgs e)
    {
      fillFields();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_license.ID == 0);
    }

    private void fillFields()
    {
      mtbNumber.Text = _license.Number;
      dateBegin.Value = Convert.ToDateTime(_license.DateBegin);
      dateEnd.Value = Convert.ToDateTime(_license.DateEnd);

      if (dateBegin.Value == dateEnd.Value)
        SetDateEnd();

      TextBox tbFile = (TextBox) ucFile.Controls["tbFile"];
      tbFile.Text = _license.File;
    }

    private void save_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        _license.Number = mtbNumber.Text;
        _license.DateBegin = dateBegin.Value.Date;
        _license.DateEnd = dateEnd.Value.Date;
        TextBox tbFile = (TextBox) ucFile.Controls["tbFile"];
        _license.File = tbFile.Text;

        _license.Save();

        DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }

    private void dateBegin_ValueChanged(object sender, EventArgs e)
    {
      SetDateEnd();
    }

    private void SetDateEnd()
    {
      dateEnd.Value = dateBegin.Value;
      dateEnd.Value = dateEnd.Value.AddYears(10);
    }
  }
}
