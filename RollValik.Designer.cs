namespace Kino
{
    partial class RollValik
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RollValik));
            this.label1 = new System.Windows.Forms.Label();
            this.radio_kasutaja = new System.Windows.Forms.RadioButton();
            this.radio_admin = new System.Windows.Forms.RadioButton();
            this.EdasiBtn = new System.Windows.Forms.Button();
            this.valjudaBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label1.Location = new System.Drawing.Point(126, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(310, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tere tulemast kinno, palun valige, kes te olete.";
            // 
            // radio_kasutaja
            // 
            this.radio_kasutaja.AutoSize = true;
            this.radio_kasutaja.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radio_kasutaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio_kasutaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.radio_kasutaja.Location = new System.Drawing.Point(129, 162);
            this.radio_kasutaja.Name = "radio_kasutaja";
            this.radio_kasutaja.Size = new System.Drawing.Size(115, 30);
            this.radio_kasutaja.TabIndex = 1;
            this.radio_kasutaja.TabStop = true;
            this.radio_kasutaja.Text = "Kasutaja";
            this.radio_kasutaja.UseVisualStyleBackColor = false;
            // 
            // radio_admin
            // 
            this.radio_admin.AutoSize = true;
            this.radio_admin.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.radio_admin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radio_admin.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.radio_admin.Location = new System.Drawing.Point(343, 162);
            this.radio_admin.Name = "radio_admin";
            this.radio_admin.Size = new System.Drawing.Size(93, 30);
            this.radio_admin.TabIndex = 2;
            this.radio_admin.TabStop = true;
            this.radio_admin.Text = "Admin";
            this.radio_admin.UseVisualStyleBackColor = false;
            // 
            // EdasiBtn
            // 
            this.EdasiBtn.Location = new System.Drawing.Point(129, 227);
            this.EdasiBtn.Name = "EdasiBtn";
            this.EdasiBtn.Size = new System.Drawing.Size(75, 23);
            this.EdasiBtn.TabIndex = 3;
            this.EdasiBtn.Text = "Edasi";
            this.EdasiBtn.UseVisualStyleBackColor = true;
            this.EdasiBtn.Click += new System.EventHandler(this.EdasiBtn_Click);
            // 
            // valjudaBtn
            // 
            this.valjudaBtn.Location = new System.Drawing.Point(361, 227);
            this.valjudaBtn.Name = "valjudaBtn";
            this.valjudaBtn.Size = new System.Drawing.Size(75, 23);
            this.valjudaBtn.TabIndex = 4;
            this.valjudaBtn.Text = "Väljuda";
            this.valjudaBtn.UseVisualStyleBackColor = true;
            this.valjudaBtn.Click += new System.EventHandler(this.valjudaBtn_Click);
            // 
            // RollValik
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(555, 394);
            this.Controls.Add(this.valjudaBtn);
            this.Controls.Add(this.EdasiBtn);
            this.Controls.Add(this.radio_admin);
            this.Controls.Add(this.radio_kasutaja);
            this.Controls.Add(this.label1);
            this.Name = "RollValik";
            this.Text = "Tere kinnost!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radio_kasutaja;
        private System.Windows.Forms.RadioButton radio_admin;
        private System.Windows.Forms.Button EdasiBtn;
        private System.Windows.Forms.Button valjudaBtn;
    }
}

