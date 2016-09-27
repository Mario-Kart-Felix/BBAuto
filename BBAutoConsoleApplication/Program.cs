﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BBAuto.Domain;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Import;
using BBAuto.Domain.DataBase;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Senders;
using BBAuto.Domain.Logger;

namespace BBAutoConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            DataBase.InitDataBase();
            Provider.InitSQLProvider();

            LogManager.Logger.Debug("Program started");
            IExcelImporter importer = new BusinessTripFromExcelFile { FilePath = @"\\bbmru08\depts\Accounting\Командировки\Реестр_" + DateTime.Today.Year + ".xls" };
            importer.StartImport();
            LogManager.Logger.Debug("BusinessTrip loading done");

            importer = new EmployeesFrom1C { FilePath = @"\\bbmru08\1cv77\Autoexchange\Lotus\BBAuto" };
            importer.StartImport();
            LogManager.Logger.Debug("EmployeesFrom1C loading done");

            importer = new TabelFrom1C { FilePath = @"\\bbmru08\1cv77\Autoexchange\Lotus\BBAuto\Time" };
            importer.StartImport();
            LogManager.Logger.Debug("TabelFrom1C loading done");

            var medicalCertList = MedicalCertList.getInstance();
            var medicalCertSender = new NotificationSender(medicalCertList);
            medicalCertSender.SendNotification();
            medicalCertSender.ClearStopIfNeed();
            medicalCertSender.SendNotificationOverdue();
            medicalCertSender.SendNotificationNotExist();
            LogManager.Logger.Debug("MedicalCerts sent");
            
            var licenseList = LicenseList.getInstance();
            var licenceSender = new NotificationSender(licenseList);
            licenceSender.SendNotification();
            licenceSender.SendNotificationOverdue();
            licenceSender.SendNotificationNotExist();
            LogManager.Logger.Debug("Licenses sent");

            var policySender = new PolicyListSender();
            policySender.SendNotification();
            LogManager.Logger.Debug("Policies sent");

            var diagCardSender = new DiagCardSender();
            diagCardSender.SendNotification();
            LogManager.Logger.Debug("DiagCards sent");

            var violationSender = new ViolationSender();
            violationSender.SendNotification();
            LogManager.Logger.Debug("Violations sent");

            LogManager.Logger.Debug("Program finished");
        }
    }
}