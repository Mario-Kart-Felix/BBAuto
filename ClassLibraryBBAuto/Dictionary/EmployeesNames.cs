using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class EmployeesNames : MyDictionary
    {
        private static EmployeesNames uniqueInstance;

        public static EmployeesNames getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new EmployeesNames();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = provider.Select("EmployeesName");

            fillList(dt);
        }
    }
}
