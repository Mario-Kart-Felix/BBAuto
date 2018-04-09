namespace BBAuto
{
    partial class ShipPart_AddEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.cbCar = new System.Windows.Forms.ComboBox();
            this.cbDriver = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.mtbDateRequest = new System.Windows.Forms.MaskedTextBox();
            this.mtbDateSent = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.ucFile = new BBAuto.Domain.FileOpenTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Автомобиль:";
            // 
            // cbCar
            // 
            this.cbCar.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCar.Enabled = false;
            this.cbCar.FormattingEnabled = true;
            this.cbCar.Location = new System.Drawing.Point(11, 25);
            this.cbCar.Name = "cbCar";
            this.cbCar.Size = new System.Drawing.Size(182, 21);
            this.cbCar.TabIndex = 1;
            // 
            // cbDriver
            // 
            this.cbDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDriver.FormattingEnabled = true;
            this.cbDriver.Location = new System.Drawing.Point(11, 65);
            this.cbDriver.Name = "cbDriver";
            this.cbDriver.Size = new System.Drawing.Size(366, 21);
            this.cbDriver.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Получатель:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(196, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Номер акта:";
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(199, 25);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(178, 20);
            this.tbNumber.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Дата запроса:";
            // 
            // mtbDateRequest
            // 
            this.mtbDateRequest.Location = new System.Drawing.Point(11, 105);
            this.mtbDateRequest.Mask = "00.00.0000";
            this.mtbDateRequest.Name = "mtbDateRequest";
            this.mtbDateRequest.Size = new System.Drawing.Size(182, 20);
            this.mtbDateRequest.TabIndex = 4;
            // 
            // mtbDateSent
            // 
            this.mtbDateSent.Location = new System.Drawing.Point(199, 105);
            this.mtbDateSent.Mask = "00.00.0000";
            this.mtbDateSent.Name = "mtbDateSent";
            this.mtbDateSent.Size = new System.Drawing.Size(178, 20);
            this.mtbDateSent.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(196, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Дата отправки:";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Location = new System.Drawing.Point(202, 189);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 23);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(303, 189);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // ucFile
            // 
            this.ucFile.Location = new System.Drawing.Point(95, 143);
            this.ucFile.Name = "ucFile";
            this.ucFile.Size = new System.Drawing.Size(240, 23);
            this.ucFile.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 145);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Путь к файлу:";
            // 
            // ShipPart_AddEdit
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(390, 224);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ucFile);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.mtbDateSent);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.mtbDateRequest);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbDriver);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbCar);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShipPart_AddEdit";
            this.StartPosition
                = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Отправка запчастей";
            this.Load += new System.EventHandler(this.ShipPart_AddEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCar;
        private System.Windows.Forms.ComboBox cbDriver;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox mtbDateRequest;
        private System.Windows.Forms.MaskedTextBox mtbDateSent;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private BBAuto.Domain.FileOpenTextBox ucFile;
        private System.Windows.Forms.Label label6;
    }
}