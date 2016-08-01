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

            IExcelImporter employeesImporter = new EmployeesFrom1C();
            employeesImporter.StartImport();

            IExcelImporter tabelImporter = new TabelFrom1C();
            tabelImporter.StartImport();
            
            MedicalCertList medicalCertList = MedicalCertList.getInstance();
            NotificationSender medicalCertSender = new NotificationSender(medicalCertList);
            medicalCertSender.SendNotification();
            medicalCertSender.ClearStopIfNeed();
            medicalCertSender.SendNotificationOverdue();
            medicalCertSender.SendNotificationNotExist();
            
            LicenseList licenseList = LicenseList.getInstance();
            NotificationSender licenceSender = new NotificationSender(licenseList);
            licenceSender.SendNotification();
            licenceSender.SendNotificationOverdue();
            licenceSender.SendNotificationNotExist();

            PolicyListSender policySender = new PolicyListSender();
            policySender.SendNotification();

            DiagCardSender diagCardSender = new DiagCardSender();
            diagCardSender.SendNotification();
        }
    }
}