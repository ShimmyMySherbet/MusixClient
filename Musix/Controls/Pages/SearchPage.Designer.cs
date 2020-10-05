namespace Musix.Controls.Pages
{
    partial class SearchPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.FlowEntries = new System.Windows.Forms.FlowLayoutPanel();
            this.pbSearch = new System.Windows.Forms.PictureBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnManualEntry = new System.Windows.Forms.Button();
            this.lblCFail = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.txtSearch.Location = new System.Drawing.Point(13, 10);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(1042, 29);
            this.txtSearch.TabIndex = 1;
            // 
            // FlowEntries
            // 
            this.FlowEntries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlowEntries.AutoScroll = true;
            this.FlowEntries.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(43)))), ((int)(((byte)(47)))));
            this.FlowEntries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FlowEntries.Location = new System.Drawing.Point(13, 45);
            this.FlowEntries.Name = "FlowEntries";
            this.FlowEntries.Size = new System.Drawing.Size(1042, 604);
            this.FlowEntries.TabIndex = 3;
            // 
            // pbSearch
            // 
            this.pbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSearch.Image = global::Musix.Assets.Search_Blue;
            this.pbSearch.Location = new System.Drawing.Point(1061, 10);
            this.pbSearch.Name = "pbSearch";
            this.pbSearch.Size = new System.Drawing.Size(36, 29);
            this.pbSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSearch.TabIndex = 2;
            this.pbSearch.TabStop = false;
            this.pbSearch.Click += new System.EventHandler(this.pbSearch_Click);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Location = new System.Drawing.Point(1062, 45);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(35, 33);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "C";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnManualEntry
            // 
            this.btnManualEntry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnManualEntry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManualEntry.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManualEntry.Location = new System.Drawing.Point(1062, 84);
            this.btnManualEntry.Name = "btnManualEntry";
            this.btnManualEntry.Size = new System.Drawing.Size(35, 33);
            this.btnManualEntry.TabIndex = 5;
            this.btnManualEntry.Text = "M";
            this.btnManualEntry.UseVisualStyleBackColor = true;
            this.btnManualEntry.Click += new System.EventHandler(this.btnTestPopup_Click);
            // 
            // lblCFail
            // 
            this.lblCFail.AutoSize = true;
            this.lblCFail.Location = new System.Drawing.Point(495, 0);
            this.lblCFail.Name = "lblCFail";
            this.lblCFail.Size = new System.Drawing.Size(38, 13);
            this.lblCFail.TabIndex = 6;
            this.lblCFail.Text = "Failed.";
            this.lblCFail.Visible = false;
            // 
            // SearchPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.Controls.Add(this.lblCFail);
            this.Controls.Add(this.btnManualEntry);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.FlowEntries);
            this.Controls.Add(this.pbSearch);
            this.Controls.Add(this.txtSearch);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.Name = "SearchPage";
            this.Size = new System.Drawing.Size(1100, 680);
            this.Load += new System.EventHandler(this.SearchPage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.PictureBox pbSearch;
        private System.Windows.Forms.FlowLayoutPanel FlowEntries;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnManualEntry;
        private System.Windows.Forms.Label lblCFail;
    }
}
