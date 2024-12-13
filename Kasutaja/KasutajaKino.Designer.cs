namespace Kino
{
    partial class KasutajaKino
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KasutajaKino));
            this.KavaBtn = new System.Windows.Forms.Button();
            this.OstaPiletBtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Valjuda = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // KavaBtn
            // 
            this.KavaBtn.BackColor = System.Drawing.Color.LavenderBlush;
            this.KavaBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.KavaBtn.Location = new System.Drawing.Point(105, 78);
            this.KavaBtn.Name = "KavaBtn";
            this.KavaBtn.Size = new System.Drawing.Size(118, 76);
            this.KavaBtn.TabIndex = 0;
            this.KavaBtn.Text = "Kava";
            this.KavaBtn.UseVisualStyleBackColor = false;
            this.KavaBtn.Click += new System.EventHandler(this.KavaBtn_Click);
            // 
            // OstaPiletBtn
            // 
            this.OstaPiletBtn.BackColor = System.Drawing.Color.LavenderBlush;
            this.OstaPiletBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.OstaPiletBtn.Location = new System.Drawing.Point(105, 237);
            this.OstaPiletBtn.Name = "OstaPiletBtn";
            this.OstaPiletBtn.Size = new System.Drawing.Size(118, 71);
            this.OstaPiletBtn.TabIndex = 1;
            this.OstaPiletBtn.Text = "Osta pilet";
            this.OstaPiletBtn.UseVisualStyleBackColor = false;
            this.OstaPiletBtn.Click += new System.EventHandler(this.OstaPiletBtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(342, 78);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(243, 230);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // Valjuda
            // 
            this.Valjuda.BackColor = System.Drawing.Color.LavenderBlush;
            this.Valjuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.Valjuda.Location = new System.Drawing.Point(425, 375);
            this.Valjuda.Name = "Valjuda";
            this.Valjuda.Size = new System.Drawing.Size(102, 35);
            this.Valjuda.TabIndex = 3;
            this.Valjuda.Text = "Väljuda";
            this.Valjuda.UseVisualStyleBackColor = false;
            this.Valjuda.Click += new System.EventHandler(this.Valjuda_Click);
            // 
            // KasutajaKino
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(685, 450);
            this.Controls.Add(this.Valjuda);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.OstaPiletBtn);
            this.Controls.Add(this.KavaBtn);
            this.Name = "KasutajaKino";
            this.Text = "KasutajaKino";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button KavaBtn;
        private System.Windows.Forms.Button OstaPiletBtn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Valjuda;
    }
}