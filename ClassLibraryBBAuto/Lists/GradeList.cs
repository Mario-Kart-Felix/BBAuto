using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class GradeList : MainList
    {
        private static GradeList _uniqueInstance;
        private List<Grade> _list;

        private GradeList()
        {
            _list = new List<Grade>();

            loadFromSql();
        }

        public static GradeList getInstance()
        {
            if (_uniqueInstance == null)
                _uniqueInstance = new GradeList();

            return _uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Grade");

            _list.Clear();

            foreach (DataRow row in dt.Rows)
            {
                Grade grade = new Grade(row);
                Add(grade);
            }
        }

        public void Add(Grade grade)
        {
            if (_list.Exists(item => item == grade))
                return;

            _list.Add(grade);
        }

        public Grade getItem(int key)
        {
            var items = _list.Where(item => item.IsEqualsID(key));

            return (items.Count() > 0) ? items.First() : null;
        }

        public void Delete(int idGrade)
        {
            Grade grade = getItem(idGrade);

            _list.Remove(grade);

            grade.Delete();
        }

        public DataTable ToDataTable(int idModel)
        {
            var grades = from grade in _list
                         where grade.isEqualModelID(idModel)
                         orderby grade.Name
                         select grade;

            return createTable(grades.ToList());
        }

        private DataTable createTable(List<Grade> grades)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Название");

            foreach (Grade grade in grades.ToList())
                dt.Rows.Add(grade.getRow());

            return dt;
        }
    }
}
