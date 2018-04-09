using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.ForCar;
using BBAuto.Logic.ForDriver;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;
using BBAuto.Logic.Tables;

namespace BBAuto.Logic.Common
{
  public class CreateDocument
  {
    private readonly Invoice _invoice;
    private readonly Car _car;
    private ExcelDoc _excelDoc;

    private DriverList _driverList;

    public CreateDocument()
    {
      Init();
    }

    public CreateDocument(Car car, Invoice invoice = null)
    {
      Init();
      _car = car;
      _invoice = invoice;
    }

    private void Init()
    {
      _driverList = DriverList.getInstance();
    }

    public void CreateExcelFromDGV(DataGridView dgv)
    {
      var minRowIndex = 0;
      var maxRowIndex = 0;
      var minColumnIndex = 0;
      var maxColumnIndex = 0;

      foreach (DataGridViewCell cell in dgv.SelectedCells)
      {
        if (cell.RowIndex > maxRowIndex)
          maxRowIndex = cell.RowIndex;
        if ((cell.RowIndex < minRowIndex) || (minRowIndex == 0))
          minRowIndex = cell.RowIndex;

        if (cell.ColumnIndex > maxColumnIndex)
          maxColumnIndex = cell.ColumnIndex;
        if ((cell.ColumnIndex < minColumnIndex) || (minColumnIndex == 0))
          minColumnIndex = cell.ColumnIndex;
      }

      var rowCount = maxRowIndex + 1;
      var columnCount = maxColumnIndex + 1;

      CreateExcelFromDGV(dgv, minRowIndex, rowCount, minColumnIndex, columnCount);
    }

    public void CreateExcelFromAllDgv(DataGridView dgv)
    {
      CreateExcelFromDGV(dgv, 0, dgv.Rows.Count, 0, dgv.Columns.Count);
    }

    private void CreateExcelFromDGV(DataGridView dgv, int minRow, int rowCount, int minColumn, int columnCount)
    {
      _excelDoc = new ExcelDoc();
      WriteHeader(dgv, minColumn, columnCount);

      int diffRow = GetDiffRows(minRow);
      int diffColumn = GetDiffColumns(minColumn);

      int index = 2;

      for (int i = minRow; i < rowCount; i++)
      {
        var isAdded = false;
        for (int j = minColumn; j < columnCount; j++)
        {
          if (dgv.Rows[i].Cells[j].Visible)
          {
            _excelDoc.setValue(index, dgv.Rows[i].Cells[j].ColumnIndex - diffColumn,
              dgv.Rows[i].Cells[j].Value.ToString());
            isAdded = true;
          }
        }
        if (isAdded)
          index++;
      }
    }

    private void WriteHeader(DataGridView dgv, int minColumn, int columnCount)
    {
      int diffColumn = GetDiffColumns(minColumn);

      int index = 1;

      for (int j = minColumn; j < columnCount; j++)
      {
        if (dgv.Columns[j].Visible)
        {
          _excelDoc.setValue(1, index, dgv.Columns[j].HeaderText);
          index++;
        }
      }
    }

    private static int GetDiffColumns(int minColumn)
    {
      var diff = 1;
      if (minColumn > 1)
        diff = minColumn - 1;

      return diff;
    }

    private static int GetDiffRows(int minRow)
    {
      var diff = 0;
      if (minRow > 0)
        diff = minRow - 1;

      return diff;
    }

    public void ShowInvoice()
    {
      _excelDoc = openDocumentExcel("Накладная");

      _excelDoc.setValue(7, 2, _car.info.Owner);

      _excelDoc.setValue(16, 82, _invoice.Number);
      _excelDoc.setValue(16, 98, _invoice.Date.ToShortDateString());

      string fullNameAuto = string.Concat("Автомобиль ", _car.Mark.Name, " ", _car.info.Model, ", ", _car.Grz);

      _excelDoc.setValue(22, 10, fullNameAuto);
      _excelDoc.setValue(22, 53, _car.dateGet.ToShortDateString());

      var grades = GradeList.getInstance();

      var grade = grades.getItem(Convert.ToInt32(_car.GradeID));

      var ptsList = PTSList.getInstance();
      var pts = ptsList.getItem(_car);

      var fullDetailAuto = string.Concat("VIN ", _car.vin, ", Двигатель ", _car.eNumber, ", № кузова ",
        _car.bodyNumber, ", Год выпуска ", _car.Year, " г., Паспорт ",
        pts.Number, " от ", pts.Date.ToShortDateString(), ", мощность двигателя ", grade.EPower, " л.с.");

      _excelDoc.setValue(47, 2, fullDetailAuto);

      Driver driver1 = _driverList.getItem(Convert.ToInt32(_invoice.DriverFromID));
      Driver driver2 = _driverList.getItem(Convert.ToInt32(_invoice.DriverToID));

      _excelDoc.setValue(9, 10, driver1.Dept);
      _excelDoc.setValue(56, 11, driver1.Position);
      _excelDoc.setValue(56, 63, driver1.GetName(NameType.Full));

      _excelDoc.setValue(11, 13, driver2.Dept);
      _excelDoc.setValue(60, 11, driver2.Position);
      _excelDoc.setValue(60, 63, driver2.GetName(NameType.Full));

      _excelDoc.Show();
    }

