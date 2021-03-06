using System;
using System.Data;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;
using BBAuto.Logic.Tables;

namespace BBAuto.Logic.Entities
{
  public class Driver : MainDictionary
  {
    private string _fio;
    private DateTime _dateBirth;
    private string _mobile;
    private int _fired;
    private int _expSince;
    private int _decret;
    private DateTime _dateStopNotification;
    private string _number;
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
        {
          if (_mobile.Length == 10)
            return string.Concat("+7 (", _mobile.Substring(0, 3), ") ", _mobile.Substring(3, 3), "-",
              _mobile.Substring(6, 2), "-", _mobile.Substring(8, 2));
          else
            return _mobile;
        }
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

    public int PositionID { get; set; }

    public string Position
    {
      get
      {
        Positions positions = Positions.getInstance();
        return positions.getItem(PositionID);
      }
      set
      {
        int tempID = PositionID;

        Positions positions = Positions.getInstance();
        PositionID = positions.getItem(value);

        if (PositionID == 0)
        {
          OneStringDictionary.save("Position", 0, value);
          positions.ReLoad();
          PositionID = positions.getItem(value);
        }
      }
    }

    public int DeptID { get; set; }

    public string Dept
    {
      get
      {
        Depts depts = Depts.getInstance();
        return depts.getItem(DeptID);
      }
      set
      {
        int tempID = DeptID;

        Depts depts = Depts.getInstance();
        DeptID = depts.getItem(value);

        if (DeptID == 0)
        {
          OneStringDictionary.save("Dept", 0, value);
          depts.ReLoad();
          DeptID = depts.getItem(value);
        }
      }
    }

    public int OwnerID { get; set; }

    public string CompanyName
    {
      get
      {
        Owners owners = Owners.getInstance();
        return owners.getItem(OwnerID);
      }
      set
      {
        if (!string.IsNullOrEmpty(value))
        {
          int tempID = OwnerID;

          Owners owners = Owners.getInstance();
          OwnerID = owners.getItem(value);

          if (OwnerID == 0)
          {
            OneStringDictionary.save("Owner", 0, value);
            owners.ReLoad();
            OwnerID = owners.getItem(value);
          }
        }
      }
    }

    public string Login { get; set; }

    public RolesList UserRole
    {
      get
      {
        UserAccessList userAccessList = UserAccessList.getInstance();
        UserAccess userAccess = userAccessList.getItem(Id);

        return (RolesList) userAccess.RoleId;
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

    public int SexIndex { get; set; }

    public string Sex
    {
      get { return SexIndex == 0 ? "мужской" : "женский"; }
      set { SexIndex = (value == "Мужской") ? 0 : 1; }
    }

    public Region Region { get; set; }

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
        return (Fired)
          ? "Уволенный"
          : (Decret)
            ? "В декрете"
            : ((OwnerID < 3) && string.IsNullOrEmpty(_number))
              ? "нет табельного"
              : "";
      }
    }

    /*TODO: проверять включена ли рассылка */
    public bool NotificationStop
    {
      get { return _dateStopNotification.Year == 1 || _dateStopNotification <= DateTime.Today ? false : true; }
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
        if ((!string.IsNullOrEmpty(value)) && (OwnerID < 3))
          From1C = true;
      }
    }

    public Driver()
    {
      Id = 0;
      _isDriver = 0;
      _mobile = string.Empty;
      suppyAddress = string.Empty;
    }

    public Driver(DataRow row)
    {
      int id;
      int.TryParse(row.ItemArray[0].ToString(), out id);
      Id = id;

      _fio = row.ItemArray[1].ToString();

      int idRegion;
      int.TryParse(row.ItemArray[2].ToString(), out idRegion);
      Region = RegionList.getInstance().getItem(idRegion);

      DateTime.TryParse(row.ItemArray[3].ToString(), out _dateBirth);
      _mobile = row.ItemArray[4].ToString();
      email = row.ItemArray[5].ToString();
      int.TryParse(row.ItemArray[6].ToString(), out _fired);
      int.TryParse(row.ItemArray[7].ToString(), out _expSince);

      int idPosition;
      int.TryParse(row.ItemArray[8].ToString(), out idPosition);
      PositionID = idPosition;

      int idDept;
      int.TryParse(row.ItemArray[9].ToString(), out idDept);
      DeptID = idDept;

      Login = row.ItemArray[10].ToString();

      int idOwner;
      int.TryParse(row.ItemArray[11].ToString(), out idOwner);
      OwnerID = idOwner;

      suppyAddress = row.ItemArray[12].ToString();

      int idSex;
      int.TryParse(row.ItemArray[13].ToString(), out idSex);
      SexIndex = idSex;

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
        dateBirthSql = string.Concat(_dateBirth.Year.ToString(), "-", _dateBirth.Month.ToString(), "-",
          _dateBirth.Day.ToString());

      string dateStopNotificationSql = string.Empty;
      if (DateStopNotification.Year != 1)
        dateStopNotificationSql = string.Concat(DateStopNotification.Year.ToString(), "-",
          DateStopNotification.Month.ToString(), "-", DateStopNotification.Day.ToString());

      int id;
      int.TryParse(Provider.Insert("Driver", Id, GetName(NameType.Full), Region.Id, dateBirthSql, _mobile, email,
        _fired, _expSince, PositionID,
        DeptID, Login, OwnerID, suppyAddress, SexIndex, _decret,
        dateStopNotificationSql, _number, _isDriver, _from1C), out id);
      Id = id;

      driverList.Add(this);
    }

    public Instraction createInstraction()
    {
      return new Instraction(this);
    }

    public MedicalCert createMedicalCert()
    {
      return new MedicalCert(this);
    }

    public Passport createPassport()
    {
      return new Passport(this);
    }

    public DriverLicense createDriverLicense()
    {
      return new DriverLicense(this);
    }

    public ColumnSize CreateColumnSize(Status status)
    {
      return new ColumnSize(Id, status);
    }

    internal override object[] GetRow()
    {
      MedicalCertList medicalCertList = MedicalCertList.getInstance();
      MedicalCert medicalCert = medicalCertList.getItem(this);
      string medicalCertStatus = ((medicalCert == null) || (!medicalCert.IsActual())) ? "нет" : "есть";

      LicenseList licenseList = LicenseList.getInstance();
      DriverLicense license = licenseList.getItem(this);
      string licenseStatus = ((license == null) || (!license.IsActual())) ? "нет" : "есть";

      DriverCarList driverCarList = DriverCarList.getInstance();
      Car car = driverCarList.GetCar(this);

      return new object[]
      {
        Id,
        0,
        GetName(NameType.Full),
        licenseStatus,
        medicalCertStatus,
        (car == null) ? "нет автомобиля" : car.ToString(),
        Region.Name,
        CompanyName,
        Status
      };
    }

    public string GetName(NameType nameType)
    {
      if (string.IsNullOrEmpty(_fio))
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
      return (list.Count() == 3)
        ? string.Concat(list[0], " ", list[1][0].ToString(), ".", list[2][0].ToString(), ".")
        : _fio;
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
  }
}
