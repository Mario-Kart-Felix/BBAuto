using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class MedicalCertList : MainList, INotificationList
    {
        private List<MedicalCert> _list;
        private static MedicalCertList _uniqueInstance;

        private MedicalCertList()
        {
            _list = new List<MedicalCert>();

            loadFromSql();
        }

        public static MedicalCertList getInstance()
        {
            if (_uniqueInstance == null)
                _uniqueInstance = new MedicalCertList();

            return _uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("MedicalCert");

            foreach (DataRow row in dt.Rows)
            {
                MedicalCert medicalCert = new MedicalCert(row);
                Add(medicalCert);
            }
        }

        public void Add(MedicalCert medicalCert)
        {
            if (_list.Exists(item => item == medicalCert))
                return;

            _list.Add(medicalCert);
        }

        public DataTable ToDataTable()
        {
            return createTable(_list);
        }

        public DataTable ToDataTable(Driver driver)
        {
            var medicalCerts = from medicalCert in _list
                               where medicalCert.idEqualDriverID(driver)
                               orderby medicalCert.DateEnd descending
                               select medicalCert;

            return createTable(medicalCerts.ToList());
        }

        private DataTable createTable(List<MedicalCert> medicalCerts)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Номер");
            dt.Columns.Add("Дата окончания действия");

            foreach (MedicalCert medicalCert in medicalCerts.ToList())
                dt.Rows.Add(medicalCert.getRow());

            return dt;
        }

        public MedicalCert getItem(int id)
        {
            var medicalCerts = _list.Where(item => item.IsEqualsID(id));

            return (medicalCerts.Count() > 0) ? medicalCerts.First() as MedicalCert : null;
        }

        public MedicalCert getItem(Driver driver)
        {
            List<MedicalCert> medicalCerts = (from medicalCert in _list
                               where medicalCert.idEqualDriverID(driver)
                               orderby medicalCert.DateEnd descending
                               select medicalCert).ToList();

            return (medicalCerts.Count() > 0) ? medicalCerts.First() : driver.createMedicalCert();
        }

        public void Delete(int idMedicalCert)
        {
            MedicalCert medicalCert = getItem(idMedicalCert);

            _list.Remove(medicalCert);

            medicalCert.Delete();
        }

        public List<INotification> ToList()
        {
            DriverList driverList = DriverList.getInstance();
            List<MedicalCert> listNew = _list.Where(item => !driverList.getItem(item.DriverID).Fired && !driverList.getItem(item.DriverID).Decret && driverList.getItem(item.DriverID).IsDriver).ToList();

            List<INotification> listNotification = new List<INotification>();

            foreach (INotification item in listNew)
                listNotification.Add(item);

            return listNotification;
        }
    }
}
