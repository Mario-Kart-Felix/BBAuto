using BBAuto.Domain.Abstract;
using BBAuto.Domain.Common;
using BBAuto.Domain.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace BBAuto.Domain.Import
{
    public class BusinessTripFromExcelFile : IExcelImporter
    {
        public string FilePath { get; set; }

        public void StartImport()
        {
            try
            {
                using (ExcelDoc excel = new ExcelDoc(FilePath))
                {
                    int i = 7;

                    string curCell = "H" + i;
                    while (excel.getValue(curCell) != null)
                    {
                        curCell = "A" + i;
                        if ((excel.getValue(curCell) == null) ||
                            ((excel.getValue(curCell).ToString().ToUpper() != "AM") && (excel.getValue(curCell).ToString().ToUpper() != "АМ")))
                        {
                            curCell = "H" + i;
                            i++;
                            continue;
                        }

                        curCell = "H" + i;
                        string number = excel.getValue(curCell).ToString();

                        curCell = "D" + i;
                        DateTime dateBegin;
                        DateTime.TryParse(excel.getValue1(curCell).ToString(), out dateBegin);

                        curCell = "E" + i;
                        DateTime dateEnd;
                        DateTime.TryParse(excel.getValue1(curCell).ToString(), out dateEnd);

                        for (DateTime date = dateBegin; date <= dateEnd; date = date.AddDays(1))
                        {
                            Tabel tabel = new Tabel(number, date) { Comment = "businessTrip" };
                            tabel.Save();
                        }

                        curCell = "H" + i;
                        i++;
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                LogManager.Logger.Error(ex, "Error in file {file}", FilePath);
            }
            catch (COMException ex)
            {
                LogManager.Logger.Error(ex, "Error in file {file}", FilePath);
            }
        }
    }
}
