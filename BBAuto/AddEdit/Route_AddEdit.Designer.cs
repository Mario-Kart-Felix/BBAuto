namespace BBAuto
{
    partial class Route_AddEdit
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
            this.label2 = new System.Windows.Forms.Label();
            this.cbMyPoint2 = new System.Windows.Forms.ComboBox();
            this.lbMyPoint1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbDistance = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lbRegion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Пункт назначения:";
            // 
            // cbMyPoint2
            // 
            this.cbMyPoint2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMyPoint2.FormattingEnabled = true;
            this.cbMyPoint2.Location = new System.Drawing.Point(15, 75);
            this.cbMyPoint2.Name = "cbMyPoint2";
            this.cbMyPoint2.Size = new System.Drawing.Size(419, 21);
            this.cbMyPoint2.TabIndex = 5;
            // 
            // lbMyPoint1
            // 
            this.lbMyPoint1.AutoSize = true;
            this.lbMyPoint1.Location = new System.Drawing.Point(12, 35);
            this.lbMyPoint1.Name = "lbMyPoint1";
            this.lbMyPoint1.Size = new System.Drawing.Size(108, 13);
            this.lbMyPoint1.TabIndex = 4;
            this.lbMyPoint1.Text = "Пункт отправления:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Дистанция:";
            // 
            // tbDistance
            // 
            this.tbDistance.Location = new System.Drawing.Point(84, 106);
            this.tbDistance.Name = "tbDistance";
            this.tbDistance.Size = new System.Drawing.Size(47, 20);
            this.tbDistance.TabIndex = 7;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(258, 143);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(95, 23);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(359, 143);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // lbRegion
            // 
            this.lbRegion.AutoSize = true;
            this.lbRegion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbRegion.Location = new System.Drawing.Point(12, 9);
            this.lbRegion.Name = "lbRegion";
            this.lbRegion.Size = new System.Drawing.Size(54, 17);
            this.lbRegion.TabIndex = 10;
            this.lbRegion.Text = "Регион";
            // 
            // Route_AddEdit
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(446, 178);
            this.Controls.Add(this.lbRegion);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbDistance);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbMyPoint2);
            this.Controls.Add(this.lbMyPoint1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Route_AddEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Маршрут";
            this.Load += new System.EventHandler(this.Route_AddEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbMyPoint2;
        private System.Windows.Forms.Label lbMyPoint1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbDistance;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lbRegion;

    }
}