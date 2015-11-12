using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class DriverList : MainList
    {
        private List<Driver> _list;
        private static DriverList uniqueInstance;

        private DriverList()
        {
            _list = new List<Driver>();

            loadFromSql();
        }

        public static DriverList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new DriverList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Driver");

            foreach (DataRow row in dt.Rows)
            {
                Driver driver = new Driver(row);
                _list.Add(driver);
            }
        }
        
        public void Add(Driver driver)
        {
            if (_list.Exists(item => item == driver))
                return;

            _list.Add(driver);
        }
        
        public DataTable ToDataTable(bool all = false)
        {
            List<Driver> tempList = (all) ? _list.ToList() : _list.Where(item => item.IsDriver).ToList();

            return CreateDataTable(tempList);
        }

        public DataTable ToDataTableNotDriver(int idOwner)
        {
            List<Driver> tempList = _list.Where(item => !item.IsDriver && item.OwnerID == idOwner).ToList();

            return CreateDataTable(tempList);
        }

        public DataTable ToDataTableByRegion(int idRegion, bool all = false)
        {
            List<Driver> tempList = (all) ? _list.Where(item => item.RegionID == idRegion || item.IsEqualsID(1)).ToList() : _list.Where(item => (item.RegionID == idRegion || item.IsEqualsID(1)) && item.IsDriver).ToList();

            return CreateDataTable(tempList);
        }

        private DataTable CreateDataTable(List<Driver> MyList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("idCar");
            dt.Columns.Add("ФИО");
            dt.Columns.Add("Скан водительского удостоверения");
            dt.Columns.Add("Скан медицинской справки");
            dt.Columns.Add("Автомобиль");
            dt.Columns.Add("Регион");
            dt.Columns.Add("Компания");
            dt.Columns.Add("Статус");
            
            foreach (Driver driver in MyList)
                dt.Rows.Add(driver.getRow());

            return dt;
        }

        public Driver getItem(int idDriver)
        {
            List<Driver> drivers = _list.Where(item => item.IsEqualsID(idDriver)).ToList();

            return (drivers.Count() > 0) ? drivers.First() : new Driver();
        }

        public Driver getItem(string login)
        {
            List<Driver> drivers = _list.Where(item => item.Login == login).ToList();

            return (drivers.Count() > 0) ? drivers.First() : null;
        }

        public Driver getItemByNumber(string number)
        {
            List<Driver> drivers = _list.Where(item => item.Number == number).ToList();

            return (drivers.Count() > 0) ? drivers.First() : new Driver();
        }

        public List<Driver> GetDriverListByRole(RolesList role)
        {
            UserAccessList userAccessList = UserAccessList.getInstance();
            List<UserAccess> userAccesses = userAccessList.ToList(role);

            if (userAccesses != null)
            {
                List<Driver> drivers = new List<Driver>();

                foreach (UserAccess userAccess in userAccesses)
                {
                    int idDriver;
                    int.TryParse(userAccess.IDDriver, out idDriver);
                    drivers.Add(getItem(idDriver));
                }

                return drivers;
            }
            else
                return null;
        }

        public List<Driver> ToList()
        {
            return _list;
        }

        internal int CountDriversInRegion(int RegionID)
        {
            return _list.Where(item => item.RegionID == RegionID && !item.Fired).Count();
        }

        public bool IsUniqueNumber(string number)
        {
            return _list.Where(item => item.Number == number).Count() == 0;
        }
    }
}
