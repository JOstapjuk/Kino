namespace Kino
{
    partial class AdminLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.LoginBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Valjuda = new System.Windows.Forms.Button();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.ParoolBox = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.label1.Location = new System.Drawing.Point(84, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "Login";
            // 
            // LoginBox
            // 
            this.LoginBox.Location = new System.Drawing.Point(151, 79);
            this.LoginBox.Name = "LoginBox";
            this.LoginBox.Size = new System.Drawing.Size(126, 20);
            this.LoginBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(74, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 26);
            this.label2.TabIndex = 2;
            this.label2.Text = "Parool";
            // 
            // Valjuda
            // 
            this.Valjuda.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Valjuda.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.Valjuda.Location = new System.Drawing.Point(191, 188);
            this.Valjuda.Name = "Valjuda";
            this.Valjuda.Size = new System.Drawing.Size(86, 34);
            this.Valjuda.TabIndex = 4;
            this.Valjuda.Text = "Väljuda";
            this.Valjuda.UseVisualStyleBackColor = false;
            this.Valjuda.Click += new System.EventHandler(this.Valjuda_Click);
            // 
            // LoginBtn
            // 
            this.LoginBtn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LoginBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.LoginBtn.Location = new System.Drawing.Point(79, 188);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(86, 34);
            this.LoginBtn.TabIndex = 5;
            this.LoginBtn.Text = "Login";
            this.LoginBtn.UseVisualStyleBackColor = false;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // ParoolBox
            // 
            this.ParoolBox.Location = new System.Drawing.Point(151, 128);
            this.ParoolBox.Name = "ParoolBox";
            this.ParoolBox.PasswordChar = '*';
            this.ParoolBox.Size = new System.Drawing.Size(126, 20);
            this.ParoolBox.TabIndex = 6;
            this.ParoolBox.UseSystemPasswordChar = true;
            // 
            // AdminLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(427, 249);
            this.Controls.Add(this.ParoolBox);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.Valjuda);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LoginBox);
            this.Controls.Add(this.label1);
            this.Name = "AdminLogin";
            this.Text = "AdminLogin";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LoginBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Valjuda;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.MaskedTextBox ParoolBox;
    }
}