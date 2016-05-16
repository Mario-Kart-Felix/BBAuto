using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class Driver : MainDictionary
    {
        private string _fio;
        private DateTime _dateBirth;        
        private string _mobile;
        private int _fired;
        private int _expSince;
        private string _login;
        private int _sex;
        private int _decret;
        private DateTime _dateStopNotification;                
        private Region _region;
        private string _number;
        private int _idPosition;
        private int _idDept;
        private int _idOwner;
        private int _isDriver;
        private int _from1C;

        public string email;
        public string suppyAddress;
        
        public string Fio
        {
            set { _fio = value.Trim(); }
        }

        public string DateBirth
        {
            get { return (_dateBirth.Year == 1) ? string.Empty : _dateBirth.ToShortDateString(); }
            set
            {
                DateTime date = DateTime.Today;
                DateTime.TryParse(value, out date);
                _dateBirth = date;
            }
        }

        public string Mobile
        {
            get
            {
                if ((_mobile == null) || (_mobile == string.Empty))
                    return "(нет данных)";
                else
                    return string.Concat("+7 (", _mobile.Substring(0, 3), ") ", _mobile.Substring(3, 3), "-", _mobile.Substring(6, 2), "-", _mobile.Substring(8, 2));
            }
            set { _mobile = value.Replace("+7", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""); }
        }

        public bool Fired
        {
            get { return Convert.ToBoolean(_fired); }
            set { _fired = Convert.ToInt32(value); }
        }

        public bool Decret
        {
            get { return Convert.ToBoolean(_decret); }
            set { _decret = Convert.ToInt32(value); }
        }

        public string ExpSince
        {
            get { return _expSince == 0 ? string.Empty : _expSince.ToString(); }
            set { int.TryParse(value, out _expSince); }
        }

        public int PositionID
        {
            get { return _idPosition; }
            set { _idPosition = value; }
        }

        public string Position
        {
            get
            {
                Positions positions = Positions.getInstance();
                return positions.getItem(_idPosition);
            }
            set
            {
                int tempID = _idPosition;

                Positions positions = Positions.getInstance();
                _idPosition = positions.getItem(value);

                if (_idPosition == 0)
                {
                    OneStringDictionary.save("Position", 0, value);
                    positions.ReLoad();
                    _idPosition = positions.getItem(value);
                }
            }
        }

        public int DeptID
        { 
            get { return _idDept; }
            set { _idDept = value; }
        }

        public string Dept
        {
            get
            {
                Depts depts = Depts.getInstance();
                return depts.getItem(_idDept);
            }
            set
            {
                int tempID = _idDept;

                Depts depts = Depts.getInstance();
                _idDept = depts.getItem(value);

                if (_idDept == 0)
                {
                    OneStringDictionary.save("Dept", 0, value);
                    depts.ReLoad();
                    _idDept = depts.getItem(value);
                }
            }
        }

        public int OwnerID
        {
            get { return _idOwner; }
            set { _idOwner = value; }
        }

        public string CompanyName
        {
            get
            {
                Owners owners = Owners.getInstance();
                return owners.getItem(_idOwner);
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int tempID = _idOwner;

                    Owners owners = Owners.getInstance();
                    _idOwner = owners.getItem(value);

                    if (_idOwner == 0)
                    {
                        OneStringDictionary.save("Owner", 0, value);
                        owners.ReLoad();
                        _idOwner = owners.getItem(value);
                    }
                }
            }
        }

        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        public RolesList UserRole
        {
            get
            {
                UserAccessList userAccessList = UserAccessList.getInstance();
                UserAccess userAccess = userAccessList.getItem(_id);

                int idRole = 0;
                int.TryParse(userAccess.IDRole, out idRole);

                return (RolesList)idRole;
            }
        }

        public bool IsOne
        {
            get
            {
                DriverList driverList = DriverList.getInstance();
                return driverList.CountDriversInRegion(Region) == 1;
            }
        }

        public int SexIndex
        {
            get { return _sex; }
            set { _sex = value; }
        }

        public string Sex
        {
            get { return _sex == 0 ? "мужской" : "женский"; }
            set { _sex = (value == "Мужской") ? 0 : 1; }
        }
        
        public Region Region
        {
            get { return _region; }
            set { _region = value; }
        }

        public bool IsDriver
        {
            get { return Convert.ToBoolean(_isDriver); }
            set { _isDriver = Convert.ToInt32(value); }
        }

        public bool From1C
        {
            get { return Convert.ToBoolean(_from1C); }
            set { _from1C = Convert.ToInt32(value); }
        }

        private string Status
        {
            get
            {
                return (Fired) ? "Уволенный" : (Decret) ? "В декрете" : ((_idOwner < 3) && string.IsNullOrEmpty(_number)) ? "нет табельного" : "";
            }
        }

        public Driver()
        {
            _id = 0;
            _isDriver = 0;
            _mobile = string.Empty;
            suppyAddress = string.Empty;
        }

        public bool NotificationStop
        {
            get { return _dateStopNotification.Year == 1 ? false : true; }
        }

        public DateTime DateStopNotification
        {
            get { return _dateStopNotification; }
            set { _dateStopNotification = value; }
        }

        public string Number
        {
            get { return _number; }
            set
            {
                _number = value;
                if ((!string.IsNullOrEmpty(value)) && (_idOwner < 3))
                    From1C = true;
            }
        }

        public Driver(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            _fio = row.ItemArray[1].ToString();

            RegionList regionList = RegionList.getInstance();
            int idRegion;
            int.TryParse(row.ItemArray[2].ToString(), out idRegion);
            _region = regionList.getItem(idRegion);

            DateTime.TryParse(row.ItemArray[3].ToString(), out _dateBirth);
            _mobile = row.ItemArray[4].ToString();
            email = row.ItemArray[5].ToString();
            int.TryParse(row.ItemArray[6].ToString(), out _fired);
            int.TryParse(row.ItemArray[7].ToString(), out _expSince);
            int.TryParse(row.ItemArray[8].ToString(), out _idPosition);
            int.TryParse(row.ItemArray[9].ToString(), out _idDept);
            _login = row.ItemArray[10].ToString();
            int.TryParse(row.ItemArray[11].ToString(), out _idOwner);
            suppyAddress = row.ItemArray[12].ToString();
            int.TryParse(row.ItemArray[13].ToString(), out _sex);
            int.TryParse(row.ItemArray[14].ToString(), out _decret);
            DateTime.TryParse(row.ItemArray[15].ToString(), out _dateStopNotification);
            _number = row.ItemArray[16].ToString();
            int.TryParse(row.ItemArray[17].ToString(), out _isDriver);
            int.TryParse(row.ItemArray[18].ToString(), out _from1C);
        }

        public override void Save()
        {
            DriverList driverList = DriverList.getInstance();
                        
            string dateBirthSql = string.Empty;
            if (DateBirth != string.Empty)
                dateBirthSql = string.Concat(_dateBirth.Year.ToString(), "-", _dateBirth.Month.ToString(), "-", _dateBirth.Day.ToString());

            string dateStopNotificationSql = string.Empty;
            if (DateStopNotification.Year != 1)
                dateStopNotificationSql = string.Concat(DateStopNotification.Year.ToString(), "-", DateStopNotification.Month.ToString(), "-", DateStopNotification.Day.ToString());

            int.TryParse(_provider.Insert("Driver", _id, GetName(NameType.Full), Region.ID, dateBirthSql, _mobile, email, _fired, _expSince, _idPosition, _idDept, _login, _idOwner, suppyAddress, SexIndex, _decret,
                dateStopNotificationSql, _number, _isDriver, _from1C), out _id);

            driverList.Add(this);
        }

        public Instraction createInstraction()
        {
            return new Instraction(_id);
        }

        public MedicalCert createMedicalCert()
        {
            return new MedicalCert(_id);
        }

        public Passport createPassport()
        {
            return new Passport(_id);
        }     

        public DriverLicense createDriverLicense()
        {
            return new DriverLicense(_id);
        }

        public ColumnSize CreateColumnSize(Status status)
        {
            return new ColumnSize(_id, status);
        }

        internal override object[] getRow()
        {
            MedicalCertList medicalCertList = MedicalCertList.getInstance();
            MedicalCert medicalCert = medicalCertList.getItem(this);

            LicenseList licenseList = LicenseList.getInstance();
            DriverLicense license = licenseList.getItem(this);

            DriverCarList driverCarList = DriverCarList.getInstance();
            Car car = driverCarList.GetCar(this);
            
            return new object[] { _id, 0, GetName(NameType.Full), (license.IsActual()) ? "есть" : "нет", (medicalCert.IsActual()) ? "есть" : "нет",
                (car == null) ? "нет автомобиля" : car.ToString(), Region.Name, CompanyName, Status };
        }
        
        public override bool Equals(object obj)
        {
            Driver driver2 = obj as Driver;
            return (_id == driver2._id);
        }
        
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
        
        public string GetName(NameType nameType)
        {
            if (FieldIsEmpty(_fio))
                return "(нет водителя)";

            if (nameType == NameType.Short)
                return GetNameShort();
            if (nameType == NameType.Genetive)
                return GetNameGenetive();

            return _fio;
        }

        private string GetNameShort()
        {
            string[] list = _fio.Split(' ');
            return (list.Count() == 3) ? string.Concat(list[0], " ", list[1][0].ToString(), ".", list[2][0].ToString(), ".") : _fio;
        }

        private string GetNameGenetive()
        {
            string[] list = _fio.Split(' ');
            if (list.Count() == 3)
            {
                string SecondName = list[0];
                char LastSymbol = SecondName[SecondName.Length - 1];

                if (Sex == "мужской")
                {
                    if ((LastSymbol == 'в') || (LastSymbol == 'н'))
                        SecondName += "а";
                }
                else
                {
                    if (LastSymbol == 'а')
                        SecondName = SecondName.Substring(0, SecondName.Length - 1) + "ой";
                }
                return string.Concat(SecondName, " ", list[1][0].ToString(), ".", list[2][0].ToString(), ".");
            }
            else
                return _fio;
        }

        private bool FieldIsEmpty(string field)
        {
            return ((field == null) || (field == string.Empty));
        }
    }
}
