using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Common;

namespace BBAuto.Domain.Import
{
    public class TabelFrom1C : IExcelImporter
    {
        private const string FILE_PATH = @"\\bbmru08\1cv77\Autoexchange\Lotus\BBAuto\Time";

        public void StartImport()
        {
            TabelList tabelList = TabelList.GetInstance();

            string[] files = Directory.GetFiles(FILE_PATH, "*.txt");

            foreach (var file in files)
            {
                string date = file.Split('_')[2].Split('.')[0];
                int month = Convert.ToInt32(string.Concat(date[0], date[1]));
                int year = Convert.ToInt32(string.Concat("20", date[2], date[3]));

                string[] lines = File.ReadAllLines(file);

                for (int i = 1; i < lines.Count(); i++)
                {
                    string[] fields = lines[i].Split(';');

                    for (int j = 2; j < fields.Count(); j++)
                    {
                        if ((fields[j] == "Я") || (fields[j] == "Я/Н"))
                        {
                            Tabel tabel = new Tabel(fields[0], new DateTime(year, month, j - 1));
                            tabel.Save();
                        }
                    }
                }

                File.Move(file, FILE_PATH + @"\processed\" + DateTime.Today.ToShortDateString() + " " + Path.GetFileName(file));
            }
        }
    }
}
