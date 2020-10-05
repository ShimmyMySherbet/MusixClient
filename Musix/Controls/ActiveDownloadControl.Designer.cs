namespace Musix.Controls
{
    partial class ActiveDownloadControl
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
            this.lblAlbumName = new System.Windows.Forms.Label();
            this.lblArtistName = new System.Windows.Forms.Label();
            this.pbprogress = new System.Windows.Forms.ProgressBar();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnCancelDownload = new System.Windows.Forms.Button();
            this.pbArtwork = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbArtwork)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTrackName
            // 
            this.lblTrackName.AutoSize = true;
            this.lblTrackName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrackName.Location = new System.Drawing.Point(156, 3);
            this.lblTrackName.Name = "lblTrackName";
            this.lblTrackName.Size = new System.Drawing.Size(108, 24);
            this.lblTrackName.TabIndex = 1;
            this.lblTrackName.Text = "TrackName";
            // 
            // lblAlbumName
            // 
            this.lblAlbumName.AutoSize = true;
            this.lblAlbumName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlbumName.Location = new System.Drawing.Point(167, 27);
            this.lblAlbumName.Name = "lblAlbumName";
            this.lblAlbumName.Size = new System.Drawing.Size(96, 20);
            this.lblAlbumName.TabIndex = 2;
            this.lblAlbumName.Text = "AlbumName";
            // 
            // lblArtistName
            // 
            this.lblArtistName.AutoSize = true;
            this.lblArtistName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArtistName.Location = new System.Drawing.Point(167, 47);
            this.lblArtistName.Name = "lblArtistName";
            this.lblArtistName.Size = new System.Drawing.Size(88, 20);
            this.lblArtistName.TabIndex = 3;
            this.lblArtistName.Text = "ArtistName";
            // 
            // pbprogress
            // 
            this.pbprogress.Location = new System.Drawing.Point(160, 107);
            this.pbprogress.Name = "pbprogress";
            this.pbprogress.Size = new System.Drawing.Size(802, 23);
            this.pbprogress.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(472, 86);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(141, 18);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Starting Download...";
            // 
            // btnCancelDownload
            // 
            this.btnCancelDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelDownload.Location = new System.Drawing.Point(968, 107);
            this.btnCancelDownload.Name = "btnCancelDownload";
            this.btnCancelDownload.Size = new System.Drawing.Size(75, 23);
            this.btnCancelDownload.TabIndex = 6;
            this.btnCancelDownload.Text = "Cancel";
            this.btnCancelDownload.UseVisualStyleBackColor = true;
            this.btnCancelDownload.Click += new System.EventHandler(this.btnCancelDownload_Click);
            // 
            // pbArtwork
            // 
            this.pbArtwork.Location = new System.Drawing.Point(3, 3);
            this.pbArtwork.Name = "pbArtwork";
            this.pbArtwork.Size = new System.Drawing.Size(147, 132);
            this.pbArtwork.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbArtwork.TabIndex = 0;
            this.pbArtwork.TabStop = false;
            // 
            // ActiveDownloadControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(39)))), ((int)(((byte)(42)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btnCancelDownload);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.pbprogress);
            this.Controls.Add(this.lblArtistName);
            this.Controls.Add(this.lblAlbumName);
            this.Controls.Add(this.lblTrackName);
            this.Controls.Add(this.pbArtwork);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(137)))), ((int)(((byte)(218)))));
            this.Name = "ActiveDownloadControl";
            this.Size = new System.Drawing.Size(1046, 136);
            this.Load += new System.EventHandler(this.ActiveDownloadControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbArtwork)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbArtwork;
        private System.Windows.Forms.Label lblTrackName;
        private System.Windows.Forms.Label lblAlbumName;
        private System.Windows.Forms.Label lblArtistName;
        private System.Windows.Forms.ProgressBar pbprogress;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnCancelDownload;
    }
}
