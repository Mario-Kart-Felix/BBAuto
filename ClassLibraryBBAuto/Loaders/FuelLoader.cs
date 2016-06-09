using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto.Loaders
{
    public class FuelLoader
    {
        private const int BENZIN_ID = 1;
        private const int DIESEL_ID = 2;
        private const string DIESEL_NAME = "ДТ";

        private string path;
        private static readonly Dictionary<FuelReport, Action<ExcelDoc>> loaders;

        private static FuelCardList fuelCardList;
        private static EngineTypeList engineTypeList;
        private static EngineType benzin;
        private static EngineType disel;

        public FuelLoader(string path)
        {
            this.path = path;
        }

        static FuelLoader()
        {
            fuelCardList = FuelCardList.getInstance();
            engineTypeList = EngineTypeList.getInstance();
            benzin = engineTypeList.getItem(BENZIN_ID);
            disel = engineTypeList.getItem(DIESEL_ID);

            loaders = new Dictionary<FuelReport, Action<ExcelDoc>>();

            loaders.Add(FuelReport.Петрол, LoadPetrol);
            loaders.Add(FuelReport.Neste, LoadPetrol);
            loaders.Add(FuelReport.Чеки, LoadPetrol);
        }

        private static void LoadPetrol(ExcelDoc excel)
        {
            int i = 4; //начальный индекс
            
            string currentCell = "B" + i;
            while (excel.getValue(currentCell, currentCell) != null)
            {
                currentCell = "D" + i;
                string number = excel.getValue(currentCell, currentCell).ToString();
                FuelCard fuelCard = fuelCardList.getItem(number);
                if (fuelCard == null) { throw new NullReferenceException("Не найдена карта №" + number); }

                currentCell = "C" + i;
                DateTime datetime;
                DateTime.TryParse(excel.getValue(currentCell, currentCell).ToString(), out datetime);//присутствует время, не забываем убирать

                currentCell = "G" + i;
                string engineTypeName = excel.getValue(currentCell, currentCell).ToString();
                EngineType engineType = GetEngineType(engineTypeName);

                currentCell = "H" + i;
                double value;
                double.TryParse(excel.getValue(currentCell, currentCell).ToString(), out value);

                Fuel fuel = new Fuel(fuelCard, datetime.Date, engineType);
                fuel.AddValue(value);
                fuel.Save();

                i++;
                currentCell = "B" + i;
            }
        }

        private static EngineType GetEngineType(string engineTypeName)
        {
            return (engineTypeName == DIESEL_NAME) ? disel : benzin;
        }

        public void Load(FuelReport fuelReport)
        {
            using (ExcelDoc excelDoc = new ExcelDoc(path))
            {
                loaders[fuelReport].Invoke(excelDoc);
            }
        }
    }
}
