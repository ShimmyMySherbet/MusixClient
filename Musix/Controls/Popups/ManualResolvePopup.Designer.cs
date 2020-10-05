namespace Musix.Controls.Popups
{
    partial class ManualResolvePopup
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
            this.txtSpotify = new System.Windows.Forms.TextBox();
            this.txtYoutube = new System.Windows.Forms.TextBox();
            this.lblt = new System.Windows.Forms.Label();
            this.pbSpotify = new System.Windows.Forms.PictureBox();
            this.pbYoutube = new System.Windows.Forms.PictureBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.pbSearchSpotify = new System.Windows.Forms.PictureBox();
            this.pbSearchYoutube = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSpotify)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbYoutube)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchSpotify)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchYoutube)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSpotify
            // 
            this.txtSpotify.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.txtSpotify.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSpotify.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSpotify.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.txtSpotify.Location = new System.Drawing.Point(67, 50);
            this.txtSpotify.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSpotify.Name = "txtSpotify";
            this.txtSpotify.Size = new System.Drawing.Size(466, 26);
            this.txtSpotify.TabIndex = 0;
            // 
            // txtYoutube
            // 
            this.txtYoutube.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.txtYoutube.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtYoutube.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtYoutube.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.txtYoutube.Location = new System.Drawing.Point(67, 95);
            this.txtYoutube.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtYoutube.Name = "txtYoutube";
            this.txtYoutube.Size = new System.Drawing.Size(466, 26);
            this.txtYoutube.TabIndex = 1;
            // 
            // lblt
            // 
            this.lblt.AutoSize = true;
            this.lblt.Location = new System.Drawing.Point(210, 16);
            this.lblt.Name = "lblt";
            this.lblt.Size = new System.Drawing.Size(165, 20);
            this.lblt.TabIndex = 2;
            this.lblt.Text = "Manual Track Resolve";
            // 
            // pbSpotify
            // 
            this.pbSpotify.Image = global::Musix.Assets.Spotify;
            this.pbSpotify.Location = new System.Drawing.Point(29, 50);
            this.pbSpotify.Name = "pbSpotify";
            this.pbSpotify.Size = new System.Drawing.Size(31, 26);
            this.pbSpotify.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSpotify.TabIndex = 3;
            this.pbSpotify.TabStop = false;
            // 
            // pbYoutube
            // 
            this.pbYoutube.Image = global::Musix.Assets.YouTube;
            this.pbYoutube.Location = new System.Drawing.Point(29, 95);
            this.pbYoutube.Name = "pbYoutube";
            this.pbYoutube.Size = new System.Drawing.Size(31, 26);
            this.pbYoutube.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbYoutube.TabIndex = 4;
            this.pbYoutube.TabStop = false;
            // 
            // btnApply
            // 
            this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApply.Location = new System.Drawing.Point(255, 129);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(79, 29);
            this.btnApply.TabIndex = 5;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // pbSearchSpotify
            // 
            this.pbSearchSpotify.Image = global::Musix.Assets.Browse_Blue;
            this.pbSearchSpotify.Location = new System.Drawing.Point(540, 50);
            this.pbSearchSpotify.Name = "pbSearchSpotify";
            this.pbSearchSpotify.Size = new System.Drawing.Size(31, 26);
            this.pbSearchSpotify.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSearchSpotify.TabIndex = 6;
            this.pbSearchSpotify.TabStop = false;
            // 
            // pbSearchYoutube
            // 
            this.pbSearchYoutube.Image = global::Musix.Assets.Browse_Blue;
            this.pbSearchYoutube.Location = new System.Drawing.Point(540, 95);
            this.pbSearchYoutube.Name = "pbSearchYoutube";
            this.pbSearchYoutube.Size = new System.Drawing.Size(31, 26);
            this.pbSearchYoutube.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbSearchYoutube.TabIndex = 7;
            this.pbSearchYoutube.TabStop = false;
            // 
            // ManualResolvePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.Controls.Add(this.pbSearchYoutube);
            this.Controls.Add(this.pbSearchSpotify);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.pbYoutube);
            this.Controls.Add(this.pbSpotify);
            this.Controls.Add(this.lblt);
            this.Controls.Add(this.txtYoutube);
            this.Controls.Add(this.txtSpotify);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ManualResolvePopup";
            this.Size = new System.Drawing.Size(600, 174);
            this.Load += new System.EventHandler(this.ManualResolvePopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSpotify)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbYoutube)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchSpotify)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSearchYoutube)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSpotify;
        private System.Windows.Forms.TextBox txtYoutube;
        private System.Windows.Forms.Label lblt;
        private System.Windows.Forms.PictureBox pbSpotify;
        private System.Windows.Forms.PictureBox pbYoutube;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.PictureBox pbSearchSpotify;
        private System.Windows.Forms.PictureBox pbSearchYoutube;
    }
}
