namespace Musix
{
    partial class MainWindow
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
            this.PNContent = new System.Windows.Forms.Panel();
            this.MDSSideBar = new Musix.Controls.MusixDynamicSidebar();
            this.SuspendLayout();
            // 
            // PNContent
            // 
            this.PNContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PNContent.Location = new System.Drawing.Point(196, 0);
            this.PNContent.Name = "PNContent";
            this.PNContent.Size = new System.Drawing.Size(1138, 694);
            this.PNContent.TabIndex = 1;
            // 
            // MDSSideBar
            // 
            this.MDSSideBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.MDSSideBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.MDSSideBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.MDSSideBar.Location = new System.Drawing.Point(-5, 0);
            this.MDSSideBar.Name = "MDSSideBar";
            this.MDSSideBar.Size = new System.Drawing.Size(195, 694);
            this.MDSSideBar.TabIndex = 2;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1334, 694);
            this.Controls.Add(this.MDSSideBar);
            this.Controls.Add(this.PNContent);
            this.Name = "MainWindow";
            this.Text = "Musix";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PNContent;
        private Controls.MusixDynamicSidebar MDSSideBar;
    }
}