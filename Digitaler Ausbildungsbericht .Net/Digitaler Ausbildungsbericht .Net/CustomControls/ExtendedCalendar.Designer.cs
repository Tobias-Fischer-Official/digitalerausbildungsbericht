namespace Digitaler_Ausbildungsbericht.Net
{
    partial class ExtendedCalendar
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblYear = new System.Windows.Forms.Label();
            this.lblYearFwd = new System.Windows.Forms.Label();
            this.lblYearBack = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lblMonth = new System.Windows.Forms.Label();
            this.lblMonthFwd = new System.Windows.Forms.Label();
            this.lblMonthBack = new System.Windows.Forms.Label();
            this.grpControls = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.MaximumSize = new System.Drawing.Size(0, 20);
            this.panel1.MinimumSize = new System.Drawing.Size(0, 20);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 20);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.lblYear);
            this.panel2.Controls.Add(this.lblYearFwd);
            this.panel2.Controls.Add(this.lblYearBack);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(188, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(80, 20);
            this.panel2.TabIndex = 4;
            // 
            // lblYear
            // 
            this.lblYear.BackColor = System.Drawing.Color.White;
            this.lblYear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblYear.Location = new System.Drawing.Point(16, 0);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(48, 20);
            this.lblYear.TabIndex = 6;
            this.lblYear.Text = "Jahr";
            this.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblYearFwd
            // 
            this.lblYearFwd.BackColor = System.Drawing.Color.White;
            this.lblYearFwd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblYearFwd.Font = new System.Drawing.Font("Wingdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lblYearFwd.Location = new System.Drawing.Point(64, 0);
            this.lblYearFwd.Name = "lblYearFwd";
            this.lblYearFwd.Size = new System.Drawing.Size(16, 20);
            this.lblYearFwd.TabIndex = 5;
            this.lblYearFwd.Text = "è";
            this.lblYearFwd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblYearBack
            // 
            this.lblYearBack.BackColor = System.Drawing.Color.White;
            this.lblYearBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblYearBack.Font = new System.Drawing.Font("Wingdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lblYearBack.Location = new System.Drawing.Point(0, 0);
            this.lblYearBack.Name = "lblYearBack";
            this.lblYearBack.Size = new System.Drawing.Size(16, 20);
            this.lblYearBack.TabIndex = 4;
            this.lblYearBack.Text = "ç";
            this.lblYearBack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.lblMonth);
            this.panel3.Controls.Add(this.lblMonthFwd);
            this.panel3.Controls.Add(this.lblMonthBack);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(124, 20);
            this.panel3.TabIndex = 3;
            // 
            // lblMonth
            // 
            this.lblMonth.BackColor = System.Drawing.Color.White;
            this.lblMonth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMonth.Location = new System.Drawing.Point(16, 0);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(92, 20);
            this.lblMonth.TabIndex = 6;
            this.lblMonth.Text = "Monatsname";
            this.lblMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMonthFwd
            // 
            this.lblMonthFwd.BackColor = System.Drawing.Color.White;
            this.lblMonthFwd.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblMonthFwd.Font = new System.Drawing.Font("Wingdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lblMonthFwd.Location = new System.Drawing.Point(108, 0);
            this.lblMonthFwd.Name = "lblMonthFwd";
            this.lblMonthFwd.Size = new System.Drawing.Size(16, 20);
            this.lblMonthFwd.TabIndex = 5;
            this.lblMonthFwd.Text = "è";
            this.lblMonthFwd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMonthBack
            // 
            this.lblMonthBack.BackColor = System.Drawing.Color.White;
            this.lblMonthBack.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMonthBack.Font = new System.Drawing.Font("Wingdings", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.lblMonthBack.Location = new System.Drawing.Point(0, 0);
            this.lblMonthBack.Name = "lblMonthBack";
            this.lblMonthBack.Size = new System.Drawing.Size(16, 20);
            this.lblMonthBack.TabIndex = 3;
            this.lblMonthBack.Text = "ç";
            this.lblMonthBack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpControls
            // 
            this.grpControls.AutoSize = true;
            this.grpControls.BackColor = System.Drawing.Color.White;
            this.grpControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpControls.Location = new System.Drawing.Point(0, 20);
            this.grpControls.Name = "grpControls";
            this.grpControls.Size = new System.Drawing.Size(268, 124);
            this.grpControls.TabIndex = 5;
            // 
            // ExtendedCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.grpControls);
            this.Controls.Add(this.panel1);
            this.Name = "ExtendedCalendar";
            this.Size = new System.Drawing.Size(268, 144);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblYearFwd;
        private System.Windows.Forms.Label lblYearBack;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblMonth;
        private System.Windows.Forms.Label lblMonthFwd;
        private System.Windows.Forms.Label lblMonthBack;
        private System.Windows.Forms.Panel grpControls;

    }
}