    public void ShowNotice(DTP dtp)
    {
      _excelDoc = openDocumentExcel("Извещение о страховом случае");

      var owners = Owners.getInstance();

      _excelDoc.setValue(6, 5, owners.getItem(Convert.ToInt32(_car.ownerID))); //страхователь
      _excelDoc.setValue(7, 6, "а/я 34, 196128"); //почтовый адрес
      _excelDoc.setValue(8, 7, "320-40-04"); //телефон

      var driverCarList = DriverCarList.getInstance();
      var driver = driverCarList.GetDriver(_car, dtp.Date);

      var passportList = PassportList.getInstance();
      var passport = passportList.getLastPassport(driver);

      if (passport.Number != string.Empty)
      {
        var number = passport.Number;
        var numbers = number.Split(' ');

        _excelDoc.setValue(10, 3, numbers[0]); //серия
        _excelDoc.setValue(10, 6, numbers[1]); //номер

        _excelDoc.setValue(11, 3, passport.GiveOrg); //кем выдан
        _excelDoc.setValue(12, 4, passport.GiveDate.ToShortDateString()); //дата выдачи
      }

      var policyList = PolicyList.getInstance();
      var policy = policyList.getItem(_car, PolicyType.КАСКО);
      _excelDoc.setValue(14, 6, policy.Number); //полис

      _excelDoc.setValue(16, 6, string.Concat(_car.Mark.Name, " ", _car.info.Model)); //марка а/м
      _excelDoc.setValue(18, 6, _car.Grz); //рег номер а/м
      _excelDoc.setValue(20, 6, _car.vin); //вин

      _excelDoc.setValue(22, 6, dtp.Date.ToShortDateString()); //дата дтп

      _excelDoc.setValue(27, 2, driver.GetName(NameType.Full)); //водитель фио

      var regions = Regions.getInstance();

      _excelDoc.setValue(29, 3, regions.getItem(Convert.ToInt32(dtp.IDRegion))); //город
      _excelDoc.setValue(31, 14, dtp.Damage); //повреждения
      _excelDoc.setValue(33, 2, dtp.Facts); //обстоятельства происшествия

      //SsDTP ssDTP = SsDTPList.getInstance().getItem(_car.Mark);

      //_excelDoc.setValue(63, 11, ssDTP.ServiceStantion);

      //DateTime date = DateTime.Today;
      //MyDateTime myDate = new MyDateTime(date.ToShortDateString());

      //_excelDoc.setValue(71, 3, string.Concat("« ", date.Day.ToString(), " »"));
      //_excelDoc.setValue(71, 4, myDate.MonthToStringGenitive());
      //_excelDoc.setValue(71, 8, date.Year.ToString().Substring(2, 2));

      _excelDoc.Show();
    }

