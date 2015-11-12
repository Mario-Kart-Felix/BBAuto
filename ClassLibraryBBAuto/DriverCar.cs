using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class DriverCar
    {
        public readonly int idCar;
        public readonly int idDriver;
        private DateTime dateBegin;
        public readonly DateTime dateEnd;
        public readonly int number;

        public DriverCar(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out idCar);
            int.TryParse(row.ItemArray[1].ToString(), out idDriver);
            DateTime.TryParse(row.ItemArray[2].ToString(), out dateBegin);
            DateTime.TryParse(row.ItemArray[3].ToString(), out dateEnd);
            int.TryParse(row.ItemArray[4].ToString(), out number);

            dateEnd = dateEnd.Date;
        }

        internal bool isDriverCar(Car car)
        {
            return car.IsEqualsID(idCar);
        }

        internal bool isDriverCar(Car car, DateTime date)
        {
            if ((date >= DateTime.Today) && (dateEnd == DateTime.Today))
                return car.IsEqualsID(idCar) && date >= dateBegin;
            else
                return car.IsEqualsID(idCar) && date >= dateBegin && date < dateEnd;
        }

        internal bool isCarsDriver(Driver driver)
        {
            return driver.IsEqualsID(idDriver);
        }

        internal bool isEqualDriverID(Driver driver)
        {
            return driver.IsEqualsID(idDriver);
        }
    }
}
