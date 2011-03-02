namespace Heroes.Core.Map
{
    partial class frmMap3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMap3));
            this.panelBigMap = new System.Windows.Forms.Panel();
            this.panelMiniMap = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.secretFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Variable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage_debug = new System.Windows.Forms.TabPage();
            this.tabPage_gameInfo = new System.Windows.Forms.TabPage();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage_Hero = new System.Windows.Forms.TabPage();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage_cellPassibility = new System.Windows.Forms.TabPage();
            this.button_LoadPassibilityCellRecord = new System.Windows.Forms.Button();
            this.button_SaveRecordedPassibilityCell = new System.Windows.Forms.Button();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.Row = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button_startRecordCellPassibility = new System.Windows.Forms.Button();
            this.button_stopRecordCellPassiblity = new System.Windows.Forms.Button();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel_leftTop = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_left = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel_leftBottom = new System.Windows.Forms.Panel();
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.panel_rightBottom = new System.Windows.Forms.Panel();
            this.panel_top = new System.Windows.Forms.Panel();
            this.panel_rightTop = new System.Windows.Forms.Panel();
            this.panel_right = new System.Windows.Forms.Panel();
            this.panel_center = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage_debug.SuspendLayout();
            this.tabPage_gameInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage_Hero.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tabPage_cellPassibility.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            this.panel_leftTop.SuspendLayout();
            this.panel_left.SuspendLayout();
            this.panel_center.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelBigMap
            // 
            this.panelBigMap.Location = new System.Drawing.Point(2, 56);
            this.panelBigMap.Name = "panelBigMap";
            this.panelBigMap.Size = new System.Drawing.Size(864, 600);
            this.panelBigMap.TabIndex = 0;
            // 
            // panelMiniMap
            // 
            this.panelMiniMap.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMiniMap.Location = new System.Drawing.Point(881, 56);
            this.panelMiniMap.Name = "panelMiniMap";
            this.panelMiniMap.Size = new System.Drawing.Size(200, 200);
            this.panelMiniMap.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(886, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mini Map";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 46);
            this.button1.TabIndex = 2;
            this.button1.Text = "Quit Game (F4)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.secretFileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1166, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // secretFileToolStripMenuItem
            // 
            this.secretFileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem,
            this.quitToolStripMenuItem1});
            this.secretFileToolStripMenuItem.Name = "secretFileToolStripMenuItem";
            this.secretFileToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.secretFileToolStripMenuItem.Text = "Secret File";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.quitToolStripMenuItem.Text = "Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem1
            // 
            this.quitToolStripMenuItem1.Name = "quitToolStripMenuItem1";
            this.quitToolStripMenuItem1.Size = new System.Drawing.Size(116, 22);
            this.quitToolStripMenuItem1.Text = "Quit";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Variable,
            this.Value});
            this.dataGridView1.Location = new System.Drawing.Point(3, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(194, 331);
            this.dataGridView1.TabIndex = 4;
            // 
            // Variable
            // 
            this.Variable.HeaderText = "Variable";
            this.Variable.Name = "Variable";
            this.Variable.Width = 120;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            this.Value.Width = 70;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage_debug);
            this.tabControl1.Controls.Add(this.tabPage_gameInfo);
            this.tabControl1.Controls.Add(this.tabPage_Hero);
            this.tabControl1.Controls.Add(this.tabPage_cellPassibility);
            this.tabControl1.Location = new System.Drawing.Point(879, 287);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(209, 369);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage_debug
            // 
            this.tabPage_debug.Controls.Add(this.dataGridView1);
            this.tabPage_debug.Location = new System.Drawing.Point(4, 22);
            this.tabPage_debug.Name = "tabPage_debug";
            this.tabPage_debug.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_debug.Size = new System.Drawing.Size(201, 343);
            this.tabPage_debug.TabIndex = 2;
            this.tabPage_debug.Text = "Debug";
            this.tabPage_debug.UseVisualStyleBackColor = true;
            // 
            // tabPage_gameInfo
            // 
            this.tabPage_gameInfo.Controls.Add(this.dataGridView2);
            this.tabPage_gameInfo.Location = new System.Drawing.Point(4, 22);
            this.tabPage_gameInfo.Name = "tabPage_gameInfo";
            this.tabPage_gameInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_gameInfo.Size = new System.Drawing.Size(201, 343);
            this.tabPage_gameInfo.TabIndex = 0;
            this.tabPage_gameInfo.Text = "Game Info";
            this.tabPage_gameInfo.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.ColumnHeadersVisible = false;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView2.GridColor = System.Drawing.SystemColors.Window;
            this.dataGridView2.Location = new System.Drawing.Point(3, 6);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.Size = new System.Drawing.Size(195, 331);
            this.dataGridView2.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.Width = 90;
            // 
            // tabPage_Hero
            // 
            this.tabPage_Hero.Controls.Add(this.dataGridView3);
            this.tabPage_Hero.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Hero.Name = "tabPage_Hero";
            this.tabPage_Hero.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Hero.Size = new System.Drawing.Size(201, 343);
            this.tabPage_Hero.TabIndex = 1;
            this.tabPage_Hero.Text = "Hero";
            this.tabPage_Hero.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AllowUserToResizeColumns = false;
            this.dataGridView3.AllowUserToResizeRows = false;
            this.dataGridView3.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.ColumnHeadersVisible = false;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column3,
            this.Column4});
            this.dataGridView3.GridColor = System.Drawing.SystemColors.Window;
            this.dataGridView3.Location = new System.Drawing.Point(3, 6);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.Size = new System.Drawing.Size(195, 331);
            this.dataGridView3.TabIndex = 1;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            this.Column4.Width = 90;
            // 
            // tabPage_cellPassibility
            // 
            this.tabPage_cellPassibility.Controls.Add(this.button_LoadPassibilityCellRecord);
            this.tabPage_cellPassibility.Controls.Add(this.button_SaveRecordedPassibilityCell);
            this.tabPage_cellPassibility.Controls.Add(this.dataGridView4);
            this.tabPage_cellPassibility.Controls.Add(this.label2);
            this.tabPage_cellPassibility.Location = new System.Drawing.Point(4, 22);
            this.tabPage_cellPassibility.Name = "tabPage_cellPassibility";
            this.tabPage_cellPassibility.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_cellPassibility.Size = new System.Drawing.Size(201, 343);
            this.tabPage_cellPassibility.TabIndex = 3;
            this.tabPage_cellPassibility.Text = "XY";
            this.tabPage_cellPassibility.UseVisualStyleBackColor = true;
            // 
            // button_LoadPassibilityCellRecord
            // 
            this.button_LoadPassibilityCellRecord.Location = new System.Drawing.Point(12, 32);
            this.button_LoadPassibilityCellRecord.Name = "button_LoadPassibilityCellRecord";
            this.button_LoadPassibilityCellRecord.Size = new System.Drawing.Size(86, 23);
            this.button_LoadPassibilityCellRecord.TabIndex = 2;
            this.button_LoadPassibilityCellRecord.Text = "Load Record";
            this.button_LoadPassibilityCellRecord.UseVisualStyleBackColor = true;
            this.button_LoadPassibilityCellRecord.Click += new System.EventHandler(this.button_LoadPassibilityCellRecord_Click);
            // 
            // button_SaveRecordedPassibilityCell
            // 
            this.button_SaveRecordedPassibilityCell.Location = new System.Drawing.Point(107, 32);
            this.button_SaveRecordedPassibilityCell.Name = "button_SaveRecordedPassibilityCell";
            this.button_SaveRecordedPassibilityCell.Size = new System.Drawing.Size(78, 23);
            this.button_SaveRecordedPassibilityCell.TabIndex = 3;
            this.button_SaveRecordedPassibilityCell.Text = "Save Record";
            this.button_SaveRecordedPassibilityCell.UseVisualStyleBackColor = true;
            this.button_SaveRecordedPassibilityCell.Click += new System.EventHandler(this.button_SaveRecordedPassibilityCell_Click);
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.AllowUserToDeleteRows = false;
            this.dataGridView4.AllowUserToResizeColumns = false;
            this.dataGridView4.AllowUserToResizeRows = false;
            this.dataGridView4.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Row,
            this.Col});
            this.dataGridView4.Location = new System.Drawing.Point(6, 61);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.ReadOnly = true;
            this.dataGridView4.RowHeadersVisible = false;
            this.dataGridView4.Size = new System.Drawing.Size(189, 276);
            this.dataGridView4.TabIndex = 1;
            // 
            // Row
            // 
            this.Row.HeaderText = "Row";
            this.Row.Name = "Row";
            this.Row.ReadOnly = true;
            this.Row.Width = 80;
            // 
            // Col
            // 
            this.Col.HeaderText = "Col";
            this.Col.Name = "Col";
            this.Col.ReadOnly = true;
            this.Col.Width = 80;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Recording of Passibility Cell";
            // 
            // checkBox1
            // 
            this.checkBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.checkBox1.Location = new System.Drawing.Point(114, 6);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(94, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "View Grid Line";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button_startRecordCellPassibility
            // 
            this.button_startRecordCellPassibility.Location = new System.Drawing.Point(389, 8);
            this.button_startRecordCellPassibility.Name = "button_startRecordCellPassibility";
            this.button_startRecordCellPassibility.Size = new System.Drawing.Size(87, 35);
            this.button_startRecordCellPassibility.TabIndex = 7;
            this.button_startRecordCellPassibility.Text = "Start Record Passibility Cell";
            this.button_startRecordCellPassibility.UseVisualStyleBackColor = true;
            this.button_startRecordCellPassibility.Click += new System.EventHandler(this.button_startRecordCellPassibility_Click);
            // 
            // button_stopRecordCellPassiblity
            // 
            this.button_stopRecordCellPassiblity.Location = new System.Drawing.Point(494, 8);
            this.button_stopRecordCellPassiblity.Name = "button_stopRecordCellPassiblity";
            this.button_stopRecordCellPassiblity.Size = new System.Drawing.Size(82, 35);
            this.button_stopRecordCellPassiblity.TabIndex = 9;
            this.button_stopRecordCellPassiblity.Text = "Stop Record Passibility Cell";
            this.button_stopRecordCellPassiblity.UseVisualStyleBackColor = true;
            this.button_stopRecordCellPassiblity.Click += new System.EventHandler(this.button_stopRecordCellPassiblity_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.ForeColor = System.Drawing.SystemColors.Window;
            this.checkBox2.Location = new System.Drawing.Point(114, 29);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(117, 17);
            this.checkBox2.TabIndex = 10;
            this.checkBox2.Text = "View Passibility Cell";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.label3.Location = new System.Drawing.Point(614, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 25);
            this.label3.TabIndex = 11;
            this.label3.Text = "label3";
            // 
            // panel_leftTop
            // 
            this.panel_leftTop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_leftTop.BackColor = System.Drawing.Color.Transparent;
            this.panel_leftTop.Controls.Add(this.panel2);
            this.panel_leftTop.Location = new System.Drawing.Point(0, 0);
            this.panel_leftTop.Name = "panel_leftTop";
            this.panel_leftTop.Size = new System.Drawing.Size(100, 87);
            this.panel_leftTop.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 75);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 14;
            // 
            // panel_left
            // 
            this.panel_left.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_left.BackColor = System.Drawing.Color.Transparent;
            this.panel_left.Controls.Add(this.panel4);
            this.panel_left.Location = new System.Drawing.Point(0, 80);
            this.panel_left.Name = "panel_left";
            this.panel_left.Size = new System.Drawing.Size(100, 654);
            this.panel_left.TabIndex = 14;
            // 
            // panel4
            // 
            this.panel4.Location = new System.Drawing.Point(0, 661);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(95, 88);
            this.panel4.TabIndex = 15;
            // 
            // panel_leftBottom
            // 
            this.panel_leftBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_leftBottom.BackColor = System.Drawing.Color.Transparent;
            this.panel_leftBottom.Location = new System.Drawing.Point(1, 736);
            this.panel_leftBottom.Name = "panel_leftBottom";
            this.panel_leftBottom.Size = new System.Drawing.Size(99, 113);
            this.panel_leftBottom.TabIndex = 15;
            // 
            // panel_bottom
            // 
            this.panel_bottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_bottom.BackColor = System.Drawing.Color.Transparent;
            this.panel_bottom.Location = new System.Drawing.Point(101, 742);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(864, 107);
            this.panel_bottom.TabIndex = 16;
            // 
            // panel_rightBottom
            // 
            this.panel_rightBottom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_rightBottom.BackColor = System.Drawing.Color.Transparent;
            this.panel_rightBottom.Location = new System.Drawing.Point(959, 738);
            this.panel_rightBottom.Name = "panel_rightBottom";
            this.panel_rightBottom.Size = new System.Drawing.Size(313, 111);
            this.panel_rightBottom.TabIndex = 0;
            // 
            // panel_top
            // 
            this.panel_top.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_top.BackColor = System.Drawing.Color.Transparent;
            this.panel_top.Location = new System.Drawing.Point(101, 0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(864, 90);
            this.panel_top.TabIndex = 17;
            // 
            // panel_rightTop
            // 
            this.panel_rightTop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_rightTop.BackColor = System.Drawing.Color.Transparent;
            this.panel_rightTop.Location = new System.Drawing.Point(959, 1);
            this.panel_rightTop.Name = "panel_rightTop";
            this.panel_rightTop.Size = new System.Drawing.Size(313, 86);
            this.panel_rightTop.TabIndex = 18;
            // 
            // panel_right
            // 
            this.panel_right.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_right.BackColor = System.Drawing.Color.Transparent;
            this.panel_right.Location = new System.Drawing.Point(1185, 75);
            this.panel_right.Name = "panel_right";
            this.panel_right.Size = new System.Drawing.Size(90, 669);
            this.panel_right.TabIndex = 19;
            // 
            // panel_center
            // 
            this.panel_center.BackColor = System.Drawing.Color.Transparent;
            this.panel_center.Controls.Add(this.label4);
            this.panel_center.Controls.Add(this.comboBox1);
            this.panel_center.Controls.Add(this.button1);
            this.panel_center.Controls.Add(this.tabControl1);
            this.panel_center.Controls.Add(this.panelBigMap);
            this.panel_center.Controls.Add(this.panelMiniMap);
            this.panel_center.Controls.Add(this.checkBox1);
            this.panel_center.Controls.Add(this.button_startRecordCellPassibility);
            this.panel_center.Controls.Add(this.label1);
            this.panel_center.Controls.Add(this.button_stopRecordCellPassiblity);
            this.panel_center.Controls.Add(this.label3);
            this.panel_center.Controls.Add(this.checkBox2);
            this.panel_center.Location = new System.Drawing.Point(101, 80);
            this.panel_center.Name = "panel_center";
            this.panel_center.Size = new System.Drawing.Size(1091, 664);
            this.panel_center.TabIndex = 20;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Normal",
            "Slow",
            "Fast"});
            this.comboBox1.Location = new System.Drawing.Point(264, 22);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(86, 21);
            this.comboBox1.TabIndex = 12;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Window;
            this.label4.Location = new System.Drawing.Point(261, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Mouse Scroll Speed:";
            // 
            // frmMap3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1266, 849);
            this.ControlBox = false;
            this.Controls.Add(this.panel_center);
            this.Controls.Add(this.panel_right);
            this.Controls.Add(this.panel_rightTop);
            this.Controls.Add(this.panel_top);
            this.Controls.Add(this.panel_rightBottom);
            this.Controls.Add(this.panel_bottom);
            this.Controls.Add(this.panel_leftBottom);
            this.Controls.Add(this.panel_left);
            this.Controls.Add(this.panel_leftTop);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMap3";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMap3_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage_debug.ResumeLayout(false);
            this.tabPage_gameInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage_Hero.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tabPage_cellPassibility.ResumeLayout(false);
            this.tabPage_cellPassibility.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            this.panel_leftTop.ResumeLayout(false);
            this.panel_left.ResumeLayout(false);
            this.panel_center.ResumeLayout(false);
            this.panel_center.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelBigMap;
        private System.Windows.Forms.Panel panelMiniMap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem secretFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Variable;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage_gameInfo;
        private System.Windows.Forms.TabPage tabPage_Hero;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.TabPage tabPage_debug;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button_startRecordCellPassibility;
        private System.Windows.Forms.Button button_stopRecordCellPassiblity;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.TabPage tabPage_cellPassibility;
        private System.Windows.Forms.Button button_LoadPassibilityCellRecord;
        private System.Windows.Forms.Button button_SaveRecordedPassibilityCell;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Row;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem1;
        private System.Windows.Forms.Panel panel_leftTop;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel_left;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel_leftBottom;
        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Panel panel_rightBottom;
        private System.Windows.Forms.Panel panel_top;
        private System.Windows.Forms.Panel panel_rightTop;
        private System.Windows.Forms.Panel panel_right;
        private System.Windows.Forms.Panel panel_center;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label4;
    }
}