    /* Старое извещение
      public void showNotice(DTP dtp)
    {
        _excelDoc = openDocumentExcel("Извещение о страховом случае");

        Owners owners = Owners.getInstance();

        _excelDoc.setValue(7, 4, owners.getItem(Convert.ToInt32(_car.ownerID)));
        _excelDoc.setValue(8, 5, "а/я 34, 196128");
        _excelDoc.setValue(9, 6, "320-40-04");

        DriverCarList driverCarList = DriverCarList.getInstance();
        Driver driver = driverCarList.GetDriver(_car, dtp.Date);

        PassportList passportList = PassportList.getInstance();
        Passport passport = passportList.getLastPassport(driver);

        if (passport.Number != string.Empty)
        {
            string number = passport.Number;
            string[] numbers = number.Split(' ');

            _excelDoc.setValue(11, 2, numbers[0]);
            _excelDoc.setValue(11, 5, numbers[1]);

            _excelDoc.setValue(12, 2, passport.GiveOrg);
            _excelDoc.setValue(13, 3, passport.GiveDate.ToShortDateString());
        }

        PolicyList policyList = PolicyList.getInstance();
        Policy policy = policyList.getItem(_car, PolicyType.КАСКО);
        _excelDoc.setValue(15, 5, policy.Number);

        _excelDoc.setValue(17, 5, string.Concat(_car.Mark.Name, " ", _car.info.Model));
        _excelDoc.setValue(19, 5, _car.Grz);
        _excelDoc.setValue(21, 5, _car.vin);

        _excelDoc.setValue(23, 5, dtp.Date.ToShortDateString());

        _excelDoc.setValue(28, 1, driver.GetName(NameType.Full));

        Regions regions = Regions.getInstance();

        _excelDoc.setValue(30, 2, regions.getItem(Convert.ToInt32(dtp.IDRegion)));
        _excelDoc.setValue(32, 13, dtp.Damage);
        _excelDoc.setValue(34, 1, dtp.Facts);

        SsDTP ssDTP = SsDTPList.getInstance().getItem(_car.Mark);

        _excelDoc.setValue(63, 11, ssDTP.ServiceStantion);

        DateTime date = DateTime.Today;
        MyDateTime myDate = new MyDateTime(date.ToShortDateString());

        _excelDoc.setValue(71, 3, string.Concat("« ", date.Day.ToString(), " »"));
        _excelDoc.setValue(71, 4, myDate.MonthToStringGenitive());
        _excelDoc.setValue(71, 8, date.Year.ToString().Substring(2, 2));

        _excelDoc.Show();
    }
     
     */

