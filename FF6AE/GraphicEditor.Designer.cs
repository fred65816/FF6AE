namespace FFVIEditor
{
    partial class GraphicEditor
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
            this.tabControlGraphics = new System.Windows.Forms.TabControl();
            this.tpLargeFont = new System.Windows.Forms.TabPage();
            this.label84 = new System.Windows.Forms.Label();
            this.btnLFClose = new System.Windows.Forms.Button();
            this.lblLFByteLeft = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnLFSave = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudLFColumns = new System.Windows.Forms.NumericUpDown();
            this.numLFPixels = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbLFWhite = new System.Windows.Forms.PictureBox();
            this.pbLFGrey = new System.Windows.Forms.PictureBox();
            this.pbLFBlue = new System.Windows.Forms.PictureBox();
            this.pbLFBlack = new System.Windows.Forms.PictureBox();
            this.pbLargeFont = new System.Windows.Forms.PictureBox();
            this.dgvLargeFont = new System.Windows.Forms.DataGridView();
            this.tpSmallFont = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSFClose = new System.Windows.Forms.Button();
            this.lblSFByteLeft = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSFSave = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.nudSFColumns = new System.Windows.Forms.NumericUpDown();
            this.nudSFPixels = new System.Windows.Forms.NumericUpDown();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.pbSFWhite = new System.Windows.Forms.PictureBox();
            this.pbSFGrey = new System.Windows.Forms.PictureBox();
            this.pbSFBlue = new System.Windows.Forms.PictureBox();
            this.pbSFBlack = new System.Windows.Forms.PictureBox();
            this.pbSmallFont = new System.Windows.Forms.PictureBox();
            this.dgvSmallFont = new System.Windows.Forms.DataGridView();
            this.imgListSmallFont = new System.Windows.Forms.ImageList(this.components);
            this.tabControlGraphics.SuspendLayout();
            this.tpLargeFont.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLFColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLFPixels)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLFWhite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLFGrey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLFBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLFBlack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLargeFont)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLargeFont)).BeginInit();
            this.tpSmallFont.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSFColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSFPixels)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSFWhite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSFGrey)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSFBlue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSFBlack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSmallFont)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSmallFont)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlGraphics
            // 
            this.tabControlGraphics.Controls.Add(this.tpLargeFont);
            this.tabControlGraphics.Controls.Add(this.tpSmallFont);
            this.tabControlGraphics.Location = new System.Drawing.Point(5, 4);
            this.tabControlGraphics.Name = "tabControlGraphics";
            this.tabControlGraphics.SelectedIndex = 0;
            this.tabControlGraphics.Size = new System.Drawing.Size(672, 508);
            this.tabControlGraphics.TabIndex = 0;
            this.tabControlGraphics.SelectedIndexChanged += new System.EventHandler(this.tabControlGraphics_SelectedIndexChanged);
            // 
            // tpLargeFont
            // 
            this.tpLargeFont.Controls.Add(this.label84);
            this.tpLargeFont.Controls.Add(this.btnLFClose);
            this.tpLargeFont.Controls.Add(this.lblLFByteLeft);
            this.tpLargeFont.Controls.Add(this.label4);
            this.tpLargeFont.Controls.Add(this.btnLFSave);
            this.tpLargeFont.Controls.Add(this.label3);
            this.tpLargeFont.Controls.Add(this.groupBox3);
            this.tpLargeFont.Controls.Add(this.groupBox1);
            this.tpLargeFont.Controls.Add(this.pbLargeFont);
            this.tpLargeFont.Controls.Add(this.dgvLargeFont);
            this.tpLargeFont.Location = new System.Drawing.Point(4, 22);
            this.tpLargeFont.Name = "tpLargeFont";
            this.tpLargeFont.Padding = new System.Windows.Forms.Padding(3);
            this.tpLargeFont.Size = new System.Drawing.Size(664, 482);
            this.tpLargeFont.TabIndex = 0;
            this.tpLargeFont.Text = "Large Font";
            this.tpLargeFont.UseVisualStyleBackColor = true;
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Location = new System.Drawing.Point(284, 463);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(373, 13);
            this.label84.TabIndex = 159;
            this.label84.Text = "Coded by Madsiur, March 2013. Special Thanks: Dragonsbrethren, Drakkhen";
            // 
            // btnLFClose
            // 
            this.btnLFClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLFClose.Location = new System.Drawing.Point(552, 414);
            this.btnLFClose.Name = "btnLFClose";
            this.btnLFClose.Size = new System.Drawing.Size(106, 23);
            this.btnLFClose.TabIndex = 15;
            this.btnLFClose.Text = "Return to menu";
            this.btnLFClose.UseVisualStyleBackColor = true;
            this.btnLFClose.Click += new System.EventHandler(this.btnLFClose_Click);
            // 
            // lblLFByteLeft
            // 
            this.lblLFByteLeft.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLFByteLeft.Location = new System.Drawing.Point(587, 21);
            this.lblLFByteLeft.Name = "lblLFByteLeft";
            this.lblLFByteLeft.Size = new System.Drawing.Size(33, 25);
            this.lblLFByteLeft.TabIndex = 14;
            this.lblLFByteLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(518, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 25);
            this.label4.TabIndex = 13;
            this.label4.Text = "Bytes left:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnLFSave
            // 
            this.btnLFSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLFSave.Location = new System.Drawing.Point(440, 323);
            this.btnLFSave.Name = "btnLFSave";
            this.btnLFSave.Size = new System.Drawing.Size(150, 30);
            this.btnLFSave.TabIndex = 12;
            this.btnLFSave.Text = "Save changes locally";
            this.btnLFSave.UseVisualStyleBackColor = true;
            this.btnLFSave.Click += new System.EventHandler(this.btnLFSave_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(394, 440);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(264, 14);
            this.label3.TabIndex = 11;
            this.label3.Text = "* number of pixels before drawing the next character";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.nudLFColumns);
            this.groupBox3.Controls.Add(this.numLFPixels);
            this.groupBox3.Location = new System.Drawing.Point(440, 243);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(150, 74);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(17, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Width (x4):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(36, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 16);
            this.label2.TabIndex = 7;
            this.label2.Text = "Pixels*:";
            // 
            // nudLFColumns
            // 
            this.nudLFColumns.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudLFColumns.Location = new System.Drawing.Point(95, 15);
            this.nudLFColumns.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudLFColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLFColumns.Name = "nudLFColumns";
            this.nudLFColumns.Size = new System.Drawing.Size(48, 21);
            this.nudLFColumns.TabIndex = 6;
            this.nudLFColumns.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLFColumns.ValueChanged += new System.EventHandler(this.nudLFColumns_ValueChanged);
            // 
            // numLFPixels
            // 
            this.numLFPixels.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numLFPixels.Location = new System.Drawing.Point(95, 44);
            this.numLFPixels.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.numLFPixels.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numLFPixels.Name = "numLFPixels";
            this.numLFPixels.Size = new System.Drawing.Size(48, 21);
            this.numLFPixels.TabIndex = 6;
            this.numLFPixels.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pbLFWhite);
            this.groupBox1.Controls.Add(this.pbLFGrey);
            this.groupBox1.Controls.Add(this.pbLFBlue);
            this.groupBox1.Controls.Add(this.pbLFBlack);
            this.groupBox1.Location = new System.Drawing.Point(590, 83);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(51, 159);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // pbLFWhite
            // 
            this.pbLFWhite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbLFWhite.Location = new System.Drawing.Point(13, 52);
            this.pbLFWhite.Margin = new System.Windows.Forms.Padding(6);
            this.pbLFWhite.Name = "pbLFWhite";
            this.pbLFWhite.Size = new System.Drawing.Size(24, 24);
            this.pbLFWhite.TabIndex = 5;
            this.pbLFWhite.TabStop = false;
            this.pbLFWhite.Click += new System.EventHandler(this.pbLFWhite_Click);
            // 
            // pbLFGrey
            // 
            this.pbLFGrey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbLFGrey.Location = new System.Drawing.Point(13, 88);
            this.pbLFGrey.Margin = new System.Windows.Forms.Padding(6);
            this.pbLFGrey.Name = "pbLFGrey";
            this.pbLFGrey.Size = new System.Drawing.Size(24, 24);
            this.pbLFGrey.TabIndex = 4;
            this.pbLFGrey.TabStop = false;
            this.pbLFGrey.Click += new System.EventHandler(this.pbLFGrey_Click);
            // 
            // pbLFBlue
            // 
            this.pbLFBlue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbLFBlue.Location = new System.Drawing.Point(13, 124);
            this.pbLFBlue.Margin = new System.Windows.Forms.Padding(6);
            this.pbLFBlue.Name = "pbLFBlue";
            this.pbLFBlue.Size = new System.Drawing.Size(24, 24);
            this.pbLFBlue.TabIndex = 3;
            this.pbLFBlue.TabStop = false;
            this.pbLFBlue.Click += new System.EventHandler(this.pbLFBlue_Click);
            // 
            // pbLFBlack
            // 
            this.pbLFBlack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbLFBlack.Location = new System.Drawing.Point(13, 16);
            this.pbLFBlack.Margin = new System.Windows.Forms.Padding(6);
            this.pbLFBlack.Name = "pbLFBlack";
            this.pbLFBlack.Size = new System.Drawing.Size(24, 24);
            this.pbLFBlack.TabIndex = 2;
            this.pbLFBlack.TabStop = false;
            this.pbLFBlack.Click += new System.EventHandler(this.pbLFBlack_Click);
            // 
            // pbLargeFont
            // 
            this.pbLargeFont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbLargeFont.Location = new System.Drawing.Point(440, 93);
            this.pbLargeFont.Name = "pbLargeFont";
            this.pbLargeFont.Size = new System.Drawing.Size(144, 144);
            this.pbLargeFont.TabIndex = 1;
            this.pbLargeFont.TabStop = false;
            this.pbLargeFont.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbLargeFont_MouseClick);
            this.pbLargeFont.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbLargeFont_MouseMove);
            // 
            // dgvLargeFont
            // 
            this.dgvLargeFont.AllowUserToAddRows = false;
            this.dgvLargeFont.AllowUserToDeleteRows = false;
            this.dgvLargeFont.AllowUserToResizeColumns = false;
            this.dgvLargeFont.AllowUserToResizeRows = false;
            this.dgvLargeFont.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLargeFont.Location = new System.Drawing.Point(19, 19);
            this.dgvLargeFont.Name = "dgvLargeFont";
            this.dgvLargeFont.Size = new System.Drawing.Size(175, 441);
            this.dgvLargeFont.TabIndex = 0;
            this.dgvLargeFont.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLargeFont_CellClick);
            // 
            // tpSmallFont
            // 
            this.tpSmallFont.Controls.Add(this.label5);
            this.tpSmallFont.Controls.Add(this.btnSFClose);
            this.tpSmallFont.Controls.Add(this.lblSFByteLeft);
            this.tpSmallFont.Controls.Add(this.label6);
            this.tpSmallFont.Controls.Add(this.btnSFSave);
            this.tpSmallFont.Controls.Add(this.label7);
            this.tpSmallFont.Controls.Add(this.groupBox2);
            this.tpSmallFont.Controls.Add(this.groupBox4);
            this.tpSmallFont.Controls.Add(this.pbSmallFont);
            this.tpSmallFont.Controls.Add(this.dgvSmallFont);
            this.tpSmallFont.Location = new System.Drawing.Point(4, 22);
            this.tpSmallFont.Name = "tpSmallFont";
            this.tpSmallFont.Padding = new System.Windows.Forms.Padding(3);
            this.tpSmallFont.Size = new System.Drawing.Size(664, 482);
            this.tpSmallFont.TabIndex = 1;
            this.tpSmallFont.Text = "Small Font";
            this.tpSmallFont.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(284, 463);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(373, 13);
            this.label5.TabIndex = 160;
            this.label5.Text = "Coded by Madsiur, March 2013. Special Thanks: Dragonsbrethren, Drakkhen";
            // 
            // btnSFClose
            // 
            this.btnSFClose.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSFClose.Location = new System.Drawing.Point(552, 414);
            this.btnSFClose.Name = "btnSFClose";
            this.btnSFClose.Size = new System.Drawing.Size(106, 23);
            this.btnSFClose.TabIndex = 24;
            this.btnSFClose.Text = "Return to menu";
            this.btnSFClose.UseVisualStyleBackColor = true;
            this.btnSFClose.Click += new System.EventHandler(this.btnSFClose_Click);
            // 
            // lblSFByteLeft
            // 
            this.lblSFByteLeft.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSFByteLeft.Location = new System.Drawing.Point(587, 21);
            this.lblSFByteLeft.Name = "lblSFByteLeft";
            this.lblSFByteLeft.Size = new System.Drawing.Size(33, 25);
            this.lblSFByteLeft.TabIndex = 23;
            this.lblSFByteLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(518, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 25);
            this.label6.TabIndex = 22;
            this.label6.Text = "Bytes left:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSFSave
            // 
            this.btnSFSave.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSFSave.Location = new System.Drawing.Point(440, 323);
            this.btnSFSave.Name = "btnSFSave";
            this.btnSFSave.Size = new System.Drawing.Size(150, 30);
            this.btnSFSave.TabIndex = 21;
            this.btnSFSave.Text = "Save changes locally";
            this.btnSFSave.UseVisualStyleBackColor = true;
            this.btnSFSave.Click += new System.EventHandler(this.btnSFSave_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(394, 440);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(264, 14);
            this.label7.TabIndex = 20;
            this.label7.Text = "* number of pixels before drawing the next character";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.nudSFColumns);
            this.groupBox2.Controls.Add(this.nudSFPixels);
            this.groupBox2.Location = new System.Drawing.Point(440, 243);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(150, 74);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(17, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "Width (x4):";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(36, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 16);
            this.label9.TabIndex = 7;
            this.label9.Text = "Pixels*:";
            // 
            // nudSFColumns
            // 
            this.nudSFColumns.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudSFColumns.Location = new System.Drawing.Point(95, 15);
            this.nudSFColumns.Maximum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudSFColumns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSFColumns.Name = "nudSFColumns";
            this.nudSFColumns.Size = new System.Drawing.Size(48, 21);
            this.nudSFColumns.TabIndex = 6;
            this.nudSFColumns.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSFColumns.ValueChanged += new System.EventHandler(this.nudSFColumns_ValueChanged);
            // 
            // nudSFPixels
            // 
            this.nudSFPixels.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudSFPixels.Location = new System.Drawing.Point(95, 44);
            this.nudSFPixels.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudSFPixels.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSFPixels.Name = "nudSFPixels";
            this.nudSFPixels.Size = new System.Drawing.Size(48, 21);
            this.nudSFPixels.TabIndex = 6;
            this.nudSFPixels.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.pbSFWhite);
            this.groupBox4.Controls.Add(this.pbSFGrey);
            this.groupBox4.Controls.Add(this.pbSFBlue);
            this.groupBox4.Controls.Add(this.pbSFBlack);
            this.groupBox4.Location = new System.Drawing.Point(590, 83);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(51, 159);
            this.groupBox4.TabIndex = 18;
            this.groupBox4.TabStop = false;
            // 
            // pbSFWhite
            // 
            this.pbSFWhite.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSFWhite.Location = new System.Drawing.Point(13, 52);
            this.pbSFWhite.Margin = new System.Windows.Forms.Padding(6);
            this.pbSFWhite.Name = "pbSFWhite";
            this.pbSFWhite.Size = new System.Drawing.Size(24, 24);
            this.pbSFWhite.TabIndex = 5;
            this.pbSFWhite.TabStop = false;
            this.pbSFWhite.Click += new System.EventHandler(this.pbSFWhite_Click);
            // 
            // pbSFGrey
            // 
            this.pbSFGrey.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSFGrey.Location = new System.Drawing.Point(13, 88);
            this.pbSFGrey.Margin = new System.Windows.Forms.Padding(6);
            this.pbSFGrey.Name = "pbSFGrey";
            this.pbSFGrey.Size = new System.Drawing.Size(24, 24);
            this.pbSFGrey.TabIndex = 4;
            this.pbSFGrey.TabStop = false;
            this.pbSFGrey.Click += new System.EventHandler(this.pbSFGrey_Click);
            // 
            // pbSFBlue
            // 
            this.pbSFBlue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSFBlue.Location = new System.Drawing.Point(13, 124);
            this.pbSFBlue.Margin = new System.Windows.Forms.Padding(6);
            this.pbSFBlue.Name = "pbSFBlue";
            this.pbSFBlue.Size = new System.Drawing.Size(24, 24);
            this.pbSFBlue.TabIndex = 3;
            this.pbSFBlue.TabStop = false;
            this.pbSFBlue.Click += new System.EventHandler(this.pbSFBlue_Click);
            // 
            // pbSFBlack
            // 
            this.pbSFBlack.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSFBlack.Location = new System.Drawing.Point(13, 16);
            this.pbSFBlack.Margin = new System.Windows.Forms.Padding(6);
            this.pbSFBlack.Name = "pbSFBlack";
            this.pbSFBlack.Size = new System.Drawing.Size(24, 24);
            this.pbSFBlack.TabIndex = 2;
            this.pbSFBlack.TabStop = false;
            this.pbSFBlack.Click += new System.EventHandler(this.pbSFBlack_Click);
            // 
            // pbSmallFont
            // 
            this.pbSmallFont.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbSmallFont.Location = new System.Drawing.Point(439, 142);
            this.pbSmallFont.Name = "pbSmallFont";
            this.pbSmallFont.Size = new System.Drawing.Size(144, 96);
            this.pbSmallFont.TabIndex = 17;
            this.pbSmallFont.TabStop = false;
            this.pbSmallFont.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbSmallFont_MouseClick);
            this.pbSmallFont.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbSmallFont_MouseMove);
            // 
            // dgvSmallFont
            // 
            this.dgvSmallFont.AllowUserToAddRows = false;
            this.dgvSmallFont.AllowUserToDeleteRows = false;
            this.dgvSmallFont.AllowUserToResizeColumns = false;
            this.dgvSmallFont.AllowUserToResizeRows = false;
            this.dgvSmallFont.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSmallFont.Location = new System.Drawing.Point(19, 19);
            this.dgvSmallFont.Name = "dgvSmallFont";
            this.dgvSmallFont.Size = new System.Drawing.Size(175, 441);
            this.dgvSmallFont.TabIndex = 16;
            this.dgvSmallFont.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSmallFont_CellClick);
            // 
            // imgListSmallFont
            // 
            this.imgListSmallFont.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgListSmallFont.ImageSize = new System.Drawing.Size(16, 16);
            this.imgListSmallFont.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // GraphicEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(678, 511);
            this.Controls.Add(this.tabControlGraphics);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "GraphicEditor";
            this.Text = "Graphic Editor";
            this.tabControlGraphics.ResumeLayout(false);
            this.tpLargeFont.ResumeLayout(false);
            this.tpLargeFont.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLFColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLFPixels)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLFWhite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLFGrey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLFBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLFBlack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbLargeFont)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLargeFont)).EndInit();
            this.tpSmallFont.ResumeLayout(false);
            this.tpSmallFont.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSFColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSFPixels)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSFWhite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSFGrey)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSFBlue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSFBlack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSmallFont)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSmallFont)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlGraphics;
        private System.Windows.Forms.TabPage tpLargeFont;
        private System.Windows.Forms.TabPage tpSmallFont;
        private System.Windows.Forms.ImageList imgListSmallFont;
        private System.Windows.Forms.DataGridView dgvLargeFont;
        private System.Windows.Forms.PictureBox pbLargeFont;
        private System.Windows.Forms.PictureBox pbLFWhite;
        private System.Windows.Forms.PictureBox pbLFGrey;
        private System.Windows.Forms.PictureBox pbLFBlue;
        private System.Windows.Forms.PictureBox pbLFBlack;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudLFColumns;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numLFPixels;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnLFSave;
        private System.Windows.Forms.Label lblLFByteLeft;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnLFClose;
        private System.Windows.Forms.Button btnSFClose;
        private System.Windows.Forms.Label lblSFByteLeft;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSFSave;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudSFColumns;
        private System.Windows.Forms.NumericUpDown nudSFPixels;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PictureBox pbSFWhite;
        private System.Windows.Forms.PictureBox pbSFGrey;
        private System.Windows.Forms.PictureBox pbSFBlue;
        private System.Windows.Forms.PictureBox pbSFBlack;
        private System.Windows.Forms.PictureBox pbSmallFont;
        private System.Windows.Forms.DataGridView dgvSmallFont;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label5;

    }
}