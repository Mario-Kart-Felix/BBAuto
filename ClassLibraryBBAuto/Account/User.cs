using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using ClassLibraryBBAuto;

namespace ClassLibraryBBAuto
{
    public static class User
    {
        private static Driver _driver;
      
        public static bool Login()
        {
            string login = WindowsIdentity.GetCurrent().Name.Replace("\\", "-");
            String[] s2 = login.Split('-');
            login = s2[1];
            //login = "milyanru";
            DriverList driverList = DriverList.getInstance();
            _driver = driverList.getItem(login);
            
            return ((_driver != null) && (User.GetRole() != RolesList.Employee));
        }

        public static Driver getDriver()
        {
            return _driver;
        }

        public static bool IsFullAccess()
        {
            return ((_driver.UserRole == RolesList.Adminstrator)
                || (_driver.UserRole == RolesList.Boss)
                || (_driver.UserRole == RolesList.Editor)
                || (_driver.UserRole == RolesList.proxyBoss));
        }

        public static RolesList GetRole()
        {
            return _driver.UserRole;
        }
    }
}