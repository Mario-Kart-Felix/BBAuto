using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class SsDTP : MainDictionary
    {
        private Mark _mark;
        private int idServiceStantion;

        public string IDServiceStantion
        {
            get
            {
                return idServiceStantion.ToString();
            }
            set
            {
                int.TryParse(value, out idServiceStantion);
            }
        }
        
        public string ServiceStantion
        {
            get
            {
                ServiceStantions serviceStantions = ServiceStantions.getInstance();
                return serviceStantions.getItem(idServiceStantion);
            }
            set
            {
                int.TryParse(value, out idServiceStantion);
            }
        }

        public SsDTP()
        {
            _id = 0;
            idServiceStantion = 0;
        }

        public SsDTP(DataRow row)
        {
            int idMark;
            int.TryParse(row.ItemArray[0].ToString(), out idMark);
            MarkList markList = MarkList.getInstance();
            _mark = markList.getItem(idMark);

            int.TryParse(row.ItemArray[1].ToString(), out idServiceStantion);
        }

        public Mark Mark
        {
            get { return _mark; }
            set { _mark = value; }
        }

        public override void Save()
        {
            _provider.Insert("ssDTP", _mark.ID, idServiceStantion);
        }

        internal override void Delete()
        {
            _provider.Delete("ssDTP", _mark.ID);
        }

        internal override object[] getRow()
        {
            return new object[3] { _mark.ID, Mark, ServiceStantion };
        }
    }
}
