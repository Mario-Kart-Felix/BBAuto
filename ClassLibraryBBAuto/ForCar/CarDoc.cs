using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class CarDoc : MainDictionary
    {
        private int idCar;
        public string file;

        public CarDoc(int idCar)
        {
            this.idCar = idCar;
            _id = 0;
        }
        
        public CarDoc(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out idCar);
            name = row.ItemArray[2].ToString();
            file = row.ItemArray[3].ToString();
            _fileBegin = file;
        }

        public override void Save()
        {
            DeleteFile(file);

            file = WorkWithFiles.fileCopyByID(file, "cars", idCar, "Documents", name);

            int.TryParse(_provider.Insert("CarDoc", _id, idCar, name, file), out _id);
        }

        internal override object[] getRow()
        {
            string show = "";
            if (file != string.Empty)
                show = "Показать";

            return new object[3] { _id, name, show };
        }

        internal override void Delete()
        {
            DeleteFile(file);

            _provider.Delete("CarDoc", _id);
        }

        internal bool isEqualCarID(Car car)
        {
            return car.IsEqualsID(idCar);
        }
    }
}
