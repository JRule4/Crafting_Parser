namespace Crafting_Parser
{
    partial class CraftingDataDisplay
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
            this.characterSelectButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTreeView = new System.Windows.Forms.TreeView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.itemTypeTreeView = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataDumpBox = new System.Windows.Forms.TextBox();
            this.directorySelectButton = new System.Windows.Forms.Button();
            this.dateFilterStart = new System.Windows.Forms.DateTimePicker();
            this.dateFilterEnd = new System.Windows.Forms.DateTimePicker();
            this.dateFromLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateGroup = new System.Windows.Forms.GroupBox();
            this.useDateFilter = new System.Windows.Forms.CheckBox();
            this.parseProgress = new System.Windows.Forms.ProgressBar();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.dateGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // characterSelectButton
            // 
            this.characterSelectButton.Location = new System.Drawing.Point(522, 485);
            this.characterSelectButton.Name = "characterSelectButton";
            this.characterSelectButton.Size = new System.Drawing.Size(100, 27);
            this.characterSelectButton.TabIndex = 0;
            this.characterSelectButton.Text = "Select Character";
            this.characterSelectButton.UseVisualStyleBackColor = true;
            this.characterSelectButton.Click += new System.EventHandler(this.FilePicker_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(298, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Data";
            // 
            // dateTreeView
            // 
            this.dateTreeView.Location = new System.Drawing.Point(0, 0);
            this.dateTreeView.Name = "dateTreeView";
            this.dateTreeView.Size = new System.Drawing.Size(602, 427);
            this.dateTreeView.TabIndex = 4;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 24);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(610, 453);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dateTreeView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(602, 427);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Items by Date";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.itemTypeTreeView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(602, 427);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Items by Type";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // itemTypeTreeView
            // 
            this.itemTypeTreeView.Location = new System.Drawing.Point(0, 0);
            this.itemTypeTreeView.Name = "itemTypeTreeView";
            this.itemTypeTreeView.Size = new System.Drawing.Size(602, 427);
            this.itemTypeTreeView.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataDumpBox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(602, 427);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Data Dump";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataDumpBox
            // 
            this.dataDumpBox.Location = new System.Drawing.Point(3, 3);
            this.dataDumpBox.MaxLength = 500000;
            this.dataDumpBox.Multiline = true;
            this.dataDumpBox.Name = "dataDumpBox";
            this.dataDumpBox.ReadOnly = true;
            this.dataDumpBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataDumpBox.Size = new System.Drawing.Size(596, 421);
            this.dataDumpBox.TabIndex = 0;
            // 
            // directorySelectButton
            // 
            this.directorySelectButton.Location = new System.Drawing.Point(522, 519);
            this.directorySelectButton.Name = "directorySelectButton";
            this.directorySelectButton.Size = new System.Drawing.Size(100, 27);
            this.directorySelectButton.TabIndex = 6;
            this.directorySelectButton.Text = "Select Directory";
            this.directorySelectButton.UseVisualStyleBackColor = true;
            this.directorySelectButton.Click += new System.EventHandler(this.directorySelectButton_Click);
            // 
            // dateFilterStart
            // 
            this.dateFilterStart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dateFilterStart.Location = new System.Drawing.Point(70, 15);
            this.dateFilterStart.Name = "dateFilterStart";
            this.dateFilterStart.Size = new System.Drawing.Size(200, 20);
            this.dateFilterStart.TabIndex = 7;
            this.dateFilterStart.ValueChanged += new System.EventHandler(this.dateFilterStart_ValueChanged);
            // 
            // dateFilterEnd
            // 
            this.dateFilterEnd.Location = new System.Drawing.Point(70, 44);
            this.dateFilterEnd.Name = "dateFilterEnd";
            this.dateFilterEnd.Size = new System.Drawing.Size(200, 20);
            this.dateFilterEnd.TabIndex = 8;
            this.dateFilterEnd.ValueChanged += new System.EventHandler(this.dateFilterEnd_ValueChanged);
            // 
            // dateFromLabel
            // 
            this.dateFromLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.dateFromLabel.AutoSize = true;
            this.dateFromLabel.Location = new System.Drawing.Point(9, 18);
            this.dateFromLabel.Name = "dateFromLabel";
            this.dateFromLabel.Size = new System.Drawing.Size(55, 13);
            this.dateFromLabel.TabIndex = 9;
            this.dateFromLabel.Text = "Start Date";
            this.dateFromLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "End Date";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateGroup
            // 
            this.dateGroup.Controls.Add(this.dateFromLabel);
            this.dateGroup.Controls.Add(this.dateFilterEnd);
            this.dateGroup.Controls.Add(this.label2);
            this.dateGroup.Controls.Add(this.dateFilterStart);
            this.dateGroup.Enabled = false;
            this.dateGroup.Location = new System.Drawing.Point(12, 477);
            this.dateGroup.Name = "dateGroup";
            this.dateGroup.Size = new System.Drawing.Size(276, 73);
            this.dateGroup.TabIndex = 11;
            this.dateGroup.TabStop = false;
            // 
            // useDateFilter
            // 
            this.useDateFilter.AutoSize = true;
            this.useDateFilter.Location = new System.Drawing.Point(294, 495);
            this.useDateFilter.Name = "useDateFilter";
            this.useDateFilter.Size = new System.Drawing.Size(115, 17);
            this.useDateFilter.TabIndex = 12;
            this.useDateFilter.Text = "Filter Logs By Date";
            this.useDateFilter.UseVisualStyleBackColor = true;
            this.useDateFilter.CheckedChanged += new System.EventHandler(this.useDateFilter_CheckedChanged);
            // 
            // parseProgress
            // 
            this.parseProgress.Location = new System.Drawing.Point(385, 520);
            this.parseProgress.Name = "parseProgress";
            this.parseProgress.Size = new System.Drawing.Size(131, 23);
            this.parseProgress.TabIndex = 5;
            this.parseProgress.Visible = false;
            // 
            // CraftingDataDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 555);
            this.Controls.Add(this.parseProgress);
            this.Controls.Add(this.useDateFilter);
            this.Controls.Add(this.dateGroup);
            this.Controls.Add(this.directorySelectButton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.characterSelectButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CraftingDataDisplay";
            this.Text = "Form1";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.dateGroup.ResumeLayout(false);
            this.dateGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button characterSelectButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView dateTreeView;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView itemTypeTreeView;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox dataDumpBox;
        private System.Windows.Forms.Button directorySelectButton;
        private System.Windows.Forms.DateTimePicker dateFilterStart;
        private System.Windows.Forms.DateTimePicker dateFilterEnd;
        private System.Windows.Forms.Label dateFromLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox dateGroup;
        private System.Windows.Forms.CheckBox useDateFilter;
        private System.Windows.Forms.ProgressBar parseProgress;

    }
}

