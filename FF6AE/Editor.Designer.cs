namespace FFVIEditor
{
    partial class Editor
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.editorMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenSubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitSubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.languageSubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishOptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frenchOptionMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infosSubMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblRomName = new System.Windows.Forms.Label();
            this.lblCountry = new System.Windows.Forms.Label();
            this.lblSaveType = new System.Windows.Forms.Label();
            this.lblSerial = new System.Windows.Forms.Label();
            this.tbCountry = new System.Windows.Forms.TextBox();
            this.tbRomName = new System.Windows.Forms.TextBox();
            this.tbSaveType = new System.Windows.Forms.TextBox();
            this.tbSerial = new System.Windows.Forms.TextBox();
            this.btnSpellEditor = new System.Windows.Forms.Button();
            this.btnItemEditor = new System.Windows.Forms.Button();
            this.btnGraphicEditor = new System.Windows.Forms.Button();
            this.btnMonsterEditor = new System.Windows.Forms.Button();
            this.btnActorEditor = new System.Windows.Forms.Button();
            this.editorMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // editorMenuStrip
            // 
            this.editorMenuStrip.BackColor = System.Drawing.Color.Gainsboro;
            this.editorMenuStrip.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.editorMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.editorMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.editorMenuStrip.Name = "editorMenuStrip";
            this.editorMenuStrip.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.editorMenuStrip.Size = new System.Drawing.Size(662, 24);
            this.editorMenuStrip.TabIndex = 2;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.infosToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // infosToolStripMenuItem
            // 
            this.infosToolStripMenuItem.Name = "infosToolStripMenuItem";
            this.infosToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.infosToolStripMenuItem.Text = "Infos";
            this.infosToolStripMenuItem.Click += new System.EventHandler(this.infosToolStripMenuItem_Click);
            // 
            // fileMenuItem
            // 
            this.fileMenuItem.Name = "fileMenuItem";
            this.fileMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // OpenSubMenuItem
            // 
            this.OpenSubMenuItem.Name = "OpenSubMenuItem";
            this.OpenSubMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // saveSubMenuItem
            // 
            this.saveSubMenuItem.Name = "saveSubMenuItem";
            this.saveSubMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // quitSubMenuItem
            // 
            this.quitSubMenuItem.Name = "quitSubMenuItem";
            this.quitSubMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // optionsMenuItem
            // 
            this.optionsMenuItem.Name = "optionsMenuItem";
            this.optionsMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // languageSubMenuItem
            // 
            this.languageSubMenuItem.Name = "languageSubMenuItem";
            this.languageSubMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // englishOptionMenuItem
            // 
            this.englishOptionMenuItem.Name = "englishOptionMenuItem";
            this.englishOptionMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // frenchOptionMenuItem
            // 
            this.frenchOptionMenuItem.Name = "frenchOptionMenuItem";
            this.frenchOptionMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // infosSubMenuItem
            // 
            this.infosSubMenuItem.Name = "infosSubMenuItem";
            this.infosSubMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // lblRomName
            // 
            this.lblRomName.AutoSize = true;
            this.lblRomName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRomName.Location = new System.Drawing.Point(16, 58);
            this.lblRomName.Name = "lblRomName";
            this.lblRomName.Size = new System.Drawing.Size(78, 16);
            this.lblRomName.TabIndex = 4;
            this.lblRomName.Text = "ROM name";
            // 
            // lblCountry
            // 
            this.lblCountry.AutoSize = true;
            this.lblCountry.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountry.Location = new System.Drawing.Point(16, 98);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Size = new System.Drawing.Size(57, 16);
            this.lblCountry.TabIndex = 6;
            this.lblCountry.Text = "Country";
            // 
            // lblSaveType
            // 
            this.lblSaveType.AutoSize = true;
            this.lblSaveType.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaveType.Location = new System.Drawing.Point(368, 58);
            this.lblSaveType.Name = "lblSaveType";
            this.lblSaveType.Size = new System.Drawing.Size(71, 16);
            this.lblSaveType.TabIndex = 9;
            this.lblSaveType.Text = "Save type";
            // 
            // lblSerial
            // 
            this.lblSerial.AutoSize = true;
            this.lblSerial.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSerial.Location = new System.Drawing.Point(368, 98);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(46, 16);
            this.lblSerial.TabIndex = 10;
            this.lblSerial.Text = "Serial";
            // 
            // tbCountry
            // 
            this.tbCountry.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbCountry.Enabled = false;
            this.tbCountry.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCountry.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbCountry.Location = new System.Drawing.Point(120, 91);
            this.tbCountry.Name = "tbCountry";
            this.tbCountry.Size = new System.Drawing.Size(186, 22);
            this.tbCountry.TabIndex = 17;
            // 
            // tbRomName
            // 
            this.tbRomName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbRomName.Enabled = false;
            this.tbRomName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbRomName.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbRomName.Location = new System.Drawing.Point(120, 54);
            this.tbRomName.Name = "tbRomName";
            this.tbRomName.Size = new System.Drawing.Size(186, 22);
            this.tbRomName.TabIndex = 19;
            // 
            // tbSaveType
            // 
            this.tbSaveType.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbSaveType.Enabled = false;
            this.tbSaveType.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSaveType.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbSaveType.Location = new System.Drawing.Point(449, 54);
            this.tbSaveType.Name = "tbSaveType";
            this.tbSaveType.Size = new System.Drawing.Size(186, 22);
            this.tbSaveType.TabIndex = 20;
            // 
            // tbSerial
            // 
            this.tbSerial.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tbSerial.Enabled = false;
            this.tbSerial.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSerial.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbSerial.Location = new System.Drawing.Point(449, 90);
            this.tbSerial.Name = "tbSerial";
            this.tbSerial.Size = new System.Drawing.Size(186, 22);
            this.tbSerial.TabIndex = 21;
            // 
            // btnSpellEditor
            // 
            this.btnSpellEditor.Enabled = false;
            this.btnSpellEditor.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpellEditor.Image = global::FFVIEditor.Properties.Resources.cyan;
            this.btnSpellEditor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSpellEditor.Location = new System.Drawing.Point(16, 250);
            this.btnSpellEditor.Name = "btnSpellEditor";
            this.btnSpellEditor.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.btnSpellEditor.Size = new System.Drawing.Size(193, 51);
            this.btnSpellEditor.TabIndex = 26;
            this.btnSpellEditor.Text = "Spell Editor";
            this.btnSpellEditor.UseVisualStyleBackColor = true;
            this.btnSpellEditor.Click += new System.EventHandler(this.btnSpellEditor_Click);
            // 
            // btnItemEditor
            // 
            this.btnItemEditor.Enabled = false;
            this.btnItemEditor.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItemEditor.Image = global::FFVIEditor.Properties.Resources.mog;
            this.btnItemEditor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnItemEditor.Location = new System.Drawing.Point(442, 183);
            this.btnItemEditor.Name = "btnItemEditor";
            this.btnItemEditor.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.btnItemEditor.Size = new System.Drawing.Size(193, 51);
            this.btnItemEditor.TabIndex = 24;
            this.btnItemEditor.Text = "Item Editor";
            this.btnItemEditor.UseVisualStyleBackColor = true;
            this.btnItemEditor.Click += new System.EventHandler(this.btnItemEditor_Click);
            // 
            // btnGraphicEditor
            // 
            this.btnGraphicEditor.Enabled = false;
            this.btnGraphicEditor.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGraphicEditor.Image = global::FFVIEditor.Properties.Resources.terra;
            this.btnGraphicEditor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGraphicEditor.Location = new System.Drawing.Point(230, 183);
            this.btnGraphicEditor.Name = "btnGraphicEditor";
            this.btnGraphicEditor.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.btnGraphicEditor.Size = new System.Drawing.Size(193, 51);
            this.btnGraphicEditor.TabIndex = 25;
            this.btnGraphicEditor.Text = "    Graphic Editor";
            this.btnGraphicEditor.UseVisualStyleBackColor = true;
            this.btnGraphicEditor.Click += new System.EventHandler(this.btnGraphicEditor_Click);
            // 
            // btnMonsterEditor
            // 
            this.btnMonsterEditor.Enabled = false;
            this.btnMonsterEditor.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMonsterEditor.Image = global::FFVIEditor.Properties.Resources.ghost;
            this.btnMonsterEditor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMonsterEditor.Location = new System.Drawing.Point(16, 183);
            this.btnMonsterEditor.Name = "btnMonsterEditor";
            this.btnMonsterEditor.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.btnMonsterEditor.Size = new System.Drawing.Size(193, 51);
            this.btnMonsterEditor.TabIndex = 23;
            this.btnMonsterEditor.Text = "    Monster Editor";
            this.btnMonsterEditor.UseVisualStyleBackColor = true;
            this.btnMonsterEditor.Click += new System.EventHandler(this.btnMonsterEditor_Click);
            // 
            // btnActorEditor
            // 
            this.btnActorEditor.Enabled = false;
            this.btnActorEditor.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActorEditor.Image = global::FFVIEditor.Properties.Resources.soldier;
            this.btnActorEditor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnActorEditor.Location = new System.Drawing.Point(230, 250);
            this.btnActorEditor.Name = "btnActorEditor";
            this.btnActorEditor.Padding = new System.Windows.Forms.Padding(14, 0, 14, 0);
            this.btnActorEditor.Size = new System.Drawing.Size(193, 51);
            this.btnActorEditor.TabIndex = 27;
            this.btnActorEditor.Text = "Actor Editor";
            this.btnActorEditor.UseVisualStyleBackColor = true;
            this.btnActorEditor.Click += new System.EventHandler(this.btnActorEditor_Click);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 381);
            this.Controls.Add(this.btnActorEditor);
            this.Controls.Add(this.btnSpellEditor);
            this.Controls.Add(this.tbRomName);
            this.Controls.Add(this.tbCountry);
            this.Controls.Add(this.lblCountry);
            this.Controls.Add(this.lblRomName);
            this.Controls.Add(this.btnItemEditor);
            this.Controls.Add(this.editorMenuStrip);
            this.Controls.Add(this.btnGraphicEditor);
            this.Controls.Add(this.lblSaveType);
            this.Controls.Add(this.lblSerial);
            this.Controls.Add(this.btnMonsterEditor);
            this.Controls.Add(this.tbSerial);
            this.Controls.Add(this.tbSaveType);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.editorMenuStrip;
            this.Name = "Editor";
            this.Text = "FF6 Advance Editor";
            this.editorMenuStrip.ResumeLayout(false);
            this.editorMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip editorMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenSubMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveSubMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitSubMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem languageSubMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishOptionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frenchOptionMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infosSubMenuItem;
        private System.Windows.Forms.Label lblRomName;
        private System.Windows.Forms.Label lblCountry;
        private System.Windows.Forms.Label lblSaveType;
        private System.Windows.Forms.Label lblSerial;
        private System.Windows.Forms.TextBox tbCountry;
        private System.Windows.Forms.TextBox tbRomName;
        private System.Windows.Forms.TextBox tbSaveType;
        private System.Windows.Forms.TextBox tbSerial;
        private System.Windows.Forms.Button btnMonsterEditor;
        private System.Windows.Forms.Button btnItemEditor;
        private System.Windows.Forms.Button btnGraphicEditor;
        private System.Windows.Forms.Button btnSpellEditor;
        private System.Windows.Forms.Button btnActorEditor;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infosToolStripMenuItem;

    }
}

