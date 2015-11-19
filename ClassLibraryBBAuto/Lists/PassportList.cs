using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class PassportList : MainList
    {
        private List<Passport> list;
        private static PassportList uniqueInstance;

        private PassportList()
        {
            list = new List<Passport>();

            loadFromSql();
        }

        public static PassportList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new PassportList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Passport");

            foreach (DataRow row in dt.Rows)
            {
                Passport passport = new Passport(row);
                Add(passport);
            }
        }

        public void Add(Passport passport)
        {
            if (list.Exists(item => item == passport))
                return;

            list.Add(passport);
        }

        public void Delete(int idPassport)
        {
            Passport passport = getPassport(idPassport);

            list.Remove(passport);

            passport.Delete();
        }

        public Passport getPassport(int idPassport)
        {
            var passports = from passport in list
                            where passport.IsEqualsID(idPassport)
                            select passport;

            return (passports.Count() > 0) ? passports.First() as Passport : null;
        }

        public DataTable ToDataTable(Driver driver)
        {
            var passports = from passport in list
                            where passport.isEqualDriverID(driver)
                            orderby passport.GiveDate descending
                            select passport;

            DataTable dt = createTable();

            foreach (Passport passport in passports)
                dt.Rows.Add(passport.getRow());

            return dt;
        }

        private DataTable createTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Номер");
            dt.Columns.Add("Дата выдачи");

            return dt;
        }

        public Passport getLastPassport(Driver driver)
        {
            var passports = list.Where(item => item.isEqualDriverID(driver)).OrderByDescending(item => item.GiveDate).ToList();

            return (passports.Count() > 0) ? passports.First() : new Passport(0);
        }

        public Passport GetPassport(Driver driver, string number)
        {
            var newList = list.Where(item => item.Number == number).ToList();

            return (newList.Count == 0) ? driver.createPassport() : newList.First();
        }
    }
}
