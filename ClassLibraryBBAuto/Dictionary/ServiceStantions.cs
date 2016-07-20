using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class ServiceStantions : MyDictionary
    {
        private static ServiceStantions uniqueInstance;

        public static ServiceStantions getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new ServiceStantions();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = provider.Select("ServiceStantion");

            fillList(dt);
        }
    }
}
