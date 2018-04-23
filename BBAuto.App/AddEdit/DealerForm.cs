using System;
using System.Windows.Forms;
using BBAuto.Logic.Services.Dealer;

namespace BBAuto.App.AddEdit
{
  public partial class DealerForm : Form
  {
    private readonly DealerModel _dealer;

    public DealerForm(DealerModel dealer)
    {
      InitializeComponent();

      _dealer = dealer;
    }

    private void DealerForm_Load(object sender, EventArgs e)
    {
      tbName.Text = _dealer.Name;
      tbText.Text = _dealer.Contacts;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      if (string.IsNullOrEmpty(tbName.Text))
      {
        MessageBox.Show("Название не может быть пустым", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      _dealer.Name = tbName.Text;
      _dealer.Contacts = tbText.Text;
    }
    
    private void btnClose_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
