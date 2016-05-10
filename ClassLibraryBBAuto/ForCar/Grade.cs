using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class Grade : MainDictionary
    {
        private string _name;
        private EngineType _engineType;
        private int _idModel;

        public string ePower;
        public string eVol;
        public string maxLoad;
        public string noLoad;

        public Grade(int idModel)
        {
            _id = 0;
            _name = string.Empty;
            ePower = string.Empty;
            eVol = string.Empty;
            maxLoad = string.Empty;
            noLoad = string.Empty;

            EngineTypeList engineTypeList = EngineTypeList.getInstance();
            _engineType = engineTypeList.getItem(1);

            _idModel = idModel;
        }

        public Grade(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            _name = row.ItemArray[1].ToString();
            ePower = row.ItemArray[2].ToString();
            eVol = row.ItemArray[3].ToString();
            maxLoad = row.ItemArray[4].ToString();
            noLoad = row.ItemArray[5].ToString();

            int idEngineType;
            int.TryParse(row.ItemArray[6].ToString(), out idEngineType);
            EngineTypeList engineTypeList = EngineTypeList.getInstance();
            _engineType = engineTypeList.getItem(idEngineType);

            int.TryParse(row.ItemArray[7].ToString(), out _idModel);
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public EngineType EngineType
        {
            get { return _engineType; }
            set { _engineType = value; }
        }
                
        internal override void Delete()
        {
            _provider.Delete("Grade", _id);
        }

        public override void Save()
        {
            int.TryParse(_provider.Insert("Grade", _id, _name, ePower, eVol, maxLoad, noLoad, _engineType.ID, _idModel), out _id);

            GradeList gradeList = GradeList.getInstance();
            gradeList.Add(this);
        }

        internal override object[] getRow()
        {
            return new object[2] { _id, _name };
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
