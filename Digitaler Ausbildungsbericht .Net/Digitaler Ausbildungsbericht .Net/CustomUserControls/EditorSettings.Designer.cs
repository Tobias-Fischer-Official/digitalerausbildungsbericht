namespace Digitaler_Ausbildungsbericht.Net
{
    partial class EditorSettings
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.tbxPDFFolder = new System.Windows.Forms.TextBox();
            this.btnChangePDFDir = new System.Windows.Forms.Button();
            this.cbxSelectReporttype = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbxAutoupdate = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblPreview = new System.Windows.Forms.Label();
            this.tbxFilenames = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Speicherort PDF-Dateien";
            // 
            // tbxPDFFolder
            // 
            this.tbxPDFFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxPDFFolder.Location = new System.Drawing.Point(134, 3);
            this.tbxPDFFolder.Name = "tbxPDFFolder";
            this.tbxPDFFolder.Size = new System.Drawing.Size(282, 20);
            this.tbxPDFFolder.TabIndex = 4;
            this.tbxPDFFolder.Text = "Anwendungsverzeichnis --> PDF";
            // 
            // btnChangePDFDir
            // 
            this.btnChangePDFDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangePDFDir.Location = new System.Drawing.Point(422, 1);
            this.btnChangePDFDir.Name = "btnChangePDFDir";
            this.btnChangePDFDir.Size = new System.Drawing.Size(75, 23);
            this.btnChangePDFDir.TabIndex = 6;
            this.btnChangePDFDir.Text = "Ändern";
            this.btnChangePDFDir.UseVisualStyleBackColor = true;
            this.btnChangePDFDir.Click += new System.EventHandler(this.btnChangePDFDir_Click);
            // 
            // cbxSelectReporttype
            // 
            this.cbxSelectReporttype.BackColor = System.Drawing.Color.White;
            this.cbxSelectReporttype.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cbxSelectReporttype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSelectReporttype.FormattingEnabled = true;
            this.cbxSelectReporttype.Items.AddRange(new object[] {
            "Tägliche Berichte",
            "Tägliche Berichte + Samstags",
            "Wöchentliche Berichte"});
            this.cbxSelectReporttype.Location = new System.Drawing.Point(0, 13);
            this.cbxSelectReporttype.Name = "cbxSelectReporttype";
            this.cbxSelectReporttype.Size = new System.Drawing.Size(500, 21);
            this.cbxSelectReporttype.TabIndex = 4;
            this.cbxSelectReporttype.SelectedIndexChanged += new System.EventHandler(this.cbxSelectReporttype_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Vorlage für Berichte wählen";
            // 
            // cbxAutoupdate
            // 
            this.cbxAutoupdate.AutoSize = true;
            this.cbxAutoupdate.Checked = true;
            this.cbxAutoupdate.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxAutoupdate.Dock = System.Windows.Forms.DockStyle.Top;
            this.cbxAutoupdate.Location = new System.Drawing.Point(0, 0);
            this.cbxAutoupdate.Name = "cbxAutoupdate";
            this.cbxAutoupdate.Size = new System.Drawing.Size(500, 17);
            this.cbxAutoupdate.TabIndex = 3;
            this.cbxAutoupdate.Text = "Automatisches Update";
            this.cbxAutoupdate.UseVisualStyleBackColor = true;
            this.cbxAutoupdate.CheckedChanged += new System.EventHandler(this.cbxAutoupdate_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cbxSelectReporttype);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(500, 34);
            this.panel2.TabIndex = 18;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.Controls.Add(this.lblPreview);
            this.panel3.Controls.Add(this.tbxFilenames);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.btnChangePDFDir);
            this.panel3.Controls.Add(this.tbxPDFFolder);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 51);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(500, 66);
            this.panel3.TabIndex = 19;
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Location = new System.Drawing.Point(134, 53);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(52, 13);
            this.lblPreview.TabIndex = 11;
            this.lblPreview.Text = "Vorschau";
            // 
            // tbxFilenames
            // 
            this.tbxFilenames.Location = new System.Drawing.Point(134, 29);
            this.tbxFilenames.MaxLength = 120;
            this.tbxFilenames.Name = "tbxFilenames";
            this.tbxFilenames.Size = new System.Drawing.Size(282, 20);
            this.tbxFilenames.TabIndex = 10;
            this.tbxFilenames.Text = "Bericht {AW} von {WS} - {WE}";
            this.tbxFilenames.TextChanged += new System.EventHandler(this.tbxFilenames_TextChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(422, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "Hilfe";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Dateinamen";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSave);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 117);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 24);
            this.panel1.TabIndex = 20;
            // 
            // btnSave
            // 
            this.btnSave.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSave.Location = new System.Drawing.Point(425, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 24);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "Speichern";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnCancel.Location = new System.Drawing.Point(0, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 24);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Abbrechen";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // EditorSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.cbxAutoupdate);
            this.Name = "EditorSettings";
            this.Size = new System.Drawing.Size(500, 141);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxPDFFolder;
        private System.Windows.Forms.Button btnChangePDFDir;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbxAutoupdate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Button btnSave;
        internal System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxFilenames;
        public System.Windows.Forms.ComboBox cbxSelectReporttype;
        private System.Windows.Forms.Label lblPreview;
    }
}
