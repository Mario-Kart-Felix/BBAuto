using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BBAuto.Domain
{
    public class CarDoc : MainDictionary
    {
        private int idCar;
        private string _name;
        private string _file;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string File
        {
            get { return _file; }
            set { _file = value; }
        }

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
            _name = row.ItemArray[2].ToString();
            _file = row.ItemArray[3].ToString();
            _fileBegin = _file;
        }

        public override void Save()
        {
            DeleteFile(_file);

            _file = WorkWithFiles.fileCopyByID(_file, "cars", idCar, "Documents", _name);

            int.TryParse(_provider.Insert("CarDoc", _id, idCar, _name, _file), out _id);
        }

        internal override object[] getRow()
        {
            return new object[3] { _id, _name, (_file == string.Empty) ? string.Empty : "Показать" };
        }

        internal override void Delete()
        {
            DeleteFile(_file);

            _provider.Delete("CarDoc", _id);
        }

        internal bool isEqualCarID(Car car)
        {
            return car.IsEqualsID(idCar);
        }
    }
}
