﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClassLibraryBBAuto;

namespace BBAuto
{
    public partial class TemplateAddEdit : Form
    {
        Template template;

        public TemplateAddEdit(Template template)
        {
            InitializeComponent();

            this.template = template;
        }

        private void TemplateAddEdit_Load(object sender, EventArgs e)
        {
            tbName.Text = template.name;
            tbPath.Text = template.Path;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            template.name = tbName.Text;
            template.Path = tbPath.Text;

            template.Save();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            tbPath.Text = getFilePath();
        }

        private string getFilePath()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.ShowDialog();

            return ofd.FileName;
        }
    }
}
