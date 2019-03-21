namespace Digitaler_Ausbildungsbericht.Net
{
    partial class frmExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExport));
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbxDates = new System.Windows.Forms.ListBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnSelectSchool = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeselectAll = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbxLog = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tcImExport = new System.Windows.Forms.TabControl();
            this.tabExp = new System.Windows.Forms.TabPage();
            this.tbxExportPath = new System.Windows.Forms.TextBox();
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.tabImp = new System.Windows.Forms.TabPage();
            this.cbxOverwrite = new System.Windows.Forms.CheckBox();
            this.tbxImportPath = new System.Windows.Forms.TextBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tcImExport.SuspendLayout();
            this.tabExp.SuspendLayout();
            this.tabImp.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(44, 19);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(112, 20);
            this.dtpStartDate.TabIndex = 0;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.dtpStartDate_ValueChanged);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(44, 44);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(112, 20);
            this.dtpEndDate.TabIndex = 1;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Bis:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Von:";
            // 
            // lbxDates
            // 
            this.lbxDates.FormattingEnabled = true;
            this.lbxDates.Location = new System.Drawing.Point(175, 6);
            this.lbxDates.Name = "lbxDates";
            this.lbxDates.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbxDates.Size = new System.Drawing.Size(135, 238);
            this.lbxDates.TabIndex = 4;
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(9, 70);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(147, 23);
            this.btnSelectAll.TabIndex = 5;
            this.btnSelectAll.Text = "Alle Berichte";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnSelectSchool
            // 
            this.btnSelectSchool.Location = new System.Drawing.Point(9, 99);
            this.btnSelectSchool.Name = "btnSelectSchool";
            this.btnSelectSchool.Size = new System.Drawing.Size(147, 23);
            this.btnSelectSchool.TabIndex = 7;
            this.btnSelectSchool.Text = "Alle Schulberichte";
            this.btnSelectSchool.UseVisualStyleBackColor = true;
            this.btnSelectSchool.Click += new System.EventHandler(this.btnSelectSchool_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeselectAll);
            this.groupBox1.Controls.Add(this.dtpStartDate);
            this.groupBox1.Controls.Add(this.btnSelectSchool);
            this.groupBox1.Controls.Add(this.dtpEndDate);
            this.groupBox1.Controls.Add(this.btnSelectAll);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(162, 175);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zeitraum wählen";
            // 
            // btnDeselectAll
            // 
            this.btnDeselectAll.Location = new System.Drawing.Point(8, 128);
            this.btnDeselectAll.Name = "btnDeselectAll";
            this.btnDeselectAll.Size = new System.Drawing.Size(147, 23);
            this.btnDeselectAll.TabIndex = 8;
            this.btnDeselectAll.Text = "Alles abwählen";
            this.btnDeselectAll.UseVisualStyleBackColor = true;
            this.btnDeselectAll.Click += new System.EventHandler(this.btnDeselectAll_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(12, 367);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(264, 367);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbxLog
            // 
            this.tbxLog.Location = new System.Drawing.Point(12, 311);
            this.tbxLog.Multiline = true;
            this.tbxLog.Name = "tbxLog";
            this.tbxLog.Size = new System.Drawing.Size(327, 50);
            this.tbxLog.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 295);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Log:";
            // 
            // tcImExport
            // 
            this.tcImExport.Controls.Add(this.tabExp);
            this.tcImExport.Controls.Add(this.tabImp);
            this.tcImExport.Location = new System.Drawing.Point(12, 12);
            this.tcImExport.Name = "tcImExport";
            this.tcImExport.SelectedIndex = 0;
            this.tcImExport.Size = new System.Drawing.Size(327, 278);
            this.tcImExport.TabIndex = 15;
            // 
            // tabExp
            // 
            this.tabExp.Controls.Add(this.tbxExportPath);
            this.tabExp.Controls.Add(this.btnSelectPath);
            this.tabExp.Controls.Add(this.lbxDates);
            this.tabExp.Controls.Add(this.groupBox1);
            this.tabExp.Location = new System.Drawing.Point(4, 22);
            this.tabExp.Name = "tabExp";
            this.tabExp.Padding = new System.Windows.Forms.Padding(3);
            this.tabExp.Size = new System.Drawing.Size(319, 252);
            this.tabExp.TabIndex = 0;
            this.tabExp.Text = "Export";
            this.tabExp.UseVisualStyleBackColor = true;
            // 
            // tbxExportPath
            // 
            this.tbxExportPath.Location = new System.Drawing.Point(7, 216);
            this.tbxExportPath.Name = "tbxExportPath";
            this.tbxExportPath.ReadOnly = true;
            this.tbxExportPath.Size = new System.Drawing.Size(162, 20);
            this.tbxExportPath.TabIndex = 14;
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(7, 187);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(162, 23);
            this.btnSelectPath.TabIndex = 13;
            this.btnSelectPath.Text = "Ausgabepfad wählen";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // tabImp
            // 
            this.tabImp.Controls.Add(this.cbxOverwrite);
            this.tabImp.Controls.Add(this.tbxImportPath);
            this.tabImp.Controls.Add(this.btnSelectFile);
            this.tabImp.Location = new System.Drawing.Point(4, 22);
            this.tabImp.Name = "tabImp";
            this.tabImp.Padding = new System.Windows.Forms.Padding(3);
            this.tabImp.Size = new System.Drawing.Size(319, 252);
            this.tabImp.TabIndex = 1;
            this.tabImp.Text = "Import";
            this.tabImp.UseVisualStyleBackColor = true;
            // 
            // cbxOverwrite
            // 
            this.cbxOverwrite.AutoSize = true;
            this.cbxOverwrite.Location = new System.Drawing.Point(6, 35);
            this.cbxOverwrite.Name = "cbxOverwrite";
            this.cbxOverwrite.Size = new System.Drawing.Size(196, 17);
            this.cbxOverwrite.TabIndex = 2;
            this.cbxOverwrite.Text = "Vorhandene Berichte überschreiben";
            this.cbxOverwrite.UseVisualStyleBackColor = true;
            // 
            // tbxImportPath
            // 
            this.tbxImportPath.Location = new System.Drawing.Point(120, 8);
            this.tbxImportPath.Name = "tbxImportPath";
            this.tbxImportPath.ReadOnly = true;
            this.tbxImportPath.Size = new System.Drawing.Size(110, 20);
            this.tbxImportPath.TabIndex = 1;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(6, 6);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(108, 23);
            this.btnSelectFile.TabIndex = 0;
            this.btnSelectFile.Text = "Datei Wählen";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // frmExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 398);
            this.Controls.Add(this.tcImExport);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbxLog);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStart);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExport";
            this.Text = "Daten Exportieren/Importieren";
            this.Load += new System.EventHandler(this.frmExport_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tcImExport.ResumeLayout(false);
            this.tabExp.ResumeLayout(false);
            this.tabExp.PerformLayout();
            this.tabImp.ResumeLayout(false);
            this.tabImp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbxDates;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnSelectSchool;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbxLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tcImExport;
        private System.Windows.Forms.TabPage tabExp;
        private System.Windows.Forms.TabPage tabImp;
        private System.Windows.Forms.TextBox tbxImportPath;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.TextBox tbxExportPath;
        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.Button btnDeselectAll;
        private System.Windows.Forms.CheckBox cbxOverwrite;
    }
}