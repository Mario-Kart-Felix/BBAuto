using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class EngineTypeList : MainList
    {
        private List<EngineType> list;
        private static EngineTypeList uniqueInstance;

        private EngineTypeList()
        {
            list = new List<EngineType>();

            loadFromSql();
        }

        public static EngineTypeList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new EngineTypeList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("EngineType");

            foreach (DataRow row in dt.Rows)
            {
                EngineType engineType = new EngineType(row);
                Add(engineType);
            }
        }

        public void Add(EngineType engineType)
        {
            if (list.Exists(item => item == engineType))
                return;

            list.Add(engineType);
        }

        public EngineType getItem(int id)
        {
            var engineTypes = list.Where(item => item.IsEqualsID(id));

            return (engineTypes.Count() > 0) ? engineTypes.First() : null;
        }
    }
}
