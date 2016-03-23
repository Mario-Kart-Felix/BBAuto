namespace BBAuto
{
    partial class formSuppyAddressList
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this._dgvSuppyAddress = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this._dgvSuppyAddress)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(407, 295);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 27;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // btnDel
            // 
            this.btnDel.Location = new System.Drawing.Point(93, 12);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(75, 23);
            this.btnDel.TabIndex = 26;
            this.btnDel.Text = "Удалить";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 12);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 25;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // _dgvSuppyAddress
            // 
            this._dgvSuppyAddress.AllowUserToAddRows = false;
            this._dgvSuppyAddress.AllowUserToDeleteRows = false;
            this._dgvSuppyAddress.AllowUserToResizeRows = false;
            this._dgvSuppyAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._dgvSuppyAddress.BackgroundColor = System.Drawing.SystemColors.Window;
            this._dgvSuppyAddress.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dgvSuppyAddress.Location = new System.Drawing.Point(12, 41);
            this._dgvSuppyAddress.Name = "_dgvSuppyAddress";
            this._dgvSuppyAddress.ReadOnly = true;
            this._dgvSuppyAddress.RowHeadersVisible = false;
            this._dgvSuppyAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._dgvSuppyAddress.Size = new System.Drawing.Size(470, 248);
            this._dgvSuppyAddress.TabIndex = 24;
            this._dgvSuppyAddress.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this._dgvSuppyAddress_CellMouseDoubleClick);
            this._dgvSuppyAddress.Resize += new System.EventHandler(this._dgvSuppyAddress_Resize);
            // 
            // formSuppyAddressList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 330);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this._dgvSuppyAddress);
            this.Name = "formSuppyAddressList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Адреса подачи";
            this.Load += new System.EventHandler(this.formSuppyAddressList_Load);
            ((System.ComponentModel.ISupportInitialize)(this._dgvSuppyAddress)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView _dgvSuppyAddress;
    }
}