namespace Kino
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
            this.SuspendLayout();
            // 
            // flowLayoutPanelSeats
            // 
            this.flowLayoutPanelSeats.Location = new System.Drawing.Point(31, 33);
            this.flowLayoutPanelSeats.Name = "flowLayoutPanelSeats";
            this.flowLayoutPanelSeats.Size = new System.Drawing.Size(995, 616);
            this.flowLayoutPanelSeats.TabIndex = 0;
            // 
            // SeatLayoutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 699);
            this.Controls.Add(this.flowLayoutPanelSeats);
            this.Name = "SeatLayoutForm";
            this.Text = "SeatLayoutForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelSeats;
    }
}