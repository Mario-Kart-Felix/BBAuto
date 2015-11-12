using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class CarSale : MainDictionary
    {
        private DateTime _date;
        public string comm;

        public DateTime DateForSort { get { return _date.Year == 1 ? DateTime.Today : _date; } }

        public string Date
        {
            get { return _date.Year == 1 ? string.Empty : _date.ToShortDateString(); }
            set { DateTime.TryParse(value, out _date); }
        }
        
        internal CarSale(DataRow row)
        {
            fillFields(row);
        }

        public CarSale(int idCar)
        {
            _id = idCar;
            comm = string.Empty;
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            Date = row.ItemArray[1].ToString();
            comm = row.ItemArray[2].ToString();
        }

        public override void Save()
        {
            string Sqldate = string.Empty;
            if (Date != string.Empty)
                Sqldate = string.Concat(_date.Year.ToString(), "-", _date.Month.ToString(), "-", _date.Day.ToString());

            _provider.Insert("CarSale", _id, comm, Sqldate);

            CarSaleList carSaleList = CarSaleList.getInstance();
            carSaleList.Add(this);
        }

        internal override object[] getRow()
        {
            CarList carList = CarList.getInstance();
            Car car = carList.getItem(_id);

            InvoiceList invoiceList = InvoiceList.getInstance();
            Invoice invoice = invoiceList.getItem(car);

            PTSList ptsList = PTSList.getInstance();
            PTS pts = ptsList.getItem(car);

            STSList stsList = STSList.getInstance();
            STS sts = stsList.getItem(car);

            int idRegion = 0;
            int.TryParse(car.regionUsingID.ToString(), out idRegion);

            Regions regions = Regions.getInstance();
            string regionName;
            if (invoice == null)
                regionName = regions.getItem(idRegion);
            else
                regionName = regions.getItem(Convert.ToInt32(invoice.RegionToID));

            return new object[] { _id, _id, car.BBNumber, car.grz, regionName, _date, comm, pts.Number, sts.Number, car.GetStatus() };
        }

        internal override void Delete()
        {
            _provider.Delete("CarSale", _id);
        }

        internal bool IsEqualsID(Car car)
        {
            return car.IsEqualsID(_id);
        }
    }
}
