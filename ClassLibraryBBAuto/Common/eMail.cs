using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class eMail
    {
        private const string SERVER_HOST = "212.0.16.135";
        private const int SERVER_PORT = 25;
        private const string SERVER_PASSWORD = "";
        private const string SERVER_USER_NAME = "";

        private const string ROBOT_EMAIL = "ru.robot@bbraun.com";

        private string _authorEmail;

        private string _subject;
        private string _body;

        public eMail()
        {
            Driver driver = User.getDriver();
            DriverList driverList = DriverList.getInstance();
            Driver employeeTransport = driverList.GetDriverListByRole(RolesList.Editor).First();
            _authorEmail = driver == null ? employeeTransport == null ? ROBOT_EMAIL : employeeTransport.email : driver.email;
        }

        public void sendMailViolation(Violation violation)
        {
            _subject = "Штраф";

            createMailAndSendViolation(violation);
        }

        private void createMailAndSendViolation(Violation violation)
        {
            DriverList driverList = DriverList.getInstance();
            
            Driver boss = driverList.GetDriverListByRole(RolesList.Boss).First();

            string[] emailList;

            if (violation.NoDeduction)
            {
                CreateBodyViolationNoDeduction(violation);
                emailList = new string[] { _authorEmail };
            }
            else
            {
                CreateBodyViolation(violation);
                Driver driver = violation.getDriver();
                emailList = new string[] { driver.email, _authorEmail };
            }

            List<Attachment> list = new List<Attachment>();
            list.Add(new Attachment(violation.File));

            Send(new List<Driver> { boss }, emailList, list);
        }

        private void CreateBodyViolation(Violation violation)
        {
            Driver driver = violation.getDriver();
            
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Добрый день!");
            sb.AppendLine("");
            if (driver.Sex == "мужской")
                sb.Append("Сообщаю о том, что сотрудник ");
            else
                sb.Append("Сообщаю о том, что сотрудница ");

            sb.Append(driver.GetName(NameType.Short));
            
            if (driver.Sex == "мужской")
                sb.AppendLine(" нарушил ПДД РФ.");
            else
                sb.AppendLine(" нарушила ПДД РФ.");

            sb.Append("Рекомендую удержание в размере ");
            sb.Append(violation.Sum);

            if (driver.Sex == "мужской")
                sb.AppendLine(" р. из заработной платы сотрудника.");
            else
                sb.AppendLine(" р. из заработной платы сотрудницы.");

            sb.AppendLine("Постановление в приложении.");

            sb.AppendLine("");
            sb.AppendLine("С уважением,");
            sb.AppendLine(User.getDriver().GetName(NameType.Full));
            sb.AppendLine(User.getDriver().Position);
            sb.AppendLine(User.getDriver().Mobile);

            _body = sb.ToString();
        }

        private void CreateBodyViolationNoDeduction(Violation violation)
        {
            Driver driver = violation.getDriver();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Добрый день!");
            sb.AppendLine("");
            sb.AppendLine("Сообщаю о том, что произошло нарушение ПДД.");
            sb.AppendLine("Прошу оплатить данное постановление.");
            sb.AppendLine("Постановление в приложении.");
            sb.AppendLine("");
            sb.AppendLine("С уважением,");
            sb.AppendLine(User.getDriver().GetName(NameType.Full));
            sb.AppendLine(User.getDriver().Position);
            sb.AppendLine(User.getDriver().Mobile);

            _body = sb.ToString();
        }

        public void sendMailPolicy(Car car, PolicyType type)
        {
            PolicyList policyList = PolicyList.getInstance();
            Policy policy = policyList.getItem(car, type);
            
            if (string.IsNullOrEmpty(policy.File))
                throw new Exception("Не найден файл полиса");
            else
            {
                _subject = "Полис " + type.ToString();

                CreateBodyPolicy(type);

                DriverCarList driverCarList = DriverCarList.getInstance();
                Driver driver = driverCarList.GetDriver(car);
                
                Send(new List<Driver> { driver }, new string[] { _authorEmail }, new List<Attachment>() { new Attachment(policy.File) });
            }
        }

        private void CreateBodyPolicy(PolicyType type)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Добрый день!");
            sb.AppendLine("");
            if (type == PolicyType.КАСКО)
                sb.AppendLine(string.Concat("Высылаю новый полис ", type.ToString(), " в эл. виде."));
            else
                sb.AppendLine(string.Concat("Вам был отправлен полис ", type.ToString(), ", прошу проинформировать меня о его получении."));
            sb.AppendLine("");
            sb.AppendLine("С уважением,");
            sb.AppendLine(User.getDriver().GetName(NameType.Full));
            sb.AppendLine(User.getDriver().Position);
            sb.AppendLine(User.getDriver().Mobile);

            _body = sb.ToString();
        }
        
        internal void sendMailAccount(Account account)
        {
            _subject = "Согласование счета " + account.Number;

            createMailAndSendAccount(account);
        }

        private void createMailAndSendAccount(Account account)
        {
            createBodyAccount(account);

            DriverList driverList = DriverList.getInstance();
            List<Driver> accountants = new List<Driver>();

            if (account.Owner == "ООО \"Б.Браун Медикал\"")
                accountants = driverList.GetDriverListByRole(RolesList.AccountantBBraun);
            else if (account.Owner == "ООО \"ГЕМАТЕК\"")
                accountants = driverList.GetDriverListByRole(RolesList.AccountantGematek);
            else
                throw new NotImplementedException("Не заданы бухгалтеры для данной фирмы.");

            if (accountants.Count == 0)
                throw new NullReferenceException("Не найдены e-mail адреса бухгалтеров");

            Driver boss = driverList.GetDriverListByRole(RolesList.Boss).First();
            
            Send(accountants, new string[] { boss.email }, new List<Attachment>() { new Attachment(account.File) });
        }

        private void createBodyAccount(Account account)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Добрый день!");
            sb.AppendLine("");
            
            Driver driver = account.GetDriver();
            string employeeSex = string.Empty;

            if (driver != null)
            {
                employeeSex = (driver.Sex == "мужской") ? "сотрудника" : "сотрудницы";
            }

            PolicyType policyType = (PolicyType)Convert.ToInt32(account.IDPolicyType);

            if ((policyType == PolicyType.расш_КАСКО) && ((!account.BusinessTrip)))
            {
                sb.AppendLine("В связи с личной поездкой за пределы РФ был сделан полис по расширению КАСКО.");
                sb.Append("Прошу оплатить счет с удержанием из заработной платы ");
                sb.Append(employeeSex);
                sb.Append(" ");
                sb.Append(driver.GetName(NameType.Genetive));
                sb.Append(" сумму в размере ");
            }
            else if ((policyType == PolicyType.расш_КАСКО) && ((account.BusinessTrip)))
            {
                sb.Append("В связи с командировкой ");
                sb.Append(employeeSex);
                sb.Append(" ");
                sb.Append(driver.GetName(NameType.Genetive));
                sb.AppendLine(" за пределы РФ, был сделан полис по расширению КАСКО.");
                sb.Append("Прошу оплатить счет в размере ");
            }
            else
            {
                sb.Append("Прошу оплатить счёт № ");
                sb.AppendLine(account.Number);

                sb.Append("Cумма оплаты ");
            }

            sb.Append(account.Sum);
            sb.AppendLine(" р..");

            if (policyType == PolicyType.ДСАГО)
            {
                sb.Append("Данную сумму необходимо удержать из заработной платы ");
                sb.Append(driver.GetName(NameType.Short));
                sb.AppendLine(".");
            }

            sb.AppendLine("Скан копия счёта в приложении.");
            
            sb.AppendLine("");
            sb.AppendLine("С уважением,");
            sb.AppendLine(User.getDriver().GetName(NameType.Full));
            sb.AppendLine(User.getDriver().Position);
            sb.AppendLine(User.getDriver().Dept);
            sb.AppendLine(User.getDriver().Mobile);

            _body = sb.ToString();
        }
        
        private void Send(List<Driver> drivers, string[] copyEmails = null, List<Attachment> files = null)
        {
            if (string.IsNullOrEmpty(_authorEmail))
                throw new Exception("ваш email не найден");

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress(_authorEmail);

            foreach (Driver driver in drivers)
            {
                if (string.IsNullOrEmpty(driver.email))
                    _subject += " не найден email сотрудника " + driver.GetName(NameType.Genetive);
                else
                    msg.To.Add(new MailAddress(driver.email));
            }

            if (msg.To.Count == 0)
                msg.To.Add(new MailAddress(_authorEmail)); 

            if (copyEmails != null)
            {
                foreach (string copyEmail in copyEmails)
                    msg.CC.Add(new MailAddress(copyEmail));
            }

            msg.Subject = _subject;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = _body;

            if (files != null)
                files.ForEach(item => msg.Attachments.Add(item));

            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;
            SmtpClient client = new SmtpClient(SERVER_HOST, SERVER_PORT);

            client.Credentials = new System.Net.NetworkCredential(SERVER_USER_NAME, SERVER_PASSWORD);

            client.Send(msg);
            msg.Dispose();
        }

        public static void OpenEmailProgram(string conEmails)
        {
            if (string.IsNullOrEmpty(conEmails))
                return;

            using (Process process = new Process())
            {
                process.StartInfo.FileName = "mailto:" + conEmails;
                process.Start();
            }
        }
        
        internal void SendNotification(Driver driver, string message, List<string> fileNames = null)
        {
            _subject = "Уведомление";
            _body = message;

            DriverList driverList = DriverList.getInstance();
            Driver transportEmployee = driverList.GetDriverListByRole(RolesList.Editor).First();
            
            List<Attachment> listAttachment = new List<Attachment>();
            if (fileNames != null)
                fileNames.ForEach(item => listAttachment.Add(new Attachment(item)));
            
            Send(new List<Driver> { driver }, new string[] { transportEmployee.email }, listAttachment);
        }
    }
}