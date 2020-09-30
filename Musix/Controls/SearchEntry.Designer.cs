namespace Musix.Controls
{
    partial class SearchEntry
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
            this.lblTrackName = new System.Windows.Forms.Label();
            this.lblAlbum = new System.Windows.Forms.Label();
            this.lblArtist = new System.Windows.Forms.Label();
            this.FlowIcons = new System.Windows.Forms.FlowLayoutPanel();
            this.pbDownload = new System.Windows.Forms.PictureBox();
            this.pbArtwork = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.FlowIcons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbDownload)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbArtwork)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTrackName
            // 
            this.lblTrackName.AutoSize = true;
            this.lblTrackName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackName.Location = new System.Drawing.Point(183, 3);
            this.lblTrackName.Name = "lblTrackName";
            this.lblTrackName.Size = new System.Drawing.Size(113, 24);
            this.lblTrackName.TabIndex = 1;
            this.lblTrackName.Text = "Track Name";
            // 
            // lblAlbum
            // 
            this.lblAlbum.AutoSize = true;
            this.lblAlbum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlbum.Location = new System.Drawing.Point(196, 29);
            this.lblAlbum.Name = "lblAlbum";
            this.lblAlbum.Size = new System.Drawing.Size(54, 20);
            this.lblAlbum.TabIndex = 2;
            this.lblAlbum.Text = "Album";
            // 
            // lblArtist
            // 
            this.lblArtist.AutoSize = true;
            this.lblArtist.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArtist.Location = new System.Drawing.Point(196, 49);
            this.lblArtist.Name = "lblArtist";
            this.lblArtist.Size = new System.Drawing.Size(46, 20);
            this.lblArtist.TabIndex = 3;
            this.lblArtist.Text = "Artist";
            // 
            // FlowIcons
            // 
            this.FlowIcons.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FlowIcons.Controls.Add(this.pictureBox1);
            this.FlowIcons.Controls.Add(this.pictureBox2);
            this.FlowIcons.Controls.Add(this.pictureBox3);
            this.FlowIcons.Location = new System.Drawing.Point(658, 1);
            this.FlowIcons.Name = "FlowIcons";
            this.FlowIcons.Size = new System.Drawing.Size(35, 122);
            this.FlowIcons.TabIndex = 5;
            // 
            // pbDownload
            // 
            this.pbDownload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pbDownload.Image = global::Musix.Assets.DownloadMain_Blue;
            this.pbDownload.Location = new System.Drawing.Point(662, 151);
            this.pbDownload.Name = "pbDownload";
            this.pbDownload.Size = new System.Drawing.Size(32, 29);
            this.pbDownload.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbDownload.TabIndex = 4;
            this.pbDownload.TabStop = false;
            this.pbDownload.Click += new System.EventHandler(this.pbDownload_Click);
            // 
            // pbArtwork
            // 
            this.pbArtwork.Location = new System.Drawing.Point(3, 3);
            this.pbArtwork.Name = "pbArtwork";
            this.pbArtwork.Size = new System.Drawing.Size(177, 177);
            this.pbArtwork.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbArtwork.TabIndex = 0;
            this.pbArtwork.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Musix.Assets.YouTube;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 23);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Musix.Assets.Spotify;
            this.pictureBox2.Location = new System.Drawing.Point(3, 32);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(28, 23);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Musix.Assets.Genius;
            this.pictureBox3.Location = new System.Drawing.Point(3, 61);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(28, 23);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // SearchEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.Controls.Add(this.FlowIcons);
            this.Controls.Add(this.pbDownload);
            this.Controls.Add(this.lblArtist);
            this.Controls.Add(this.lblAlbum);
            this.Controls.Add(this.lblTrackName);
            this.Controls.Add(this.pbArtwork);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.Name = "SearchEntry";
            this.Size = new System.Drawing.Size(693, 182);
            this.Load += new System.EventHandler(this.SearchEntry_Load);
            this.FlowIcons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbDownload)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbArtwork)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pbDownload;
        private System.Windows.Forms.FlowLayoutPanel FlowIcons;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        public System.Windows.Forms.PictureBox pbArtwork;
        public System.Windows.Forms.Label lblTrackName;
        public System.Windows.Forms.Label lblAlbum;
        public System.Windows.Forms.Label lblArtist;
    }
}
