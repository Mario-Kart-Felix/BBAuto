using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class ViolationList : MainList
    {
        private List<Violation> list;
        private static ViolationList uniqueInstance;

        private ViolationList()
        {
            list = new List<Violation>();

            loadFromSql();
        }

        public static ViolationList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new ViolationList();

            return uniqueInstance;
        }
        
        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Violation");

            foreach (DataRow row in dt.Rows)
            {
                Violation violation = new Violation(row);
                Add(violation);
            }
        }

        public void Add(Violation violation)
        {
            if (list.Exists(item => item == violation))
                return;

            list.Add(violation);
        }

        public Violation getItem(int id)
        {
            var violations = list.Where(item => item.IsEqualsID(id));

            return (violations.Count() > 0) ? violations.First() : null;
        }

        public Violation getItem(Driver driver)
        {
            if (driver.IsEqualsID(0))
                return new Violation(0);

            var violations = list.Where(item => item.isEqualDriverID(driver));

            return (violations.Count() > 0) ? violations.First() : new Violation(0);
        }

        public DataTable ToDataTable()
        {
            var violations = list.OrderByDescending(item => item.Date);

            return createTable(violations.ToList());
        }

        public DataTable ToDataTable(Car car)
        {
            var violations = from violation in list
                             where violation.isEqualCarID(car)
                             orderby violation.Date descending
                             select violation;

            return createTable(violations.ToList());
        }

        public object ToDataTable(Driver driver)
        {
            if (driver.IsEqualsID(0))
                return null;

            var violations = from violation in list
                             where violation.isEqualDriverID(driver)
                             orderby violation.Date descending
                             select violation;

            return createTable(violations.ToList());
        }

        private DataTable createTable(List<Violation> violations)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("idCar");
            dt.Columns.Add("Бортовой номер");
            dt.Columns.Add("Регистрационный знак");
            dt.Columns.Add("Регион");
            dt.Columns.Add("Дата", Type.GetType("System.DateTime"));
            dt.Columns.Add("Водитель");
            dt.Columns.Add("№ постановления");
            dt.Columns.Add("Дата оплаты", Type.GetType("System.DateTime"));
            dt.Columns.Add("Тип нарушения");
            dt.Columns.Add("Сумма штрафа", Type.GetType("System.Int32"));

            foreach (Violation violation in violations)
                dt.Rows.Add(violation.getRow());

            return dt;
        }

        public void Delete(int idViolation)
        {
            Violation violation = getItem(idViolation);

            list.Remove(violation);

            violation.Delete();
        }
    }
}
