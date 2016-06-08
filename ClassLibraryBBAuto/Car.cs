using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class Car : MainDictionary
    {
        public CarInfo info;

        private int _idGrade;
        private int _year;
        private int _idColor;
        private int _idRegionBuy;
        private int _idRegionUsing;
        private int _idOwner;
        private int _idDriver;
        private int _isGet;
        private int _isLising;
        private DateTime _lisingDate;
        private string _invertoryNumber;
                
        private int _idMark;
        private int _idModel;
        public string vin;
        public string grz;

        private int _number;

        public string eNumber;
        public string bodyNumber;        
        public DateTime dateOrder;
        public DateTime dateGet;
        public string dop;
        public string events;
        public int idDiller;
        public double cost;

        public string GradeID
        {
            get { return _idGrade.ToString(); }
            set { int.TryParse(value, out _idGrade); }
        }

        public string ModelID
        {
            get { return _idModel.ToString(); }
            set { int.TryParse(value, out _idModel); }
        }

        public string MarkID
        {
            get { return _idMark.ToString(); }
            set { int.TryParse(value, out _idMark); }
        }

        public string Year
        {
            get { return _year.ToString(); }
            set { _year = Convert.ToInt32(value); }
        }

        public object ColorID
        {
            get { return _idColor.ToString(); }
            set
            {
                if (value != null)
                    int.TryParse(value.ToString(), out _idColor);
            }
        }

        public object RegionBuyID
        {
            get { return _idRegionBuy.ToString(); }
            set
            {
                if (value != null)
                    int.TryParse(value.ToString(), out _idRegionBuy);
            }
        }

        public object ownerID
        {
            get { return _idOwner; }
            set
            {
                if (value != null)
                    int.TryParse(value.ToString(), out _idOwner);
            }
        }

        public object regionUsingID
        {
            get { return _idRegionUsing.ToString(); }
            set
            {
                if (value != null)
                    int.TryParse(value.ToString(), out _idRegionUsing);
            }
        }

        public object driverID
        {
            get { return _idDriver.ToString(); }
            set
            {
                if (value != null)
                    int.TryParse(value.ToString(), out _idDriver);
            }
        }

        public bool IsGet
        {
            get { return Convert.ToBoolean(_isGet); }
            set { _isGet = Convert.ToInt32(value); }
        }

        public string BBNumber
        {
            get { return _number < 100 ? "АМ-0" + _number.ToString() : "АМ-" + _number.ToString(); }
        }

        public int BBNumberInt
        {
            get { return _number; }
            set { _number = value; }
        }

        public string Lising
        {
            get { return _isLising == 1 ? _lisingDate.Date.ToShortDateString() : string.Empty; }
            set
            {
                if (DateTime.TryParse(value, out _lisingDate))
                    _isLising = 1;
                else
                {
                    _isLising = 0;
                    _lisingDate = DateTime.Today;
                }
            }
        }

        public string InvertoryNumber
        {
            get { return _invertoryNumber; }
            set { _invertoryNumber = value; }
        }

        public Car()
        {
            _id = 0;

            CarList carList = CarList.getInstance();
            _number = carList.getNextBBNumber();
            dateOrder = DateTime.Today;
            dateGet = DateTime.Today;
            _year = DateTime.Today.Year;

            Init();
        }

        public Car(DataRow row)
        {
            fillField(row);

            Init();
        }

        private void Init()
        {
            info = new CarInfo(this);
        }

        private void fillField(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out _number);
            grz = row.ItemArray[2].ToString();
            vin = row.ItemArray[3].ToString();
            Year = row.ItemArray[4].ToString();
            eNumber = row.ItemArray[5].ToString();
            bodyNumber = row.ItemArray[6].ToString();
            int.TryParse(row.ItemArray[7].ToString(), out _idMark);
            int.TryParse(row.ItemArray[8].ToString(), out _idModel);
            GradeID = row.ItemArray[9].ToString();
            ColorID = row.ItemArray[10];

            fillCarBuy(row);
        }

        private void fillCarBuy(DataRow row)
        {
            ownerID = row.ItemArray[11].ToString();
            RegionBuyID = row.ItemArray[12].ToString();
            regionUsingID = row.ItemArray[13].ToString();
            driverID = row.ItemArray[14].ToString();

            if (!DateTime.TryParse(row.ItemArray[15].ToString(), out dateOrder))
                dateOrder = DateTime.Today;

            _isGet = Convert.ToInt32(row.ItemArray[16]);

            if (!DateTime.TryParse(row.ItemArray[17].ToString(), out dateGet))
                dateGet = DateTime.Today;

            double.TryParse(row.ItemArray[18].ToString(), out cost);

            dop = row.ItemArray[19].ToString();
            events = row.ItemArray[20].ToString();

            int.TryParse(row.ItemArray[21].ToString(), out idDiller);

            Lising = row.ItemArray[22].ToString();
            InvertoryNumber = row.ItemArray[23].ToString();
        }

        public override void Save()
        {
            int.TryParse(_provider.Insert("Car", _id, _number, grz, vin, Year, eNumber, bodyNumber, GradeID, ColorID, _isLising, _lisingDate, _invertoryNumber), out _id);

            CarList carList = CarList.getInstance();
            carList.Add(this);

            saveCarBuy();
        }

        private void saveCarBuy()
        {
            _provider.Insert("CarBuy", _id, _idOwner, _idRegionBuy, _idRegionUsing, driverID, dateOrder, _isGet, dateGet, cost, dop, events, idDiller);
        }

        public DTP createDTP()
        {
            return new DTP(_id);
        }
        
        public Policy CreatePolicy()
        {
            return new Policy(_id);
        }
        
        public Violation createViolation()
        {
            return new Violation(_id);
        }

        public ShipPart createShipPart()
        {
            return new ShipPart(_id);
        }

        public DataTable getCarInfo()
        {
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("Название");
            dt2.Columns.Add("Значение");

            dt2.Rows.Add("Год выпуска", Year);
            dt2.Rows.Add("Цвет", info.Color);
            dt2.Rows.Add("Собственник", info.Owner);
            dt2.Rows.Add("Дата покупки", dateGet.ToShortDateString());
            dt2.Rows.Add("Мощность двигателя", info.Grade.ePower);
            dt2.Rows.Add("Объем двигателя", info.Grade.eVol);
            dt2.Rows.Add("Разрешенная максимальная масса", info.Grade.maxLoad);
            dt2.Rows.Add("Масса без нагрузки", info.Grade.noLoad);
            dt2.Rows.Add("Модель № двигателя", eNumber);
            dt2.Rows.Add("№ кузова", bodyNumber);

            return dt2;
        }

        public DataTable getDataTableDiagCard()
        {
            DiagCardList diagCardList = DiagCardList.getInstance();

            return diagCardList.ToDataTable(this);
        }

        public DiagCard createDiagCard()
        {
            return new DiagCard(_id);
        }

        public Mileage createMileage()
        {
            return new Mileage(_id);
        }

        public Invoice createInvoice()
        {
            return new Invoice(_id);
        }
        
        public Repair createRepair()
        {
            return new Repair(_id);
        }

        public PTS createPTS()
        {
            return new PTS(_id);
        }

        public STS createSTS()
        {
            return new STS(_id);
        }

        public TempMove createTempMove()
        {
            return new TempMove(_id);
        }

        internal override object[] getRow()
        {
            MileageList mileageList = MileageList.getInstance();
            Mileage mileage = mileageList.getItem(this);
            InvoiceList invoiceList = InvoiceList.getInstance();
            Invoice invoice = invoiceList.getItem(this);

            PTSList ptsList = PTSList.getInstance();
            PTS pts = ptsList.getItem(this);

            STSList stsList = STSList.getInstance();
            STS sts = stsList.getItem(this);

            Regions regions = Regions.getInstance();
            string regionName = (invoice == null) ? regions.getItem(_idRegionUsing) : regions.getItem(Convert.ToInt32(invoice.RegionToID));

            int mileageInt;
            int.TryParse(mileage.Count, out mileageInt);

            return new object[] { _id, _id, BBNumber, grz, info.Mark, info.Model, vin, regionName,
                info.Driver.GetName(NameType.Full), pts.Number, sts.Number, Year, mileageInt,
                mileage.MonthToString(), info.Owner, info.Guarantee, GetStatus()};
        }

        public CarDoc createCarDoc(string file)
        {
            CarDoc carDoc = new CarDoc(_id);
            carDoc.File = file;
            carDoc.Name = System.IO.Path.GetFileNameWithoutExtension(file);

            return carDoc;
        }

        public override string ToString()
        {
            return (_id == 0) ? "нет данных" : string.Concat(info.Mark, " ", info.Model, " ", grz);
        }

        internal override void Delete()
        {
            _provider.Delete("Car", _id);
        }

        public string GetStatus()
        {
            DTPList dtpList = DTPList.getInstance();
            DTP dtp = dtpList.GetLast(this);

            StatusAfterDTPs statusAfterDTPs = StatusAfterDTPs.getInstance();
            string statusAfterDTP = statusAfterDTPs.getItem(Convert.ToInt32(dtp.IDStatusAfterDTP));
            
            CarSaleList carSaleList = CarSaleList.getInstance();
            CarSale carSale = carSaleList.getItem(_id);

            if (info.IsSale && carSale.Date != string.Empty)
                return "продан";
            if (info.IsSale)
                return "на продажу";

            if (!this.IsGet)
                return "покупка";

            if (statusAfterDTP == "А/м НЕ на ходу")
                return "в ремонте";

            return "на ходу";
        }
    }
}