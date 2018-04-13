using System;
using System.Windows.Forms;
using BBAuto.App.Events;
using BBAuto.Logic.ForCar;

namespace BBAuto.App.FormsForCar.AddEdit
{
  public partial class DTPFile_AddEdit : Form
  {
    private readonly DTPFile _dtpFile;

    private WorkWithForm _workWithForm;

    public DTPFile_AddEdit(DTPFile dtpFile)
    {
      InitializeComponent();

      _dtpFile = dtpFile;
    }

    private void DTPFile_AddEdit_Load(object sender, EventArgs e)
    {
      FillFields();

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_dtpFile.Id == 0);
    }

    private void FillFields()
    {
      tbName.Text = _dtpFile.Name;
      TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
      tbFile.Text = _dtpFile.File;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        _dtpFile.Name = tbName.Text;
        TextBox tbFile = ucFile.Controls["tbFile"] as TextBox;
        _dtpFile.File = tbFile.Text;

        _dtpFile.Save();

        DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
