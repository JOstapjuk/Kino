namespace Kino.AdminActions
{
    partial class SaalHaldus
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
            this.components = new System.ComponentModel.Container();
            this.dataGridViewHalls = new System.Windows.Forms.DataGridView();
            this.saalBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.kinoDBDataSet = new Kino.KinoDBDataSet();
            this.btnViewLayout = new System.Windows.Forms.Button();
            this.saalTableAdapter = new Kino.KinoDBDataSetTableAdapters.saalTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHalls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kinoDBDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewHalls
            // 
            this.dataGridViewHalls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHalls.Location = new System.Drawing.Point(23, 25);
            this.dataGridViewHalls.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dataGridViewHalls.Name = "dataGridViewHalls";
            this.dataGridViewHalls.RowHeadersWidth = 62;
            this.dataGridViewHalls.RowTemplate.Height = 28;
            this.dataGridViewHalls.Size = new System.Drawing.Size(713, 143);
            this.dataGridViewHalls.TabIndex = 0;
            // 
            // saalBindingSource
            // 
            this.saalBindingSource.DataMember = "saal";
            this.saalBindingSource.DataSource = this.kinoDBDataSet;
            // 
            // kinoDBDataSet
            // 
            this.kinoDBDataSet.DataSetName = "KinoDBDataSet";
            this.kinoDBDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnViewLayout
            // 
            this.btnViewLayout.Location = new System.Drawing.Point(23, 194);
            this.btnViewLayout.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnViewLayout.Name = "btnViewLayout";
            this.btnViewLayout.Size = new System.Drawing.Size(118, 34);
            this.btnViewLayout.TabIndex = 1;
            this.btnViewLayout.Text = "Näita saal";
            this.btnViewLayout.UseVisualStyleBackColor = true;
            this.btnViewLayout.Click += new System.EventHandler(this.btnViewLayout_Click);
            // 
            // saalTableAdapter
            // 
            this.saalTableAdapter.ClearBeforeFill = true;
            // 
            // SaalHaldus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 463);
            this.Controls.Add(this.btnViewLayout);
            this.Controls.Add(this.dataGridViewHalls);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "SaalHaldus";
            this.Text = "SaalHaldus";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHalls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.saalBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.kinoDBDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewHalls;
        private System.Windows.Forms.Button btnViewLayout;
        private KinoDBDataSet kinoDBDataSet;
        private System.Windows.Forms.BindingSource saalBindingSource;
        private KinoDBDataSetTableAdapters.saalTableAdapter saalTableAdapter;
    }
}