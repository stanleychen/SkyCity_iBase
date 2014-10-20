namespace iTrak.ImporterMain
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label_Message = new System.Windows.Forms.Label();
            this.listBox_Status = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label_Statistics = new System.Windows.Forms.Label();
            this.label_TotalRows = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.testConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPerspective = new System.Windows.Forms.Button();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.btnIncident = new System.Windows.Forms.Button();
            this.btnConvertSubject = new System.Windows.Forms.Button();
            this.btnFinalize = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 445);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(577, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // label_Message
            // 
            this.label_Message.AutoSize = true;
            this.label_Message.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label_Message.Location = new System.Drawing.Point(0, 432);
            this.label_Message.Name = "label_Message";
            this.label_Message.Size = new System.Drawing.Size(0, 13);
            this.label_Message.TabIndex = 3;
            // 
            // listBox_Status
            // 
            this.listBox_Status.FormattingEnabled = true;
            this.listBox_Status.Location = new System.Drawing.Point(25, 86);
            this.listBox_Status.Name = "listBox_Status";
            this.listBox_Status.Size = new System.Drawing.Size(254, 303);
            this.listBox_Status.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Status";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(311, 43);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(254, 343);
            this.dataGridView1.TabIndex = 14;
            // 
            // label_Statistics
            // 
            this.label_Statistics.AutoSize = true;
            this.label_Statistics.Location = new System.Drawing.Point(308, 27);
            this.label_Statistics.Name = "label_Statistics";
            this.label_Statistics.Size = new System.Drawing.Size(78, 13);
            this.label_Statistics.TabIndex = 15;
            this.label_Statistics.Text = "Imported Rows";
            // 
            // label_TotalRows
            // 
            this.label_TotalRows.AutoSize = true;
            this.label_TotalRows.Location = new System.Drawing.Point(308, 389);
            this.label_TotalRows.Name = "label_TotalRows";
            this.label_TotalRows.Size = new System.Drawing.Size(31, 13);
            this.label_TotalRows.TabIndex = 16;
            this.label_TotalRows.Text = "Total";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(577, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testConfigurationToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // testConfigurationToolStripMenuItem
            // 
            this.testConfigurationToolStripMenuItem.Name = "testConfigurationToolStripMenuItem";
            this.testConfigurationToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.testConfigurationToolStripMenuItem.Text = "Test Configuration";
            this.testConfigurationToolStripMenuItem.Click += new System.EventHandler(this.testConfigurationToolStripMenuItem_Click);
            // 
            // btnPerspective
            // 
            this.btnPerspective.Location = new System.Drawing.Point(25, 27);
            this.btnPerspective.Name = "btnPerspective";
            this.btnPerspective.Size = new System.Drawing.Size(254, 25);
            this.btnPerspective.TabIndex = 21;
            this.btnPerspective.Text = "Import Data";
            this.btnPerspective.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPerspective.UseVisualStyleBackColor = true;
            this.btnPerspective.Click += new System.EventHandler(this.btnImportData_Click);
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(232, 416);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(238, 23);
            this.btnLoadData.TabIndex = 22;
            this.btnLoadData.Text = "1. Load Data";
            this.btnLoadData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Visible = false;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // btnIncident
            // 
            this.btnIncident.Location = new System.Drawing.Point(311, 405);
            this.btnIncident.Name = "btnIncident";
            this.btnIncident.Size = new System.Drawing.Size(238, 23);
            this.btnIncident.TabIndex = 23;
            this.btnIncident.Text = "2. Convert Incident";
            this.btnIncident.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIncident.UseVisualStyleBackColor = true;
            this.btnIncident.Visible = false;
            this.btnIncident.Click += new System.EventHandler(this.btnIncident_Click);
            // 
            // btnConvertSubject
            // 
            this.btnConvertSubject.Location = new System.Drawing.Point(25, 414);
            this.btnConvertSubject.Name = "btnConvertSubject";
            this.btnConvertSubject.Size = new System.Drawing.Size(238, 23);
            this.btnConvertSubject.TabIndex = 24;
            this.btnConvertSubject.Text = "3. Convert Subject";
            this.btnConvertSubject.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConvertSubject.UseVisualStyleBackColor = true;
            this.btnConvertSubject.Visible = false;
            this.btnConvertSubject.Click += new System.EventHandler(this.btnConvertSubject_Click);
            // 
            // btnFinalize
            // 
            this.btnFinalize.Location = new System.Drawing.Point(286, 414);
            this.btnFinalize.Name = "btnFinalize";
            this.btnFinalize.Size = new System.Drawing.Size(238, 23);
            this.btnFinalize.TabIndex = 26;
            this.btnFinalize.Text = "4. Finalize";
            this.btnFinalize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFinalize.UseVisualStyleBackColor = true;
            this.btnFinalize.Visible = false;
            this.btnFinalize.Click += new System.EventHandler(this.btnFinalize_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 468);
            this.Controls.Add(this.btnFinalize);
            this.Controls.Add(this.btnConvertSubject);
            this.Controls.Add(this.btnIncident);
            this.Controls.Add(this.btnLoadData);
            this.Controls.Add(this.btnPerspective);
            this.Controls.Add(this.label_TotalRows);
            this.Controls.Add(this.label_Statistics);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox_Status);
            this.Controls.Add(this.label_Message);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Importer Main";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label_Message;
        private System.Windows.Forms.ListBox listBox_Status;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label_Statistics;
        private System.Windows.Forms.Label label_TotalRows;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem testConfigurationToolStripMenuItem;
        private System.Windows.Forms.Button btnPerspective;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.Button btnIncident;
        private System.Windows.Forms.Button btnConvertSubject;
        private System.Windows.Forms.Button btnFinalize;
    }
}

