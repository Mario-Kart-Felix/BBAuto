using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBAuto.Domain;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Import;
using BBAuto.Domain.DataBase;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Senders;

namespace BBAutoConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBase.InitDataBase();
            Provider.InitSQLProvider();

            IExcelImporter importer = new BusinessTripFromExcelFile { FilePath = @"\\bbmru08\depts\Accounting\Командировки\Реестр_" + DateTime.Today.Year + ".xls" };
            importer.StartImport();

            importer = new EmployeesFrom1C { FilePath = @"\\bbmru08\1cv77\Autoexchange\Lotus\BBAuto" };
            importer.StartImport();

            importer = new TabelFrom1C { FilePath = @"\\bbmru08\1cv77\Autoexchange\Lotus\BBAuto\Time" };
            importer.StartImport();

            var medicalCertList = MedicalCertList.getInstance();
            var medicalCertSender = new NotificationSender(medicalCertList);
            medicalCertSender.SendNotification();
            medicalCertSender.ClearStopIfNeed();
            medicalCertSender.SendNotificationOverdue();
            medicalCertSender.SendNotificationNotExist();
            
            var licenseList = LicenseList.getInstance();
            var licenceSender = new NotificationSender(licenseList);
            licenceSender.SendNotification();
            licenceSender.SendNotificationOverdue();
            licenceSender.SendNotificationNotExist();

            var policySender = new PolicyListSender();
            policySender.SendNotification();

            var diagCardSender = new DiagCardSender();
            diagCardSender.SendNotification();

            var violationSender = new ViolationSender();
            violationSender.SendNotification();
        }
    }
}