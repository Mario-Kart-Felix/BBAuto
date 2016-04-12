using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class MileageList : MainList
    {
        private List<Mileage> list;
        private static MileageList uniqueInstance;

        private MileageList()
        {
            list = new List<Mileage>();

            loadFromSql();
        }

        public static MileageList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new MileageList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Mileage");

            foreach (DataRow row in dt.Rows)
            {
                Mileage mileage = new Mileage(row);
                Add(mileage);
            }
        }

        public void Add(Mileage mileage)
        {
            if (list.Exists(item => item == mileage))
                return;

            list.Add(mileage);
        }

        public Mileage getItem(int id)
        {
            var mileages = list.Where(item => item.IsEqualsID(id));

            return getItem(mileages.ToList());
        }

        public Mileage getItem(Car car)
        {
            var mileages = list.Where(item => item.isEqualCarID(car)).OrderByDescending(item => item.Date);

            return getItem(mileages.ToList());
        }

        public Mileage getItem(Car car, Mileage current)
        {
            var mileages = list.Where(item => item.isEqualCarID(car) && item != current).OrderByDescending(item => item.Date);

            return getItem(mileages.ToList());
        }

        private Mileage getItem(List<Mileage> mileages)
        {
            return (mileages.Count() > 0) ? mileages.First() : new Mileage(0);
        }

        public void Delete(int idMileage)
        {
            Mileage mileage = getItem(idMileage);

            list.Remove(mileage);

            mileage.Delete();
        }

        public DataTable ToDataTable(Car car)
        {
            DataTable dt = createTable();

            var mileages = list.Where(item => item.isEqualCarID(car)).OrderBy(item => item.Date);

            foreach (Mileage mileage in mileages)
                dt.Rows.Add(mileage.getRow());

            return dt;
        }

        private DataTable createTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Дата", Type.GetType("System.DateTime"));
            dt.Columns.Add("Пробег", Type.GetType("System.Int32"));

            return dt;
        }

        internal int GetDistance(Car car, DateTime date)
        {
            DateTime datePrev = (date.Month == 12) ? new DateTime(date.Year - 1, 11, 1) : (date.Month == 1) ? new DateTime(date.Year - 1, 12, 1) : new DateTime(date.Year, date.Month - 1, 1);

            var listPrev = (from item in list
                           where item.isEqualCarID(car) && (item.Date.Year == datePrev.Year && item.Date.Month == datePrev.Month)
                           orderby item.Count descending
                           select Convert.ToInt32(item.Count)).ToList();

            var listCurrent = (from item in list
                           where item.isEqualCarID(car) && (item.Date.Year == date.Year && item.Date.Month == date.Month)
                           orderby item.Count descending
                           select Convert.ToInt32(item.Count)).ToList();
            
            if ((listCurrent.Count == 0) && (listPrev.Count == 0))
                throw new NullReferenceException("Показания одометра не найдены");
            else if (listCurrent.Count > 1)
                return listCurrent.First() - listCurrent.Last();
            else if ((listCurrent.Count == 1) && (listPrev.Count == 0))
                return listCurrent.First();
            else
                return listCurrent.First() - listPrev.First();
        }

        internal int GetBeginDistance(Car car, DateTime date)
        {
            DateTime datePrev = (date.Month == 12) ? new DateTime(date.Year - 1, 11, 1) : (date.Month == 1) ? new DateTime(date.Year - 1, 12, 1) : new DateTime(date.Year, date.Month - 1, 1);

            var listPrev = (from item in list
                            where item.isEqualCarID(car) && (item.Date.Year == datePrev.Year && item.Date.Month == datePrev.Month)
                            orderby item.Count descending
                            select Convert.ToInt32(item.Count)).ToList();

            var listCurrent = (from item in list
                               where item.isEqualCarID(car) && (item.Date.Year == date.Year && item.Date.Month == date.Month)
                               orderby item.Count descending
                               select Convert.ToInt32(item.Count)).ToList();

            if ((listCurrent.Count == 0) && (listPrev.Count == 0))
                throw new NullReferenceException("Показания одометра не найдены");
            else if (listCurrent.Count > 1)
                return listCurrent.Last();
            else if ((listCurrent.Count == 1) && (listPrev.Count == 0))
                return listCurrent.First();
            else
                return listPrev.First();
        }

        internal int GetEndDistance(Car car, DateTime date)
        {
            DateTime datePrev = (date.Month == 12) ? new DateTime(date.Year - 1, 11, 1) : (date.Month == 1) ? new DateTime(date.Year - 1, 12, 1) : new DateTime(date.Year, date.Month - 1, 1);

            var listPrev = (from item in list
                            where item.isEqualCarID(car) && (item.Date.Year == datePrev.Year && item.Date.Month == datePrev.Month)
                            orderby item.Count descending
                            select Convert.ToInt32(item.Count)).ToList();

            var listCurrent = (from item in list
                               where item.isEqualCarID(car) && (item.Date.Year == date.Year && item.Date.Month == date.Month)
                               orderby item.Count descending
                               select Convert.ToInt32(item.Count)).ToList();

            if ((listCurrent.Count == 0) && (listPrev.Count == 0))
                throw new NullReferenceException("Показания одометра не найдены");
            else if (listCurrent.Count > 1)
                return listCurrent.First();
            else if ((listCurrent.Count == 1) && (listPrev.Count == 0))
                return listCurrent.First();
            else
                return listCurrent.First();
        }
    }
}
