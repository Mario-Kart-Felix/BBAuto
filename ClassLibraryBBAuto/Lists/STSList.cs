using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class STSList : MainList
    {
        private List<STS> list;
        private static STSList uniqueInstance;

        private STSList()
        {
            list = new List<STS>();

            loadFromSql();
        }

        public static STSList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new STSList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("STS");

            foreach (DataRow row in dt.Rows)
            {
                STS sts = new STS(row);
                Add(sts);
            }
        }

        public void Add(STS sts)
        {
            if (list.Exists(x => x == sts))
                return;
            
            list.Add(sts);
        }

        public void Delete(Car car)
        {
            STS sts = getItem(car);

            list.Remove(sts);

            sts.Delete();
        }

        public STS getItem(Car car)
        {
            var STSs = from sts in list
                          where sts.isEqualCarID(car)
                          select sts;

            if (STSs.Count() > 0)
                return STSs.First() as STS;
            else
                return car.createSTS();
        }
    }
}
