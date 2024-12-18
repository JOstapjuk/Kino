namespace Kino
{
    partial class Kava
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Kava));
            this.NimetusLabel = new System.Windows.Forms.Label();
            this.ZanrLabel = new System.Windows.Forms.Label();
            this.RezisoorLabel = new System.Windows.Forms.Label();
            this.PikkusLabel = new System.Windows.Forms.Label();
            this.OsataitjadLabel = new System.Windows.Forms.Label();
            this.PosterPictureBox = new System.Windows.Forms.PictureBox();
            this.PreviousButton = new System.Windows.Forms.Button();
            this.NextButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PosterPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // NimetusLabel
            // 
            this.NimetusLabel.AutoSize = true;
            this.NimetusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.NimetusLabel.Location = new System.Drawing.Point(55, 45);
            this.NimetusLabel.Name = "NimetusLabel";
            this.NimetusLabel.Size = new System.Drawing.Size(70, 26);
            this.NimetusLabel.TabIndex = 0;
            this.NimetusLabel.Text = "label1";
            // 
            // ZanrLabel
            // 
            this.ZanrLabel.AutoSize = true;
            this.ZanrLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.ZanrLabel.Location = new System.Drawing.Point(55, 92);
            this.ZanrLabel.Name = "ZanrLabel";
            this.ZanrLabel.Size = new System.Drawing.Size(70, 26);
            this.ZanrLabel.TabIndex = 1;
            this.ZanrLabel.Text = "label2";
            // 
            // RezisoorLabel
            // 
            this.RezisoorLabel.AutoSize = true;
            this.RezisoorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.RezisoorLabel.Location = new System.Drawing.Point(55, 137);
            this.RezisoorLabel.Name = "RezisoorLabel";
            this.RezisoorLabel.Size = new System.Drawing.Size(70, 26);
            this.RezisoorLabel.TabIndex = 2;
            this.RezisoorLabel.Text = "label3";
            // 
            // PikkusLabel
            // 
            this.PikkusLabel.AutoSize = true;
            this.PikkusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.PikkusLabel.Location = new System.Drawing.Point(55, 183);
            this.PikkusLabel.Name = "PikkusLabel";
            this.PikkusLabel.Size = new System.Drawing.Size(70, 26);
            this.PikkusLabel.TabIndex = 3;
            this.PikkusLabel.Text = "label4";
            // 
            // OsataitjadLabel
            // 
            this.OsataitjadLabel.AutoSize = true;
            this.OsataitjadLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.OsataitjadLabel.Location = new System.Drawing.Point(55, 232);
            this.OsataitjadLabel.Name = "OsataitjadLabel";
            this.OsataitjadLabel.Size = new System.Drawing.Size(70, 26);
            this.OsataitjadLabel.TabIndex = 4;
            this.OsataitjadLabel.Text = "label5";
            // 
            // PosterPictureBox
            // 
            this.PosterPictureBox.Location = new System.Drawing.Point(218, 12);
            this.PosterPictureBox.Name = "PosterPictureBox";
            this.PosterPictureBox.Size = new System.Drawing.Size(250, 331);
            this.PosterPictureBox.TabIndex = 5;
            this.PosterPictureBox.TabStop = false;
            // 
            // PreviousButton
            // 
            this.PreviousButton.Location = new System.Drawing.Point(218, 383);
            this.PreviousButton.Name = "PreviousButton";
            this.PreviousButton.Size = new System.Drawing.Size(75, 23);
            this.PreviousButton.TabIndex = 6;
            this.PreviousButton.Text = "Eelmine film";
            this.PreviousButton.UseVisualStyleBackColor = true;
            this.PreviousButton.Click += new System.EventHandler(this.PreviousButton_Click);
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(393, 383);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 23);
            this.NextButton.TabIndex = 7;
            this.NextButton.Text = "Järgmine film";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.NextButton_Click);
            // 
            // Kava
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(563, 450);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.PreviousButton);
            this.Controls.Add(this.PosterPictureBox);
            this.Controls.Add(this.OsataitjadLabel);
            this.Controls.Add(this.PikkusLabel);
            this.Controls.Add(this.RezisoorLabel);
            this.Controls.Add(this.ZanrLabel);
            this.Controls.Add(this.NimetusLabel);
            this.Name = "Kava";
            this.Text = "Kava";
            ((System.ComponentModel.ISupportInitialize)(this.PosterPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NimetusLabel;
        private System.Windows.Forms.Label ZanrLabel;
        private System.Windows.Forms.Label RezisoorLabel;
        private System.Windows.Forms.Label PikkusLabel;
        private System.Windows.Forms.Label OsataitjadLabel;
        private System.Windows.Forms.PictureBox PosterPictureBox;
        private System.Windows.Forms.Button PreviousButton;
        private System.Windows.Forms.Button NextButton;
    }
}