namespace BBAuto
{
    partial class FormWayBillDaily
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
            this.dgv = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lbCar = new System.Windows.Forms.Label();
            this.btnLoadWayBillCurrent = new System.Windows.Forms.Button();
            this.btnOpenInExcelAllFields = new System.Windows.Forms.Button();
            this.btnOpenInExcelSomeFields = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnPrintSomeFieldsAll = new System.Windows.Forms.Button();
            this.btnPrintAllFieldsAll = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnPrintSomeFieldsCurrent = new System.Windows.Forms.Button();
            this.btnPrintAllFieldsCurrent = new System.Windows.Forms.Button();
            this.lbCars = new System.Windows.Forms.ListBox();
            this.btnCreateWayBill = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvFuel = new System.Windows.Forms.DataGridView();
            this.btnAddFuel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFuel)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.AllowUserToResizeRows = false;
            this.dgv.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgv.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(253, 47);
            this.dgv.Name = "dgv";
            this.dgv.ReadOnly = true;
            this.dgv.RowHeadersVisible = false;
            this.dgv.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgv.Size = new System.Drawing.Size(286, 316);
            this.dgv.TabIndex = 40;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Дата:";
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "MMMM yyyy";
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(54, 7);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(147, 20);
            this.dtpDate.TabIndex = 41;
            this.dtpDate.ValueChanged += new System.EventHandler(this.dtpDate_ValueChanged);
            // 
            // lbCar
            // 
            this.lbCar.AutoSize = true;
            this.lbCar.Location = new System.Drawing.Point(250, 9);
            this.lbCar.Name = "lbCar";
            this.lbCar.Size = new System.Drawing.Size(68, 13);
            this.lbCar.TabIndex = 43;
            this.lbCar.Text = "автомобиль";
            // 
            // btnLoadWayBillCurrent
            // 
            this.btnLoadWayBillCurrent.Location = new System.Drawing.Point(37, 21);
            this.btnLoadWayBillCurrent.Name = "btnLoadWayBillCurrent";
            this.btnLoadWayBillCurrent.Size = new System.Drawing.Size(116, 23);
            this.btnLoadWayBillCurrent.TabIndex = 44;
            this.btnLoadWayBillCurrent.Text = "Выбранный авто";
            this.btnLoadWayBillCurrent.UseVisualStyleBackColor = true;
            this.btnLoadWayBillCurrent.Click += new System.EventHandler(this.btnLoadWayBillCurrent_Click);
            // 
            // btnOpenInExcelAllFields
            // 
            this.btnOpenInExcelAllFields.Location = new System.Drawing.Point(6, 21);
            this.btnOpenInExcelAllFields.Name = "btnOpenInExcelAllFields";
            this.btnOpenInExcelAllFields.Size = new System.Drawing.Size(125, 23);
            this.btnOpenInExcelAllFields.TabIndex = 45;
            this.btnOpenInExcelAllFields.Text = "Заполнить все поля";
            this.btnOpenInExcelAllFields.UseVisualStyleBackColor = true;
            this.btnOpenInExcelAllFields.Click += new System.EventHandler(this.btnOpenInExcelAllFields_Click);
            // 
            // btnOpenInExcelSomeFields
            // 
            this.btnOpenInExcelSomeFields.Location = new System.Drawing.Point(8, 50);
            this.btnOpenInExcelSomeFields.Name = "btnOpenInExcelSomeFields";
            this.btnOpenInExcelSomeFields.Size = new System.Drawing.Size(123, 23);
            this.btnOpenInExcelSomeFields.TabIndex = 46;
            this.btnOpenInExcelSomeFields.Text = "Заполнить частично";
            this.btnOpenInExcelSomeFields.UseVisualStyleBackColor = true;
            this.btnOpenInExcelSomeFields.Click += new System.EventHandler(this.btnOpenInExcelSomeFields_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOpenInExcelAllFields);
            this.groupBox1.Controls.Add(this.btnOpenInExcelSomeFields);
            this.groupBox1.Location = new System.Drawing.Point(847, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 94);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Открыть в Excel";
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.Enabled = false;
            this.btnNext.Location = new System.Drawing.Point(931, 371);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(107, 23);
            this.btnNext.TabIndex = 48;
            this.btnNext.Text = "Следующий авто";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrev.Enabled = false;
            this.btnPrev.Location = new System.Drawing.Point(811, 371);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(114, 23);
            this.btnPrev.TabIndex = 49;
            this.btnPrev.Text = "Предыдущий авто";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Location = new System.Drawing.Point(847, 149);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(170, 212);
            this.groupBox2.TabIndex = 48;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Печать";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnPrintSomeFieldsAll);
            this.groupBox5.Controls.Add(this.btnPrintAllFieldsAll);
            this.groupBox5.Location = new System.Drawing.Point(8, 111);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(149, 86);
            this.groupBox5.TabIndex = 48;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Все авто";
            // 
            // btnPrintSomeFieldsAll
            // 
            this.btnPrintSomeFieldsAll.Location = new System.Drawing.Point(13, 53);
            this.btnPrintSomeFieldsAll.Name = "btnPrintSomeFieldsAll";
            this.btnPrintSomeFieldsAll.Size = new System.Drawing.Size(123, 23);
            this.btnPrintSomeFieldsAll.TabIndex = 46;
            this.btnPrintSomeFieldsAll.Text = "Заполнить частично";
            this.btnPrintSomeFieldsAll.UseVisualStyleBackColor = true;
            this.btnPrintSomeFieldsAll.Click += new System.EventHandler(this.btnPrintSomeFieldsAll_Click);
            // 
            // btnPrintAllFieldsAll
            // 
            this.btnPrintAllFieldsAll.Location = new System.Drawing.Point(13, 24);
            this.btnPrintAllFieldsAll.Name = "btnPrintAllFieldsAll";
            this.btnPrintAllFieldsAll.Size = new System.Drawing.Size(125, 23);
            this.btnPrintAllFieldsAll.TabIndex = 45;
            this.btnPrintAllFieldsAll.Text = "Заполнить все поля";
            this.btnPrintAllFieldsAll.UseVisualStyleBackColor = true;
            this.btnPrintAllFieldsAll.Click += new System.EventHandler(this.btnPrintAllFieldsAll_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnPrintSomeFieldsCurrent);
            this.groupBox4.Controls.Add(this.btnPrintAllFieldsCurrent);
            this.groupBox4.Location = new System.Drawing.Point(8, 19);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(149, 86);
            this.groupBox4.TabIndex = 47;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Выбранный авто";
            // 
            // btnPrintSomeFieldsCurrent
            // 
            this.btnPrintSomeFieldsCurrent.Location = new System.Drawing.Point(13, 53);
            this.btnPrintSomeFieldsCurrent.Name = "btnPrintSomeFieldsCurrent";
            this.btnPrintSomeFieldsCurrent.Size = new System.Drawing.Size(123, 23);
            this.btnPrintSomeFieldsCurrent.TabIndex = 46;
            this.btnPrintSomeFieldsCurrent.Text = "Заполнить частично";
            this.btnPrintSomeFieldsCurrent.UseVisualStyleBackColor = true;
            this.btnPrintSomeFieldsCurrent.Click += new System.EventHandler(this.btnPrintSomeFieldsCurrent_Click);
            // 
            // btnPrintAllFieldsCurrent
            // 
            this.btnPrintAllFieldsCurrent.Location = new System.Drawing.Point(13, 24);
            this.btnPrintAllFieldsCurrent.Name = "btnPrintAllFieldsCurrent";
            this.btnPrintAllFieldsCurrent.Size = new System.Drawing.Size(125, 23);
            this.btnPrintAllFieldsCurrent.TabIndex = 45;
            this.btnPrintAllFieldsCurrent.Text = "Заполнить все поля";
            this.btnPrintAllFieldsCurrent.UseVisualStyleBackColor = true;
            this.btnPrintAllFieldsCurrent.Click += new System.EventHandler(this.btnPrintAllFieldsCurrent_Click);
            // 
            // lbCars
            // 
            this.lbCars.FormattingEnabled = true;
            this.lbCars.Location = new System.Drawing.Point(12, 165);
            this.lbCars.Name = "lbCars";
            this.lbCars.Size = new System.Drawing.Size(222, 199);
            this.lbCars.TabIndex = 50;
            // 
            // btnCreateWayBill
            // 
            this.btnCreateWayBill.Location = new System.Drawing.Point(37, 50);
            this.btnCreateWayBill.Name = "btnCreateWayBill";
            this.btnCreateWayBill.Size = new System.Drawing.Size(116, 23);
            this.btnCreateWayBill.TabIndex = 51;
            this.btnCreateWayBill.Text = "Все авто";
            this.btnCreateWayBill.UseVisualStyleBackColor = true;
            this.btnCreateWayBill.Click += new System.EventHandler(this.btnCreateWayBill_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(250, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 52;
            this.label1.Text = "Путевые листы:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnLoadWayBillCurrent);
            this.groupBox3.Controls.Add(this.btnCreateWayBill);
            this.groupBox3.Location = new System.Drawing.Point(12, 33);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 86);
            this.groupBox3.TabIndex = 53;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Загрузка путевых листов";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 13);
            this.label3.TabIndex = 54;
            this.label3.Text = "Список автомобилей:";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(464, 21);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 55;
            this.btnClear.Text = "Очистить";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(545, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 57;
            this.label4.Text = "Заправки:";
            // 
            // dgvFuel
            // 
            this.dgvFuel.AllowUserToAddRows = false;
            this.dgvFuel.AllowUserToDeleteRows = false;
            this.dgvFuel.AllowUserToResizeRows = false;
            this.dgvFuel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvFuel.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvFuel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFuel.Location = new System.Drawing.Point(548, 47);
            this.dgvFuel.Name = "dgvFuel";
            this.dgvFuel.ReadOnly = true;
            this.dgvFuel.RowHeadersVisible = false;
            this.dgvFuel.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvFuel.Size = new System.Drawing.Size(286, 316);
            this.dgvFuel.TabIndex = 56;
            // 
            // btnAddFuel
            // 
            this.btnAddFuel.Location = new System.Drawing.Point(759, 18);
            this.btnAddFuel.Name = "btnAddFuel";
            this.btnAddFuel.Size = new System.Drawing.Size(75, 23);
            this.btnAddFuel.TabIndex = 58;
            this.btnAddFuel.Text = "Добавить";
            this.btnAddFuel.UseVisualStyleBackColor = true;
            // 
            // FormWayBillDaily
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 402);
            this.Controls.Add(this.btnAddFuel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgvFuel);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbCars);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbCar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.dgv);
            this.MinimumSize = new System.Drawing.Size(796, 440);
            this.Name = "FormWayBillDaily";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Путевые листы на каждый день";
            this.Load += new System.EventHandler(this.FormWayBillDaily_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFuel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lbCar;
        private System.Windows.Forms.Button btnLoadWayBillCurrent;
        private System.Windows.Forms.Button btnOpenInExcelAllFields;
        private System.Windows.Forms.Button btnOpenInExcelSomeFields;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnPrintAllFieldsCurrent;
        private System.Windows.Forms.Button btnPrintSomeFieldsCurrent;
        private System.Windows.Forms.ListBox lbCars;
        private System.Windows.Forms.Button btnCreateWayBill;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnPrintSomeFieldsAll;
        private System.Windows.Forms.Button btnPrintAllFieldsAll;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvFuel;
        private System.Windows.Forms.Button btnAddFuel;
    }
}