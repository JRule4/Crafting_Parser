namespace Crafting_Parser
{
    partial class CharacterPicker
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
            this.characterList = new System.Windows.Forms.ListBox();
            this.cancelLoadButton = new System.Windows.Forms.Button();
            this.loadCharacterButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // characterList
            // 
            this.characterList.FormattingEnabled = true;
            this.characterList.Location = new System.Drawing.Point(12, 27);
            this.characterList.Name = "characterList";
            this.characterList.Size = new System.Drawing.Size(260, 199);
            this.characterList.TabIndex = 0;
            // 
            // cancelLoadButton
            // 
            this.cancelLoadButton.Location = new System.Drawing.Point(152, 230);
            this.cancelLoadButton.Name = "cancelLoadButton";
            this.cancelLoadButton.Size = new System.Drawing.Size(75, 23);
            this.cancelLoadButton.TabIndex = 1;
            this.cancelLoadButton.Text = "Cancel";
            this.cancelLoadButton.UseVisualStyleBackColor = true;
            this.cancelLoadButton.Click += new System.EventHandler(this.cancelLoadButton_Click);
            // 
            // loadCharacterButton
            // 
            this.loadCharacterButton.Location = new System.Drawing.Point(57, 230);
            this.loadCharacterButton.Name = "loadCharacterButton";
            this.loadCharacterButton.Size = new System.Drawing.Size(75, 23);
            this.loadCharacterButton.TabIndex = 2;
            this.loadCharacterButton.Text = "Load";
            this.loadCharacterButton.UseVisualStyleBackColor = true;
            this.loadCharacterButton.Click += new System.EventHandler(this.loadCharacterButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(104, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Characters";
            // 
            // CharacterPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.loadCharacterButton);
            this.Controls.Add(this.cancelLoadButton);
            this.Controls.Add(this.characterList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CharacterPicker";
            this.Text = "CharacterPicker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cancelLoadButton;
        private System.Windows.Forms.Button loadCharacterButton;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListBox characterList;
    }
}