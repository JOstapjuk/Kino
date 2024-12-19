namespace Kino.AdminActions
{
    partial class SeansHaldus
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
            this.cmbFilms = new System.Windows.Forms.ComboBox();
            this.cmbSaals = new System.Windows.Forms.ComboBox();
            this.btnAddSeans = new System.Windows.Forms.Button();
            this.btnDeleteSeans = new System.Windows.Forms.Button();
            this.dgvSeanss = new System.Windows.Forms.DataGridView();
            this.lblFilm = new System.Windows.Forms.Label();
            this.lblSaal = new System.Windows.Forms.Label();
            this.lblShowtime = new System.Windows.Forms.Label();
            this.txtHind = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Aegtxt = new System.Windows.Forms.TextBox();
            this.Paevtxt = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeanss)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbFilms
            // 
            this.cmbFilms.FormattingEnabled = true;
            this.cmbFilms.Location = new System.Drawing.Point(208, 67);
            this.cmbFilms.Name = "cmbFilms";
            this.cmbFilms.Size = new System.Drawing.Size(214, 28);
            this.cmbFilms.TabIndex = 0;
            // 
            // cmbSaals
            // 
            this.cmbSaals.FormattingEnabled = true;
            this.cmbSaals.Location = new System.Drawing.Point(208, 127);
            this.cmbSaals.Name = "cmbSaals";
            this.cmbSaals.Size = new System.Drawing.Size(214, 28);
            this.cmbSaals.TabIndex = 1;
            // 
            // btnAddSeans
            // 
            this.btnAddSeans.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.btnAddSeans.Location = new System.Drawing.Point(65, 382);
            this.btnAddSeans.Name = "btnAddSeans";
            this.btnAddSeans.Size = new System.Drawing.Size(255, 49);
            this.btnAddSeans.TabIndex = 3;
            this.btnAddSeans.Text = "Lisa seans";
            this.btnAddSeans.UseVisualStyleBackColor = true;
            this.btnAddSeans.Click += new System.EventHandler(this.btnAddSeans_Click);
            // 
            // btnDeleteSeans
            // 
            this.btnDeleteSeans.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.btnDeleteSeans.Location = new System.Drawing.Point(389, 373);
            this.btnDeleteSeans.Name = "btnDeleteSeans";
            this.btnDeleteSeans.Size = new System.Drawing.Size(264, 58);
            this.btnDeleteSeans.TabIndex = 4;
            this.btnDeleteSeans.Text = "Kustuta seans";
            this.btnDeleteSeans.UseVisualStyleBackColor = true;
            this.btnDeleteSeans.Click += new System.EventHandler(this.btnDeleteSeans_Click);
            // 
            // dgvSeanss
            // 
            this.dgvSeanss.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSeanss.Location = new System.Drawing.Point(65, 471);
            this.dgvSeanss.Name = "dgvSeanss";
            this.dgvSeanss.RowHeadersWidth = 62;
            this.dgvSeanss.RowTemplate.Height = 28;
            this.dgvSeanss.Size = new System.Drawing.Size(872, 150);
            this.dgvSeanss.TabIndex = 5;
            // 
            // lblFilm
            // 
            this.lblFilm.AutoSize = true;
            this.lblFilm.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblFilm.Location = new System.Drawing.Point(102, 67);
            this.lblFilm.Name = "lblFilm";
            this.lblFilm.Size = new System.Drawing.Size(78, 37);
            this.lblFilm.TabIndex = 6;
            this.lblFilm.Text = "Film";
            // 
            // lblSaal
            // 
            this.lblSaal.AutoSize = true;
            this.lblSaal.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblSaal.Location = new System.Drawing.Point(99, 127);
            this.lblSaal.Name = "lblSaal";
            this.lblSaal.Size = new System.Drawing.Size(81, 37);
            this.lblSaal.TabIndex = 7;
            this.lblSaal.Text = "Saal";
            // 
            // lblShowtime
            // 
            this.lblShowtime.AutoSize = true;
            this.lblShowtime.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.lblShowtime.Location = new System.Drawing.Point(92, 176);
            this.lblShowtime.Name = "lblShowtime";
            this.lblShowtime.Size = new System.Drawing.Size(88, 37);
            this.lblShowtime.TabIndex = 8;
            this.lblShowtime.Text = "Päev";
            // 
            // txtHind
            // 
            this.txtHind.Location = new System.Drawing.Point(208, 291);
            this.txtHind.Name = "txtHind";
            this.txtHind.Size = new System.Drawing.Size(214, 26);
            this.txtHind.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(97, 281);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 37);
            this.label1.TabIndex = 10;
            this.label1.Text = "Hind";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label2.Location = new System.Drawing.Point(97, 230);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 37);
            this.label2.TabIndex = 11;
            this.label2.Text = "Aeg";
            // 
            // Aegtxt
            // 
            this.Aegtxt.Location = new System.Drawing.Point(208, 241);
            this.Aegtxt.Name = "Aegtxt";
            this.Aegtxt.Size = new System.Drawing.Size(214, 26);
            this.Aegtxt.TabIndex = 12;
            // 
            // Paevtxt
            // 
            this.Paevtxt.Location = new System.Drawing.Point(208, 186);
            this.Paevtxt.Name = "Paevtxt";
            this.Paevtxt.Size = new System.Drawing.Size(214, 26);
            this.Paevtxt.TabIndex = 13;
            // 
            // SeansHaldus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 651);
            this.Controls.Add(this.Paevtxt);
            this.Controls.Add(this.Aegtxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtHind);
            this.Controls.Add(this.lblShowtime);
            this.Controls.Add(this.lblSaal);
            this.Controls.Add(this.lblFilm);
            this.Controls.Add(this.dgvSeanss);
            this.Controls.Add(this.btnDeleteSeans);
            this.Controls.Add(this.btnAddSeans);
            this.Controls.Add(this.cmbSaals);
            this.Controls.Add(this.cmbFilms);
            this.Name = "SeansHaldus";
            this.Text = "SeansHaldus";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeanss)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbFilms;
        private System.Windows.Forms.ComboBox cmbSaals;
        private System.Windows.Forms.Button btnAddSeans;
        private System.Windows.Forms.Button btnDeleteSeans;
        private System.Windows.Forms.DataGridView dgvSeanss;
        private System.Windows.Forms.Label lblFilm;
        private System.Windows.Forms.Label lblSaal;
        private System.Windows.Forms.Label lblShowtime;
        private System.Windows.Forms.TextBox txtHind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Aegtxt;
        private System.Windows.Forms.TextBox Paevtxt;
    }
}