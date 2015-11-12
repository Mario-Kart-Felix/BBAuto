using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class MailText : MainDictionary, IDictionaryMVC
    {
        private string _text;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public MailText()
        {
            _id = 0;
            Name = string.Empty;
            Text = string.Empty;
        }

        public MailText(DataRow row)
        {
            FillFields(row);
        }

        private void FillFields(DataRow row)
        {
            int.TryParse(row.ItemArray[0].ToString(), out _id);
            Name = row.ItemArray[1].ToString();
            Text = row.ItemArray[2].ToString();
        }

        public override void Save()
        {
            int.TryParse(_provider.Insert("MailText", _id, Name, Text), out _id);

            MailTextList mailTextList = MailTextList.getInstance();
            mailTextList.Add(this);
        }

        internal override void Delete()
        {
            _provider.Delete("MailText", _id);
        }

        internal override object[] getRow()
        {
            return new object[2] { _id, name };
        }
    }
}
