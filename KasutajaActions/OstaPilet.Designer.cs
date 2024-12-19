namespace Kino
{
    partial class OstaPilet
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
            this.cmbSeans = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numTickets = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBuyTickets = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numTickets)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbSeans
            // 
            this.cmbSeans.FormattingEnabled = true;
            this.cmbSeans.Location = new System.Drawing.Point(173, 71);
            this.cmbSeans.Name = "cmbSeans";
            this.cmbSeans.Size = new System.Drawing.Size(121, 21);
            this.cmbSeans.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(81, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seans";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(173, 119);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(121, 20);
            this.txtEmail.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(81, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Email";
            // 
            // numTickets
            // 
            this.numTickets.Location = new System.Drawing.Point(173, 173);
            this.numTickets.Name = "numTickets";
            this.numTickets.Size = new System.Drawing.Size(121, 20);
            this.numTickets.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(81, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Piletid";
            // 
            // btnBuyTickets
            // 
            this.btnBuyTickets.Location = new System.Drawing.Point(99, 232);
            this.btnBuyTickets.Name = "btnBuyTickets";
            this.btnBuyTickets.Size = new System.Drawing.Size(75, 23);
            this.btnBuyTickets.TabIndex = 6;
            this.btnBuyTickets.Text = "Vali koht";
            this.btnBuyTickets.UseVisualStyleBackColor = true;
            this.btnBuyTickets.Click += new System.EventHandler(this.btnBuyTickets_Click);
            // 
            // OstaPilet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 311);
            this.Controls.Add(this.btnBuyTickets);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numTickets);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSeans);
            this.Name = "OstaPilet";
            this.Text = "OstaPilet";
            this.Load += new System.EventHandler(this.OstaPilet_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numTickets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbSeans;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numTickets;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBuyTickets;
    }
}