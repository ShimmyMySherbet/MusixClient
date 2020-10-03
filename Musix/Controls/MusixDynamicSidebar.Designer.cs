namespace Musix.Controls
{
    partial class MusixDynamicSidebar
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
            this.flowElements = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // flowElements
            // 
            this.flowElements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowElements.Location = new System.Drawing.Point(0, 0);
            this.flowElements.Name = "flowElements";
            this.flowElements.Size = new System.Drawing.Size(231, 740);
            this.flowElements.TabIndex = 0;
            // 
            // MusixDynamicSidebar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.Controls.Add(this.flowElements);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.Name = "MusixDynamicSidebar";
            this.Size = new System.Drawing.Size(231, 740);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowElements;
    }
}
