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
            this.FilmHaldus = new System.Windows.Forms.Button();
            this.SeansHaldusBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SaalHaldus
            // 
            this.SaalHaldus.BackColor = System.Drawing.Color.Transparent;
            this.SaalHaldus.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.SaalHaldus.Location = new System.Drawing.Point(42, 214);
            this.SaalHaldus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SaalHaldus.Name = "SaalHaldus";
            this.SaalHaldus.Size = new System.Drawing.Size(158, 111);
            this.SaalHaldus.TabIndex = 0;
            this.SaalHaldus.Text = "Saal Haldus";
            this.SaalHaldus.UseVisualStyleBackColor = false;
            this.SaalHaldus.Click += new System.EventHandler(this.SaalHaldus_Click);
            // 
            // FilmHaldus
            // 
            this.FilmHaldus.BackColor = System.Drawing.Color.Transparent;
            this.FilmHaldus.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.FilmHaldus.Location = new System.Drawing.Point(454, 214);
            this.FilmHaldus.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FilmHaldus.Name = "FilmHaldus";
            this.FilmHaldus.Size = new System.Drawing.Size(150, 111);
            this.FilmHaldus.TabIndex = 2;
            this.FilmHaldus.Text = "Filmi Haldus";
            this.FilmHaldus.UseVisualStyleBackColor = false;
            this.FilmHaldus.Click += new System.EventHandler(this.FilmHaldus_Click);
            // 
            // SeansHaldusBtn
            // 
            this.SeansHaldusBtn.BackColor = System.Drawing.Color.Transparent;
            this.SeansHaldusBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.SeansHaldusBtn.Location = new System.Drawing.Point(254, 214);
            this.SeansHaldusBtn.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SeansHaldusBtn.Name = "SeansHaldusBtn";
            this.SeansHaldusBtn.Size = new System.Drawing.Size(158, 111);
            this.SeansHaldusBtn.TabIndex = 3;
            this.SeansHaldusBtn.Text = "Seans Haldus";
            this.SeansHaldusBtn.UseVisualStyleBackColor = false;
            this.SeansHaldusBtn.Click += new System.EventHandler(this.SeansHaldusBtn_Click);
            // 
            // AdminHaldus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(726, 595);
            this.Controls.Add(this.SeansHaldusBtn);
            this.Controls.Add(this.FilmHaldus);
            this.Controls.Add(this.SaalHaldus);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AdminHaldus";
            this.Text = "AdminHaldus";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SaalHaldus;
        private System.Windows.Forms.Button FilmHaldus;
        private System.Windows.Forms.Button SeansHaldusBtn;
    }
}