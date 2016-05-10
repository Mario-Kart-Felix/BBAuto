using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLibraryBBAuto;

namespace BBAuto
{
    public partial class formEmployeesList : Form
    {
        EmployeesList employeesList;

        public formEmployeesList()
        {
            InitializeComponent();

            employeesList = EmployeesList.getInstance();
        }

        private void formEmployeesList_Load(object sender, EventArgs e)
        {
            loadData();

            ResizeDGV();
        }

        private void loadData()
        {
            _dgvEmployees.DataSource = employeesList.ToDataTable();
            _dgvEmployees.Columns[0].Visible = false;
            _dgvEmployees.Columns[1].Visible = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Employees employees = new Employees();

            Employees_AddEdit aeemployees = new Employees_AddEdit(employees);

            if (aeemployees.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                employeesList.Add(employees);
                loadData();
            }
        }

        private void _dgvEmployees_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Region region = getRegion();

            string EmployeesName = _dgvEmployees.Rows[_dgvEmployees.SelectedCells[0].RowIndex].Cells[3].Value.ToString();

            Employees employees = employeesList.getItem(region, EmployeesName);

            Employees_AddEdit aeemployees = new Employees_AddEdit(employees);

            if (aeemployees.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                loadData();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            Region region = getRegion();

            int idEmployeesName = 0;
            int.TryParse(_dgvEmployees.Rows[_dgvEmployees.SelectedCells[0].RowIndex].Cells[1].Value.ToString(), out idEmployeesName);

            employeesList.Delete(region, idEmployeesName);
        }

        private Region getRegion()
        {
            int idRegion = 0;
            int.TryParse(_dgvEmployees.Rows[_dgvEmployees.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out idRegion);

            RegionList regionList = RegionList.getInstance();
            return regionList.getItem(idRegion);
        }

        private void formEmployeesList_Resize(object sender, EventArgs e)
        {
            ResizeDGV();
        }

        private void ResizeDGV()
        {
            _dgvEmployees.Columns[2].Width = _dgvEmployees.Width / 3;
            _dgvEmployees.Columns[3].Width = _dgvEmployees.Width / 3;
            _dgvEmployees.Columns[4].Width = _dgvEmployees.Width / 3;
        }
    }
}
