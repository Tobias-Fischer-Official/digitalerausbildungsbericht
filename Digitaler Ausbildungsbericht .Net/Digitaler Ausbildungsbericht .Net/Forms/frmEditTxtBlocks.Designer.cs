namespace Digitaler_Ausbildungsbericht.Net
{
    partial class frmEditTxtBlocks
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditTxtBlocks));
            this.label2 = new System.Windows.Forms.Label();
            this.btnSaveBlock = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnDeleteBlock = new System.Windows.Forms.Button();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.ribbonControl1 = new DevComponents.DotNetBar.RibbonControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lbxTxtBlocks = new System.Windows.Forms.ListBox();
            this.rtbTxtBlockText = new System.Windows.Forms.RichTextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxTxtBlockName = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "                                                Textbaustein:";
            // 
            // btnSaveBlock
            // 
            this.btnSaveBlock.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnSaveBlock.Location = new System.Drawing.Point(0, 0);
            this.btnSaveBlock.Name = "btnSaveBlock";
            this.btnSaveBlock.Size = new System.Drawing.Size(123, 26);
            this.btnSaveBlock.TabIndex = 5;
            this.btnSaveBlock.Text = "Baustein speichern";
            this.btnSaveBlock.UseVisualStyleBackColor = true;
            this.btnSaveBlock.Click += new System.EventHandler(this.btnSaveBlock_Click);
            // 
            // btnExit
            // 
            this.btnExit.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnExit.Location = new System.Drawing.Point(0, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(123, 26);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "Bearbeitung beenden";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnDeleteBlock
            // 
            this.btnDeleteBlock.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDeleteBlock.Location = new System.Drawing.Point(153, 0);
            this.btnDeleteBlock.Name = "btnDeleteBlock";
            this.btnDeleteBlock.Size = new System.Drawing.Size(123, 26);
            this.btnDeleteBlock.TabIndex = 7;
            this.btnDeleteBlock.Text = "Baustein löschen";
            this.btnDeleteBlock.UseVisualStyleBackColor = true;
            this.btnDeleteBlock.Click += new System.EventHandler(this.btnDeleteBlock_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Black;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(26))))));
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.AutoExpand = false;
            // 
            // 
            // 
            this.ribbonControl1.BackgroundStyle.Class = "";
            this.ribbonControl1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.ribbonControl1.CaptionVisible = true;
            this.ribbonControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ribbonControl1.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
            this.ribbonControl1.Location = new System.Drawing.Point(5, 1);
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 2);
            this.ribbonControl1.Size = new System.Drawing.Size(426, 30);
            this.ribbonControl1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ribbonControl1.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
            this.ribbonControl1.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
            this.ribbonControl1.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
            this.ribbonControl1.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
            this.ribbonControl1.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
            this.ribbonControl1.SystemText.QatDialogAddButton = "&Add >>";
            this.ribbonControl1.SystemText.QatDialogCancelButton = "Cancel";
            this.ribbonControl1.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
            this.ribbonControl1.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
            this.ribbonControl1.SystemText.QatDialogOkButton = "OK";
            this.ribbonControl1.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
            this.ribbonControl1.SystemText.QatDialogRemoveButton = "&Remove";
            this.ribbonControl1.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
            this.ribbonControl1.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
            this.ribbonControl1.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
            this.ribbonControl1.TabGroupHeight = 14;
            this.ribbonControl1.TabIndex = 8;
            this.ribbonControl1.Text = "ribbonControl1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSaveBlock);
            this.panel1.Controls.Add(this.btnDeleteBlock);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(150, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(276, 26);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(5, 581);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(426, 26);
            this.panel2.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.splitContainer1);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(5, 31);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(426, 550);
            this.panel3.TabIndex = 11;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 13);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lbxTxtBlocks);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rtbTxtBlockText);
            this.splitContainer1.Size = new System.Drawing.Size(426, 516);
            this.splitContainer1.SplitterDistance = 142;
            this.splitContainer1.TabIndex = 6;
            // 
            // lbxTxtBlocks
            // 
            this.lbxTxtBlocks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxTxtBlocks.FormattingEnabled = true;
            this.lbxTxtBlocks.Location = new System.Drawing.Point(0, 0);
            this.lbxTxtBlocks.Name = "lbxTxtBlocks";
            this.lbxTxtBlocks.Size = new System.Drawing.Size(142, 516);
            this.lbxTxtBlocks.TabIndex = 6;
            this.lbxTxtBlocks.SelectedIndexChanged += new System.EventHandler(this.lbxTxtBlocks_SelectedIndexChanged);
            // 
            // rtbTxtBlockText
            // 
            this.rtbTxtBlockText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbTxtBlockText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbTxtBlockText.Location = new System.Drawing.Point(0, 0);
            this.rtbTxtBlockText.Name = "rtbTxtBlockText";
            this.rtbTxtBlockText.Size = new System.Drawing.Size(280, 516);
            this.rtbTxtBlockText.TabIndex = 7;
            this.rtbTxtBlockText.Text = resources.GetString("rtbTxtBlockText.Text");
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.tbxTxtBlockName);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 529);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(426, 21);
            this.panel4.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Name des Textbausteins:";
            // 
            // tbxTxtBlockName
            // 
            this.tbxTxtBlockName.Dock = System.Windows.Forms.DockStyle.Right;
            this.tbxTxtBlockName.Location = new System.Drawing.Point(233, 0);
            this.tbxTxtBlockName.Name = "tbxTxtBlockName";
            this.tbxTxtBlockName.Size = new System.Drawing.Size(193, 20);
            this.tbxTxtBlockName.TabIndex = 1;
            // 
            // frmEditTxtBlocks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 609);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.ribbonControl1);
            this.EnableGlass = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEditTxtBlocks";
            this.Text = "Textbausteine Editieren";
            this.Load += new System.EventHandler(this.frmEditTxtBlocks_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSaveBlock;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnDeleteBlock;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.RibbonControl ribbonControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lbxTxtBlocks;
        private System.Windows.Forms.RichTextBox rtbTxtBlockText;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxTxtBlockName;
    }
}