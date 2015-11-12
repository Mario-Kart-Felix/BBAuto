namespace BBAuto
{
    partial class formMedicalCertList
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
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvMedicalCert = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicalCert)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(93, 12);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 14;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.delete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 13;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.add_Click);
            // 
            // dgvMedicalCert
            // 
            this.dgvMedicalCert.AllowUserToAddRows = false;
            this.dgvMedicalCert.AllowUserToDeleteRows = false;
            this.dgvMedicalCert.AllowUserToResizeRows = false;
            this.dgvMedicalCert.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMedicalCert.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvMedicalCert.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedicalCert.Location = new System.Drawing.Point(12, 41);
            this.dgvMedicalCert.Name = "dgvMedicalCert";
            this.dgvMedicalCert.ReadOnly = true;
            this.dgvMedicalCert.RowHeadersVisible = false;
            this.dgvMedicalCert.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvMedicalCert.Size = new System.Drawing.Size(340, 314);
            this.dgvMedicalCert.TabIndex = 12;
            this.dgvMedicalCert.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvMedicalCert_CellDoubleClick);
            // 
            // formMedicalCertList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 367);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvMedicalCert);
            this.Name = "formMedicalCertList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Медицинские справки";
            this.Load += new System.EventHandler(this.MedicalCertList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMedicalCert)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvMedicalCert;
    }
}