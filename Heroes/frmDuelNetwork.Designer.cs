namespace Heroes
{
    partial class frmDuelNetwork
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
            this.cmdNextTurn = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblPlayerId = new System.Windows.Forms.Label();
            this.cmdHeroInfo = new System.Windows.Forms.Button();
            this.cmdGold = new System.Windows.Forms.Button();
            this.cmdGem = new System.Windows.Forms.Button();
            this.cmdCrystal = new System.Windows.Forms.Button();
            this.cmdSulfur = new System.Windows.Forms.Button();
            this.cmdMercury = new System.Windows.Forms.Button();
            this.cmdOre = new System.Windows.Forms.Button();
            this.cmdWood = new System.Windows.Forms.Button();
            this.cmdCastle = new System.Windows.Forms.Button();
            this.cmdSteal = new System.Windows.Forms.Button();
            this.cmdConquer = new System.Windows.Forms.Button();
            this.cmdOtherHeroInfo = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.cboOtherPlayerId = new System.Windows.Forms.ComboBox();
            this.cmdKnowledgeStone = new System.Windows.Forms.Button();
            this.cmdWarriorTomb = new System.Windows.Forms.Button();
            this.cmdDwarvenTreasury = new System.Windows.Forms.Button();
            this.cmdGriffinConserv = new System.Windows.Forms.Button();
            this.cmdDragonUtopia = new System.Windows.Forms.Button();
            this.chkQuickCombat = new System.Windows.Forms.CheckBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdNextTurn
            // 
            this.cmdNextTurn.Location = new System.Drawing.Point(8, 320);
            this.cmdNextTurn.Name = "cmdNextTurn";
            this.cmdNextTurn.Size = new System.Drawing.Size(80, 40);
            this.cmdNextTurn.TabIndex = 0;
            this.cmdNextTurn.Text = "Next Turn";
            this.cmdNextTurn.UseVisualStyleBackColor = true;
            this.cmdNextTurn.Click += new System.EventHandler(this.cmdNextTurn_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 363);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(727, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslblStatus
            // 
            this.tsslblStatus.Name = "tsslblStatus";
            this.tsslblStatus.Size = new System.Drawing.Size(66, 17);
            this.tsslblStatus.Text = "tsslblStatus";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "My ID:";
            // 
            // lblPlayerId
            // 
            this.lblPlayerId.AutoSize = true;
            this.lblPlayerId.Location = new System.Drawing.Point(56, 8);
            this.lblPlayerId.Name = "lblPlayerId";
            this.lblPlayerId.Size = new System.Drawing.Size(35, 13);
            this.lblPlayerId.TabIndex = 3;
            this.lblPlayerId.Text = "label2";
            // 
            // cmdHeroInfo
            // 
            this.cmdHeroInfo.Location = new System.Drawing.Point(8, 72);
            this.cmdHeroInfo.Name = "cmdHeroInfo";
            this.cmdHeroInfo.Size = new System.Drawing.Size(80, 40);
            this.cmdHeroInfo.TabIndex = 30;
            this.cmdHeroInfo.Text = "Hero";
            this.cmdHeroInfo.UseVisualStyleBackColor = true;
            this.cmdHeroInfo.Click += new System.EventHandler(this.cmdHeroInfo_Click);
            // 
            // cmdGold
            // 
            this.cmdGold.Location = new System.Drawing.Point(88, 272);
            this.cmdGold.Name = "cmdGold";
            this.cmdGold.Size = new System.Drawing.Size(80, 40);
            this.cmdGold.TabIndex = 29;
            this.cmdGold.Text = "Gold";
            this.cmdGold.UseVisualStyleBackColor = true;
            this.cmdGold.Click += new System.EventHandler(this.cmdWood_Click);
            // 
            // cmdGem
            // 
            this.cmdGem.Location = new System.Drawing.Point(88, 232);
            this.cmdGem.Name = "cmdGem";
            this.cmdGem.Size = new System.Drawing.Size(80, 40);
            this.cmdGem.TabIndex = 28;
            this.cmdGem.Text = "Gems";
            this.cmdGem.UseVisualStyleBackColor = true;
            this.cmdGem.Click += new System.EventHandler(this.cmdWood_Click);
            // 
            // cmdCrystal
            // 
            this.cmdCrystal.Location = new System.Drawing.Point(88, 192);
            this.cmdCrystal.Name = "cmdCrystal";
            this.cmdCrystal.Size = new System.Drawing.Size(80, 40);
            this.cmdCrystal.TabIndex = 27;
            this.cmdCrystal.Text = "Crystal";
            this.cmdCrystal.UseVisualStyleBackColor = true;
            this.cmdCrystal.Click += new System.EventHandler(this.cmdWood_Click);
            // 
            // cmdSulfur
            // 
            this.cmdSulfur.Location = new System.Drawing.Point(88, 152);
            this.cmdSulfur.Name = "cmdSulfur";
            this.cmdSulfur.Size = new System.Drawing.Size(80, 40);
            this.cmdSulfur.TabIndex = 26;
            this.cmdSulfur.Text = "Sulfur";
            this.cmdSulfur.UseVisualStyleBackColor = true;
            this.cmdSulfur.Click += new System.EventHandler(this.cmdWood_Click);
            // 
            // cmdMercury
            // 
            this.cmdMercury.Location = new System.Drawing.Point(88, 112);
            this.cmdMercury.Name = "cmdMercury";
            this.cmdMercury.Size = new System.Drawing.Size(80, 40);
            this.cmdMercury.TabIndex = 25;
            this.cmdMercury.Text = "Mercury";
            this.cmdMercury.UseVisualStyleBackColor = true;
            this.cmdMercury.Click += new System.EventHandler(this.cmdWood_Click);
            // 
            // cmdOre
            // 
            this.cmdOre.Location = new System.Drawing.Point(88, 72);
            this.cmdOre.Name = "cmdOre";
            this.cmdOre.Size = new System.Drawing.Size(80, 40);
            this.cmdOre.TabIndex = 24;
            this.cmdOre.Text = "Ore";
            this.cmdOre.UseVisualStyleBackColor = true;
            this.cmdOre.Click += new System.EventHandler(this.cmdWood_Click);
            // 
            // cmdWood
            // 
            this.cmdWood.Location = new System.Drawing.Point(88, 32);
            this.cmdWood.Name = "cmdWood";
            this.cmdWood.Size = new System.Drawing.Size(80, 40);
            this.cmdWood.TabIndex = 23;
            this.cmdWood.Text = "Wood";
            this.cmdWood.UseVisualStyleBackColor = true;
            this.cmdWood.Click += new System.EventHandler(this.cmdWood_Click);
            // 
            // cmdCastle
            // 
            this.cmdCastle.Location = new System.Drawing.Point(8, 32);
            this.cmdCastle.Name = "cmdCastle";
            this.cmdCastle.Size = new System.Drawing.Size(80, 40);
            this.cmdCastle.TabIndex = 22;
            this.cmdCastle.Text = "Castle";
            this.cmdCastle.UseVisualStyleBackColor = true;
            this.cmdCastle.Click += new System.EventHandler(this.cmdCastle_Click);
            // 
            // cmdSteal
            // 
            this.cmdSteal.Location = new System.Drawing.Point(584, 104);
            this.cmdSteal.Name = "cmdSteal";
            this.cmdSteal.Size = new System.Drawing.Size(80, 40);
            this.cmdSteal.TabIndex = 31;
            this.cmdSteal.Text = "Steal";
            this.cmdSteal.UseVisualStyleBackColor = true;
            this.cmdSteal.Click += new System.EventHandler(this.cmdSteal_Click);
            // 
            // cmdConquer
            // 
            this.cmdConquer.Location = new System.Drawing.Point(584, 144);
            this.cmdConquer.Name = "cmdConquer";
            this.cmdConquer.Size = new System.Drawing.Size(80, 40);
            this.cmdConquer.TabIndex = 32;
            this.cmdConquer.Text = "Conquer";
            this.cmdConquer.UseVisualStyleBackColor = true;
            this.cmdConquer.Click += new System.EventHandler(this.cmdConquer_Click);
            // 
            // cmdOtherHeroInfo
            // 
            this.cmdOtherHeroInfo.Location = new System.Drawing.Point(584, 64);
            this.cmdOtherHeroInfo.Name = "cmdOtherHeroInfo";
            this.cmdOtherHeroInfo.Size = new System.Drawing.Size(80, 40);
            this.cmdOtherHeroInfo.TabIndex = 33;
            this.cmdOtherHeroInfo.Text = "See Hero";
            this.cmdOtherHeroInfo.UseVisualStyleBackColor = true;
            this.cmdOtherHeroInfo.Click += new System.EventHandler(this.cmdOtherHeroInfo_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(488, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 34;
            this.label2.Text = "Other Player ID:";
            // 
            // cboOtherPlayerId
            // 
            this.cboOtherPlayerId.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOtherPlayerId.FormattingEnabled = true;
            this.cboOtherPlayerId.Location = new System.Drawing.Point(584, 40);
            this.cboOtherPlayerId.Name = "cboOtherPlayerId";
            this.cboOtherPlayerId.Size = new System.Drawing.Size(80, 21);
            this.cboOtherPlayerId.TabIndex = 35;
            // 
            // cmdKnowledgeStone
            // 
            this.cmdKnowledgeStone.Location = new System.Drawing.Point(168, 32);
            this.cmdKnowledgeStone.Name = "cmdKnowledgeStone";
            this.cmdKnowledgeStone.Size = new System.Drawing.Size(80, 40);
            this.cmdKnowledgeStone.TabIndex = 36;
            this.cmdKnowledgeStone.Text = "Know. Stone";
            this.cmdKnowledgeStone.UseVisualStyleBackColor = true;
            this.cmdKnowledgeStone.Click += new System.EventHandler(this.cmdKnowledgeStone_Click);
            // 
            // cmdWarriorTomb
            // 
            this.cmdWarriorTomb.Location = new System.Drawing.Point(248, 32);
            this.cmdWarriorTomb.Name = "cmdWarriorTomb";
            this.cmdWarriorTomb.Size = new System.Drawing.Size(80, 40);
            this.cmdWarriorTomb.TabIndex = 37;
            this.cmdWarriorTomb.Text = "Warrior\'s Tomb";
            this.cmdWarriorTomb.UseVisualStyleBackColor = true;
            this.cmdWarriorTomb.Click += new System.EventHandler(this.cmdWarriorTomb_Click);
            // 
            // cmdDwarvenTreasury
            // 
            this.cmdDwarvenTreasury.Location = new System.Drawing.Point(248, 72);
            this.cmdDwarvenTreasury.Name = "cmdDwarvenTreasury";
            this.cmdDwarvenTreasury.Size = new System.Drawing.Size(80, 40);
            this.cmdDwarvenTreasury.TabIndex = 42;
            this.cmdDwarvenTreasury.Text = "Drawen Treasury";
            this.cmdDwarvenTreasury.UseVisualStyleBackColor = true;
            this.cmdDwarvenTreasury.Click += new System.EventHandler(this.cmdWarriorTomb_Click);
            // 
            // cmdGriffinConserv
            // 
            this.cmdGriffinConserv.Location = new System.Drawing.Point(248, 112);
            this.cmdGriffinConserv.Name = "cmdGriffinConserv";
            this.cmdGriffinConserv.Size = new System.Drawing.Size(80, 40);
            this.cmdGriffinConserv.TabIndex = 43;
            this.cmdGriffinConserv.Text = "Griffin Conservatory";
            this.cmdGriffinConserv.UseVisualStyleBackColor = true;
            this.cmdGriffinConserv.Click += new System.EventHandler(this.cmdWarriorTomb_Click);
            // 
            // cmdDragonUtopia
            // 
            this.cmdDragonUtopia.Location = new System.Drawing.Point(248, 152);
            this.cmdDragonUtopia.Name = "cmdDragonUtopia";
            this.cmdDragonUtopia.Size = new System.Drawing.Size(80, 40);
            this.cmdDragonUtopia.TabIndex = 45;
            this.cmdDragonUtopia.Text = "Dragon Utopia";
            this.cmdDragonUtopia.UseVisualStyleBackColor = true;
            this.cmdDragonUtopia.Click += new System.EventHandler(this.cmdWarriorTomb_Click);
            // 
            // chkQuickCombat
            // 
            this.chkQuickCombat.AutoSize = true;
            this.chkQuickCombat.Location = new System.Drawing.Point(624, 336);
            this.chkQuickCombat.Name = "chkQuickCombat";
            this.chkQuickCombat.Size = new System.Drawing.Size(93, 17);
            this.chkQuickCombat.TabIndex = 46;
            this.chkQuickCombat.Text = "Quick Combat";
            this.chkQuickCombat.UseVisualStyleBackColor = true;
            // 
            // frmDuelNetwork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(727, 385);
            this.Controls.Add(this.chkQuickCombat);
            this.Controls.Add(this.cmdDragonUtopia);
            this.Controls.Add(this.cmdGriffinConserv);
            this.Controls.Add(this.cmdDwarvenTreasury);
            this.Controls.Add(this.cmdWarriorTomb);
            this.Controls.Add(this.cmdKnowledgeStone);
            this.Controls.Add(this.cboOtherPlayerId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdOtherHeroInfo);
            this.Controls.Add(this.cmdSteal);
            this.Controls.Add(this.cmdConquer);
            this.Controls.Add(this.cmdHeroInfo);
            this.Controls.Add(this.cmdGold);
            this.Controls.Add(this.cmdGem);
            this.Controls.Add(this.cmdCrystal);
            this.Controls.Add(this.cmdSulfur);
            this.Controls.Add(this.cmdMercury);
            this.Controls.Add(this.cmdOre);
            this.Controls.Add(this.cmdWood);
            this.Controls.Add(this.cmdCastle);
            this.Controls.Add(this.lblPlayerId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.cmdNextTurn);
            this.Name = "frmDuelNetwork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Duel Network";
            this.Load += new System.EventHandler(this.frmDuelNetwork_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdNextTurn;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslblStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPlayerId;
        private System.Windows.Forms.Button cmdHeroInfo;
        private System.Windows.Forms.Button cmdGold;
        private System.Windows.Forms.Button cmdGem;
        private System.Windows.Forms.Button cmdCrystal;
        private System.Windows.Forms.Button cmdSulfur;
        private System.Windows.Forms.Button cmdMercury;
        private System.Windows.Forms.Button cmdOre;
        private System.Windows.Forms.Button cmdWood;
        private System.Windows.Forms.Button cmdCastle;
        private System.Windows.Forms.Button cmdSteal;
        private System.Windows.Forms.Button cmdConquer;
        private System.Windows.Forms.Button cmdOtherHeroInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboOtherPlayerId;
        private System.Windows.Forms.Button cmdKnowledgeStone;
        private System.Windows.Forms.Button cmdWarriorTomb;
        private System.Windows.Forms.Button cmdDwarvenTreasury;
        private System.Windows.Forms.Button cmdGriffinConserv;
        private System.Windows.Forms.Button cmdDragonUtopia;
        private System.Windows.Forms.CheckBox chkQuickCombat;
    }
}