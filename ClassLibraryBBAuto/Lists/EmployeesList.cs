using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class EmployeesList : MainList
    {
        private List<Employees> list;
        private static EmployeesList uniqueInstance;

        private EmployeesList()
        {
            list = new List<Employees>();

            loadFromSql();
        }

        public static EmployeesList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new EmployeesList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Employees");

            foreach (DataRow row in dt.Rows)
            {
                Employees employees = new Employees(row);
                Add(employees);
            }
        }

        public void Add(Employees employees)
        {
            if (list.Exists(item => item == employees))
                return;

            list.Add(employees);
        }

        public void Delete(int idRegion, int idEmployeesName)
        {
            Employees employees = getItem(idRegion, idEmployeesName);

            list.Remove(employees);

            employees.Delete();
        }

        public Employees getItem(int idRegion, string EmployeesName, bool allowNull = false)
        {
            int idEmployeesName = 0;
            EmployeesNames employeesNames = EmployeesNames.getInstance();
            idEmployeesName = employeesNames.getItem(EmployeesName);

            return getItem(idRegion, idEmployeesName, allowNull);
        }

        private Employees getItem(int idRegion, int idEmployeesName, bool allowNull = false)
        {
            List<Employees> EmployeesList = getList(idRegion, idEmployeesName);
            Employees employees;

            if (EmployeesList.Count() > 0)
                employees = EmployeesList.First() as Employees;
            else if (allowNull)
                return null;
            else
            {
                EmployeesList = getList(1, idEmployeesName);

                if (EmployeesList.Count() > 0)
                    employees = EmployeesList.First() as Employees;
                else
                    employees = new Employees();
            }

            return employees;
        }

        private List<Employees> getList(int idRegion, int idEmployeesName)
        {
            var EmployeesList = from employees in list
                                where employees.IsEqualsID(idRegion) & employees.IDEmployeesName == idEmployeesName.ToString()
                                select employees;

            return EmployeesList.ToList();
        }

        public DataTable ToDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("idRegion");
            dt.Columns.Add("idEmployeesName");
            dt.Columns.Add("Регион");
            dt.Columns.Add("Должность");
            dt.Columns.Add("Фамилия");

            var empList = from employee in list
                          orderby employee.Region, employee.EmployeeName
                          select employee;

            foreach (Employees employees in empList.ToList())
                dt.Rows.Add(employees.getRow());

            return dt;
        }
    }
}
