using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain
{
    public class Mileage : MainDictionary
    {
        private int _idCar;
        private DateTime _date;
        private int _count;

        public string Count
        {
            get { return _count == 0 ? string.Empty : _count.ToString(); }
        }
        
        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        internal Mileage(int idCar)
        {
            _idCar = idCar;
            _date = DateTime.Today.Date;
            _id = 0;
        }

        public Mileage(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out _idCar);
            DateTime.TryParse(row.ItemArray[2].ToString(), out _date);
            int.TryParse(row.ItemArray[3].ToString(), out _count);
        }

        public override void Save()
        {
            int.TryParse(_provider.Insert("Mileage", _id, _idCar, _date, _count), out _id);

            MileageList mileageList = MileageList.getInstance();
            mileageList.Add(this);
        }

        internal override object[] getRow()
        {
            return new object[3] { _id, _date, _count };
        }

        public bool isEqualCarID(Car car)
        {
            return car.IsEqualsID(_idCar);
        }

        internal override void Delete()
        {
            _provider.Delete("Mileage", _id);
        }

        internal DateTime MonthToString()
        {
            MyDateTime myDate = new MyDateTime(Date.ToShortDateString());

            return _count == 0 ? new DateTime(DateTime.Today.Year, 1, 31) : Date;
        }

        public void SetCount(string value)
        {
            Mileage mileage = GetPrev();

            int count;

            if (!int.TryParse(value.Replace(" ", ""), out count))
                throw new InvalidCastException();

            int prevCount;
            int.TryParse(mileage.Count, out prevCount);

            if ((count < prevCount) && (Date > mileage.Date))
                throw new InvalidConstraintException();

            if (count >= 1000000)
                throw new OverflowException();

            _count = count;
        }

        public string PrevToString()
        {
            Mileage mileage = GetPrev();

            return mileage.ToString();
        }

        private Mileage GetPrev()
        {
            CarList carList = CarList.getInstance();
            Car car = carList.getItem(_idCar);

            MileageList mileageList = MileageList.getInstance();
            return mileageList.getItem(car, this);
        }

        public override string ToString()
        {
            return (Count == string.Empty) ? "(нет данных)" : string.Concat(MyString.GetFormatedDigitInteger(Count), " км от ", Date.ToShortDateString());
        }
    }
}
