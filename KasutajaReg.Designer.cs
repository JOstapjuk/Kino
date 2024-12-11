namespace Kino
{
    partial class KasutajaReg
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KasutajaReg));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nimetusBox = new System.Windows.Forms.TextBox();
            this.paroolBox = new System.Windows.Forms.TextBox();
            this.emailBox = new System.Windows.Forms.TextBox();
            this.registerBtn = new System.Windows.Forms.Button();
            this.valjudaBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(38, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nimetus";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label2.Location = new System.Drawing.Point(56, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 26);
            this.label2.TabIndex = 1;
            this.label2.Text = "Parool";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label3.Location = new System.Drawing.Point(63, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 26);
            this.label3.TabIndex = 2;
            this.label3.Text = "Email";
            // 
            // nimetusBox
            // 
            this.nimetusBox.Location = new System.Drawing.Point(156, 91);
            this.nimetusBox.Name = "nimetusBox";
            this.nimetusBox.Size = new System.Drawing.Size(151, 20);
            this.nimetusBox.TabIndex = 3;
            // 
            // paroolBox
            // 
            this.paroolBox.Location = new System.Drawing.Point(156, 133);
            this.paroolBox.Name = "paroolBox";
            this.paroolBox.Size = new System.Drawing.Size(151, 20);
            this.paroolBox.TabIndex = 4;
            // 
            // emailBox
            // 
            this.emailBox.Location = new System.Drawing.Point(156, 188);
            this.emailBox.Name = "emailBox";
            this.emailBox.Size = new System.Drawing.Size(151, 20);
            this.emailBox.TabIndex = 5;
            // 
            // registerBtn
            // 
            this.registerBtn.Location = new System.Drawing.Point(61, 256);
            this.registerBtn.Name = "registerBtn";
            this.registerBtn.Size = new System.Drawing.Size(75, 23);
            this.registerBtn.TabIndex = 6;
            this.registerBtn.Text = "Registreeri";
            this.registerBtn.UseVisualStyleBackColor = true;
            this.registerBtn.Click += new System.EventHandler(this.registerBtn_Click);
            // 
            // valjudaBtn
            // 
            this.valjudaBtn.Location = new System.Drawing.Point(156, 256);
            this.valjudaBtn.Name = "valjudaBtn";
            this.valjudaBtn.Size = new System.Drawing.Size(75, 23);
            this.valjudaBtn.TabIndex = 7;
            this.valjudaBtn.Text = "Väljuda";
            this.valjudaBtn.UseVisualStyleBackColor = true;
            this.valjudaBtn.Click += new System.EventHandler(this.valjudaBtn_Click);
            // 
            // KasutajaReg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.valjudaBtn);
            this.Controls.Add(this.registerBtn);
            this.Controls.Add(this.emailBox);
            this.Controls.Add(this.paroolBox);
            this.Controls.Add(this.nimetusBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "KasutajaReg";
            this.Text = "KasutajaReg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox nimetusBox;
        private System.Windows.Forms.TextBox paroolBox;
        private System.Windows.Forms.TextBox emailBox;
        private System.Windows.Forms.Button registerBtn;
        private System.Windows.Forms.Button valjudaBtn;
    }
}