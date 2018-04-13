using System;
using System.Windows.Forms;
using BBAuto.Logic.Common;

namespace BBAuto.Logic
{
  public partial class FileOpenTextBox : UserControl
  {
    public FileOpenTextBox()
    {
      InitializeComponent();
      HaveFile();
    }

    private void btnBrowse_Click(object sender, EventArgs e)
    {
      OpenFileDialog ofd = new OpenFileDialog();
      ofd.Multiselect = false;
      if (ofd.ShowDialog() == DialogResult.OK)
        tbFile.Text = ofd.FileName;
    }

    private void btnShow_Click(object sender, EventArgs e)
    {
      WorkWithFiles.OpenFile(tbFile.Text);
    }

    private void tbFile_TextChanged(object sender, EventArgs e)
    {
      HaveFile();
    }

    private void HaveFile()
    {
      btnShow.Visible = (!string.IsNullOrEmpty(tbFile.Text));
      label1.Text = (string.IsNullOrEmpty(tbFile.Text)) ? "Отсутствует" : "Имеется";
    }

    private void btnShow_EnabledChanged(object sender, EventArgs e)
    {
      btnShow.Enabled = (!string.IsNullOrEmpty(tbFile.Text));
    }
  }
}
