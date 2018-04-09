namespace BBAuto.App.FormsForDriver
{
    partial class formInstractionList
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
            this.dgvInstractions = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInstractions)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(93, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 11;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.delete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(12, 10);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.add_Click);
            // 
            // dgvInstractions
            // 
            this.dgvInstractions.AllowUserToAddRows = false;
            this.dgvInstractions.AllowUserToDeleteRows = false;
            this.dgvInstractions.AllowUserToResizeRows = false;
            this.dgvInstractions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInstractions.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvInstractions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInstractions.Location = new System.Drawing.Point(12, 40);
            this.dgvInstractions.Name = "dgvInstractions";
            this.dgvInstractions.ReadOnly = true;
            this.dgvInstractions.RowHeadersVisible = false;
            this.dgvInstractions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvInstractions.Size = new System.Drawing.Size(340, 321);
            this.dgvInstractions.TabIndex = 9;
            this.dgvInstractions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInstractions_CellDoubleClick);
            // 
            // formInstractionList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 373);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvInstractions);
            this.Name = "formInstractionList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Инструкции";
            this.Load += new System.EventHandler(this.InstractionList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInstractions)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvInstractions;
    }
}
