﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class ImportFrom1C
    {
        private const string FILE_PATH = @"\\bbmru08\1cv77\Autoexchange\Lotus\BBAuto";

        public void Start()
        {
            string[] files = Directory.GetFiles(FILE_PATH, "*.txt");

            foreach (var file in files)
            {
                string[] lines = File.ReadAllLines(file);

                for (int i = 1; i < lines.Count(); i++)
                {
                    string[] fields = lines[i].Split(';');

                    DriverList driverList = DriverList.getInstance();
                    Driver driver = driverList.getItemByNumber(fields[1]);
                    
                    driver.name = fields[0];
                    driver.Number = fields[1];
                    driver.Sex = fields[2];
                    driver.Region = fields[3];
                    driver.CompanyName = fields[4];
                    driver.Dept = fields[5];
                    driver.Position = fields[6];
                    driver.DateBirth = fields[7];
                    driver.Login = fields[9];
                    if ((string.IsNullOrEmpty(fields[10])) || (string.IsNullOrEmpty(driver.email)))
                        driver.email = fields[10];

                    driver.Decret = ((fields[15] == "Временно не работает") || (fields[15] == "В декретном отпуске"));
                    driver.Fired = (!string.IsNullOrEmpty(fields[15]) && (fields[15].Split(' ')[0] == "Уволен"));
                    driver.Save();
                    
                    if (!string.IsNullOrEmpty(fields[11]))
                    {
                        string passportNumber = fields[11].Replace(" ", "");
                        if (passportNumber.Length < 10)
                            continue;
                        passportNumber = string.Concat(passportNumber.Substring(0, 2), " ", passportNumber.Substring(2, 2), " ", passportNumber.Substring(4, 6));

                        PassportList passportList = PassportList.getInstance();
                        Passport passport;
                        passport = passportList.GetPassport(driver, passportNumber);
                        passport.number = passportNumber;

                        string[] fio = fields[0].Split(' ');
                        passport.name = fio[0];
                        passport.lastName = fio[1];
                        passport.secondName = fio[2];

                        passport.GiveDate = fields[12];
                        passport.giveOrg = fields[13];
                        passport.address = fields[14];
                        passport.Save();
                    }

                }
                
                File.Move(file, FILE_PATH + @"\processed\" + DateTime.Today.ToShortDateString() + " " + Path.GetFileName(file));
            }
        }
    }
}