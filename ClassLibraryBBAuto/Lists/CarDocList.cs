using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class CarDocList : MainList
    {
        private List<CarDoc> list;
        private static CarDocList uniqueInstance;

        private CarDocList()
        {
            list = new List<CarDoc>();

            loadFromSql();
        }

        public static CarDocList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new CarDocList();

            return uniqueInstance;
        }
        
        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("CarDoc");

            foreach (DataRow row in dt.Rows)
            {
                CarDoc carDoc = new CarDoc(row);
                Add(carDoc);
            }
        }

        public void Add(CarDoc carDoc)
        {
            if (list.Exists(item => item == carDoc))
                return;

            list.Add(carDoc);
        }

        public void Delete(int idCarDoc)
        {
            CarDoc carDoc = getItem(idCarDoc);

            list.Remove(carDoc);

            carDoc.Delete();
        }

        public CarDoc getItem(int id)
        {
            var carDocs = from carDoc in list
                          where carDoc.IsEqualsID(id)
                          select carDoc;

            if (carDocs.Count() > 0)
                return carDocs.First() as CarDoc;
            else
                return new CarDoc(0);
        }

        public DataTable ToDataTableByCar(Car car)
        {
            DataTable dt = createTable();

            var carDocs = from carDoc in list
                          where carDoc.isEqualCarID(car)
                          select carDoc;

            foreach (CarDoc carDoc in carDocs)
                dt.Rows.Add(carDoc.getRow());

            return dt;
        }

        private DataTable createTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Название");
            dt.Columns.Add("Файл");

            return dt;
        }
    }
}
