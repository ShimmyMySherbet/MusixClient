namespace Musix.Controls.MenuItems
{
    partial class PanelPopup
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
            this.pnContent = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pnContent
            // 
            this.pnContent.Location = new System.Drawing.Point(389, 231);
            this.pnContent.Name = "pnContent";
            this.pnContent.Size = new System.Drawing.Size(200, 100);
            this.pnContent.TabIndex = 0;
            // 
            // PanelPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Controls.Add(this.pnContent);
            this.Name = "PanelPopup";
            this.Size = new System.Drawing.Size(1001, 585);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnContent;
    }
}
