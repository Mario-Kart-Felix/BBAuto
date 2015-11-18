using ClassLibraryBBAuto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BBAuto
{
    public class MyMenuItemFactory
    {
        private MainDGV _dgvMain;
        private CarList _carList;
        private MainStatus _mainStatus;

        public MyMenuItemFactory(MainDGV dgvMain)
        {
            _dgvMain = dgvMain;
            _mainStatus = MainStatus.getInstance();
            _carList = CarList.getInstance();
        }

        public ToolStripItem CreateItem(ContextMenuItem item)
        {
            switch (item)
            {
                case ContextMenuItem.Separator:
                    return CreateSeparator();
                case ContextMenuItem.NewInvoice:
                    return CreateNewInvoice();
                case ContextMenuItem.NewDTP:
                    return CreateNewDTP();
                case ContextMenuItem.NewViolation:
                    return CreateNewViolation();
                case ContextMenuItem.NewPolicy:
                    return CreateNewPolicy();
                case ContextMenuItem.NewDiagCard:
                    return CreateNewDiagCard();
                case ContextMenuItem.NewMileage:
                    return CreateNewMileage();
                case ContextMenuItem.NewTempMove:
                    return CreateNewTempMove();
                case ContextMenuItem.ToSale:
                    return CreateToSale();
                case ContextMenuItem.DeleteFromSale:
                    return CreateDeleteFromSale();
                case ContextMenuItem.LotusMail:
                    return CreateLotusMail();
                case ContextMenuItem.SendPolicyOsago:
                    return CreateSendPolicyOsago();
                case ContextMenuItem.SendPolicyKasko:
                    return CreateSendPolicyKasko();
                case ContextMenuItem.Copy:
                    return CreateCopy();
                case ContextMenuItem.Print:
                    return CreatePrint();
                case ContextMenuItem.PrintWayBill:
                    return CreatePrintWayBill();
                case ContextMenuItem.ShowWayBill:
                    return CreateShowWayBill();
                //------------------------------
                case ContextMenuItem.ShowInvoice:
                    return CreateShowInvoice();
                case ContextMenuItem.ShowAttacheToOrder:
                    return CreateShowAttacheToOrder();
                case ContextMenuItem.ShowProxyOnSTO:
                    return CreateShowProxyOnSTO();
                case ContextMenuItem.ShowPolicyKasko:
                    return CreateShowPolicyKasko();
                case ContextMenuItem.ShowActFuelCard:
                    return CreateShowActFuelCard();
                case ContextMenuItem.ShowNotice:
                    return CreateShowNotice();
                case ContextMenuItem.ShowSTS:
                    return CreateShowSTS();
                case ContextMenuItem.ShowDriverLicense:
                    return CreateShowDriverLicense();
                //----------------------------------
                case ContextMenuItem.Exit:
                    return CreateExit();
                case ContextMenuItem.Documents:
                    return CreateDocuments();
                case ContextMenuItem.NewCar:
                    return CreateNewCar();
                case ContextMenuItem.NewAccount:
                    return CreateNewAccount();
                case ContextMenuItem.NewFuelCard:
                    return CreateNewFuelCard();
                case ContextMenuItem.ShowPolicyList:
                    return CreateShowPolicyList();
                case ContextMenuItem.PrintAllTable:
                    return CreatePrintAllTable();
                case ContextMenuItem.ShowAllTable:
                    return CreateShowAllTable();
                //---------------------------------
                case ContextMenuItem.Driver:
                    return CreateDriver();
                case ContextMenuItem.Region:
                    return CreateRegion();
                case ContextMenuItem.SuppyAddress:
                    return CreateSuppyAddress();
                case ContextMenuItem.Employee:
                    return CreateEmployee();
                case ContextMenuItem.Mark:
                    return CreateMark();
                case ContextMenuItem.Model:
                    return CreateModel();
                case ContextMenuItem.Grade:
                    return CreateGrade();
                case ContextMenuItem.EngineType:
                    return CreateEngineType();
                case ContextMenuItem.Color:
                    return CreateColor();
                case ContextMenuItem.Dealer:
                    return CreateDiler();
                case ContextMenuItem.Owner:
                    return CreateOwner();
                case ContextMenuItem.Comp:
                    return CreateComp();
                case ContextMenuItem.ServiceStantion:
                    return CreateServiceStantion();
                case ContextMenuItem.ServiceStantionComp:
                    return CreateServiceStantionComp();
                case ContextMenuItem.Culprit:
                    return CreateCulprit();
                case ContextMenuItem.RepairType:
                    return CreateRepairType();
                case ContextMenuItem.StatusAfterDTP:
                    return CreateStatusAfterDTP();
                case ContextMenuItem.CurrentStatusAfterDTP:
                    return CreateCurrentStatusAfterDTP();
                case ContextMenuItem.ViolationType:
                    return CreateViolationType();
                case ContextMenuItem.ProxyType:
                    return CreateProxyType();
                case ContextMenuItem.FuelCardType:
                    return CreateFuelCardType();
                case ContextMenuItem.MailText:
                    return CreateMailText();
                case ContextMenuItem.Template:
                    return CreateTemplate();
                case ContextMenuItem.UserAccess:
                    return CreateUserAccess();
                case ContextMenuItem.Profession:
                    return CreateProfession();
                case ContextMenuItem.Sort:
                    return CreateSort();
                case ContextMenuItem.Filter:
                    return CreateFilter();
                case ContextMenuItem.AddDriver:
                    return CreateAddDriver();
                case ContextMenuItem.DeleteDriver:
                    return CreateDeleteDriver();
                case ContextMenuItem.MyPointList:
                    return CreateMyPointList();
                default:
                    throw new NotImplementedException();
            }
        }

        public ToolStripItem CreateItem(Status status)
        {
            switch (status)
            {
                case Status.Account:
                    return CreateAccount();
                case Status.Actual:
                    return CreateActual();
                case Status.Buy:
                    return CreateBuy();
                case Status.DiagCard:
                    return CreateDiagCard();
                case Status.DTP:
                    return CreateDTP();
                case Status.FuelCard:
                    return CreateFuelCard();
                case Status.Invoice:
                    return CreateInvoice();
                case Status.Policy:
                    return CreatePolicy();
                case Status.Repair:
                    return CreateRepair();
                case Status.Sale:
                    return CreateSale();
                case Status.ShipPart:
                    return CreateShipPart();
                case Status.TempMove:
                    return CreateTempMove();
                case Status.Violation:
                    return CreateViolation();
                default:
                    throw new NotImplementedException();
            }
        }

        private ToolStripSeparator CreateSeparator()
        {
            return new ToolStripSeparator();
        }

        private ToolStripMenuItem CreateNewInvoice()
        {
            ToolStripMenuItem item = CreateItem("Новое перемещение");
            item.Click += NewInvoice_Click;
            return item;
        }

        private ToolStripMenuItem CreateNewDTP()
        {
            ToolStripMenuItem item = CreateItem("Новое ДТП");
            item.Click += NewDTP_Click;
            return item;
        }

        private ToolStripMenuItem CreateNewViolation()
        {
            ToolStripMenuItem item = CreateItem("Новое нарушение ПДД");
            item.Click += NewViolation_Click;
            return item;
        }

        private ToolStripMenuItem CreateNewPolicy()
        {
            ToolStripMenuItem item = CreateItem("Новый полис");
            item.Click += NewPolicy_Click;
            return item;
        }

        private ToolStripMenuItem CreateNewDiagCard()
        {
            ToolStripMenuItem item = CreateItem("Новая диагностическая карта");
            item.Click += NewDiagCard_Click;
            return item;
        }

        private ToolStripMenuItem CreateNewMileage()
        {
            ToolStripMenuItem item = CreateItem("Новая запись о пробеге");
            item.Click += NewMileage_Click;
            return item;
        }

        private ToolStripMenuItem CreateNewTempMove()
        {
            ToolStripMenuItem item = CreateItem("Новое временное перемещение");
            item.Click += NewTempMove_Click;
            return item;
        }

        private ToolStripMenuItem CreateToSale()
        {
            ToolStripMenuItem item = CreateItem("На продажу");
            item.Click += ToSale_Click;
            return item;
        }

        private ToolStripMenuItem CreateDeleteFromSale()
        {
            ToolStripMenuItem item = CreateItem("Снять с продажи");
            item.Click += DeleteFromSale_Click;
            return item;
        }

        private ToolStripMenuItem CreateLotusMail()
        {
            ToolStripMenuItem item = CreateItem("Создать письмо Lotus");
            item.Click += LotusMail_Click;
            return item;
        }

        private ToolStripMenuItem CreateSendPolicyOsago()
        {
            ToolStripMenuItem item = CreateItem("Отправить полис Осаго");
            item.Click += SendPolicyOsago_Click;
            return item;
        }

        private ToolStripMenuItem CreateSendPolicyKasko()
        {
            ToolStripMenuItem item = CreateItem("Отправить полис Каско");
            item.Click += SendPolicyKasko_Click;
            return item;
        }

        private ToolStripMenuItem CreateCopy()
        {
            ToolStripMenuItem item = CreateItem("Копировать");
            item.Click += Copy_Click;
            item.ShortcutKeys = Keys.Control | Keys.C;
            return item;
        }

        private ToolStripMenuItem CreatePrint()
        {
            ToolStripMenuItem item = CreateItem("Печать");
            item.Click += Print_Click;
            item.ShortcutKeys = Keys.Control | Keys.P;
            return item;
        }

        private ToolStripMenuItem CreatePrintWayBill()
        {
            ToolStripMenuItem item = CreateItem("Печать путевого листа");
            item.Click += PrintWayBill_Click;
            return item;
        }

        private ToolStripMenuItem CreateShowWayBill()
        {
            ToolStripMenuItem item = CreateItem("Просмотр путевого листа");
            item.Click += ShowWayBill_Click;
            return item;
        }

        private ToolStripMenuItem CreateShowInvoice()
        {
            ToolStripMenuItem item = CreateItem("Накладная на перемещение");
            item.Click += ShowInvoice_Click;
            return item;
        }

        private ToolStripMenuItem CreateShowAttacheToOrder()
        {
            ToolStripMenuItem item = CreateItem("Приложение к приказу");
            item.Click += ShowAttacheToOrder_Click;
            return item;
        }

        private ToolStripMenuItem CreateShowProxyOnSTO()
        {
            ToolStripMenuItem item = CreateItem("Доверенность на предоставление интересов на СТО");
            item.Click += ShowProxyOnSTO_Click;
            return item;
        }

        private ToolStripMenuItem CreateShowPolicyKasko()
        {
            ToolStripMenuItem item = CreateItem("Полис Каско");
            item.Click += ShowPolicyKasko_Click;
            return item;
        }

        private ToolStripMenuItem CreateShowActFuelCard()
        {
            ToolStripMenuItem item = CreateItem("Акт передачи топливной карты");
            item.Click += ShowActFuelCard_Click;
            return item;
        }

        private ToolStripMenuItem CreateShowNotice()
        {
            ToolStripMenuItem item = CreateItem("Извещение о страховом случае");
            item.Click += ShowNotice_Click;
            return item;
        }

        private ToolStripMenuItem CreateShowSTS()
        {
            ToolStripMenuItem item = CreateItem("Свидетельство о регистрации ТС");
            item.Click += ShowSTS_Click;
            return item;
        }

        private ToolStripMenuItem CreateShowDriverLicense()
        {
            ToolStripMenuItem item = CreateItem("Водительское удостоверение");
            item.Click += ShowDriverLicense_Click;
            return item;
        }

        private ToolStripMenuItem CreateExit()
        {
            ToolStripMenuItem item = CreateItem("Выход из программы");
            item.Click += Exit_Click;
            return item;
        }

        private ToolStripMenuItem CreateDocuments()
        {
            ToolStripMenuItem item = CreateItem("Документы");
            item.Click += Documents_Click;
            return item;
        }

        private ToolStripMenuItem CreateNewCar()
        {
            ToolStripMenuItem item = CreateItem("Покупка автомобиля");
            item.Click += NewCar_Click;
            return item;
        }

        private ToolStripMenuItem CreateNewAccount()
        {
            ToolStripMenuItem item = CreateItem("Добавить счёт");
            item.Click += NewAccount_Click;
            return item;
        }

        private ToolStripMenuItem CreateNewFuelCard()
        {
            ToolStripMenuItem item = CreateItem("Добавить топливную карту");
            item.Click += NewFuelCard_Click;
            return item;
        }

        private ToolStripMenuItem CreateShowPolicyList()
        {
            ToolStripMenuItem item = CreateItem("Сформировать таблицу страхования");
            item.Click += ShowPolicyList_Click;
            return item;
        }

        private ToolStripMenuItem CreatePrintAllTable()
        {
            ToolStripMenuItem item = CreateItem("Текущий справочник");
            item.Click += PrintAllTable_Click;
            return item;
        }

        private ToolStripMenuItem CreateShowAllTable()
        {
            ToolStripMenuItem item = CreateItem("Экспорт текущего справочника в Excel");
            item.Click += ShowAllTable_Click;
            return item;
        }

        private ToolStripMenuItem CreateActual()
        {
            ToolStripMenuItem item = CreateItem("На ходу");
            item.Click += Actual_Click;
            return item;
        }
        
        private ToolStripMenuItem CreateBuy()
        {
            ToolStripMenuItem item = CreateItem("Покупка");
            item.Click += Buy_Click;
            return item;
        }
        
        private ToolStripMenuItem CreateSale()
        {
            ToolStripMenuItem item = CreateItem("Продажа");
            item.Click += Sale_Click;
            return item;
        }
        
        private ToolStripMenuItem CreateInvoice()
        {
            ToolStripMenuItem item = CreateItem("Перемещения");
            item.Click += Invoice_Click;
            return item;
        }
        
        private ToolStripMenuItem CreateTempMove()
        {
            ToolStripMenuItem item = CreateItem("Временные перемещения");
            item.Click += TempMove_Click;
            return item;
        }
        
        private ToolStripMenuItem CreatePolicy()
        {
            ToolStripMenuItem item = CreateItem("Страховые полисы");
            item.Click += Policy_Click;
            return item;
        }

        private ToolStripMenuItem CreateViolation()
        {
            ToolStripMenuItem item = CreateItem("Нарушения ПДД");
            item.Click += Violation_Click;
            return item;
        }
        
        private ToolStripMenuItem CreateDTP()
        {
            ToolStripMenuItem item = CreateItem("ДТП");
            item.Click += DTP_Click;
            return item;
        }
        
        private ToolStripMenuItem CreateDiagCard()
        {
            ToolStripMenuItem item = CreateItem("Диагностические карты");
            item.Click += DiagCard_Click;
            return item;
        }
        
        private ToolStripMenuItem CreateRepair()
        {
            ToolStripMenuItem item = CreateItem("Сервисное обслуживание");
            item.Click += Repair_Click;
            return item;
        }
        
        private ToolStripMenuItem CreateShipPart()
        {
            ToolStripMenuItem item = CreateItem("Отправка запчастей");
            item.Click += ShipPart_Click;
            return item;
        }
        
        private ToolStripMenuItem CreateAccount()
        {
            ToolStripMenuItem item = CreateItem("Согласования");
            item.Click += Account_Click;
            return item;
        }

        private ToolStripMenuItem CreateFuelCard()
        {
            ToolStripMenuItem item = CreateItem("Топливные карты");
            item.Click += FuelCard_Click;
            return item;
        }

        private ToolStripMenuItem CreateDriver()
        {
            ToolStripMenuItem item = CreateItem("Водители");
            item.Click += Driver_Click;
            return item;
        }
        
        private ToolStripMenuItem CreateRegion()
        {
            ToolStripMenuItem item = CreateItem("Регионы");
            item.Click += Region_Click;
            return item;
        }

        private ToolStripMenuItem CreateSuppyAddress()
        {
            ToolStripMenuItem item = CreateItem("Адреса подачи");
            item.Click += SuppyAddress_Click;
            return item;
        }

        private ToolStripMenuItem CreateEmployee()
        {
            ToolStripMenuItem item = CreateItem("Сотрудники в регионе");
            item.Click += Employee_Click;
            return item;
        }

        private ToolStripMenuItem CreateMark()
        {
            ToolStripMenuItem item = CreateItem("Марки");
            item.Click += Mark_Click;
            return item;
        }

        private ToolStripMenuItem CreateModel()
        {
            ToolStripMenuItem item = CreateItem("Модели");
            item.Click += Model_Click;
            return item;
        }
        
        private ToolStripMenuItem CreateGrade()
        {
            ToolStripMenuItem item = CreateItem("Комплектации");
            item.Click += Grade_Click;
            return item;
        }

        private ToolStripMenuItem CreateEngineType()
        {
            ToolStripMenuItem item = CreateItem("Типы двигателей");
            item.Click += EngineType_Click;
            return item;
        }

        private ToolStripMenuItem CreateColor()
        {
            ToolStripMenuItem item = CreateItem("Цвета");
            item.Click += Color_Click;
            return item;
        }

        private ToolStripMenuItem CreateDiler()
        {
            ToolStripMenuItem item = CreateItem("Дилеры");
            item.Click += Diler_Click;
            return item;
        }

        private ToolStripMenuItem CreateOwner()
        {
            ToolStripMenuItem item = CreateItem("Собственники");
            item.Click += Owner_Click;
            return item;
        }

        private ToolStripMenuItem CreateComp()
        {
            ToolStripMenuItem item = CreateItem("Страховые компании");
            item.Click += Comp_Click;
            return item;
        }

        private ToolStripMenuItem CreateServiceStantion()
        {
            ToolStripMenuItem item = CreateItem("СТО");
            item.Click += ServiceStantion_Click;
            return item;
        }

        private ToolStripMenuItem CreateServiceStantionComp()
        {
            ToolStripMenuItem item = CreateItem("СТО страховых");
            item.Click += ServiceStantionComp_Click;
            return item;
        }
        
        private ToolStripMenuItem CreateCulprit()
        {
            ToolStripMenuItem item = CreateItem("Виновники ДТП");
            item.Click += Culprit_Click;
            return item;
        }
        
        private ToolStripMenuItem CreateRepairType()
        {
            ToolStripMenuItem item = CreateItem("Виды ремонта");
            item.Click += RepairType_Click;
            return item;
        }
        
        private ToolStripMenuItem CreateStatusAfterDTP()
        {
            ToolStripMenuItem item = CreateItem("Статусы после ДТП");
            item.Click += StatusAfterDTP_Click;
            return item;
        }

        private ToolStripMenuItem CreateCurrentStatusAfterDTP()
        {
            ToolStripMenuItem item = CreateItem("Текущее состояние после ДТП");
            item.Click += CurrentStatusAfterDTP_Click;
            return item;
        }

        private ToolStripMenuItem CreateViolationType()
        {
            ToolStripMenuItem item = CreateItem("Типы нарушений ПДД");
            item.Click += ViolationType_Click;
            return item;
        }
        
        private ToolStripMenuItem CreateProxyType()
        {
            ToolStripMenuItem item = CreateItem("Типы доверенностей");
            item.Click += ProxyType_Click;
            return item;
        }

        private ToolStripMenuItem CreateFuelCardType()
        {
            ToolStripMenuItem item = CreateItem("Типы топливных карт");
            item.Click += FuelCardType_Click;
            return item;
        }
        
        private ToolStripMenuItem CreateMailText()
        {
            ToolStripMenuItem item = CreateItem("Тексты уведомлений");
            item.Click += MailText_Click;
            return item;
        }

        private ToolStripMenuItem CreateTemplate()
        {
            ToolStripMenuItem item = CreateItem("Шаблоны документов");
            item.Click += Template_Click;
            return item;
        }

        private ToolStripMenuItem CreateUserAccess()
        {
            ToolStripMenuItem item = CreateItem("Доступ пользователей");
            item.Click += UserAccess_Click;
            return item;
        }

        private ToolStripMenuItem CreateProfession()
        {
            ToolStripMenuItem item = CreateItem("Должности пользователей");
            item.Click += Profession_Click;
            return item;
        }

        private ToolStripMenuItem CreateSort()
        {
            ToolStripMenuItem item = CreateItem("Сортировать");
            item.Click += Sort_Click;
            return item;
        }

        private ToolStripMenuItem CreateFilter()
        {
            ToolStripMenuItem item = CreateItem("Фильтр по значению этого поля");
            item.Click += Filter_Click;
            return item;
        }

        private ToolStripMenuItem CreateAddDriver()
        {
            ToolStripMenuItem item = CreateItem("Добавить водителя");
            item.Click += AddDriver_Click;
            return item;
        }

        private ToolStripMenuItem CreateDeleteDriver()
        {
            ToolStripMenuItem item = CreateItem("Удалить водителя");
            item.Click += DeleteDriver_Click;
            return item;
        }

        private ToolStripMenuItem CreateMyPointList()
        {
            ToolStripMenuItem item = CreateItem("Список пунктов назначения");
            item.Click += MyPointList_Click;
            return item;
        }

        private ToolStripMenuItem CreateItem(string name)
        {
            return new ToolStripMenuItem(name);
        }
        
        private void NewInvoice_Click(object sender, EventArgs e)
        {
            if (_dgvMain.GetCarID() == 0)
                return;

            InvoiceDialog.CreateNewInvoiceAndOpen(_dgvMain.GetCarID());
        }

        private void NewDTP_Click(object sender, EventArgs e)
        {
            if (_dgvMain.GetCarID() == 0)
                return;
            
            Car car = _carList.getItem(_dgvMain.GetCarID());

            DTP dtp = car.createDTP();

            DTP_AddEdit dtpAE = new DTP_AddEdit(dtp);
            dtpAE.ShowDialog();
        }

        private void NewViolation_Click(object sender, EventArgs e)
        {
            if (_dgvMain.GetCarID() == 0)
                return;

            Violation violation = new Violation(_dgvMain.GetCarID());

            Violation_AddEdit vAE = new Violation_AddEdit(violation);
            vAE.ShowDialog();
        }

        private void NewPolicy_Click(object sender, EventArgs e)
        {
            if (_dgvMain.GetCarID() == 0)
                return;

            Car car = _carList.getItem(_dgvMain.GetCarID());

            Policy_AddEdit policyAE = new Policy_AddEdit(car.CreatePolicy());
            policyAE.ShowDialog();        
        }

        private void NewDiagCard_Click(object sender, EventArgs e)
        {
            if (_dgvMain.GetCarID() == 0)
                return;

            Car car = _carList.getItem(_dgvMain.GetCarID());

            DiagCard diagCard = car.createDiagCard();

            DiagCard_AddEdit diagcardAE = new DiagCard_AddEdit(diagCard);
            diagcardAE.ShowDialog();
        }

        private void NewMileage_Click(object sender, EventArgs e)
        {
            if (_dgvMain.GetCarID() == 0)
                return;

            Car car = _carList.getItem(_dgvMain.GetCarID());

            Mileage mileage = car.createMileage();

            Mileage_AddEdit mAE = new Mileage_AddEdit(mileage);
            if (mAE.ShowDialog() == DialogResult.OK)
                _mainStatus.Set(_mainStatus.Get());
        }

        private void NewTempMove_Click(object sender, EventArgs e)
        {
            if (_dgvMain.GetCarID() == 0)
                return;

            Car car = _carList.getItem(_dgvMain.GetCarID());
            TempMove tempMove = car.createTempMove();

            TempMove_AddEdit tempMoveAE = new TempMove_AddEdit(tempMove);
            tempMoveAE.ShowDialog();
        }

        private void ToSale_Click(object sender, EventArgs e)
        {
            if (_dgvMain.GetCarID() == 0)
                return;

            if (MessageBox.Show("Вы действительно хотите переместить автомобиль на продажу?", "Снятие с продажи", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            == DialogResult.Yes)
            {
                CarSale carSale = new CarSale(_dgvMain.GetCarID());
                carSale.Save();

                _mainStatus.Set(_mainStatus.Get());
            }
        }

        private void DeleteFromSale_Click(object sender, EventArgs e)
        {
            if (_dgvMain.GetCarID() == 0)
                return;

            if (MessageBox.Show("Вы действительно хотите убрать автомобиль с продажи?", "Снятие с продажи", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                            == DialogResult.Yes)
            {
                CarSaleList carSaleList = CarSaleList.getInstance();
                carSaleList.Delete(_dgvMain.GetCarID());

                _mainStatus.Set(_mainStatus.Get());
            }
        }

        private void LotusMail_Click(object sender, EventArgs e)
        {
            if (_dgvMain.GetCarID() == 0)
                return;

            DriverMails driverMails = new DriverMails(_dgvMain);
            string driverList = driverMails.ToString();

            if (string.IsNullOrEmpty(driverList))
                MessageBox.Show("Email-адреса не обнаружены", "Невозножно создать письмо", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                eMail.OpenEmailProgram(driverList);
        }

        private void SendPolicyOsago_Click(object sender, EventArgs e)
        {
            SendPolicy(PolicyType.ОСАГО);
        }

        private void SendPolicyKasko_Click(object sender, EventArgs e)
        {
            SendPolicy(PolicyType.КАСКО);
        }

        private void SendPolicy(PolicyType type)
        {
            if (_dgvMain.GetCarID() == 0)
                return;

            Car car = _carList.getItem(_dgvMain.GetCarID());

            string result = MailPolicy.Send(car, type);

            MessageBox.Show(result, "Отправка", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Copy_Click(object sender, EventArgs e)
        {
            try
            {
                MyBuffer.Copy(_dgvMain.GetDGV());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Print_Click(object sender, EventArgs e)
        {
            CreateDocument doc = new CreateDocument();
            doc.CreateExcelFromDGV(_dgvMain.GetDGV());
            doc.Print();
        }

        private void PrintWayBill_Click(object sender, EventArgs e)
        {
            InputDate inputDate = new InputDate(_dgvMain, Actions.Print);
            inputDate.ShowDialog();
        }

        private void ShowWayBill_Click(object sender, EventArgs e)
        {
            InputDate inputDate = new InputDate(_dgvMain, Actions.Show);
            inputDate.ShowDialog();
        }

        private void ShowInvoice_Click(object sender, EventArgs e)
        {
            CreateDocument doc = createDocument();

            if (doc != null)
                doc.ShowInvoice();
        }

        private void ShowAttacheToOrder_Click(object sender, EventArgs e)
        {
            CreateDocument doc = createDocument();

            if (doc != null)
                doc.ShowAttacheToOrder();
        }

        private void ShowProxyOnSTO_Click(object sender, EventArgs e)
        {
            CreateDocument doc = createDocument();

            if (doc != null)
                doc.ShowProxyOnSTO();
        }

        private CreateDocument createDocument()
        {
            if (_dgvMain.GetCarID() == 0)
                return null;

            return new CreateDocument(_dgvMain.GetCarID(), _dgvMain.GetID());
        }

        private void ShowPolicyKasko_Click(object sender, EventArgs e)
        {
            if (_dgvMain.GetCarID() == 0)
                return;

            Car car = _carList.getItem(_dgvMain.GetCarID());

            PolicyList policyList = PolicyList.getInstance();
            Policy kasko = policyList.getItem(car, PolicyType.КАСКО);

            if (!string.IsNullOrEmpty(kasko.file))
                WorkWithFiles.openFile(kasko.file);
        }

        private void ShowActFuelCard_Click(object sender, EventArgs e)
        {
            if (_dgvMain.GetCarID() == 0)
                MessageBox.Show("Для формирования акта выберите ячейку в таблице", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                CreateDocument doc = new CreateDocument(_dgvMain.GetCarID(), _dgvMain.GetID());
                doc.ShowActFuelCard();
            }
        }

        private void ShowNotice_Click(object sender, EventArgs e)
        {
            if (_dgvMain.GetCarID() == 0)
                return;

            if (_mainStatus.Get() == Status.DTP)
            {
                DTPList dtpList = DTPList.getInstance();
                DTP dtp = dtpList.getItem(_dgvMain.GetID());

                Car car = _carList.getItem(_dgvMain.GetCarID());

                CreateDocument doc = new CreateDocument(car);

                doc.showNotice(dtp);
            }
            else
                MessageBox.Show("Для формирования извещения необходимо перейти на вид \"ДТП\"", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ShowSTS_Click(object sender, EventArgs e)
        {
            if (_dgvMain.GetCarID() == 0)
                return;

            Car car = _carList.getItem(_dgvMain.GetCarID());

            STSList stsList = STSList.getInstance();
            STS sts = stsList.getItem(car);

            if (!string.IsNullOrEmpty(sts.File))
                WorkWithFiles.openFile(sts.File);
        }

        private void ShowDriverLicense_Click(object sender, EventArgs e)
        {
            if (_dgvMain.GetID() == 0)
                return;

            DateTime date = DateTime.Today;

            if (_mainStatus.Get() == Status.DTP)
            {
                DTPList dtpList = DTPList.getInstance();
                DTP dtp = dtpList.getItem(_dgvMain.GetID());
                date = dtp.Date;
            }

            Car car = _carList.getItem(_dgvMain.GetCarID());

            DriverCarList driverCarList = DriverCarList.getInstance();
            Driver driver = driverCarList.GetDriver(car, date);

            LicenseList licencesList = LicenseList.getInstance();
            DriverLicense driverLicense = licencesList.getItem(driver);

            if (!string.IsNullOrEmpty(driverLicense.File))
                WorkWithFiles.openFile(driverLicense.File);
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Documents_Click(object sender, EventArgs e)
        {
            Process.Start(@"\\bbmru08.bbmag.bbraun.com\Depts\Logistics\Автохозяйство\документы на авто");
        }

        private void NewCar_Click(object sender, EventArgs e)
        {
            Car_AddEdit aeCar = new Car_AddEdit(new Car());
            if (aeCar.ShowDialog() == DialogResult.OK)
                _mainStatus.Set(_mainStatus.Get());
        }

        private void NewAccount_Click(object sender, EventArgs e)
        {
            Account_AddEdit aeaAcountForm = new Account_AddEdit(new Account());
            aeaAcountForm.ShowDialog();
            if (aeaAcountForm.ShowDialog() == DialogResult.OK)
                _mainStatus.Set(_mainStatus.Get());
        }

        private void NewFuelCard_Click(object sender, EventArgs e)
        {
            FuelCard_AddEdit fuelCardAddEdit = new FuelCard_AddEdit(new FuelCard());
            fuelCardAddEdit.ShowDialog();
            if (fuelCardAddEdit.ShowDialog() == DialogResult.OK)
                _mainStatus.Set(_mainStatus.Get());
        }

        private void ShowPolicyList_Click(object sender, EventArgs e)
        {
            CreateDocument doc = new CreateDocument();
            doc.CreatePolicyTable();
        }

        private void PrintAllTable_Click(object sender, EventArgs e)
        {
            MyPrinter myprinter = new MyPrinter();

            string printerName = myprinter.GetDefaultPrinterName();

            if (string.IsNullOrEmpty(printerName))
            {
                MessageBox.Show("Принтер по умолчанию не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string message = string.Concat("Вывести справочник \"", _mainStatus.ToString(), "\" на печать на принтер ", printerName, "?");

            if (MessageBox.Show(message, "Печать", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CreateDocument doc = DgvToExcel();
                doc.Print();
            }
        }

        private void ShowAllTable_Click(object sender, EventArgs e)
        {
            CreateDocument doc = DgvToExcel();
            doc.Show();
        }

        private CreateDocument DgvToExcel()
        {
            CreateDocument doc = new CreateDocument();
            doc.CreateExcelFromAllDGV(_dgvMain.GetDGV());
            doc.CreateHeader("Справочник \"" + _mainStatus.ToString() + "\"");

            return doc;
        }

        private void Actual_Click(object sender, EventArgs e)
        {
            _mainStatus.Set(Status.Actual);
        }

        private void Buy_Click(object sender, EventArgs e)
        {
            _mainStatus.Set(Status.Buy);
        }

        private void Sale_Click(object sender, EventArgs e)
        {
            _mainStatus.Set(Status.Sale);
        }

        private void Invoice_Click(object sender, EventArgs e)
        {
            _mainStatus.Set(Status.Invoice);
        }

        private void TempMove_Click(object sender, EventArgs e)
        {
            _mainStatus.Set(Status.TempMove);
        }

        private void Policy_Click(object sender, EventArgs e)
        {
            _mainStatus.Set(Status.Policy);
        }

        private void Violation_Click(object sender, EventArgs e)
        {
            _mainStatus.Set(Status.Violation);
        }

        private void DTP_Click(object sender, EventArgs e)
        {
            _mainStatus.Set(Status.DTP);
        }

        private void DiagCard_Click(object sender, EventArgs e)
        {
            _mainStatus.Set(Status.DiagCard);
        }

        private void Repair_Click(object sender, EventArgs e)
        {
            _mainStatus.Set(Status.Repair);
        }

        private void ShipPart_Click(object sender, EventArgs e)
        {
            _mainStatus.Set(Status.ShipPart);
        }

        private void Account_Click(object sender, EventArgs e)
        {
            _mainStatus.Set(Status.Account);
        }

        private void FuelCard_Click(object sender, EventArgs e)
        {
            _mainStatus.Set(Status.FuelCard);
        }

        private void Driver_Click(object sender, EventArgs e)
        {
            _mainStatus.Set(Status.Driver);
            //formDriversList fDriversList = new formDriversList();
            //fDriversList.ShowDialog();
        }

        private void Region_Click(object sender, EventArgs e)
        {
            loadDictionary("Region", "Справочник \"Регионы\"");
        }

        private void SuppyAddress_Click(object sender, EventArgs e)
        {
            formSuppyAddressList formsuppyAddressList = new formSuppyAddressList();
            formsuppyAddressList.ShowDialog();
        }

        private void Employee_Click(object sender, EventArgs e)
        {
            formEmployeesList formemployeesList = new formEmployeesList();
            formemployeesList.ShowDialog();
        }

        private void loadDictionary(string name, string title)
        {
            formOneStringDictionary oneSD = new formOneStringDictionary(name, title);
            oneSD.ShowDialog();
        }
        
        private void Mark_Click(object sender, EventArgs e)
        {
            loadDictionary("Mark", "Справочник \"Марки автомобилей\"");
        }

        private void Model_Click(object sender, EventArgs e)
        {
            formModelList mList = new formModelList();
            mList.ShowDialog();
        }

        private void Grade_Click(object sender, EventArgs e)
        {
            formGradeList gList = new formGradeList();
            gList.ShowDialog();
        }

        private void EngineType_Click(object sender, EventArgs e)
        {
            loadDictionary("EngineType", "Справочник \"Типы двигателей\"");
        }

        private void Color_Click(object sender, EventArgs e)
        {
            loadDictionary("Color", "Справочник \"Цветов кузова\"");
        }

        private void Diler_Click(object sender, EventArgs e)
        {
            formDillerList dList = new formDillerList();
            dList.ShowDialog();
        }

        private void Owner_Click(object sender, EventArgs e)
        {
            loadDictionary("Owner", "Справочник \"Собственники\"");
        }

        private void Comp_Click(object sender, EventArgs e)
        {
            loadDictionary("Comp", "Справочник \"Страховые компании\"");
        }

        private void ServiceStantion_Click(object sender, EventArgs e)
        {
            loadDictionary("ServiceStantion", "Справочник \"Станции технического обслуживания\"");

            ServiceStantions serviceStantions = ServiceStantions.getInstance();
            serviceStantions.ReLoad();
        }

        private void ServiceStantionComp_Click(object sender, EventArgs e)
        {
            formSsDTPList formssDTPList = new formSsDTPList();
            formssDTPList.ShowDialog();
        }

        private void Culprit_Click(object sender, EventArgs e)
        {
            loadDictionary("culprit", "Справочник \"Виновники ДТП\"");
        }
        
        private void RepairType_Click(object sender, EventArgs e)
        {
            loadDictionary("RepairType", "Справочник \"Типы ремонта\"");

            RepairTypes repairTypes = RepairTypes.getInstance();
            repairTypes.ReLoad();
        }

        private void StatusAfterDTP_Click(object sender, EventArgs e)
        {
            loadDictionary("StatusAfterDTP", "Справочник \"Статусы автомобиля после ДТП\"");
        }

        private void CurrentStatusAfterDTP_Click(object sender, EventArgs e)
        {
            loadDictionary("CurrentStatusAfterDTP", "Справочник \"Текущее состояние после ДТП\"");

            CurrentStatusAfterDTPs currentStatusAfterDTPs = CurrentStatusAfterDTPs.getInstance();
            currentStatusAfterDTPs.ReLoad();
        }
        
        private void ViolationType_Click(object sender, EventArgs e)
        {
            loadDictionary("ViolationType", "Справочник \"Типы нарушений ПДД\"");

            ViolationTypes violationType = ViolationTypes.getInstance();
            violationType.ReLoad();
        }

        private void ProxyType_Click(object sender, EventArgs e)
        {
            loadDictionary("proxyType", "Справочник \"Типы доверенностей\"");
        }
        
        private void FuelCardType_Click(object sender, EventArgs e)
        {
            loadDictionary("FuelCardType", "Справочник \"Типы топливных карт\"");

            FuelCardTypes fuelCardTypes = FuelCardTypes.getInstance();
            fuelCardTypes.ReLoad();
        }
        
        private void MailText_Click(object sender, EventArgs e)
        {
            formMailText fMailText = new formMailText();
            fMailText.ShowDialog();
        }
        
        private void Template_Click(object sender, EventArgs e)
        {
            formTemplateList formtemplateList = new formTemplateList();
            formtemplateList.ShowDialog();
        }

        private void UserAccess_Click(object sender, EventArgs e)
        {
            formUsersAccess fUserAccess = new formUsersAccess();
            fUserAccess.ShowDialog();
        }

        private void Profession_Click(object sender, EventArgs e)
        {
            loadDictionary("EmployeesName", "Справочник \"Профессий\"");

            EmployeesNames employeesNames = EmployeesNames.getInstance();
            employeesNames.ReLoad();
        }

        private void Sort_Click(object sender, EventArgs e)
        {
            DataGridView dgv = _dgvMain.GetDGV();

            if (dgv.SelectedCells.Count == 0)
                return;

            int rowIndex = dgv.CurrentCell.RowIndex;
            int columnIndex = dgv.CurrentCell.ColumnIndex;

            DataGridViewColumn column = dgv.Columns[dgv.CurrentCell.ColumnIndex];
            System.ComponentModel.ListSortDirection sortDirection;

            if ((dgv.SortedColumn == null) || (dgv.SortedColumn != column))
                sortDirection = System.ComponentModel.ListSortDirection.Ascending;
            else if (dgv.SortOrder == SortOrder.Ascending)
                sortDirection = System.ComponentModel.ListSortDirection.Descending;
            else
                sortDirection = System.ComponentModel.ListSortDirection.Ascending;

            dgv.Sort(column, sortDirection);

            dgv.CurrentCell = dgv.Rows[rowIndex].Cells[columnIndex];
        }

        private void Filter_Click(object sender, EventArgs e)
        {
            DataGridView dgv = _dgvMain.GetDGV();

            if (dgv.CurrentCell == null)
                return;

            string columnName = dgv.Columns[dgv.CurrentCell.ColumnIndex].HeaderText;
            
            Point point = new Point(dgv.CurrentCell.ColumnIndex, dgv.CurrentCell.RowIndex);

            MyFilter myFilter = (dgv.Name == "_dgvCar") ? MyFilter.GetInstanceCars() : MyFilter.GetInstanceDrivers();
            myFilter.SetFilterValue(string.Concat(columnName, ":"), point);
        }

        private void AddDriver_Click(object sender, EventArgs e)
        {
            AddNewDriver addNewDriver = new AddNewDriver();
            if (addNewDriver.ShowDialog() == DialogResult.OK)
                _mainStatus.Set(_mainStatus.Get());
        }

        private void DeleteDriver_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить водителя из списка?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                DriverList driverList = DriverList.getInstance();
                Driver driver = driverList.getItem(_dgvMain.GetID());
                DriverCarList driverCarList = DriverCarList.getInstance();

                if (driverCarList.IsDriverHaveCar(driver))
                    MessageBox.Show("За водителем закреплён автомобиль, удаление невозможно", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                else
                {
                    driver.IsDriver = false;
                    driver.Save();
                    _mainStatus.Set(_mainStatus.Get());
                }
            }
        }

        private void MyPointList_Click(object sender, EventArgs e)
        {
            formMyPointList myPointList = new formMyPointList();
            myPointList.ShowDialog();
        }
    }
}
