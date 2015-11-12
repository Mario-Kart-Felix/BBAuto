using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class Marks : MyDictionary
    {
        private static Marks uniqueInstance;

        public static Marks getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new Marks();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = provider.Select("Mark");

            fillList(dt);
        }
    }
}
