using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class AccountList : MainList
    {
        private List<Account> list;
        private static AccountList uniqueInstance;

        private AccountList()
        {
            list = new List<Account>();

            loadFromSql();
        }

        public static AccountList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new AccountList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Account");

            foreach (DataRow row in dt.Rows)
            {
                Account account = new Account(row);
                Add(account);
            }
        }

        public void Add(Account account)
        {
            if (list.Exists(item => item == account))
                return;

            list.Add(account);
        }

        public void Delete(int idAccount)
        {
            Account account = getItem(idAccount);

            list.Remove(account);

            account.Delete();
        }

        public Account getItem(int id)
        {
            var accounts = list.Where(item => item.IsEqualsID(id));

            return (accounts.Count() > 0) ? accounts.First() : new Account();
        }

        public DataTable ToDataTable()
        {
            var accounts = list.OrderByDescending(item => item.Position);

            return CreateTable(accounts.ToList());
        }

        private DataTable CreateTable(List<Account> accounts)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("idCar");
            dt.Columns.Add("Номер счёта");
            dt.Columns.Add("Тип полиса");
            dt.Columns.Add("Собственник");
            dt.Columns.Add("Сумма", Type.GetType("System.Double"));
            dt.Columns.Add("Согласование");
            dt.Columns.Add("Файл");

            foreach (Account account in accounts)
                dt.Rows.Add(account.getRow());

            return dt;
        }

        internal bool Exists(string name)
        {
            return list.Exists(item => item.Number == name);
        }
    }
}
