using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class Grade : MainDictionary
    {
        private int _idEngineType;
        private int _idModel;

        public string ePower;
        public string eVol;
        public string maxLoad;
        public string noLoad;

        public string IDEngineType
        {
            get { return _idEngineType.ToString(); }
            set { int.TryParse(value, out _idEngineType); }
        }

        public Grade(int idModel)
        {
            _id = 0;
            name = string.Empty;
            ePower = string.Empty;
            eVol = string.Empty;
            maxLoad = string.Empty;
            noLoad = string.Empty;
            _idEngineType = 0;
            _idModel = idModel;
        }

        public Grade(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            name = row.ItemArray[1].ToString();
            ePower = row.ItemArray[2].ToString();
            eVol = row.ItemArray[3].ToString();
            maxLoad = row.ItemArray[4].ToString();
            noLoad = row.ItemArray[5].ToString();
            int.TryParse(row.ItemArray[6].ToString(), out _idEngineType);
            int.TryParse(row.ItemArray[7].ToString(), out _idModel);
        }

        internal override void Delete()
        {
            _provider.Delete("Grade", _id);
        }

        public override void Save()
        {
            int.TryParse(_provider.Insert("Grade", _id, name, ePower, eVol, maxLoad, noLoad, _idEngineType, _idModel), out _id);

            GradeList gradeList = GradeList.getInstance();
            gradeList.Add(this);
        }

        internal override object[] getRow()
        {
            return new object[2] { _id, name };
        }

        internal bool isEqualModelID(int idModel)
        {
            return _idModel == idModel;
        }

        public DataTable ToDataTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Название");
            dt.Columns.Add("Значение");

            dt.Rows.Add("Мощность двигателя", ePower);
            dt.Rows.Add("Объем двигателя", eVol);
            dt.Rows.Add("Разрешенная максимальная масса", maxLoad);
            dt.Rows.Add("Масса без нагрузки", noLoad);

            return dt;
        }
    }
}
