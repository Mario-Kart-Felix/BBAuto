using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ClassLibraryBBAuto
{
    public static class MileageFill
    {
        private static DateTime _date;

        public static void Begin(string folder, DateTime date)
        {
            _date = date;

            string[] filenames = Directory.GetFiles(folder);

            foreach (string fileName in filenames)
            {
                ReadFile(fileName);
            }
        }

        private static void ReadFile(string path)
        {
            ExcelDoc excelDoc = new ExcelDoc(path);
            excelDoc.SetList("Расходы по а-м");

            string grz = excelDoc.getValue("4", "2").ToString();

            Car car = GetCar(grz);

            if (car != null)
            {
                string value = excelDoc.getValue("8", "3").ToString();

                SetMileage(car, value);
            }
        }

        private static Car GetCar(string grz)
        {
            if (grz == string.Empty)
                return null;

            CarList carList = CarList.getInstance();
            return carList.getItem(grz);
        }

        private static void SetMileage(Car car, string value)
        {
            int count;
            int.TryParse(value, out count);

            if (count == 0)
                return;

            MileageList mileageList = MileageList.getInstance();
            Mileage mileage = mileageList.getItem(car);

            if (count > Convert.ToInt32(mileage.Count))
            {
                if (mileage.Count != string.Empty)
                {
                    mileage = car.createMileage();
                }

                mileage.Date = new DateTime(_date.Year, _date.Month, DateTime.DaysInMonth(_date.Year, _date.Month));
                mileage.SetCount(value);
            }
        }
    }
}
