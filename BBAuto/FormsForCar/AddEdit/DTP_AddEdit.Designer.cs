namespace BBAuto
{
    partial class DTP_AddEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbRegion = new System.Windows.Forms.Label();
            this.cbRegion = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comm = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.facts = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.damage = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numberLoss = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.cbStatusAfterDTP = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cbCulprit = new System.Windows.Forms.ComboBox();
            this._dgvFile = new System.Windows.Forms.DataGridView();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.btnDelFile = new System.Windows.Forms.Button();
            this.mtpDateCallInsure = new System.Windows.Forms.MaskedTextBox();
            this.cbCurrentStatusAfterDTP = new System.Windows.Forms.ComboBox();
            this.llDriver = new System.Windows.Forms.LinkLabel();
            this.label28 = new System.Windows.Forms.Label();
            this.lbCarInfo = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._dgvFile)).BeginInit();
            this.SuspendLayout();
            // 
            // lbRegion
            // 
            this.lbRegion.AutoSize = true;
            this.lbRegion.Location = new System.Drawing.Point(14, 91);
            this.lbRegion.Name = "lbRegion";
            this.lbRegion.Size = new System.Drawing.Size(69, 13);
            this.lbRegion.TabIndex = 0;
            this.lbRegion.Text = "Место ДТП:";
            // 
            // cbRegion
            // 
            this.cbRegion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRegion.FormattingEnabled = true;
            this.cbRegion.Location = new System.Drawing.Point(112, 87);
            this.cbRegion.Name = "cbRegion";
            this.cbRegion.Size = new System.Drawing.Size(145, 21);
            this.cbRegion.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Дата:";
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(112, 58);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(145, 20);
            this.dtpDate.TabIndex = 0;
            this.dtpDate.ValueChanged += new System.EventHandler(this.date_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 145);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Виновник:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Дата обращения";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 366);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Текущее состояние:";
            // 
            // tbSum
            // 
            this.tbSum.Location = new System.Drawing.Point(404, 113);
            this.tbSum.MaxLength = 50;
            this.tbSum.Name = "tbSum";
            this.tbSum.Size = new System.Drawing.Size(129, 20);
            this.tbSum.TabIndex = 5;
            this.tbSum.TextChanged += new System.EventHandler(this.sum_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(268, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(135, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Сумма возмещения, руб:";
            // 
            // comm
            // 
            this.comm.Location = new System.Drawing.Point(18, 343);
            this.comm.MaxLength = 100;
            this.comm.Name = "comm";
            this.comm.Size = new System.Drawing.Size(519, 20);
            this.comm.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 327);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Примечание:";
            // 
            // facts
            // 
            this.facts.Location = new System.Drawing.Point(18, 269);
            this.facts.MaxLength = 500;
            this.facts.Multiline = true;
            this.facts.Name = "facts";
            this.facts.Size = new System.Drawing.Size(519, 55);
            this.facts.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Обстоятельства ДТП:";
            // 
            // damage
            // 
            this.damage.Location = new System.Drawing.Point(18, 200);
            this.damage.MaxLength = 300;
            this.damage.Multiline = true;
            this.damage.Name = "damage";
            this.damage.Size = new System.Drawing.Size(519, 50);
            this.damage.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 184);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Повреждения:";
            // 
            // numberLoss
            // 
            this.numberLoss.Location = new System.Drawing.Point(404, 87);
            this.numberLoss.MaxLength = 50;
            this.numberLoss.Name = "numberLoss";
            this.numberLoss.Size = new System.Drawing.Size(133, 20);
            this.numberLoss.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(268, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(115, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "№ убытка страховой:";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(779, 384);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 12;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(676, 384);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 23);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 123);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "в страховую:";
            // 
            // cbStatusAfterDTP
            // 
            this.cbStatusAfterDTP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatusAfterDTP.FormattingEnabled = true;
            this.cbStatusAfterDTP.Location = new System.Drawing.Point(404, 60);
            this.cbStatusAfterDTP.Name = "cbStatusAfterDTP";
            this.cbStatusAfterDTP.Size = new System.Drawing.Size(133, 21);
            this.cbStatusAfterDTP.TabIndex = 3;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(268, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(104, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "Статус после ДТП:";
            // 
            // cbCulprit
            // 
            this.cbCulprit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCulprit.FormattingEnabled = true;
            this.cbCulprit.Location = new System.Drawing.Point(18, 161);
            this.cbCulprit.Name = "cbCulprit";
            this.cbCulprit.Size = new System.Drawing.Size(195, 21);
            this.cbCulprit.TabIndex = 30;
            // 
            // _dgvFile
            // 
            this._dgvFile.AllowUserToAddRows = false;
            this._dgvFile.AllowUserToDeleteRows = false;
            this._dgvFile.AllowUserToResizeRows = false;
            this._dgvFile.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvFile.BackgroundColor = System.Drawing.SystemColors.Window;
            this._dgvFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvFile.Location = new System.Drawing.Point(552, 45);
            this._dgvFile.Name = "_dgvFile";
            this._dgvFile.ReadOnly = true;
            this._dgvFile.RowHeadersVisible = false;
            this._dgvFile.Size = new System.Drawing.Size(302, 331);
            this._dgvFile.TabIndex = 31;
            this._dgvFile.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._dgvFile_CellDoubleClick);
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(698, 17);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(75, 23);
            this.btnAddFile.TabIndex = 32;
            this.btnAddFile.Text = "Добавить";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(549, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(95, 13);
            this.label13.TabIndex = 33;
            this.label13.Text = "Материалы ДТП:";
            // 
            // btnDelFile
            // 
            this.btnDelFile.Location = new System.Drawing.Point(779, 16);
            this.btnDelFile.Name = "btnDelFile";
            this.btnDelFile.Size = new System.Drawing.Size(75, 23);
            this.btnDelFile.TabIndex = 34;
            this.btnDelFile.Text = "Удалить";
            this.btnDelFile.UseVisualStyleBackColor = true;
            this.btnDelFile.Click += new System.EventHandler(this.btnDelFile_Click);
            // 
            // mtpDateCallInsure
            // 
            this.mtpDateCallInsure.Location = new System.Drawing.Point(112, 114);
            this.mtpDateCallInsure.Mask = "00/00/0000";
            this.mtpDateCallInsure.Name = "mtpDateCallInsure";
            this.mtpDateCallInsure.Size = new System.Drawing.Size(145, 20);
            this.mtpDateCallInsure.TabIndex = 35;
            // 
            // cbCurrentStatusAfterDTP
            // 
            this.cbCurrentStatusAfterDTP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCurrentStatusAfterDTP.FormattingEnabled = true;
            this.cbCurrentStatusAfterDTP.Location = new System.Drawing.Point(18, 382);
            this.cbCurrentStatusAfterDTP.Name = "cbCurrentStatusAfterDTP";
            this.cbCurrentStatusAfterDTP.Size = new System.Drawing.Size(520, 21);
            this.cbCurrentStatusAfterDTP.TabIndex = 36;
            // 
            // llDriver
            // 
            this.llDriver.AutoSize = true;
            this.llDriver.Location = new System.Drawing.Point(109, 27);
            this.llDriver.Name = "llDriver";
            this.llDriver.Size = new System.Drawing.Size(55, 13);
            this.llDriver.TabIndex = 63;
            this.llDriver.TabStop = true;
            this.llDriver.Text = "linkLabel1";
            this.llDriver.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDriver_LinkClicked);
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(13, 27);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(58, 13);
            this.label28.TabIndex = 62;
            this.label28.Text = "Водитель:";
            // 
            // lbCarInfo
            // 
            this.lbCarInfo.AutoSize = true;
            this.lbCarInfo.Location = new System.Drawing.Point(109, 9);
            this.lbCarInfo.Name = "lbCarInfo";
            this.lbCarInfo.Size = new System.Drawing.Size(69, 13);
            this.lbCarInfo.TabIndex = 64;
            this.lbCarInfo.Text = "Автомобиль";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 9);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 13);
            this.label12.TabIndex = 65;
            this.label12.Text = "Автомобиль:";
            // 
            // DTP_AddEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(866, 419);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.lbCarInfo);
            this.Controls.Add(this.llDriver);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.cbCurrentStatusAfterDTP);
            this.Controls.Add(this.mtpDateCallInsure);
            this.Controls.Add(this.btnDelFile);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this._dgvFile);
            this.Controls.Add(this.cbCulprit);
            this.Controls.Add(this.cbStatusAfterDTP);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.numberLoss);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbSum);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.damage);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.facts);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comm);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbRegion);
            this.Controls.Add(this.lbRegion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DTP_AddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Карточка ДТП";
            this.Load += new System.EventHandler(this.aeDTP_Load);
            ((System.ComponentModel.ISupportInitialize)(this._dgvFile)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbRegion;
        private System.Windows.Forms.ComboBox cbRegion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox comm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox facts;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox damage;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox numberLoss;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbStatusAfterDTP;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbCulprit;
        private System.Windows.Forms.DataGridView _dgvFile;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnDelFile;
        private System.Windows.Forms.MaskedTextBox mtpDateCallInsure;
        private System.Windows.Forms.ComboBox cbCurrentStatusAfterDTP;
        private System.Windows.Forms.LinkLabel llDriver;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label lbCarInfo;
        private System.Windows.Forms.Label label12;
    }
}