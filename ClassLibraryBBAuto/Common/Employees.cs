using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class Employees : MainDictionary
    {
        private int idEmployeesName;
        private int idDriver;

        public string IDEmployeesName
        {
            get
            {
                return idEmployeesName.ToString();
            }
            set
            {
                int.TryParse(value, out idEmployeesName);
            }
        }

        public string IDDriver
        {
            get
            {
                return idDriver.ToString();
            }
            set
            {
                int.TryParse(value, out idDriver);
            }
        }

        public string ID
        {
            get
            {
                return _id.ToString();
            }
            set
            {
                int.TryParse(value, out _id);
            }
        }

        public string Name
        {
            get
            {
                DriverList driverList = DriverList.getInstance();
                Driver driver = driverList.getItem(idDriver);

                return driver.GetName(NameType.Short);
            }
        }

        public string Region
        {
            get
            {
                Regions regions = Regions.getInstance();
                return regions.getItem(_id);
            }
        }

        public string DriverName
        {
            get
            {
                DriverList driverList = DriverList.getInstance();
                Driver driver = driverList.getItem(idDriver);
                return driver.GetName(NameType.Full);
            }
        }

        public string EmployeeName
        {
            get
            {
                EmployeesNames employeesNames = EmployeesNames.getInstance();
                return employeesNames.getItem(idEmployeesName);
            }
        }

        public Employees()
        {
            _id = 0;
        }

        public Employees(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out idEmployeesName);
            int.TryParse(row.ItemArray[2].ToString(), out idDriver);
        }

        internal override void Delete()
        {
            _provider.DoOther("exec Employees_Delete @p1, @p2", _id, idEmployeesName);
        }

        internal override object[] getRow()
        {            
            return new object[5] { _id, idEmployeesName, Region, EmployeeName, DriverName };
        }

        public override void Save()
        {
            _provider.Insert("Employees", _id, idEmployeesName, idDriver);
        }
    }
}
