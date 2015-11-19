using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer;

namespace ClassLibraryBBAuto
{
    public abstract class MainDictionary
    {
        protected int _id;
        protected string _fileBegin;
        protected static IProvider _provider;

        internal abstract object[] getRow();

        public virtual void Save() { }
        internal virtual void Delete() { }

        protected MainDictionary()
        {
            _provider = Provider.GetProvider();
        }

        public bool IsEqualsID(int id)
        {
            return _id == id;
        }

        protected void DeleteFile(string newFile)
        {
            if ((_fileBegin != string.Empty) && (_fileBegin != newFile))
                WorkWithFiles.Delete(_fileBegin);
        }
    }
}
