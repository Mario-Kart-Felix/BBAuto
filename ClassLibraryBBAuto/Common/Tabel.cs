using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class Tabel
    {
        DriverList driverList = DriverList.getInstance();

        private IProvider _provider;

        private Driver _driver;
        private DateTime _date;

        public Tabel(string number, DateTime date)
        {
            _driver = driverList.getItemByNumber(number);

            _date = date;

            _provider = Provider.GetProvider();
        }

        public Driver driver { get { return _driver; } }
        public DateTime Date { get { return _date; } }

        public Tabel(DataRow row)
        {
            int idDriver;
            int.TryParse(row[0].ToString(), out idDriver);

            _driver = driverList.getItem(idDriver);

            DateTime.TryParse(row[1].ToString(), out _date);
        }

        public void Save()
        {
            _provider.Insert("Tabel", _driver.ID, _date);

            TabelList tabelList = TabelList.GetInstance();

            tabelList.Add(this);
        }

        
    }
}
