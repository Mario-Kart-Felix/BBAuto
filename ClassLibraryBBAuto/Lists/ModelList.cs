using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class ModelList : MainList
    {
        private static ModelList uniqueInstance;
        private List<Model> list;

        private ModelList()
        {
            list = new List<Model>();

            loadFromSql();
        }

        public static ModelList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new ModelList();

            return uniqueInstance;
        }
        
        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Model");

            clearList();

            foreach (DataRow row in dt.Rows)
            {
                Model model = new Model(row);
                Add(model);
            }
        }

        public void Add(Model model)
        {
            if (list.Exists(item => item == model))
                return;

            list.Add(model);
        }

        private void clearList()
        {
            if (list.Count > 0)
                list.Clear();
        }

        public Model getItem(int key)
        {
            var models = from model in list
                       where model.IsEqualsID(key)
                       select model;

            if (models.Count() > 0)
                return models.First() as Model;
            else
                return new Model(0);
        }

        public void Delete(int idModel)
        {
            Model model = getItem(idModel);

            list.Remove(model);

            model.Delete();
        }

        public DataTable ToDataTable(int idMark)
        {
            var models = from model in list
                         where model.isEqualMarkID(idMark)
                         orderby model.name
                         select model;

            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Название");

            foreach (Model model in models.ToList())
            {
                dt.Rows.Add(model.getRow());
            }

            return dt;
        }
    }
}
