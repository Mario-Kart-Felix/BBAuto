using BBAuto.Domain.Dictionary;
using BBAuto.Domain.ForCar;
using BBAuto.Domain.Lists;
using System;
using System.Windows.Forms;

namespace BBAuto
{
  public partial class SsDTP_AddEdit : Form
  {
    private SsDTP _ssDTP;

    private WorkWithForm _workWithForm;

    public SsDTP_AddEdit(SsDTP ssDTP)
    {
      InitializeComponent();

      _ssDTP = ssDTP;
    }

    private void aeSsDTP_Load(object sender, EventArgs e)
    {
      loadDictionary();

      cbMark.SelectedValue = _ssDTP.ID;
      cbServiceStantion.SelectedValue = _ssDTP.IDServiceStantion;

      _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
      _workWithForm.SetEditMode(_ssDTP.ID == 0);
    }

    private void loadDictionary()
    {
      Marks marks = Marks.getInstance();

      cbMark.DataSource = marks.ToDataTable();
      cbMark.DisplayMember = "Название";
      cbMark.ValueMember = "id";

      ServiceStantions serviceStantions = ServiceStantions.getInstance();

      cbServiceStantion.DataSource = serviceStantions.ToDataTable();
      cbServiceStantion.DisplayMember = "Название";
      cbServiceStantion.ValueMember = "id";
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (_workWithForm.IsEditMode())
      {
        MarkList markList = MarkList.getInstance();
        int idMark;
        int.TryParse(cbMark.SelectedValue.ToString(), out idMark);
        _ssDTP.Mark = markList.getItem(idMark);

        _ssDTP.IDServiceStantion = cbServiceStantion.SelectedValue.ToString();

        _ssDTP.Save();

        DialogResult = System.Windows.Forms.DialogResult.OK;
      }
      else
        _workWithForm.SetEditMode(true);
    }
  }
}
