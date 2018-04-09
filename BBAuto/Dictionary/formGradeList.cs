using System;
using System.Windows.Forms;
using BBAuto.App.AddEdit;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.App.Dictionary
{
  public partial class formGradeList : Form
  {
    private bool _load;

    public formGradeList()
    {
      InitializeComponent();

      loadMark();
      loadModel();
      loadGrade();
    }

    private void loadMark()
    {
      _load = false;
      cbMark.DataSource = OneStringDictionary.getDataTable("Mark");
      cbMark.DisplayMember = "Название";
      cbMark.ValueMember = "mark_id";
      _load = true;
    }

    private void loadModel()
    {
      _load = false;

      int idMark = 0;
      int.TryParse(cbMark.SelectedValue.ToString(), out idMark);
      ModelList models = ModelList.getInstance();

      cbModel.DataSource = models.ToDataTable(idMark);
      cbModel.DisplayMember = "Название";
      cbModel.ValueMember = "id";

      _load = true;
    }

    private void loadGrade()
    {
      int idModel = 0;
      GradeList grades = GradeList.getInstance();
      int.TryParse(cbModel.SelectedValue.ToString(), out idModel);


      _dgv.DataSource = grades.ToDataTable(idModel);
      _dgv.Columns["id"].Visible = false;
    }

    private void cbMark_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_load)
      {
        loadModel();
        loadGrade();
      }
    }

    private void cbModel_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (_load)
        loadGrade();
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
      int idModel = Convert.ToInt32(cbModel.SelectedValue);
      Model model = ModelList.getInstance().getItem(idModel);

      Grade_AddEdit aeG = new Grade_AddEdit(new Grade(model));
      if (aeG.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        loadGrade();
      }
    }

    private void _dgv_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
    {
      if ((e.ColumnIndex < 0) || (e.RowIndex < 0))
        return;

      int idGrade = Convert.ToInt32(_dgv.Rows[e.RowIndex].Cells[0].Value);

      GradeList grades = GradeList.getInstance();
      Grade grade = grades.getItem(idGrade);

      Grade_AddEdit aeG = new Grade_AddEdit(grade);

      if (aeG.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        loadGrade();
      }
    }

    private void btnDel_Click(object sender, EventArgs e)
    {
      GradeList grades = GradeList.getInstance();

      int idGrade = 0;

      foreach (DataGridViewCell cell in _dgv.SelectedCells)
      {
        int.TryParse(_dgv.Rows[cell.RowIndex].Cells[0].Value.ToString(), out idGrade);

        grades.Delete(idGrade);
      }

      loadGrade();
    }
  }
}