    public void CreateWaybill(DateTime date, Driver driver = null)
    {
      date = new DateTime(date.Year, date.Month, 1);

      if (driver == null)
      {
        var driverCarList = DriverCarList.getInstance();
        driver = driverCarList.GetDriver(_car, date);

        if (driver == null)
        {
          driver = driverCarList.GetDriver(_car);
          var invoiceList = InvoiceList.getInstance();
          var invoice = invoiceList.getItem(_car);

          if (!string.IsNullOrEmpty(invoice?.DateMove))
          {
            DateTime.TryParse(invoice.DateMove, out DateTime dateMove);
            if (dateMove.Year == date.Year && dateMove.Month == date.Month)
              date = new DateTime(date.Year, date.Month, dateMove.Day);
          }
        }
      }

      _excelDoc = openDocumentExcel("Путевой лист");

      _excelDoc.setValue(4, 28, _car.BBNumber);

      var myDate = new MyDateTime(date.ToShortDateString());

      _excelDoc.setValue(4, 39, driver.Id + "/01/" + myDate.MonthSlashYear());
      _excelDoc.setValue(6, 15, myDate.DaysRange);
      _excelDoc.setValue(6, 19, myDate.MonthToStringNominative());
      _excelDoc.setValue(6, 32, date.Year.ToString());

      _excelDoc.setValue(29, 35, _car.info.Grade.EngineType.ShortName);

      var mml = new MileageMonthList(_car.Id, date.Year + "-" + date.Month + "-01");
      /* Из файла Татьяны Мироновой пробег за месяц */
      _excelDoc.setValue(19, 39, mml.PSN);
      _excelDoc.setValue(33, 41, mml.Gas);
      _excelDoc.setValue(35, 41, mml.GasBegin);
      _excelDoc.setValue(36, 41, mml.GasEnd);
      _excelDoc.setValue(37, 41, mml.GasNorm);
      _excelDoc.setValue(38, 41, mml.GasNorm);
      _excelDoc.setValue(43, 39, mml.PSK);
      _excelDoc.setValue(41, 59, mml.Mileage);

      var owners = Owners.getInstance();
      var owner = owners.getItem(1);

      _excelDoc.setValue(8, 8, owner);

      _excelDoc.setValue(10, 11, string.Concat(_car.Mark.Name, " ", _car.info.Model));
      _excelDoc.setValue(11, 17, _car.Grz);

      _excelDoc.setValue(12, 6, driver.GetName(NameType.Full));
      _excelDoc.setValue(44, 16, driver.GetName(NameType.Short));
      _excelDoc.setValue(26, 40, driver.GetName(NameType.Short));

      var licencesList = LicenseList.getInstance();
      var driverLicense = licencesList.getItem(driver);

      _excelDoc.setValue(14, 10, driverLicense.Number);

      _excelDoc.setValue(20, 9, owner);

      string suppyAddressName;

      if (driver.suppyAddress != string.Empty)
      {
        suppyAddressName = driver.suppyAddress;
      }
      else
      {
        var suppyAddressList = SuppyAddressList.getInstance();
        var suppyAddress = suppyAddressList.getItemByRegion(driver.Region.Id);

        if (suppyAddress != null)
          suppyAddressName = suppyAddress.ToString();
        else
        {
          var passportList = PassportList.getInstance();
          var passport = passportList.getLastPassport(driver);
          suppyAddressName = passport.Address;
        }
      }

      var suppyAddressName2 = string.Empty;

      if (suppyAddressName.Length > 40)
      {
        for (var i = 30; i < suppyAddressName.Length; i++)
        {
          if (suppyAddressName[i] == ' ')
          {
            suppyAddressName2 = suppyAddressName.Substring(i, suppyAddressName.Length - i);
            suppyAddressName = suppyAddressName.Substring(0, i);
          }
        }
      }

      _excelDoc.setValue(25, 8, suppyAddressName);
      _excelDoc.setValue(26, 1, suppyAddressName2);

      string mechanicName;

      var employeesList = EmployeesList.getInstance();
      var accountant = employeesList.getItem(driver.Region, "Бухгалтер Б.Браун");

      if (driver.IsOne)
      {
        mechanicName = driver.GetName(NameType.Short);
      }
      else
      {
        var mechanic = employeesList.getItem(driver.Region, "Механик", true);
        mechanicName = mechanic == null
          ? driver.GetName(NameType.Short)
          : mechanic.Name;
      }

      var dispatcher = employeesList.getItem(driver.Region, "Диспечер-нарядчик");
      var dispatcherName = dispatcher.Name;

      _excelDoc.setValue(22, 40, mechanicName);
      _excelDoc.setValue(44, 40, mechanicName);

      _excelDoc.setValue(31, 18, dispatcherName);
      _excelDoc.setValue(35, 18, dispatcherName);

      _excelDoc.setValue(43, 72, accountant.Name);
    }

    public void AddRouteInWayBill(DateTime date, Fields fields)
    {
      var wayBillDaily = new WayBillDaily(_car, date);
      wayBillDaily.Load();

      CopyWayBill(wayBillDaily);

      var k = 0;
      var beginDistance = wayBillDaily.BeginDistance;
      var endDistance = wayBillDaily.EndDistance;

      var curDistance = beginDistance;

      foreach (WayBillDay wayBillDay in wayBillDaily)
      {
        var i = 6 + (47 * k);
        foreach (Route route in wayBillDay)
        {
          var pointBegin = route.MyPoint1;
          var pointEnd = route.MyPoint2;

          _excelDoc.setValue(i, 59, pointBegin.Name);
          _excelDoc.setValue(i, 64, pointEnd.Name);
          _excelDoc.setValue(i, 78, route.Distance.ToString());
          i += 2;
        }

        _excelDoc.setValue(29 + (47 * k), 20, wayBillDay.Date.ToShortDateString());
        _excelDoc.setValue(19 + (47 * k), 39, curDistance.ToString());
        curDistance += wayBillDay.Distance;
        if (fields == Fields.All)
        {
          _excelDoc.setValue(43 + (47 * k), 39, curDistance.ToString());
          _excelDoc.setValue(41 + (47 * k), 59, wayBillDay.Distance.ToString());
          _excelDoc.setValue(33 + (47 * k), 20, wayBillDay.Date.ToShortDateString());
        }
        k++;
      }

      if (k > 0 && fields == Fields.All)
        _excelDoc.setValue(43 + (47 * (k - 1)), 39, endDistance.ToString());
    }

