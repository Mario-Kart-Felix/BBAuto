using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ClassLibraryBBAuto
{
    public class Template : MainDictionary
    {
        private string _file;

        public string Path
        {
            get { return _file; }
            set { _file = value; }
        }

        public Template()
        {
            _id = 0;
        }
        
        public Template(DataRow row)
        {
            fillFields(row);
        }

        private void fillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            name = row.ItemArray[1].ToString();
            _file = row.ItemArray[2].ToString();
            _fileBegin = _file;
        }

        public override void Save()
        {
            DeleteFile(_file);

            _file = WorkWithFiles.fileCopy(_file, "Template", name);

            _provider.Insert("Template", _id, name, _file);
        }
                
        internal override void Delete()
        {
            DeleteFile(_file);

            _provider.Delete("Template", _id);
        }

        internal override object[] getRow()
        {
            return new object[] { _id, name, _file };
        }
    }
}
