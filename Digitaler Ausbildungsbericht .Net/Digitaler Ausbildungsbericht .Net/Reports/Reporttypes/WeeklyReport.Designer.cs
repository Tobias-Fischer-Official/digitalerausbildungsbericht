namespace Digitaler_Ausbildungsbericht.Net
{
    partial class WeeklyReport
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
            this.gpEdit = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tpCompany = new System.Windows.Forms.TabPage();
            this.tbxCompanyWork = new System.Windows.Forms.RichTextBox();
            this.tpTalk = new System.Windows.Forms.TabPage();
            this.tbxTalkWork = new System.Windows.Forms.RichTextBox();
            this.tpSchool = new System.Windows.Forms.TabPage();
            this.tbxSchoolWork = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxDelivered = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnClearReport = new DevComponents.DotNetBar.ButtonX();
            this.btnSaveReport = new DevComponents.DotNetBar.ButtonX();
            this.lblSaved = new DevComponents.DotNetBar.LabelX();
            this.gpEdit.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tpCompany.SuspendLayout();
            this.tpTalk.SuspendLayout();
            this.tpSchool.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gpEdit
            // 
            this.gpEdit.BackColor = System.Drawing.Color.Transparent;
            this.gpEdit.CanvasColor = System.Drawing.Color.Transparent;
            this.gpEdit.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gpEdit.Controls.Add(this.tabControl1);
            this.gpEdit.Controls.Add(this.panel1);
            this.gpEdit.Controls.Add(this.lblSaved);
            this.gpEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpEdit.Location = new System.Drawing.Point(0, 0);
            this.gpEdit.Name = "gpEdit";
            this.gpEdit.Size = new System.Drawing.Size(675, 431);
            // 
            // 
            // 
            this.gpEdit.Style.BackColor = System.Drawing.Color.Transparent;
            this.gpEdit.Style.BackColor2 = System.Drawing.Color.Transparent;
            this.gpEdit.Style.BackColorGradientAngle = 90;
            this.gpEdit.Style.BorderBottomWidth = 1;
            this.gpEdit.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gpEdit.Style.BorderLeftWidth = 1;
            this.gpEdit.Style.BorderRightWidth = 1;
            this.gpEdit.Style.BorderTopWidth = 1;
            this.gpEdit.Style.Class = "";
            this.gpEdit.Style.CornerDiameter = 4;
            this.gpEdit.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gpEdit.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gpEdit.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gpEdit.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gpEdit.StyleMouseDown.Class = "";
            this.gpEdit.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gpEdit.StyleMouseOver.Class = "";
            this.gpEdit.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gpEdit.TabIndex = 37;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tpCompany);
            this.tabControl1.Controls.Add(this.tpTalk);
            this.tabControl1.Controls.Add(this.tpSchool);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 15);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(675, 390);
            this.tabControl1.TabIndex = 43;
            // 
            // tpCompany
            // 
            this.tpCompany.Controls.Add(this.tbxCompanyWork);
            this.tpCompany.Location = new System.Drawing.Point(4, 22);
            this.tpCompany.Name = "tpCompany";
            this.tpCompany.Padding = new System.Windows.Forms.Padding(3);
            this.tpCompany.Size = new System.Drawing.Size(667, 364);
            this.tpCompany.TabIndex = 0;
            this.tpCompany.Text = "Betriebliche Tätigkeiten";
            this.tpCompany.UseVisualStyleBackColor = true;
            // 
            // tbxCompanyWork
            // 
            this.tbxCompanyWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxCompanyWork.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCompanyWork.Location = new System.Drawing.Point(3, 3);
            this.tbxCompanyWork.Name = "tbxCompanyWork";
            this.tbxCompanyWork.Size = new System.Drawing.Size(661, 358);
            this.tbxCompanyWork.TabIndex = 0;
            this.tbxCompanyWork.Text = "";
            this.tbxCompanyWork.TextChanged += new System.EventHandler(this.tbxCompanyWork_TextChanged);
            // 
            // tpTalk
            // 
            this.tpTalk.Controls.Add(this.tbxTalkWork);
            this.tpTalk.Location = new System.Drawing.Point(4, 22);
            this.tpTalk.Name = "tpTalk";
            this.tpTalk.Padding = new System.Windows.Forms.Padding(3);
            this.tpTalk.Size = new System.Drawing.Size(667, 379);
            this.tpTalk.TabIndex = 1;
            this.tpTalk.Text = "Unterweisungen/Lehrgespräche";
            this.tpTalk.UseVisualStyleBackColor = true;
            // 
            // tbxTalkWork
            // 
            this.tbxTalkWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxTalkWork.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxTalkWork.Location = new System.Drawing.Point(3, 3);
            this.tbxTalkWork.Name = "tbxTalkWork";
            this.tbxTalkWork.Size = new System.Drawing.Size(661, 373);
            this.tbxTalkWork.TabIndex = 1;
            this.tbxTalkWork.Text = "";
            this.tbxTalkWork.TextChanged += new System.EventHandler(this.tbxTalkWork_TextChanged);
            // 
            // tpSchool
            // 
            this.tpSchool.Controls.Add(this.tbxSchoolWork);
            this.tpSchool.Location = new System.Drawing.Point(4, 22);
            this.tpSchool.Name = "tpSchool";
            this.tpSchool.Size = new System.Drawing.Size(667, 379);
            this.tpSchool.TabIndex = 2;
            this.tpSchool.Text = "Unterricht in der Berufsschule";
            this.tpSchool.UseVisualStyleBackColor = true;
            // 
            // tbxSchoolWork
            // 
            this.tbxSchoolWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbxSchoolWork.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSchoolWork.Location = new System.Drawing.Point(0, 0);
            this.tbxSchoolWork.Name = "tbxSchoolWork";
            this.tbxSchoolWork.Size = new System.Drawing.Size(667, 379);
            this.tbxSchoolWork.TabIndex = 1;
            this.tbxSchoolWork.Text = "";
            this.tbxSchoolWork.TextChanged += new System.EventHandler(this.tbxSchoolWork_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbxDelivered);
            this.panel1.Controls.Add(this.btnClearReport);
            this.panel1.Controls.Add(this.btnSaveReport);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 405);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(675, 26);
            this.panel1.TabIndex = 42;
            // 
            // cbxDelivered
            // 
            this.cbxDelivered.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.cbxDelivered.BackgroundStyle.Class = "";
            this.cbxDelivered.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbxDelivered.CheckSignSize = new System.Drawing.Size(12, 12);
            this.cbxDelivered.Dock = System.Windows.Forms.DockStyle.Left;
            this.cbxDelivered.Location = new System.Drawing.Point(164, 0);
            this.cbxDelivered.Name = "cbxDelivered";
            this.cbxDelivered.Size = new System.Drawing.Size(159, 26);
            this.cbxDelivered.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxDelivered.TabIndex = 28;
            this.cbxDelivered.Text = "Bericht liegt Ausbilder vor";
            this.cbxDelivered.CheckedChanged += new System.EventHandler(this.cbxDelivered_CheckedChanged);
            // 
            // btnClearReport
            // 
            this.btnClearReport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClearReport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClearReport.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnClearReport.Location = new System.Drawing.Point(75, 0);
            this.btnClearReport.Name = "btnClearReport";
            this.btnClearReport.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlDel);
            this.btnClearReport.Size = new System.Drawing.Size(89, 26);
            this.btnClearReport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClearReport.TabIndex = 27;
            this.btnClearReport.Text = "Felder Löschen";
            this.btnClearReport.Click += new System.EventHandler(this.btnClearReport_Click);
            // 
            // btnSaveReport
            // 
            this.btnSaveReport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveReport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSaveReport.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSaveReport.Location = new System.Drawing.Point(0, 0);
            this.btnSaveReport.Margin = new System.Windows.Forms.Padding(0);
            this.btnSaveReport.Name = "btnSaveReport";
            this.btnSaveReport.Shortcuts.Add(DevComponents.DotNetBar.eShortcut.CtrlS);
            this.btnSaveReport.Size = new System.Drawing.Size(75, 26);
            this.btnSaveReport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSaveReport.TabIndex = 24;
            this.btnSaveReport.Text = "Speichern";
            this.btnSaveReport.Click += new System.EventHandler(this.btnSaveReport_Click);
            // 
            // lblSaved
            // 
            this.lblSaved.AutoSize = true;
            // 
            // 
            // 
            this.lblSaved.BackgroundStyle.Class = "";
            this.lblSaved.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSaved.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSaved.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaved.ForeColor = System.Drawing.Color.LightCyan;
            this.lblSaved.Location = new System.Drawing.Point(0, 0);
            this.lblSaved.Name = "lblSaved";
            this.lblSaved.Size = new System.Drawing.Size(144, 15);
            this.lblSaved.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.lblSaved.TabIndex = 37;
            this.lblSaved.Text = "Bericht wurde gespeichert...";
            this.lblSaved.Visible = false;
            // 
            // WeeklyReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.gpEdit);
            this.MinimumSize = new System.Drawing.Size(505, 280);
            this.Name = "WeeklyReport";
            this.Size = new System.Drawing.Size(675, 431);
            this.gpEdit.ResumeLayout(false);
            this.gpEdit.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tpCompany.ResumeLayout(false);
            this.tpTalk.ResumeLayout(false);
            this.tpSchool.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.GroupPanel gpEdit;
        private DevComponents.DotNetBar.LabelX lblSaved;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tpCompany;
        private System.Windows.Forms.RichTextBox tbxCompanyWork;
        private System.Windows.Forms.TabPage tpTalk;
        private System.Windows.Forms.RichTextBox tbxTalkWork;
        private System.Windows.Forms.TabPage tpSchool;
        private System.Windows.Forms.RichTextBox tbxSchoolWork;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxDelivered;
        private DevComponents.DotNetBar.ButtonX btnClearReport;
        private DevComponents.DotNetBar.ButtonX btnSaveReport;
    }
}
