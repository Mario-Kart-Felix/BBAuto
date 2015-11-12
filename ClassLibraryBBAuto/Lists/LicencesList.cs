using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class LicenseList : MainList, INotificationList
    {
        private List<DriverLicense> _list;
        private static LicenseList _uniqueInstance;

        private LicenseList()
        {
            _list = new List<DriverLicense>();

            loadFromSql();
        }

        public static LicenseList getInstance()
        {
            if (_uniqueInstance == null)
                _uniqueInstance = new LicenseList();

            return _uniqueInstance;
        }
        
        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("DriverLicense");

            foreach (DataRow row in dt.Rows)
            {
                DriverLicense driverLicense = new DriverLicense(row);
                Add(driverLicense);
            }
        }

        public void Add(DriverLicense driverLicense)
        {
            if (_list.Exists(item => item == driverLicense))
                return;

            _list.Add(driverLicense);
        }

        public DataTable ToDataTable(Driver driver)
        {
            var driverLicenses = from driverLicense in _list
                                 where driverLicense.idEqualDriverID(driver)
                                 orderby driverLicense.getDateEnd() descending
                                 select driverLicense;

            return createTable(driverLicenses.ToList());
        }

        private DataTable createTable(List<DriverLicense> driverLicenses)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Номер");
            dt.Columns.Add("Дата окончания действия");

            foreach (DriverLicense driverLicense in driverLicenses)
                dt.Rows.Add(driverLicense.getRow());

            return dt;
        }

        public DriverLicense getItem(int id)
        {
            var driverLicenses = from driverLicense in _list
                                 where driverLicense.IsEqualsID(id)
                                 select driverLicense;

            return (driverLicenses.Count() > 0) ? driverLicenses.First() as DriverLicense : null;
        }

        public DriverLicense getItem(Driver driver)
        {
            var driverLicenses = from driverLicense in _list
                                 where driverLicense.idEqualDriverID(driver)
                                 orderby driverLicense.getDateEnd() descending
                                 select driverLicense;

            return (driverLicenses.Count() > 0) ? driverLicenses.First() as DriverLicense : driver.createDriverLicense();
        }

        public List<INotification> ToList()
        {
            DriverList driverList = DriverList.getInstance();
            List<DriverLicense> listNew = _list.Where(item => !driverList.getItem(item.DriverID).Fired && !driverList.getItem(item.DriverID).Decret && driverList.getItem(item.DriverID).IsDriver).ToList();

            List<INotification> listNotification = new List<INotification>();
            foreach (INotification item in listNew)
                listNotification.Add(item);

            return listNotification;
        }

        public void Delete(int idLicence)
        {
            DriverLicense licence = getItem(idLicence);

            _list.Remove(licence);

            licence.Delete();
        }
    }
}
