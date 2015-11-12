namespace BBAuto
{
    partial class Account_AddEdit
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbOwner = new System.Windows.Forms.ComboBox();
            this.cbPolicyType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDelPolicy = new System.Windows.Forms.Button();
            this.btnAddPolicy = new System.Windows.Forms.Button();
            this.dgvPolicy = new System.Windows.Forms.DataGridView();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tbSum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPayment = new System.Windows.Forms.ComboBox();
            this.lbPayment = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.chbAgreed = new System.Windows.Forms.CheckBox();
            this.chbBusinessTrip = new System.Windows.Forms.CheckBox();
            this.ucFile = new ClassLibraryBBAuto.FileOpenTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPolicy)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Счёт №:";
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(64, 10);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(141, 20);
            this.tbNumber.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Собственник:";
            // 
            // cbOwner
            // 
            this.cbOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOwner.FormattingEnabled = true;
            this.cbOwner.Location = new System.Drawing.Point(14, 58);
            this.cbOwner.Name = "cbOwner";
            this.cbOwner.Size = new System.Drawing.Size(150, 21);
            this.cbOwner.TabIndex = 3;
            this.cbOwner.SelectedIndexChanged += new System.EventHandler(this.cbOwner_SelectedIndexChanged);
            // 
            // cbPolicyType
            // 
            this.cbPolicyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPolicyType.FormattingEnabled = true;
            this.cbPolicyType.Location = new System.Drawing.Point(14, 98);
            this.cbPolicyType.Name = "cbPolicyType";
            this.cbPolicyType.Size = new System.Drawing.Size(150, 21);
            this.cbPolicyType.TabIndex = 5;
            this.cbPolicyType.SelectedIndexChanged += new System.EventHandler(this.cbPolicyType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Тип полиса:";
            // 
            // btnDelPolicy
            // 
            this.btnDelPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelPolicy.Location = new System.Drawing.Point(506, 9);
            this.btnDelPolicy.Name = "btnDelPolicy";
            this.btnDelPolicy.Size = new System.Drawing.Size(75, 23);
            this.btnDelPolicy.TabIndex = 38;
            this.btnDelPolicy.Text = "Удалить";
            this.btnDelPolicy.UseVisualStyleBackColor = true;
            this.btnDelPolicy.Click += new System.EventHandler(this.btnDelPolicy_Click);
            // 
            // btnAddPolicy
            // 
            this.btnAddPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPolicy.Location = new System.Drawing.Point(425, 10);
            this.btnAddPolicy.Name = "btnAddPolicy";
            this.btnAddPolicy.Size = new System.Drawing.Size(75, 23);
            this.btnAddPolicy.TabIndex = 36;
            this.btnAddPolicy.Text = "Добавить";
            this.btnAddPolicy.UseVisualStyleBackColor = true;
            this.btnAddPolicy.Click += new System.EventHandler(this.btnAddPolicy_Click);
            // 
            // dgvPolicy
            // 
            this.dgvPolicy.AllowUserToAddRows = false;
            this.dgvPolicy.AllowUserToDeleteRows = false;
            this.dgvPolicy.AllowUserToResizeRows = false;
            this.dgvPolicy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvPolicy.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPolicy.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPolicy.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPolicy.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPolicy.Location = new System.Drawing.Point(182, 38);
            this.dgvPolicy.Name = "dgvPolicy";
            this.dgvPolicy.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPolicy.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvPolicy.RowHeadersVisible = false;
            this.dgvPolicy.Size = new System.Drawing.Size(399, 274);
            this.dgvPolicy.TabIndex = 35;
            this.dgvPolicy.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPolicy_CellDoubleClick);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(405, 344);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 23);
            this.btnSave.TabIndex = 39;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(506, 344);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 40;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // tbSum
            // 
            this.tbSum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSum.Location = new System.Drawing.Point(481, 318);
            this.tbSum.Name = "tbSum";
            this.tbSum.ReadOnly = true;
            this.tbSum.Size = new System.Drawing.Size(100, 20);
            this.tbSum.TabIndex = 42;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(434, 321);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 41;
            this.label4.Text = "Сумма";
            // 
            // cbPayment
            // 
            this.cbPayment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPayment.FormattingEnabled = true;
            this.cbPayment.Items.AddRange(new object[] {
            "Первый",
            "Второй"});
            this.cbPayment.Location = new System.Drawing.Point(14, 141);
            this.cbPayment.Name = "cbPayment";
            this.cbPayment.Size = new System.Drawing.Size(150, 21);
            this.cbPayment.TabIndex = 44;
            this.cbPayment.Visible = false;
            // 
            // lbPayment
            // 
            this.lbPayment.AutoSize = true;
            this.lbPayment.Location = new System.Drawing.Point(11, 125);
            this.lbPayment.Name = "lbPayment";
            this.lbPayment.Size = new System.Drawing.Size(49, 13);
            this.lbPayment.TabIndex = 43;
            this.lbPayment.Text = "Платёж:";
            this.lbPayment.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 325);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 13);
            this.label5.TabIndex = 46;
            this.label5.Text = "Скан копии счёта:";
            // 
            // chbAgreed
            // 
            this.chbAgreed.AutoSize = true;
            this.chbAgreed.Enabled = false;
            this.chbAgreed.Location = new System.Drawing.Point(211, 12);
            this.chbAgreed.Name = "chbAgreed";
            this.chbAgreed.Size = new System.Drawing.Size(86, 17);
            this.chbAgreed.TabIndex = 47;
            this.chbAgreed.Text = "Согласован";
            this.chbAgreed.UseVisualStyleBackColor = true;
            // 
            // chbBusinessTrip
            // 
            this.chbBusinessTrip.AutoSize = true;
            this.chbBusinessTrip.Location = new System.Drawing.Point(14, 145);
            this.chbBusinessTrip.Name = "chbBusinessTrip";
            this.chbBusinessTrip.Size = new System.Drawing.Size(127, 17);
            this.chbBusinessTrip.TabIndex = 48;
            this.chbBusinessTrip.Text = "Служебная поездка";
            this.chbBusinessTrip.UseVisualStyleBackColor = true;
            this.chbBusinessTrip.Visible = false;
            // 
            // ucFile
            // 
            this.ucFile.Location = new System.Drawing.Point(117, 323);
            this.ucFile.Name = "ucFile";
            this.ucFile.Size = new System.Drawing.Size(239, 23);
            this.ucFile.TabIndex = 45;
            // 
            // Account_AddEdit
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(593, 379);
            this.Controls.Add(this.chbBusinessTrip);
            this.Controls.Add(this.chbAgreed);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ucFile);
            this.Controls.Add(this.cbPayment);
            this.Controls.Add(this.lbPayment);
            this.Controls.Add(this.tbSum);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDelPolicy);
            this.Controls.Add(this.btnAddPolicy);
            this.Controls.Add(this.dgvPolicy);
            this.Controls.Add(this.cbPolicyType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbOwner);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Account_AddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Счёт";
            this.Load += new System.EventHandler(this.aeAccount_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPolicy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbOwner;
        private System.Windows.Forms.ComboBox cbPolicyType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDelPolicy;
        private System.Windows.Forms.Button btnAddPolicy;
        private System.Windows.Forms.DataGridView dgvPolicy;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox tbSum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbPayment;
        private System.Windows.Forms.Label lbPayment;
        private ClassLibraryBBAuto.FileOpenTextBox ucFile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox chbAgreed;
        private System.Windows.Forms.CheckBox chbBusinessTrip;
    }
}