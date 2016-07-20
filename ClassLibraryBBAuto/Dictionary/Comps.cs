using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class Comps : MyDictionary
    {
        private static Comps uniqueInstance;

        public static Comps getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new Comps();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = provider.Select("Comp");

            fillList(dt);
        }
    }
}
