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
            this.Valjuda = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHalls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.saalBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.kinoDBDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewHalls
            // 
            this.dataGridViewHalls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHalls.Location = new System.Drawing.Point(34, 38);
            this.dataGridViewHalls.Name = "dataGridViewHalls";
            this.dataGridViewHalls.RowHeadersWidth = 62;
            this.dataGridViewHalls.RowTemplate.Height = 28;
            this.dataGridViewHalls.Size = new System.Drawing.Size(1070, 220);
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
            this.btnViewLayout.Location = new System.Drawing.Point(34, 298);
            this.btnViewLayout.Name = "btnViewLayout";
            this.btnViewLayout.Size = new System.Drawing.Size(177, 52);
            this.btnViewLayout.TabIndex = 1;
            this.btnViewLayout.Text = "Näita saal";
            this.btnViewLayout.UseVisualStyleBackColor = true;
            this.btnViewLayout.Click += new System.EventHandler(this.btnViewLayout_Click);
            // 
            // saalTableAdapter
            // 
            this.saalTableAdapter.ClearBeforeFill = true;
            // 
            // Valjuda
            // 
            this.Valjuda.Location = new System.Drawing.Point(247, 303);
            this.Valjuda.Name = "Valjuda";
            this.Valjuda.Size = new System.Drawing.Size(145, 47);
            this.Valjuda.TabIndex = 2;
            this.Valjuda.Text = "Väljuda";
            this.Valjuda.UseVisualStyleBackColor = true;
            this.Valjuda.Click += new System.EventHandler(this.Valjuda_Click);
            // 
            // SaalHaldus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1215, 712);
            this.Controls.Add(this.Valjuda);
            this.Controls.Add(this.btnViewLayout);
            this.Controls.Add(this.dataGridViewHalls);
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
        private System.Windows.Forms.Button Valjuda;
    }
}