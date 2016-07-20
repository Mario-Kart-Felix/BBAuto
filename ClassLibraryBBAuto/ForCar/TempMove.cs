using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain
{
    public class TempMove : MainDictionary
    {
        private int idCar;
        private int idDriver;
        public DateTime dateBegin;
        public DateTime dateEnd;

        public string IDCar
        {
            get { return idCar.ToString(); }
            set { int.TryParse(value, out idCar); }
        }

        public string IDDriver
        {
            get { return idDriver.ToString(); }
            set { int.TryParse(value, out idDriver); }
        }

        internal TempMove(int idCar)
        {
            _id = 0;
            this.idCar = idCar;
            idDriver = 0;
            dateBegin = DateTime.Today;
            dateEnd = DateTime.Today;
        }

        public TempMove(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out idCar);
            int.TryParse(row.ItemArray[2].ToString(), out idDriver);
            DateTime.TryParse(row.ItemArray[3].ToString(), out dateBegin);
            DateTime.TryParse(row.ItemArray[4].ToString(), out dateEnd);
        }

        public override void Save()
        {
            int.TryParse(_provider.Insert("TempMove", _id, idCar, idDriver, dateBegin, dateEnd), out _id);
        }

        internal override object[] getRow()
        {
            CarList carList = CarList.getInstance();
            Car car = carList.getItem(idCar);

            DriverList driverList = DriverList.getInstance();
            Driver driver = driverList.getItem(idDriver);

            return new object[] { _id, idCar, car.BBNumber, car.Grz, driver.GetName(NameType.Full), dateBegin, dateEnd };
        }

        internal bool isDriverCar(Car car, DateTime date)
        {
            return car.IsEqualsID(idCar) && date >= dateBegin && date <= dateEnd;
        }

        internal Driver getDriver()
        {
            DriverList driverList = DriverList.getInstance();
            return driverList.getItem(idDriver);
        }
    }
}
