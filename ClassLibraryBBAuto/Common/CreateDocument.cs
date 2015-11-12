using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ClassLibraryBBAuto
{
    public class CreateDocument
    {
        private Invoice _invoice;
        private Car _car;
        private ExcelDoc _excelDoc;

        private DriverList driverList;

        public CreateDocument()
        {
            Init();
        }

        public CreateDocument(Car car)
        {
            Init();
            _car = car;
        }

        public CreateDocument(int idCar, int idInvoice)
        {
            Init();

            CarList carList = CarList.getInstance();
            _car = carList.getItem(Convert.ToInt32(idCar));

            InvoiceList invoiceList = InvoiceList.getInstance();
            _invoice = invoiceList.getItem(idInvoice);
        }

        private void Init()
        {
            driverList = DriverList.getInstance();
        }

        public void CreateExcelFromDGV(DataGridView dgv)
        {
            int minRowIndex = 0;
            int maxRowIndex = 0;
            int minColumnIndex = 0;
            int maxColumnIndex = 0;

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

            int rowCount = maxRowIndex + 1;
            int columnCount = maxColumnIndex + 1;

            CreateExcelFromDGV(dgv, minRowIndex, rowCount, minColumnIndex, columnCount);
        }

        public void CreateExcelFromAllDGV(DataGridView dgv)
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
                bool IsAdded = false;
                for (int j = minColumn; j < columnCount; j++)
                {
                    if (dgv.Rows[i].Cells[j].Visible)
                    {
                        _excelDoc.setValue(index, dgv.Rows[i].Cells[j].ColumnIndex - diffColumn, dgv.Rows[i].Cells[j].Value.ToString());
                        IsAdded = true;
                    }
                    
                }
                if (IsAdded)
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

        private int GetDiffColumns(int minColumn)
        {
            int diff = 1;
            if (minColumn > 1)
                diff = minColumn - 1;

            return diff;
        }

        private int GetDiffRows(int minRow)
        {
            int diff = 0;
            if (minRow > 0)
                diff = minRow - 1;

            return diff;
        }

        public void ShowInvoice()
        {
            _excelDoc = openDocumentExcel("Накладная");

            _excelDoc.setValue(7, 2, _car.info.Owner);

            _excelDoc.setValue(16, 82, _invoice.name);
            _excelDoc.setValue(16, 98, _invoice.Date.ToShortDateString());

            string fullNameAuto = string.Concat("Автомобиль ", _car.info.Mark, " ", _car.info.Model, ", ", _car.grz);

            _excelDoc.setValue(22, 10, fullNameAuto);
            _excelDoc.setValue(22, 53, _car.dateGet.ToShortDateString());
            _excelDoc.setValue(22, 71, _car.name);

            GradeList grades = GradeList.getInstance();

            Grade grade = grades.getItem(Convert.ToInt32(_car.GradeID));

            PTSList ptsList = PTSList.getInstance();
            PTS pts = ptsList.getItem(_car);

            string fullDetailAuto = string.Concat("VIN ", _car.vin, ", Двигатель ", _car.eNumber, ", № кузова ", _car.bodyNumber, ", Год выпуска ", _car.Year, " г., Паспорт ",
                pts.Number, " от ", pts.Date.ToShortDateString(), ", мощность двигателя ", grade.ePower, " л.с.");

            _excelDoc.setValue(47, 2, fullDetailAuto);

            Driver driver1 = driverList.getItem(Convert.ToInt32(_invoice.DriverFromID));
            Driver driver2 = driverList.getItem(Convert.ToInt32(_invoice.DriverToID));

            _excelDoc.setValue(9, 10, driver1.Dept);
            _excelDoc.setValue(56, 11, driver1.Position);
            _excelDoc.setValue(56, 63, driver1.name);

            _excelDoc.setValue(11, 13, driver2.Dept);
            _excelDoc.setValue(60, 11, driver2.Position);
            _excelDoc.setValue(60, 63, driver2.name);

            _excelDoc.Show();
        }

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

            if (passport.number != string.Empty)
            {
                string number = passport.number;
                string[] numbers = number.Split(' ');

                _excelDoc.setValue(11, 2, numbers[0]);
                _excelDoc.setValue(11, 5, numbers[1]);

                _excelDoc.setValue(12, 2, passport.giveOrg);
                _excelDoc.setValue(13, 3, passport.GiveDate);
            }

            PolicyList policyList = PolicyList.getInstance();
            Policy policy = policyList.getItem(_car, PolicyType.КАСКО);
            _excelDoc.setValue(15, 5, policy.Number);

            _excelDoc.setValue(17, 5, string.Concat(_car.info.Mark, " ", _car.info.Model));
            _excelDoc.setValue(19, 5, _car.grz);
            _excelDoc.setValue(21, 5, _car.vin);

            _excelDoc.setValue(23, 5, dtp.Date.ToShortDateString());

            _excelDoc.setValue(28, 1, driver.GetName(NameType.Full));

            Regions regions = Regions.getInstance();

            _excelDoc.setValue(30, 2, regions.getItem(Convert.ToInt32(dtp.IDRegion)));
            _excelDoc.setValue(32, 13, dtp.damage);
            _excelDoc.setValue(34, 1, dtp.facts);

            SsDTPList ssDTPs = SsDTPList.getInstance();
            int idMark;
            int.TryParse(_car.MarkID, out idMark);
            SsDTP ssDTP = ssDTPs.getItem(idMark);

            _excelDoc.setValue(63, 11, ssDTP.ServiceStantion);

            DateTime date = DateTime.Today;
            MyDateTime myDate = new MyDateTime(date.ToShortDateString());

            _excelDoc.setValue(71, 3, string.Concat("« ", date.Day.ToString(), " »"));
            _excelDoc.setValue(71, 4, myDate.MonthToStringGenitive());
            _excelDoc.setValue(71, 8, date.Year.ToString().Substring(2, 2));

            _excelDoc.Show();
        }
        
        public void createWaybill(DateTime date, Driver driver = null)
        {
            if (driver == null)
            {
                DriverCarList driverCarList = DriverCarList.getInstance();
                driver = driverCarList.GetDriver(_car, date);
            }

            _excelDoc = openDocumentExcel("Путевой лист");

            _excelDoc.setValue(4, 28, _car.BBNumber.ToString());

            MyDateTime myDate = new MyDateTime(date.ToShortDateString());

            _excelDoc.setValue(4, 39, myDate.MonthSlashYear());
            _excelDoc.setValue(6, 15, myDate.DaysRange);
            _excelDoc.setValue(6, 19, myDate.MonthToStringNominative());
            _excelDoc.setValue(6, 32, date.Year.ToString());

            Owners owners = Owners.getInstance();
            string owner = owners.getItem(1);

            _excelDoc.setValue(8, 8, owner);

            _excelDoc.setValue(10, 11, string.Concat(_car.info.Mark, " ", _car.info.Model));
            _excelDoc.setValue(11, 17, _car.grz);

            _excelDoc.setValue(12, 6, driver.GetName(NameType.Full));
            _excelDoc.setValue(44, 16, driver.GetName(NameType.Short));
            _excelDoc.setValue(26, 40, driver.GetName(NameType.Short));

            LicenseList licencesList = LicenseList.getInstance();
            DriverLicense driverLicense = licencesList.getItem(driver);

            _excelDoc.setValue(14, 10, driverLicense.name);

            _excelDoc.setValue(20, 9, owner);

            string suppyAddressName;

            if (driver.suppyAddress != string.Empty)
            {
                suppyAddressName = driver.suppyAddress;
            }
            else
            {
                SuppyAddressList suppyAddressList = SuppyAddressList.getInstance();
                SuppyAddress suppyAddress = suppyAddressList.getItem(driver.RegionID);

                if (suppyAddress != null)
                    suppyAddressName = suppyAddress.name;
                else
                {
                    PassportList passportList = PassportList.getInstance();
                    Passport passport = passportList.getLastPassport(driver);
                    suppyAddressName = passport.address;
                }
            }

            string suppyAddressName2 = string.Empty;

            if (suppyAddressName.Length > 40)
            {
                for (int i = 30; i < suppyAddressName.Length; i++)
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

            EmployeesList employeesList = EmployeesList.getInstance();
            Employees accountant = employeesList.getItem(driver.RegionID, "Бухгалтер Б.Браун");

            if (driver.IsOne)
            {
                mechanicName = driver.GetName(NameType.Short);
            }
            else
            {
                Employees mechanic = employeesList.getItem(driver.RegionID, "Механик", true);
                if (mechanic == null)
                    mechanicName = driver.GetName(NameType.Short);
                else
                    mechanicName = mechanic.Name;
            }

            Employees dispatcher = employeesList.getItem(driver.RegionID, "Диспечер-нарядчик");
            string dispatcherName = dispatcher.Name;
            
            _excelDoc.setValue(22, 40, mechanicName);
            _excelDoc.setValue(44, 40, mechanicName);

            _excelDoc.setValue(31, 18, dispatcherName);
            _excelDoc.setValue(35, 18, dispatcherName);

            _excelDoc.setValue(43, 72, accountant.Name);
        }

        public void ShowAttacheToOrder()
        {
            _excelDoc = openDocumentExcel("Приложение к приказу");

            string fullNameAuto = string.Concat(_car.info.Mark, " ", _car.info.Model);

            _excelDoc.setValue(18, 2, fullNameAuto);
            _excelDoc.setValue(18, 3, _car.grz);

            Driver driver = driverList.getItem(Convert.ToInt32(_invoice.DriverToID));

            _excelDoc.setValue(18, 4, driver.name);
            _excelDoc.setValue(18, 5, driver.Position);

            _excelDoc.Show();
        }

        public void ShowProxyOnSTO()
        {
            WordDoc wordDoc = openDocumentWord("Доверенность на предоставление интересов на СТО");

            Driver driver = driverList.getItem(Convert.ToInt32(_invoice.DriverToID));

            MyDateTime myDate = new MyDateTime(DateTime.Today.ToShortDateString());
            wordDoc.setValue("текущая дата", myDate.ToLongString());

            wordDoc.setValue("ФИО регионального представителя", driver.name);

            PassportList passportList = PassportList.getInstance();
            Passport passport = passportList.getLastPassport(driver);

            string passportToString = "нет данных";

            if (passport != null)
                passportToString = string.Concat("паспорт гражданина Российской Федерации, ", passport.number, ", выдан ", passport.giveDate.ToShortDateString(),
                    ", ", passport.giveOrg, ", Адрес: ", passport.address);

            wordDoc.setValue("паспорт регионального представителя", passportToString);

            string fullNameAuto = string.Concat(_car.info.Mark, " ", _car.info.Model);
            wordDoc.setValue("Название марки автомобиля", fullNameAuto);
            wordDoc.setValue("VIN-автомобиля", _car.vin);
            wordDoc.setValue("Модель и марка двигателя автомобиля", _car.eNumber);
            wordDoc.setValue("Номер кузова автомобиля", _car.bodyNumber);
            wordDoc.setValue("Год выпуска автомобиля", _car.Year);
            wordDoc.setValue("Цвет автомобиля", _car.info.Color);

            PTSList ptsList = PTSList.getInstance();
            PTS pts = ptsList.getItem(_car);

            string ptsName = string.Concat(pts.Number, ", выдан ", pts.Date.ToShortDateString(), " ", pts.GiveOrg);

            wordDoc.setValue("ПТС автомобиля", ptsName);
            wordDoc.setValue("ГРЗ автомобиля", _car.grz);
            wordDoc.setValue("текущий год", DateTime.Today.Year.ToString());

            wordDoc.Show();
        }

        public void ShowActFuelCard()
        {
            WordDoc wordDoc = openDocumentWord("Акт передачи топливной карты");

            FuelCardDriverList fuelCardDriverList = FuelCardDriverList.getInstance();

            Driver driverTo = driverList.getItem(Convert.ToInt32(_invoice.DriverToID));
            List<FuelCardDriver> list = fuelCardDriverList.ToList(driverTo);
            
            Regions regions = Regions.getInstance();
            string regionName = regions.getItem(Convert.ToInt32(_invoice.RegionToID));

            int i = 1;

            foreach (FuelCardDriver fuelCardDriver in list)
            {
                wordDoc.AddRowInTable(1, i.ToString(), driverTo.name, regionName, fuelCardDriver.fuelCard.Number);
                wordDoc.AddRowInTable(2, i.ToString(), driverTo.name, regionName, fuelCardDriver.fuelCard.Number, fuelCardDriver.fuelCard.Pin);
                
                i++;
            }

            if (list.Count == 1)
                wordDoc.setValue("Количество карт", "1 (одна) карта.");
            else if (list.Count == 2)
                wordDoc.setValue("Количество карт", "2 (две) карты.");
            else if (list.Count != 0)
                wordDoc.setValue("Количество карт", list.Count.ToString() + "карт(ы).");
            
            wordDoc.Show();
        }

        public void CreatePolicyTable()
        {
            const int INDEX_BEGIN = 6;
            DateTime date = DateTime.Today.AddMonths(1);

            _excelDoc = openDocumentExcel("Таблица страхования");

            MyDateTime myDate = new MyDateTime(date.ToShortDateString());

            _excelDoc.setValue(2, 1, "Страхуем в " + myDate.MonthToStringPrepositive() + " " + myDate.Year + " г.");

            PolicyList policyList = PolicyList.getInstance();
            List<Policy> list = policyList.GetPolicyList(date);
            List<Car> listCar = policyList.GetCarListByPolicyList(list);

            DiagCardList diagCardList = DiagCardList.getInstance();

            int rowIndex = INDEX_BEGIN;

            foreach (Car car in listCar)
            {
                _excelDoc.setValue(rowIndex, 2, car.grz);
                _excelDoc.setValue(rowIndex, 3, car.info.Mark);
                _excelDoc.setValue(rowIndex, 4, car.info.Model);
                _excelDoc.setValue(rowIndex, 5, car.vin);
                _excelDoc.setValue(rowIndex, 6, car.Year);
                _excelDoc.setValue(rowIndex, 7, GetPolicyBeginDate(list, car, PolicyType.ОСАГО));
                _excelDoc.setValue(rowIndex, 8, GetPolicyBeginDate(list, car, PolicyType.КАСКО));
                _excelDoc.setValue(rowIndex, 9, car.info.Owner);
                _excelDoc.setValue(rowIndex, 10, car.info.Owner);
                _excelDoc.setValue(rowIndex, 11, car.info.Owner);

                DiagCard diagCard = diagCardList.getItem(car);

                _excelDoc.setValue(rowIndex, 12, diagCard.date.ToShortDateString());
                _excelDoc.setValue(rowIndex, 13, diagCard.name);

                rowIndex++;
            }

            _excelDoc.Show();
        }

        private static string GetPolicyBeginDate(List<Policy> list, Car car, PolicyType policyType)
        {
            List<Policy> newList = list.Where(policy => policy.isEqualCarID(car) && policy.Type == policyType).ToList();

            string osagoBeginDate = "не надо";

            if (newList.Count > 0)
                osagoBeginDate = newList.First().DateBegin.ToShortDateString();

            return osagoBeginDate;
        }

        private ExcelDoc openDocumentExcel(string name)
        {
            TemplateList templateList = TemplateList.getInstance();
            Template template = templateList.getItem(name);
            return new ExcelDoc(template.Path);
        }

        private WordDoc openDocumentWord(string name)
        {
            TemplateList templateList = TemplateList.getInstance();
            Template template = templateList.getItem(name);
            return new WordDoc(template.Path);
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