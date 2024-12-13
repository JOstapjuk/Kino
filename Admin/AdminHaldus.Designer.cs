namespace Kino
{
    partial class AdminHaldus
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminHaldus));
            this.SaalHaldus = new System.Windows.Forms.Button();
            this.PiletHaldus = new System.Windows.Forms.Button();
            this.FilmHaldus = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SaalHaldus
            // 
            this.SaalHaldus.BackColor = System.Drawing.Color.Transparent;
            this.SaalHaldus.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.SaalHaldus.Location = new System.Drawing.Point(28, 139);
            this.SaalHaldus.Name = "SaalHaldus";
            this.SaalHaldus.Size = new System.Drawing.Size(105, 72);
            this.SaalHaldus.TabIndex = 0;
            this.SaalHaldus.Text = "Saal Haldus";
            this.SaalHaldus.UseVisualStyleBackColor = false;
            this.SaalHaldus.Click += new System.EventHandler(this.SaalHaldus_Click);
            // 
            // PiletHaldus
            // 
            this.PiletHaldus.BackColor = System.Drawing.Color.Transparent;
            this.PiletHaldus.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.PiletHaldus.Location = new System.Drawing.Point(167, 139);
            this.PiletHaldus.Name = "PiletHaldus";
            this.PiletHaldus.Size = new System.Drawing.Size(103, 72);
            this.PiletHaldus.TabIndex = 1;
            this.PiletHaldus.Text = "Pileti Haldus";
            this.PiletHaldus.UseVisualStyleBackColor = false;
            this.PiletHaldus.Click += new System.EventHandler(this.PiletHaldus_Click);
            // 
            // FilmHaldus
            // 
            this.FilmHaldus.BackColor = System.Drawing.Color.Transparent;
            this.FilmHaldus.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.FilmHaldus.Location = new System.Drawing.Point(303, 139);
            this.FilmHaldus.Name = "FilmHaldus";
            this.FilmHaldus.Size = new System.Drawing.Size(100, 72);
            this.FilmHaldus.TabIndex = 2;
            this.FilmHaldus.Text = "Filmi Haldus";
            this.FilmHaldus.UseVisualStyleBackColor = false;
            this.FilmHaldus.Click += new System.EventHandler(this.FilmHaldus_Click);
            // 
            // AdminHaldus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(484, 387);
            this.Controls.Add(this.FilmHaldus);
            this.Controls.Add(this.PiletHaldus);
            this.Controls.Add(this.SaalHaldus);
            this.Name = "AdminHaldus";
            this.Text = "AdminHaldus";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SaalHaldus;
        private System.Windows.Forms.Button PiletHaldus;
        private System.Windows.Forms.Button FilmHaldus;
    }
}