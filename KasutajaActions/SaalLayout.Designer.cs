namespace Kino.KasutajaActions
{
    partial class SaalLayout
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
            this.flowLayoutPanelSeats = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnReserveSeats = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // flowLayoutPanelSeats
            // 
            this.flowLayoutPanelSeats.Location = new System.Drawing.Point(202, 112);
            this.flowLayoutPanelSeats.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.flowLayoutPanelSeats.Name = "flowLayoutPanelSeats";
            this.flowLayoutPanelSeats.Size = new System.Drawing.Size(720, 480);
            this.flowLayoutPanelSeats.TabIndex = 0;
            // 
            // BtnReserveSeats
            // 
            this.BtnReserveSeats.Location = new System.Drawing.Point(994, 617);
            this.BtnReserveSeats.Name = "BtnReserveSeats";
            this.BtnReserveSeats.Size = new System.Drawing.Size(114, 46);
            this.BtnReserveSeats.TabIndex = 1;
            this.BtnReserveSeats.Text = "Reserveeri";
            this.BtnReserveSeats.UseVisualStyleBackColor = true;
            this.BtnReserveSeats.Click += new System.EventHandler(this.BtnReserveSeats_Click);
            // 
            // SaalLayout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.BtnReserveSeats);
            this.Controls.Add(this.flowLayoutPanelSeats);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SaalLayout";
            this.Text = "SaalLayout";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSeats;
        private System.Windows.Forms.Button BtnReserveSeats;
    }
}