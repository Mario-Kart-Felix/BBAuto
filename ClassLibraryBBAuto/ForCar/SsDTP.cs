using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BBAuto.Domain.Abstract;
using BBAuto.Domain.Tables;
using BBAuto.Domain.Lists;
using BBAuto.Domain.Dictionary;

namespace BBAuto.Domain.ForCar
{
    public class SsDTP : MainDictionary
    {
        private int idServiceStantion;

        public string IDServiceStantion
        {
            get { return idServiceStantion.ToString(); }
            set { int.TryParse(value, out idServiceStantion); }
        }
        
        public string ServiceStantion
        {
            get { return ServiceStantions.getInstance().getItem(idServiceStantion); }
            set { int.TryParse(value, out idServiceStantion); }
        }

        public Mark Mark { get; set; }

        public SsDTP()
        {
            idServiceStantion = 0;
        }

        public SsDTP(DataRow row)
        {
            int idMark;
            int.TryParse(row.ItemArray[0].ToString(), out idMark);
            Mark = MarkList.getInstance().getItem(idMark);

            int.TryParse(row.ItemArray[1].ToString(), out idServiceStantion);
        }
        
        public override void Save()
        {
            _provider.Insert("ssDTP", Mark.ID, idServiceStantion);
        }

        internal override void Delete()
        {
            _provider.Delete("ssDTP", Mark.ID);
        }

        internal override object[] getRow()
        {
            return new object[3] { Mark.ID, Mark.Name, ServiceStantion };
        }
    }
}
