namespace BBAuto.App.FormsForDriver
{
    partial class AddNewDriver
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
            this.gbComp = new System.Windows.Forms.GroupBox();
            this.rbOther = new System.Windows.Forms.RadioButton();
            this.rbGematek = new System.Windows.Forms.RadioButton();
            this.rbBraun = new System.Windows.Forms.RadioButton();
            this.chbEmployeeIn1C = new System.Windows.Forms.CheckBox();
            this.cbFio = new System.Windows.Forms.ComboBox();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gbComp.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbComp
            // 
            this.gbComp.Controls.Add(this.rbOther);
            this.gbComp.Controls.Add(this.rbGematek);
            this.gbComp.Controls.Add(this.rbBraun);
            this.gbComp.Location = new System.Drawing.Point(12, 12);
            this.gbComp.Name = "gbComp";
            this.gbComp.Size = new System.Drawing.Size(192, 94);
            this.gbComp.TabIndex = 37;
            this.gbComp.TabStop = false;
            this.gbComp.Text = "Компания";
            // 
            // rbOther
            // 
            this.rbOther.AutoSize = true;
            this.rbOther.Location = new System.Drawing.Point(16, 65);
            this.rbOther.Name = "rbOther";
            this.rbOther.Size = new System.Drawing.Size(62, 17);
            this.rbOther.TabIndex = 2;
            this.rbOther.TabStop = true;
            this.rbOther.Text = "Прочее";
            this.rbOther.UseVisualStyleBackColor = true;
            this.rbOther.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // rbGematek
            // 
            this.rbGematek.AutoSize = true;
            this.rbGematek.Location = new System.Drawing.Point(16, 42);
            this.rbGematek.Name = "rbGematek";
            this.rbGematek.Size = new System.Drawing.Size(105, 17);
            this.rbGematek.TabIndex = 1;
            this.rbGematek.TabStop = true;
            this.rbGematek.Text = "ООО \"Гематек\"";
            this.rbGematek.UseVisualStyleBackColor = true;
            this.rbGematek.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // rbBraun
            // 
            this.rbBraun.AutoSize = true;
            this.rbBraun.Location = new System.Drawing.Point(16, 19);
            this.rbBraun.Name = "rbBraun";
            this.rbBraun.Size = new System.Drawing.Size(153, 17);
            this.rbBraun.TabIndex = 0;
            this.rbBraun.TabStop = true;
            this.rbBraun.Text = "ООО \"Б. Браун Медикал\"";
            this.rbBraun.UseVisualStyleBackColor = true;
            this.rbBraun.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // chbEmployeeIn1C
            // 
            this.chbEmployeeIn1C.AutoSize = true;
            this.chbEmployeeIn1C.Enabled = false;
            this.chbEmployeeIn1C.Location = new System.Drawing.Point(12, 112);
            this.chbEmployeeIn1C.Name = "chbEmployeeIn1C";
            this.chbEmployeeIn1C.Size = new System.Drawing.Size(191, 17);
            this.chbEmployeeIn1C.TabIndex = 39;
            this.chbEmployeeIn1C.Text = "Сотрудник пока не заведён в 1С";
            this.chbEmployeeIn1C.UseVisualStyleBackColor = true;
            this.chbEmployeeIn1C.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // cbFio
            // 
            this.cbFio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFio.Enabled = false;
            this.cbFio.FormattingEnabled = true;
            this.cbFio.Location = new System.Drawing.Point(12, 157);
            this.cbFio.Name = "cbFio";
            this.cbFio.Size = new System.Drawing.Size(279, 21);
            this.cbFio.TabIndex = 38;
            // 
            // btnNext
            // 
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnNext.Location = new System.Drawing.Point(163, 194);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 40;
            this.btnNext.Text = "Далее";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(244, 194);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 41;
            this.btnClose.Text = "Отмена";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 141);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 42;
            this.label1.Text = "Сотрудник:";
            // 
            // AddNewDriver
            // 
            this.AcceptButton = this.btnNext;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(331, 229);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.chbEmployeeIn1C);
            this.Controls.Add(this.cbFio);
            this.Controls.Add(this.gbComp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddNewDriver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Добавление нового водителя";
            this.Load += new System.EventHandler(this.AddNewDriver_Load);
            this.gbComp.ResumeLayout(false);
            this.gbComp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbComp;
        private System.Windows.Forms.RadioButton rbOther;
        private System.Windows.Forms.RadioButton rbGematek;
        private System.Windows.Forms.RadioButton rbBraun;
        private System.Windows.Forms.CheckBox chbEmployeeIn1C;
        private System.Windows.Forms.ComboBox cbFio;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label label1;
    }
}
