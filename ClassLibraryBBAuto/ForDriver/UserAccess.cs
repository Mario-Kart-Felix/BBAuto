using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataLayer;

namespace BBAuto.Domain
{
    public class UserAccess : MainDictionary
    {
        private int _idRole;

        public string IDDriver
        {
            get { return _id.ToString(); }
            set { int.TryParse(value, out _id); }
        }

        public string IDRole
        {
            get { return _idRole.ToString(); }
            set { int.TryParse(value, out _idRole); }
        }

        public Driver driver
        {
            get
            {
                DriverList driverList = DriverList.getInstance();
                Driver driver = driverList.getItem(_id);
                return driver;
            }
        }

        public string Role
        {
            get
            {
                if (_idRole == 0)
                    return "Нет доступа";

                Roles roles = Roles.getInstance();
                return roles.getItem(_idRole);
            }
        }

        public UserAccess()
        {
            _id = 0;
            _idRole = 0;
        }

        internal UserAccess(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            int.TryParse(row.ItemArray[1].ToString(), out _idRole);
        }

        internal override object[] getRow()
        {
            return new object[] { _id, driver.Login, driver.GetName(NameType.Full), Role };
        }

        internal override void Delete()
        {
            _provider.DoOther("exec UserAccess_Delete @p1, @p2", _id, _idRole);
        }

        public override void Save()
        {
            _provider.Insert("UserAccess", _id, _idRole);
        }
    }
}
