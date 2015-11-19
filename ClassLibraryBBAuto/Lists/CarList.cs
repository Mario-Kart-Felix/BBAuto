using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataLayer;

namespace ClassLibraryBBAuto
{
    public class CarList : MainList
    {
        private List<Car> list;
        private static CarList uniqueInstance;
        
        private CarList()
        {
            list = new List<Car>();

            loadFromSql();
        }

        public static CarList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new CarList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Car");

            foreach (DataRow row in dt.Rows)
            {
                Car car = new Car(row);
                Add(car);
            }
        }
        
        public void Add(Car car)
        {
            if (list.Exists(item => item == car))
                return;

            list.Add(car);
        }

        private DataTable ToDataTable()
        {
            return createTable(list);
        }

        private DataTable ToDataTableActual()
        {
            List<Car> cars;

            if (User.GetRole() == RolesList.Employee)
            {
                DriverCarList driverCarList = DriverCarList.getInstance();
                Car myCar = driverCarList.GetCar(User.getDriver());

                cars = list.Where(car => car == myCar).ToList();
            }
            else
            {
                cars = list.Where(car => car.IsGet && !car.info.IsSale).ToList();
            }

            return createTable(cars);
        }

        private DataTable ToDataTableBuy()
        {
            var cars = list.Where(car => !car.IsGet);

            return createTable(cars.ToList());
        }
        
        internal DataTable createTable(List<Car> cars)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("idCar");
            dt.Columns.Add("Бортовой номер");
            dt.Columns.Add("Регистрационный знак");
            dt.Columns.Add("Марка");
            dt.Columns.Add("Модель");
            dt.Columns.Add("VIN");
            dt.Columns.Add("Регион");
            dt.Columns.Add("Водитель");
            dt.Columns.Add("№ ПТС");
            dt.Columns.Add("№ СТС");
            dt.Columns.Add("Год выпуска");
            dt.Columns.Add("Пробег", System.Type.GetType("System.Int32"));
            dt.Columns.Add("Дата последней записи о пробеге", Type.GetType("System.DateTime"));
            dt.Columns.Add("Собственник");
            dt.Columns.Add("Дата окончания гарантии", Type.GetType("System.DateTime"));
            dt.Columns.Add("Статус");

            foreach (Car car in cars)
                dt.Rows.Add(car.getRow());

            return dt;
        }

        public Car getItem(int idCar)
        {
            var cars = list.Where(car => car.IsEqualsID(idCar));
            
            return cars.Count() > 0 ? cars.First() as Car : null;
        }

        public DataTable ToDataTable(Status status)
        {
            switch (status)
            {
                case Status.Buy:
                    {
                        return ToDataTableBuy();
                    }
                case Status.Actual:
                    {
                        return ToDataTableActual();
                    }
                case Status.Repair:
                    {
                        RepairList repairList = RepairList.getInstance();
                        return repairList.ToDataTable();
                    }
                case Status.Sale:
                    {
                        CarSaleList carSaleList = CarSaleList.getInstance();
                        return carSaleList.ToDataTable();
                    }
                case Status.Invoice:
                    {
                        InvoiceList invoiceList = InvoiceList.getInstance();
                        return invoiceList.ToDataTable();
                    }
                case Status.Policy:
                    {
                        PolicyList policyList = PolicyList.getInstance();
                        return policyList.ToDataTable();
                    }
                case Status.DTP:
                    {
                        DTPList dtpList = DTPList.getInstance();
                        return dtpList.ToDataTable();
                    }
                case Status.Violation:
                    {
                        ViolationList violationList = ViolationList.getInstance();
                        return violationList.ToDataTable();
                    }
                case Status.DiagCard:
                    {
                        DiagCardList diagCardList = DiagCardList.getInstance();
                        return diagCardList.ToDataTable();
                    }
                case Status.TempMove:
                    {
                        TempMoveList tempMoveList = TempMoveList.getInstance();
                        return tempMoveList.ToDataTable();
                    }
                case Status.ShipPart:
                    {
                        ShipPartList shipPartList = ShipPartList.getInstance();
                        return shipPartList.ToDataTable();
                    }
                case Status.Account:
                    {
                        AccountList accountList = AccountList.getInstance();
                        return accountList.ToDataTable();
                    }
                case Status.FuelCard:
                    FuelCardList fuelCardList = FuelCardList.getInstance();
                    return fuelCardList.ToDataTable();
                case Status.Driver:
                    DriverList driverList = DriverList.getInstance();
                    return driverList.ToDataTable();
                default:
                    return ToDataTable();
            }
        }

        internal int getNextBBNumber()
        {
            if (list.Count > 0)
            {
                int maxNumber = list.Max(item => item.BBNumberInt);

                return maxNumber + 1;
            }
            
            return 1;
        }

        public void Delete(int idCar)
        {
            Car car = getItem(idCar);
            
            list.Remove(car);

            if (car != null)
                car.Delete();
        }
    }
}
