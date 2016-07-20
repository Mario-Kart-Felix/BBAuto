using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class UserAccessList : MainList
    {
        private List<UserAccess> list;
        private static UserAccessList uniqueInstance;

        private UserAccessList()
        {
            list = new List<UserAccess>();

            loadFromSql();
        }

        public static UserAccessList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new UserAccessList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("UserAccess");

            foreach (DataRow row in dt.Rows)
            {
                UserAccess userAccess = new UserAccess(row);
                Add(userAccess);
            }
        }

        public void Add(UserAccess userAccess)
        {
            if (list.Exists(item => item == userAccess))
                return;

            list.Add(userAccess);
        }

        public void Delete(int idDriver)
        {
            UserAccess userAccess = getItem(idDriver);

            list.Remove(userAccess);

            userAccess.Delete();
        }

        public UserAccess getItem(int idDriver)
        {
            List<UserAccess> userAccesses = list.Where(item => item.IsEqualsID(idDriver)).ToList();

            return (userAccesses.Count() > 0) ? userAccesses.First() : new UserAccess();
        }

        public UserAccess getItem(RolesList role)
        {
            List<UserAccess> userAccesses = ToList(role);

            return (userAccesses != null) ? userAccesses.First() : new UserAccess();
        }

        public DataTable ToDataTable()
        {
            DataTable dt = createTable();

            List<UserAccess> userAccesses = list.OrderBy(item => item.driver.GetName(NameType.Full)).ToList();

            foreach (UserAccess userAccess in userAccesses)
                dt.Rows.Add(userAccess.getRow());

            return dt;
        }

        private DataTable createTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("idDriver");
            dt.Columns.Add("login");
            dt.Columns.Add("ФИО");
            dt.Columns.Add("Роль");

            return dt;
        }

        public List<UserAccess> ToList(RolesList role)
        {
            int idRole = (int)role;

            List<UserAccess> userAccesses = list.Where(item => item.IDRole == idRole.ToString()).ToList();

            return (userAccesses.Count() > 0) ? userAccesses : null;
        }
    }
}
