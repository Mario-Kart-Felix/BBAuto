using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Domain;

namespace BBAuto
{
    public partial class formMedicalCertList : Form
    {
        private Driver _driver;
        private MedicalCertList _medicalCertList;

        public formMedicalCertList(Driver driver)
        {
            InitializeComponent();

            _driver = driver;            
            _medicalCertList = MedicalCertList.getInstance();
        }

        private void MedicalCertList_Load(object sender, EventArgs e)
        {
            loadData();
            SetEnable();
        }

        private void loadData()
        {
            dgvMedicalCert.DataSource = _medicalCertList.ToDataTable(_driver);

            formatDGV();
        }

        private void SetEnable()
        {
            btnAdd.Enabled = User.IsFullAccess();
            btnDelete.Enabled = User.IsFullAccess();
        }

        private void formatDGV()
        {
            dgvMedicalCert.Columns[0].Visible = false;

            foreach (DataGridViewRow row in dgvMedicalCert.Rows)
            {
                int id = 0;
                int.TryParse(row.Cells[0].Value.ToString(), out id);

                MedicalCert medicalCert = _medicalCertList.getItem(id);

                if (medicalCert.IsActual())
                    row.DefaultCellStyle.BackColor = BBColors.bbGreen3;
            }
        }

        private void add_Click(object sender, EventArgs e)
        {
            MedicalCert medicalCert = _driver.createMedicalCert();

            MedicalCert_AddEdit medicalCertAE = new MedicalCert_AddEdit(medicalCert);
            if (medicalCertAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                loadData();
        }

        private void dgvMedicalCert_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isCellNoHeader(e.RowIndex))
            {
                int idMedicalCert = Convert.ToInt32(dgvMedicalCert.Rows[e.RowIndex].Cells[0].Value);
                
                MedicalCert medicalCert = _medicalCertList.getItem(idMedicalCert);

                if ((e.ColumnIndex == 1) && (medicalCert.File != string.Empty))
                {
                    tryOpenFile(medicalCert.File);
                }
                else
                {
                    MedicalCert_AddEdit medicalCertAE = new MedicalCert_AddEdit(medicalCert);
                    if (medicalCertAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        loadData();
                }
            }
        }

        private bool isCellNoHeader(int rowIndex)
        {
            return rowIndex >= 0;
        }

        private void delete_Click(object sender, EventArgs e)
        {
            int idMedicalCert = 0;
            int.TryParse(dgvMedicalCert.Rows[dgvMedicalCert.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out idMedicalCert);

            _medicalCertList.Delete(idMedicalCert);

            loadData();
        }

        private void tryOpenFile(string file)
        {
            try
            {
                WorkWithFiles.openFile(file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Не найден файл", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
