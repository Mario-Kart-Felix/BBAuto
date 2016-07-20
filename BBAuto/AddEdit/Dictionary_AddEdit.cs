using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BBAuto.Domain;

namespace BBAuto
{
    public partial class Dictionary_AddEdit : Form, IViewDictionary
    {
        public event EventHandler<EventArgs> SaveClick;
        public event EventHandler<EventArgs> LoadData;

        public string InputText
        {
            get { return tbText.Text; }
        }
        public string InputName
        {
            get { return tbName.Text; }
        }

        public Dictionary_AddEdit(string Text)
        {
            InitializeComponent();

            this.Text = Text;
        }

        private void Diller_AddEdit_Load(object sender, EventArgs e)
        {
            if (LoadData != null)
                LoadData(sender, EventArgs.Empty);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveClick != null)
                SaveClick(sender, EventArgs.Empty);
        }

        public void SetName(string name)
        {
            tbName.Text = name;
        }

        public void SetText(string contacts)
        {
            tbText.Text = contacts;
        }
    }
}
