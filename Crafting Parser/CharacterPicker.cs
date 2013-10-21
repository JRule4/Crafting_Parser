using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Crafting_Parser
{
    public partial class CharacterPicker : Form
    {
        public string characterSelected = "";

        public CharacterPicker()
        {
            InitializeComponent();
        }

        private void loadCharacterButton_Click(object sender, EventArgs e)
        {
            if (characterList.SelectedIndex > -1)
            {
                characterSelected = characterList.Items[characterList.SelectedIndex].ToString();
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void cancelLoadButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
