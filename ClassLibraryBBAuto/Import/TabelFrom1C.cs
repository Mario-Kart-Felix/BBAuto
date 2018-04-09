using System;
using System.IO;
using System.Linq;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Common;
using BBAuto.Logic.Lists;

namespace BBAuto.Logic.Import
{
  public class TabelFrom1C : IExcelImporter
  {
    public string FilePath { get; set; }

    public void StartImport()
    {
      TabelList tabelList = TabelList.GetInstance();

      string[] files = Directory.GetFiles(FilePath, "*.txt");

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

        File.Move(file, FilePath + @"\processed\" + DateTime.Today.ToShortDateString() + " " + Path.GetFileName(file));
      }
    }
  }
}
