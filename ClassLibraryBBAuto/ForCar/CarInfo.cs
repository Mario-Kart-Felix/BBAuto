using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class CarInfo
    {
        private const int MILEAGE_GUARANTEE = 100000;
        private Car _car;

        public CarInfo(Car car)
        {
            _car = car;
        }

        public string Mark
        {
            get
            {
                Marks marks = Marks.getInstance();
                return marks.getItem(Convert.ToInt32(_car.MarkID));
            }
        }

        public string Model
        {
            get
            {
                ModelList models = ModelList.getInstance();
                return models.getItem(Convert.ToInt32(_car.ModelID)).Name;
            }
        }

        public string Color
        {
            get
            {
                Colors colors = Colors.getInstance();
                return colors.getItem(Convert.ToInt32(_car.ColorID));
            }
        }

        public string Owner
        {
            get
            {
                Owners owners = Owners.getInstance();
                return owners.getItem(Convert.ToInt32(_car.ownerID));
            }
        }

        public Grade Grade
        {
            get
            {
                GradeList gradeList = GradeList.getInstance();
                return gradeList.getItem(Convert.ToInt32(_car.GradeID));
            } 
        }

        public DateTime Guarantee
        {
            get
            {
                MileageList mileageList = MileageList.getInstance();
                Mileage mileage = mileageList.getItem(_car);

                DateTime dateEnd = _car.dateGet.AddYears(3);

                int miles;
                int.TryParse(mileage.Count, out miles);

                return ((miles < MILEAGE_GUARANTEE) && (DateTime.Today < dateEnd)) ? dateEnd : new DateTime(1, 1, 1);
            }
        }

        public bool IsSale
        {
            get
            {
                CarSaleList carSaleList = CarSaleList.getInstance();
                CarSale carSale = carSaleList.getItem(_car);
                return carSale != null;
            }
        }
        
        public Driver Driver
        {
            get
            {
                DriverCarList driverCarList = DriverCarList.getInstance();
                return driverCarList.GetDriver(_car) ?? new Driver();
            }
        }
        
        public PTS pts
        {
            get
            {
                PTSList ptsList = PTSList.getInstance();
                return ptsList.getItem(_car);
            }
        }

        public STS sts
        {
            get
            {
                STSList stsList = STSList.getInstance();
                return stsList.getItem(_car);
            }
        }
        
        public DataTable ToDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Название");
            dt.Columns.Add("Значение");

            dt.Rows.Add("Марка", Mark);
            dt.Rows.Add("Модель", Model);
            dt.Rows.Add("Год выпуска", _car.Year);
            dt.Rows.Add("Цвет", Color);
            dt.Rows.Add("Собственник", Owner);
            dt.Rows.Add("Дата покупки", _car.dateGet.ToShortDateString());
            dt.Rows.Add("Модель № двигателя", _car.eNumber);
            dt.Rows.Add("№ кузова", _car.bodyNumber);
            dt.Rows.Add("Дата выдачи ПТС:", pts.Date.ToShortDateString());
            dt.Rows.Add("Дата выдачи СТС:", sts.Date.ToShortDateString());
            
            return dt;
        }
    }
}
