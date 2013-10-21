using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;

namespace Crafting_Parser
{
    public partial class CraftingDataDisplay : Form
    {
        CraftingParser craftingParser = new CraftingParser();
        readonly string ffCharactersPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\My Games\\FINAL FANTASY XIV - A Realm Reborn\\";

        public CraftingDataDisplay()
        {
            InitializeComponent();

            dateFilterStart.MaxDate = DateTime.Now;
            dateFilterStart.Value = DateTime.Now.AddDays(-14);
            dateFilterEnd.MaxDate = DateTime.Now;

            craftingParser.Init(dateTreeView, itemTypeTreeView, ffCharactersPath);
        }

        private void FilePicker_Click(object sender, EventArgs e)
        {
            CharacterPicker picker = new CharacterPicker();


            picker.DesktopLocation = DesktopLocation;
            picker.characterList.BeginUpdate();
            foreach (string key in craftingParser.CharacterLogs.Keys)
            {
                picker.characterList.Items.Add(key);
            }
            
            picker.characterList.EndUpdate();

            if (picker.ShowDialog(this) == DialogResult.OK)
            {
                if (craftingParser.CharacterLogs.ContainsKey(picker.characterSelected))
                {
                    Thread parseThread = new Thread(new ParameterizedThreadStart(ParseDirectory));
                    parseThread.Start(craftingParser.CharacterLogs[picker.characterSelected]);
                }
            }

            return;
        }

        private void directorySelectButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFileDialog1 = new FolderBrowserDialog();

            if (Directory.Exists(ffCharactersPath))
            {
                openFileDialog1.SelectedPath = ffCharactersPath;
            }
            else
            {
                openFileDialog1.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Thread parseThread = new Thread(new ParameterizedThreadStart(ParseDirectory));
                parseThread.Start(openFileDialog1.SelectedPath);
            }
        }

        private void useDateFilter_CheckedChanged(object sender, EventArgs e)
        {
            dateGroup.Enabled = useDateFilter.Checked;
        }

        void ParseDirectory(object path)
        {
            string pathString = path as string;

            //Ugly anonymous function delegates just used to turn all these objects off and back on.
            dateGroup.Invoke(((MethodInvoker)delegate() { dateGroup.Enabled = false; }));
            useDateFilter.Invoke(((MethodInvoker)delegate() { useDateFilter.Enabled = false; }));
            characterSelectButton.Invoke(((MethodInvoker)delegate() { characterSelectButton.Enabled = false; }));
            directorySelectButton.Invoke(((MethodInvoker)delegate() { directorySelectButton.Enabled = false; }));
           ;
            parseProgress.Invoke(((MethodInvoker)delegate() 
            {
                parseProgress.Value = 0;
                parseProgress.Visible = true;
            }));

            dataDumpBox.Text = craftingParser.ParseLogsFromDirectory(pathString, useDateFilter.Checked, dateFilterStart.Value, dateFilterEnd.Value, parseProgress);

            //Ugly anonymous function delegates just used to turn all these objects off and back on.
            dateGroup.Invoke(((MethodInvoker)delegate() { dateGroup.Enabled = useDateFilter.Checked; }));
            useDateFilter.Invoke(((MethodInvoker)delegate() { useDateFilter.Enabled = true; }));
            characterSelectButton.Invoke(((MethodInvoker)delegate() { characterSelectButton.Enabled = true; }));
            directorySelectButton.Invoke(((MethodInvoker)delegate() { directorySelectButton.Enabled = true; }));
            parseProgress.Invoke(((MethodInvoker)delegate() { parseProgress.Visible = false; }));
        }

        private void dateFilterStart_ValueChanged(object sender, EventArgs e)
        {
            dateFilterStart.Value = new DateTime(dateFilterStart.Value.Year, dateFilterStart.Value.Month, dateFilterStart.Value.Day);
        }

        private void dateFilterEnd_ValueChanged(object sender, EventArgs e)
        {
            dateFilterEnd.Value = new DateTime(dateFilterEnd.Value.Year, dateFilterEnd.Value.Month, dateFilterEnd.Value.Day, 23, 59, 59);
        }
    }
}
