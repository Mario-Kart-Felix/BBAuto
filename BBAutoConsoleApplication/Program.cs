using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibraryBBAuto;

namespace BBAutoConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBase.InitDataBase();
            Provider.InitSQLProvider();
            
            ImportFrom1C importFrom1C = new ImportFrom1C();
            importFrom1C.Start();
            
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