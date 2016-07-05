using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class DriverCarList : MainList
    {
        private List<DriverCar> list;
        private static DriverCarList uniqueInstance;

        private DriverCarList()
        {
            list = new List<DriverCar>();

            loadFromSql();
        }

        public static DriverCarList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new DriverCarList();

            return uniqueInstance;
        }
        
        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("DriverCar");

            foreach (DataRow row in dt.Rows)
            {
                DriverCar drCar = new DriverCar(row);
                Add(drCar);
            }
        }
        
        private void Add(DriverCar drCar)
        {
            if (list.Exists(item => item == drCar))
                return;

            list.Add(drCar);
        }

        public Driver GetDriver(Car car)
        {
            var driverCars = from driverCar in list
                             where driverCar.isDriverCar(car)
                             orderby driverCar.dateEnd descending, driverCar.number descending
                             select driverCar;

            if ((driverCars.ToList().Count == 0) && (!car.IsGet))
            {
                DriverList driverList = DriverList.getInstance();
                return driverList.getItem(Convert.ToInt32(car.driverID));
            }
            else
            {
                return getDriver(driverCars.ToList());
            }
        }

        public Driver GetDriver(Car car, DateTime date)
        {
            var driverCars = from driverCar in list
                             where driverCar.isDriverCar(car, date)
                             orderby driverCar.dateEnd descending, driverCar.number descending
                             select driverCar;

            TempMoveList tempMoveList = TempMoveList.getInstance();

            Driver driver = tempMoveList.getDriver(car, date);
            return (driver == null) ? getDriver(driverCars.ToList()) : driver;
        }
        
        private Driver getDriver(List<DriverCar> driverCars)
        {
            if (driverCars.Count() > 0)
            {
                DriverCar driverCar = driverCars.First() as DriverCar;
                DriverList driverList = DriverList.getInstance();
                return driverList.getItem(driverCar.idDriver);
            }
            else
            {
                return null;
            }
        }

        public Car GetCar(Driver driver)
        {
            DateTime date = DateTime.Today;

            var driverCars = list.Where(item => item.isEqualDriverID(driver) && item.dateEnd == date).OrderByDescending(item => item.dateEnd);
            
            if (driverCars.Count() > 0)
            {
                CarList carList = CarList.getInstance();

                foreach (var driverCar in driverCars)
                {
                    if (list.Where(item => !item.isEqualDriverID(driver) && item.dateEnd == date && item.idCar == driverCar.idCar && item.number > driverCar.number).Count() == 0)
                        return carList.getItem(driverCar.idCar);
                }
                
                return null;
            }
            else
                return null;
        }

        public Car GetCar(Driver driver, DateTime date)
        {
            var driverCars = from driverCar in list
                             where driverCar.isCarsDriver(driver)
                             orderby driverCar.dateEnd descending, driverCar.number descending
                             select driverCar;

            return (driverCars.Count() > 0) ? CarList.getInstance().getItem(driverCars.First().idCar) : null;
        }
        
        public bool IsDriverHaveCar(Driver driver)
        {
            return GetCar(driver) != null;
        }

        public DataTable ToDataTableCar(Driver driver)
        {
            var driverCars = list.Where(item => item.isCarsDriver(driver)).OrderByDescending(item => item.dateEnd);

            CarList carList = CarList.getInstance();
            List<Car> cars = new List<Car>();

            foreach (DriverCar driverCar in driverCars)
            {
                Car car = carList.getItem(driverCar.idCar);
                cars.Add(car);
            }

            return carList.createTable(cars);
        }
    }
}