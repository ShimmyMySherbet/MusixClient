namespace Musix.Controls.Pages
{
    partial class DownloadsPage
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
            this.label1 = new System.Windows.Forms.Label();
            this.FlowDownloads = new System.Windows.Forms.FlowLayoutPanel();
            this.lblNoActiveDownloads = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Downloads";
            // 
            // FlowDownloads
            // 
            this.FlowDownloads.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlowDownloads.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(43)))), ((int)(((byte)(47)))));
            this.FlowDownloads.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FlowDownloads.Location = new System.Drawing.Point(9, 32);
            this.FlowDownloads.Name = "FlowDownloads";
            this.FlowDownloads.Size = new System.Drawing.Size(1067, 623);
            this.FlowDownloads.TabIndex = 1;
            // 
            // lblNoActiveDownloads
            // 
            this.lblNoActiveDownloads.AutoSize = true;
            this.lblNoActiveDownloads.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(43)))), ((int)(((byte)(47)))));
            this.lblNoActiveDownloads.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoActiveDownloads.Location = new System.Drawing.Point(377, 42);
            this.lblNoActiveDownloads.Name = "lblNoActiveDownloads";
            this.lblNoActiveDownloads.Size = new System.Drawing.Size(216, 25);
            this.lblNoActiveDownloads.TabIndex = 2;
            this.lblNoActiveDownloads.Text = "No Active Downloads";
            this.lblNoActiveDownloads.Visible = false;
            // 
            // DownloadsPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.Controls.Add(this.lblNoActiveDownloads);
            this.Controls.Add(this.FlowDownloads);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.Name = "DownloadsPage";
            this.Size = new System.Drawing.Size(1100, 680);
            this.Load += new System.EventHandler(this.DownloadsPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel FlowDownloads;
        private System.Windows.Forms.Label lblNoActiveDownloads;
    }
}