    private void CopyWayBill(WayBillDaily wayBillDaily)
    {
      var i = 0;
      foreach (WayBillDay item in wayBillDaily)
      {
        if (i > 0)
          _excelDoc.CopyRange("A1", "CF46", "A" + ((47 * i) + 1).ToString());

        _excelDoc.setValue(6 + (47 * i), 15, item.Day);

        _excelDoc.setValue(4 + (47 * i), 39, GetWaBillFullNumber(i + 1));

        _excelDoc.setValue(12 + (47 * i), 6, item.Driver.GetName(NameType.Full));
        _excelDoc.setValue(44 + (47 * i), 16, item.Driver.GetName(NameType.Short));
        _excelDoc.setValue(26 + (47 * i), 40, item.Driver.GetName(NameType.Short));

        i++;
      }
    }

    private string GetWaBillFullNumber(int currentNumber)
    {
      string[] wayBillFullNumber = _excelDoc.getValue("AM4").ToString().Split('/');

      wayBillFullNumber[1] = currentNumber < 10 ? "0" : string.Empty;
      wayBillFullNumber[1] += currentNumber;

      StringBuilder sb = new StringBuilder();

      foreach (string item in wayBillFullNumber)
      {
        if (sb.Length > 0)
          sb.Append("/");

        sb.Append(item);
      }

      return sb.ToString();
    }

    public void ShowAttacheToOrder()
    {
      _excelDoc = openDocumentExcel("Приложение к приказу");

      string fullNameAuto = string.Concat(_car.Mark.Name, " ", _car.info.Model);

      _excelDoc.setValue(18, 2, fullNameAuto);
      _excelDoc.setValue(18, 3, _car.Grz);

      var driver = _driverList.getItem(Convert.ToInt32(_invoice.DriverToID));

      _excelDoc.setValue(18, 4, driver.GetName(NameType.Full));
      _excelDoc.setValue(18, 5, driver.Position);

      _excelDoc.Show();
    }

    public void ShowProxyOnSto()
    {
      var wordDoc = CreateProxyOnSto();

      wordDoc.Show();
    }

    public void PrintProxyOnSto()
    {
      var wordDoc = CreateProxyOnSto();

      wordDoc.setValue("до 31 декабря 2017 года", "до 31 декабря 2018 года");

      var myDate = new MyDateTime(DateTime.Today.ToShortDateString());
      wordDoc.setValue(myDate.ToLongString(), "01 января 2018");

      //wordDoc.Show();
      wordDoc.Print();
    }

    private WordDoc CreateProxyOnSto()
    {
      var wordDoc = openDocumentWord("Доверенность на предоставление интересов на СТО");

      var driverCarList = DriverCarList.getInstance();

      var driver = _invoice == null
        ? driverCarList.GetDriver(_car)
        : _driverList.getItem(Convert.ToInt32(_invoice.DriverToID));

      var myDate = new MyDateTime(DateTime.Today.ToShortDateString());
      wordDoc.setValue("текущая дата", myDate.ToLongString());

      var fio = string.Empty;
      if (driver != null)
        fio = driver.GetName(NameType.Full);

      wordDoc.setValue("ФИО регионального представителя", fio);

      var passportList = PassportList.getInstance();

      Passport passport = null;
      if (driver != null)
        passport = passportList.getLastPassport(driver);

      var passportToString = "нет данных";

      if (passport != null)
        passportToString = string.Concat(passport.Number, ", выдан ", passport.GiveDate.ToShortDateString(), ", ",
          passport.GiveOrg, ", Адрес: ", passport.Address);

      wordDoc.setValue("паспорт регионального представителя", passportToString);

      var fullNameAuto = string.Concat(_car.Mark.Name, " ", _car.info.Model);
      wordDoc.setValue("Название марки автомобиля", fullNameAuto);
      wordDoc.setValue("VIN-автомобиля", _car.vin);
      wordDoc.setValue("Модель и марка двигателя автомобиля", _car.eNumber);
      wordDoc.setValue("Номер кузова автомобиля", _car.bodyNumber);
      wordDoc.setValue("Год выпуска автомобиля", _car.Year);
      wordDoc.setValue("Цвет автомобиля", _car.info.Color);

      var ptsList = PTSList.getInstance();
      var pts = ptsList.getItem(_car);

      var ptsName = string.Concat(pts.Number, ", выдан ", pts.Date.ToShortDateString(), " ", pts.GiveOrg);

      wordDoc.setValue("ПТС автомобиля", ptsName);
      wordDoc.setValue("ГРЗ автомобиля", _car.Grz);
      wordDoc.setValue("текущий год", DateTime.Today.Year.ToString());

      return wordDoc;
    }

