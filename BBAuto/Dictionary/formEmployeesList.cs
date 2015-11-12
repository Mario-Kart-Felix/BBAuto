using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            int idRegion = 0;
            int.TryParse(_dgvEmployees.Rows[_dgvEmployees.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out idRegion);

            string EmployeesName = _dgvEmployees.Rows[_dgvEmployees.SelectedCells[0].RowIndex].Cells[3].Value.ToString();

            Employees employees = employeesList.getItem(idRegion, EmployeesName);

            Employees_AddEdit aeemployees = new Employees_AddEdit(employees);

            if (aeemployees.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                loadData();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            int idRegion = 0;
            int.TryParse(_dgvEmployees.Rows[_dgvEmployees.SelectedCells[0].RowIndex].Cells[0].Value.ToString(), out idRegion);

            int idEmployeesName = 0;
            int.TryParse(_dgvEmployees.Rows[_dgvEmployees.SelectedCells[0].RowIndex].Cells[1].Value.ToString(), out idEmployeesName);

            employeesList.Delete(idRegion, idEmployeesName);
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
