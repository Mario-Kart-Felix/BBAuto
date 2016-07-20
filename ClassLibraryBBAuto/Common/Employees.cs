using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class Employees : MainDictionary
    {
        private Region _region;
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
        public string Name
        {
            get
            {
                DriverList driverList = DriverList.getInstance();
                Driver driver = driverList.getItem(idDriver);

                return driver.GetName(NameType.Short);
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
            int idRegion;
            int.TryParse(row.ItemArray[0].ToString(), out idRegion);
            RegionList regionList = RegionList.getInstance();
            _region = regionList.getItem(idRegion);

            int.TryParse(row.ItemArray[1].ToString(), out idEmployeesName);
            int.TryParse(row.ItemArray[2].ToString(), out idDriver);
        }

        public Region Region
        {
            get { return _region; }
            set { _region = value; }
        }

        internal override void Delete()
        {
            _provider.DoOther("exec Employees_Delete @p1, @p2", _id, idEmployeesName);
        }

        internal override object[] getRow()
        {            
            return new object[5] { Region.ID, idEmployeesName, Region.Name, EmployeeName, DriverName };
        }

        public override void Save()
        {
            _provider.Insert("Employees", Region.ID, idEmployeesName, idDriver);
        }
    }
}
