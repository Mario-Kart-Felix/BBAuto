﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLibraryBBAuto;

namespace BBAuto
{
    public partial class DTP_AddEdit : Form
    {
        private DTP _dtp;

        DTPFileList dtpFileList;

        private WorkWithForm _workWithForm;
        
        public DTP_AddEdit(DTP dtp)
        {
            InitializeComponent();

            dtpFileList = DTPFileList.getInstance();

            _dtp = dtp;
        }
        
        private void aeDTP_Load(object sender, EventArgs e)
        {
            loadData();

            _workWithForm = new WorkWithForm(this.Controls, btnSave, btnClose);
            _workWithForm.SetEditMode(_dtp.IsEqualsID(0));
        }

        private void loadData()
        {
            loadOneStringDictionary(cbRegion, "Region");
            loadOneStringDictionary(cbStatusAfterDTP, "StatusAfterDTP");
            loadOneStringDictionary(cbCurrentStatusAfterDTP, "CurrentStatusAfterDTP");

            Culprits culpits = Culprits.getInstance();

            cbCulprit.DataSource = culpits.ToDataTable(_dtp);
            cbCulprit.DisplayMember = "Название";
            cbCulprit.ValueMember = "id";

            fillFields();
        }

        private void loadOneStringDictionary(ComboBox combo, string name)
        {
            combo.DataSource = OneStringDictionary.getDataTable(name);
            combo.DisplayMember = "Название";
            combo.ValueMember = name + "_id";
        }

        private void fillFields()
        {
            dtpDate.Value = _dtp.Date;
            cbRegion.SelectedValue = _dtp.IDRegion;
            mtpDateCallInsure.Text = _dtp.DateCallInsure;
            cbCulprit.SelectedValue = _dtp.IDCulprit;
            cbStatusAfterDTP.SelectedValue = _dtp.IDStatusAfterDTP;
            numberLoss.Text = _dtp.numberLoss;
            tbSum.Text = _dtp.Sum;            
            damage.Text = _dtp.damage;
            facts.Text = _dtp.facts;            
            comm.Text = _dtp.comm;
            cbCurrentStatusAfterDTP.SelectedValue = _dtp.IDcurrentStatusAfterDTP;

            lbCarInfo.Text = _dtp.getCar().ToString();

            llDriver.Text = _dtp.GetDriver().GetName(NameType.Full);

            FillDgv();
        }

        private void FillDgv()
        {           
            _dgvFile.DataSource = dtpFileList.ToDataTable(_dtp);
            
            _dgvFile.Columns[0].Visible = false;

            _dgvFile.Columns[1].Width = _dgvFile.Width / 2;
            _dgvFile.Columns[2].Width = _dgvFile.Width / 2;

            foreach (DataGridViewRow row in _dgvFile.Rows)
            {
                int id = 0;
                int.TryParse(row.Cells[0].Value.ToString(), out id);

                DTPFile dtpFile = dtpFileList.getItem(id);

                if (dtpFile.file != string.Empty)
                    row.DefaultCellStyle.BackColor = BBColors.bbGreen3;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workWithForm.IsEditMode())
            {
                if (tryCopyData())
                {
                    _dtp.Save();
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            else
                _workWithForm.SetEditMode(true);
        }

        private bool tryCopyData()
        {
            try
            {
                copyData();
                return true;
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Необходимо выбрать значение из выпадающего списка", "Не удалось сохранить", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Не удалось сохранить", MessageBoxButtons.OK, MessageBoxIcon.Error);    
            }

            return false;
        }

        private void copyData()
        {
            _dtp.Date = dtpDate.Value.Date;
            _dtp.IDRegion = cbRegion.SelectedValue.ToString();
            _dtp.DateCallInsure = mtpDateCallInsure.Text;
            _dtp.IDCulprit = cbCulprit.SelectedValue.ToString();
            _dtp.IDStatusAfterDTP = cbStatusAfterDTP.SelectedValue.ToString();
            _dtp.numberLoss = numberLoss.Text;
            _dtp.Sum = tbSum.Text;
            _dtp.damage = damage.Text;
            _dtp.facts = facts.Text;
            _dtp.comm = comm.Text;
            _dtp.IDcurrentStatusAfterDTP = cbCurrentStatusAfterDTP.SelectedValue;
        }

        private void date_ValueChanged(object sender, EventArgs e)
        {
            mtpDateCallInsure.Text = dtpDate.Value.Date.ToShortDateString();
        }

        private void btnAddFile_Click(object sender, EventArgs e)
        {
            if (tryCopyData())
            {
                _dtp.Save();

                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = true;
                ofd.ShowDialog();

                foreach (string file in ofd.FileNames)
                {
                    DTPFile dtpFile = _dtp.createFile();

                    dtpFile.name = Path.GetFileNameWithoutExtension(file);
                    dtpFile.file = file;
                    dtpFile.Save();

                    dtpFileList.Add(dtpFile);
                }

                FillDgv();
            }
        }

        private void _dgvFile_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (isCellNoHeader(e.RowIndex))
            {
                int idDTPFile;
                int.TryParse(_dgvFile.Rows[e.RowIndex].Cells[0].Value.ToString(), out idDTPFile);

                DTPFile dtpFile = dtpFileList.getItem(idDTPFile);

                if ((e.ColumnIndex == 2) && (dtpFile.file != string.Empty))
                    WorkWithFiles.openFile(dtpFile.file);
                else
                {
                    DTPFile_AddEdit dtpFAE = new DTPFile_AddEdit(dtpFile);
                    if (dtpFAE.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        FillDgv();
                }
            }
        }

        private bool isCellNoHeader(int rowIndex)
        {
            return rowIndex >= 0;
        }

        private void btnDelFile_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Удалить файл?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                int idDTPFile = 0;
                int.TryParse(_dgvFile.Rows[_dgvFile.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out idDTPFile);

                dtpFileList.Delete(idDTPFile);

                FillDgv();
            }
        }

        private void sum_TextChanged(object sender, EventArgs e)
        {
            try
            {
                tbSum.Text = MyString.GetFormatedDigit(tbSum.Text);
                tbSum.SelectionStart = tbSum.Text.Length - 3;
            }
            catch (ArgumentOutOfRangeException) { }
        }

        private void llDriver_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Driver driver = _dtp.GetDriver();

            if (driver.DriverID == 0)
                return;

            Driver_AddEdit driverAE = new Driver_AddEdit(driver);
            driverAE.ShowDialog();
        }
    }
}
