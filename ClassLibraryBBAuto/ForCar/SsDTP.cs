using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class SsDTP : MainDictionary
    {
        private int idServiceStantion;

        public string ID
        {
            get
            {
                return _id.ToString();
            }
            set
            {
                int.TryParse(value, out _id);
            }
        }

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

        public string Mark
        {
            get
            {
                Marks marks = Marks.getInstance();
                return marks.getItem(_id);
            }
            set
            {
                int.TryParse(value, out _id);
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
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out idServiceStantion);
        }

        public override void Save()
        {
            _provider.Insert("ssDTP", _id, idServiceStantion);
        }

        internal override void Delete()
        {
            _provider.Delete("ssDTP", _id);
        }

        internal override object[] getRow()
        {
            return new object[3] { _id, Mark, ServiceStantion };
        }
    }
}