    public void ShowActFuelCard()
    {
      var wordDoc = openDocumentWord("Акт передачи топливной карты");

      var fuelCardDriverList = FuelCardDriverList.getInstance();

      var driverTo = _driverList.getItem(Convert.ToInt32(_invoice.DriverToID));
      var list = fuelCardDriverList.ToList(driverTo);

      var regions = Regions.getInstance();
      var regionName = regions.getItem(Convert.ToInt32(_invoice.RegionToID));

      var i = 1;

      foreach (var fuelCardDriver in list)
      {
        wordDoc.AddRowInTable(1, i.ToString(), driverTo.GetName(NameType.Full), regionName,
          fuelCardDriver.FuelCard.Number);
        wordDoc.AddRowInTable(2, i.ToString(), driverTo.GetName(NameType.Full), regionName,
          fuelCardDriver.FuelCard.Number, fuelCardDriver.FuelCard.Pin);

        i++;
      }

      if (list.Count == 1)
        wordDoc.setValue("Количество карт", "1 (одна) карта.");
      else if (list.Count == 2)
        wordDoc.setValue("Количество карт", "2 (две) карты.");
      else if (list.Count != 0)
        wordDoc.setValue("Количество карт", list.Count + "карт(ы).");

      wordDoc.Show();
    }

    public void CreatePolicyTable()
    {
      const int indexBegin = 6;
      var date = DateTime.Today.AddMonths(1);

      _excelDoc = openDocumentExcel("Таблица страхования");

      var myDate = new MyDateTime(date.ToShortDateString());

      _excelDoc.setValue(2, 1, "Страхуем в " + myDate.MonthToStringPrepositive() + " " + myDate.Year + " г.");

      var policyList = PolicyList.getInstance();
      var list = policyList.GetPolicyList(date);
      var listCar = policyList.GetCarListByPolicyList(list);

      var diagCardList = DiagCardList.getInstance();

      var rowIndex = indexBegin;

      foreach (var car in listCar)
      {
        _excelDoc.setValue(rowIndex, 2, car.Grz);
        _excelDoc.setValue(rowIndex, 3, car.Mark.Name);
        _excelDoc.setValue(rowIndex, 4, car.info.Model);
        _excelDoc.setValue(rowIndex, 5, car.vin);
        _excelDoc.setValue(rowIndex, 6, car.Year);
        _excelDoc.setValue(rowIndex, 7, GetPolicyBeginDate(list, car, PolicyType.ОСАГО));
        _excelDoc.setValue(rowIndex, 8, GetPolicyBeginDate(list, car, PolicyType.КАСКО));
        _excelDoc.setValue(rowIndex, 9, car.info.Owner);
        _excelDoc.setValue(rowIndex, 10, car.info.Owner);
        _excelDoc.setValue(rowIndex, 11, car.info.Owner);

        var diagCard = diagCardList.getItem(car);

        if (diagCard != null)
        {
          _excelDoc.setValue(rowIndex, 12, diagCard.Date.ToShortDateString());
          _excelDoc.setValue(rowIndex, 13, diagCard.Number);
        }

        rowIndex++;
      }

      _excelDoc.Show();
    }

    private static string GetPolicyBeginDate(IEnumerable<Policy> list, Car car, PolicyType policyType)
    {
      var newList = list.Where(policy => policy.Car.Id == car.Id && policy.Type == policyType).ToList();

      var osagoBeginDate = "не надо";

      if (newList.Count > 0)
        osagoBeginDate = newList.First().DateBegin.ToShortDateString();

      return osagoBeginDate;
    }

    private ExcelDoc openDocumentExcel(string name)
    {
      var templateList = TemplateList.getInstance();
      var template = templateList.getItem(name);
      return new ExcelDoc(template.File);
    }

    private WordDoc openDocumentWord(string name)
    {
      var templateList = TemplateList.getInstance();
      var template = templateList.getItem(name);
      return new WordDoc(template.File);
    }

    public void Print()
    {
      _excelDoc.Print();
    }

    public void Show()
    {
      _excelDoc.Show();
    }

    public void CreateHeader(string text)
    {
      _excelDoc.SetHeader(text);
    }

    public void Exit()
    {
      _excelDoc.Dispose();
    }
  }
}
