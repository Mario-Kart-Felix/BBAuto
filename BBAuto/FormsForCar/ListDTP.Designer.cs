namespace BBAuto
{
    partial class ListDTP
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
            this._dgvDTP = new System.Windows.Forms.DataGridView();
            this.Add = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._dgvDTP)).BeginInit();
            this.SuspendLayout();
            // 
            // _dgvDTP
            // 
            this._dgvDTP.AllowUserToAddRows = false;
            this._dgvDTP.AllowUserToDeleteRows = false;
            this._dgvDTP.AllowUserToResizeRows = false;
            this._dgvDTP.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvDTP.BackgroundColor = System.Drawing.SystemColors.Window;
            this._dgvDTP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvDTP.Location = new System.Drawing.Point(12, 41);
            this._dgvDTP.Name = "_dgvDTP";
            this._dgvDTP.ReadOnly = true;
            this._dgvDTP.RowHeadersVisible = false;
            this._dgvDTP.Size = new System.Drawing.Size(647, 212);
            this._dgvDTP.TabIndex = 1;
            this._dgvDTP.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this._dgvDTP_CellDoubleClick);
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(12, 12);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(75, 23);
            this.Add.TabIndex = 2;
            this.Add.Text = "Добавить";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(93, 12);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(75, 23);
            this.delete.TabIndex = 3;
            this.delete.Text = "Удалить";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // ListDTP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 265);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.Add);
            this.Controls.Add(this._dgvDTP);
            this.Name = "ListDTP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Список ДТП";
            ((System.ComponentModel.ISupportInitialize)(this._dgvDTP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView _dgvDTP;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button delete;
    }
}