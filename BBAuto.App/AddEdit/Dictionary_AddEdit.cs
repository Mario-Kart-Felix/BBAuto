using System;
using System.Windows.Forms;
using BBAuto.Logic.Views;

namespace BBAuto.App.AddEdit
{
  public partial class Dictionary_AddEdit : Form, IViewDictionary
  {
    public event EventHandler<EventArgs> SaveClick;
    public event EventHandler<EventArgs> LoadData;

    public string InputText => tbText.Text;

    public string InputName => tbName.Text;

    public Dictionary_AddEdit(string text)
    {
      InitializeComponent();

      Text = text;
    }

    private void Diller_AddEdit_Load(object sender, EventArgs e)
    {
      if (LoadData != null)
        LoadData(sender, EventArgs.Empty);
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (SaveClick != null)
        SaveClick(sender, EventArgs.Empty);
    }

    public void SetName(string name)
    {
      tbName.Text = name;
    }

    public void SetText(string contacts)
    {
      tbText.Text = contacts;
    }
  }
}
