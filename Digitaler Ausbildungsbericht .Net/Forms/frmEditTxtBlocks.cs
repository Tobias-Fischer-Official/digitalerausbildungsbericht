using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace Digitaler_Ausbildungsbericht.Net
{
    public partial class frmEditTxtBlocks : Office2007RibbonForm
    {
        public frmEditTxtBlocks()
        {
            InitializeComponent();
        }

        private Dictionary<string, string> _blocks = new Dictionary<string, string>();
        private string _selectedBlockName = string.Empty;

        private void frmEditTxtBlocks_Load(object sender, EventArgs e)
        {
            if (DataManager.LoadTxtBlocks() != null)
            {
                _blocks = DataManager.LoadTxtBlocks();
                foreach (string s in _blocks.Keys)
                    lbxTxtBlocks.Items.Add(s);
            }
        }

        private void btnSaveBlock_Click(object sender, EventArgs e)
        {
            if (rtbTxtBlockText.Text != string.Empty && tbxTxtBlockName.Text != string.Empty)
            {
                if (!_blocks.ContainsKey(tbxTxtBlockName.Text))
                    _blocks.Add(tbxTxtBlockName.Text, rtbTxtBlockText.Rtf);
                else
                {
                    _blocks[tbxTxtBlockName.Text] = rtbTxtBlockText.Rtf;
                }
                if (!lbxTxtBlocks.Items.Contains(tbxTxtBlockName.Text))
                    lbxTxtBlocks.Items.Add(tbxTxtBlockName.Text);
                DataManager.SaveTxtBlocks(_blocks);
                lbxTxtBlocks.SelectedItem = tbxTxtBlockName.Text;
            }
            else if (rtbTxtBlockText.Text == string.Empty)
                MessageBox.Show("Bitte einen Text eingeben.", "Fehler");
            else if (tbxTxtBlockName.Text != string.Empty)
                MessageBox.Show("Bitte einen Text eingeben.", "Fehler");
        }

        private void btnDeleteBlock_Click(object sender, EventArgs e)
        {
            _blocks.Remove(_selectedBlockName);
            lbxTxtBlocks.Items.Remove(_selectedBlockName);
            DataManager.SaveTxtBlocks(_blocks);
            rtbTxtBlockText.ResetText();
        }

        private void lbxTxtBlocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxTxtBlocks.Text != string.Empty)
            {
                _selectedBlockName = lbxTxtBlocks.Text;
                rtbTxtBlockText.Rtf = _blocks[lbxTxtBlocks.Text];
                tbxTxtBlockName.Text = lbxTxtBlocks.Text;
            }
            else if (lbxTxtBlocks.Items.Count > 0)
                lbxTxtBlocks.SelectedIndex = 0;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DataManager.SaveTxtBlocks(_blocks);
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

    }
}
