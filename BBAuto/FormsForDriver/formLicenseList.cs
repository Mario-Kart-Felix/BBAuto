using BBAuto.Domain.Common;
using BBAuto.Domain.Entities;
using BBAuto.Domain.ForDriver;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Static;
using System;
using System.Windows.Forms;

namespace BBAuto
{
  public partial class formLicenseList : Form
  {
    private Driver _driver;
    private LicenseList _licencesList;

    public formLicenseList(Driver driver)
    {
      InitializeComponent();

      _driver = driver;
      _licencesList = LicenseList.getInstance();
    }

    private void LicenseList_Load(object sender, EventArgs e)
    {
      loadData();
      SetEnable();
    }

    private void loadData()
    {
      _dgvLicense.DataSource = _licencesList.ToDataTable(_driver);

      formatDGV();
    }

    private void SetEnable()
    {
      btnAdd.Enabled = User.IsFullAccess();
      btnDelete.Enabled = User.IsFullAccess();
    }

    private void formatDGV()
    {
      _dgvLicense.Columns[0].Visible = false;

      foreach (DataGridViewRow row in _dgvLicense.Rows)
      {
        int idDriverLicense = 0;
        int.TryParse(_dgvLicense.Rows[_dgvLicense.SelectedCells[0].RowIndex].Cells[0].Value.ToString(),
          out idDriverLicense);

        DriverLicense driverLicense = _licencesList.getItem(idDriverLicense);

        if (driverLicense.IsActual())
          row.DefaultCellStyle.BackColor = BBColors.bbGreen3;
      }
    }

    private void add_Click(object sender, EventArgs e)
    {
      DriverLicense driverLicense = _driver.createDriverLicense();

      if (openAddEditDialog(driverLicense))
        loadData();
    }

    private void dgvLicense_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
      int idDriverLicense = Convert.ToInt32(_dgvLicense.Rows[_dgvLicense.SelectedCells[0].RowIndex].Cells[0].Value);

      DriverLicense driverLicense = _licencesList.getItem(idDriverLicense);

      if ((e.ColumnIndex == 1) && (driverLicense.File != string.Empty))
        tryOpenFile(driverLicense.File);
      else if (openAddEditDialog(driverLicense))
        loadData();
    }

    private void tryOpenFile(string fileName)
    {
      try
      {
        WorkWithFiles.OpenFile(fileName);
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
    }

    private bool openAddEditDialog(DriverLicense driverLicense)
    {
      License_AddEdit licenseAE = new License_AddEdit(driverLicense);
      if (licenseAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        return true;
      else
        return false;
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
      int idLicence = 0;
      int.TryParse(_dgvLicense.Rows[_dgvLicense.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out idLicence);

      _licencesList.Delete(idLicence);

      loadData();
    }
  }
}
