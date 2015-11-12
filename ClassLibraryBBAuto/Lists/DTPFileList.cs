using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class DTPFileList : MainList
    {
        private List<DTPFile> list;
        private static DTPFileList uniqueInstance;

        private DTPFileList()
        {
            list = new List<DTPFile>();

            loadFromSql();
        }

        public static DTPFileList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new DTPFileList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("DTPFile");

            foreach (DataRow row in dt.Rows)
            {
                DTPFile dtpFile = new DTPFile(row);
                Add(dtpFile);
            }
        }
        
        public void Add(DTPFile dtpFile)
        {
            if (list.Exists(item => item == dtpFile))
                return;

            list.Add(dtpFile);
        }

        public DTPFile getItem(int id)
        {
            var dtpFiles = from dtpFile in list
                           where dtpFile.IsEqualsID(id)
                           select dtpFile;

            if (dtpFiles.Count() > 0)
                return dtpFiles.First() as DTPFile;
            else
                return null;
        }

        public void Delete(int idDTPFile)
        {
            DTPFile dtpFile = getItem(idDTPFile);

            list.Remove(dtpFile);

            dtpFile.Delete();
        }

        public DataTable ToDataTable(DTP dtp)
        {
            var dtpFiles = from dtpFile in list
                           where dtpFile.isEqualDtpID(dtp)
                           select dtpFile;

            return createTable(dtpFiles.ToList());
        }

        private DataTable createTable(List<DTPFile> dtpFiles)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("Название файла");
            dt.Columns.Add("Просмотр скана");

            foreach (DTPFile dtpFile in dtpFiles)
                dt.Rows.Add(dtpFile.getRow());

            return dt;
        }
    }
}
