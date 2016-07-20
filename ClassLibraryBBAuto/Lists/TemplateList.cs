using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace BBAuto.Domain
{
    public class TemplateList : MainList
    {
        private static TemplateList uniqueInstance;

        private List<Template> list;

        private TemplateList()
        {
            list = new List<Template>();

            loadFromSql();
        }

        public static TemplateList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new TemplateList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Template");

            foreach (DataRow row in dt.Rows)
            {
                Template template = new Template(row);
                Add(template);
            }
        }

        public void Add(Template template)
        {
            if (list.Exists(item => item == template))
                return;

            list.Add(template);
        }

        public void Delete(int idTemplate)
        {
            Template template = getItem(idTemplate);

            list.Remove(template);

            template.Delete();
        }

        public Template getItem(int id)
        {
            var templates = list.Where(item => item.IsEqualsID(id));

            return (templates.Count() > 0) ? templates.First() : null;
        }

        public Template getItem(string name)
        {
            var templates = list.Where(item => item.Name == name);

            return (templates.Count() > 0) ? templates.First() : null;
        }
        
        public DataTable ToDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Название");
            dt.Columns.Add("Файл");

            foreach (var item in list)
            {
                dt.Rows.Add(item.getRow());
            }

            return dt;
        }
    }
}
