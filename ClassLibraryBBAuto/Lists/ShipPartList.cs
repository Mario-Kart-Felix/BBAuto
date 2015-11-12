using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class ShipPartList : MainList
    {
        private static ShipPartList uniqueInstance;
        private List<ShipPart> list;

        private ShipPartList()
        {
            list = new List<ShipPart>();

            loadFromSql();
        }

        public static ShipPartList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new ShipPartList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("ShipPart");

            foreach (DataRow row in dt.Rows)
            {
                ShipPart shipPart = new ShipPart(row);
                Add(shipPart);
            }
        }

        public void Add(ShipPart shipPart)
        {
            if (list.Exists(item => item == shipPart))
                return;

            list.Add(shipPart);
        }

        public ShipPart getItem(int idShipPart)
        {
            var shipParts = list.Where(item => item.IsEqualsID(idShipPart));

            return (shipParts.Count() > 0) ? shipParts.First() : new ShipPart(0);
        }

        public void Delete(int idShipPart)
        {
            ShipPart shipPart = getItem(idShipPart);

            list.Remove(shipPart);

            shipPart.Delete();
        }

        public DataTable ToDataTable()
        {
            return createTable(list);
        }

        public DataTable ToDataTable(Car car)
        {
            var shipParts = list.Where(item => item.isEqualCarID(car));

            return createTable(shipParts.ToList());
        }

        private DataTable createTable(List<ShipPart> shipParts)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("");
            dt.Columns.Add("Бортовой номер");
            dt.Columns.Add("Регистрационный знак");
            dt.Columns.Add("Водитель");
            dt.Columns.Add("Номер заказа");
            dt.Columns.Add("Дата заказа", Type.GetType("System.DateTime"));
            dt.Columns.Add("Дата отправки", Type.GetType("System.DateTime"));

            foreach (ShipPart shipPart in shipParts)
                dt.Rows.Add(shipPart.getRow());

            return dt;
        }
    }
}
