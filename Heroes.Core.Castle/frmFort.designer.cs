namespace Heroes.Core.Castle
{
    partial class frmFort
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
            this.cmdBuySwordsmen = new System.Windows.Forms.Button();
            this.cmdBuyGriffin = new System.Windows.Forms.Button();
            this.cmdBuyArcher = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdBuyPikemen = new System.Windows.Forms.Button();
            this.cmdBuyMonks = new System.Windows.Forms.Button();
            this.cmdBuyCavaliers = new System.Windows.Forms.Button();
            this.cmdBuyAngels = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdBuySwordsmen
            // 
            this.cmdBuySwordsmen.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.SwordsmenBarrack;
            this.cmdBuySwordsmen.Location = new System.Drawing.Point(404, 155);
            this.cmdBuySwordsmen.Name = "cmdBuySwordsmen";
            this.cmdBuySwordsmen.Size = new System.Drawing.Size(388, 127);
            this.cmdBuySwordsmen.TabIndex = 4;
            this.cmdBuySwordsmen.UseVisualStyleBackColor = true;
            this.cmdBuySwordsmen.Click += new System.EventHandler(this.cmdBuyPikemen_Click);
            // 
            // cmdBuyGriffin
            // 
            this.cmdBuyGriffin.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.GriffinGriffinTower;
            this.cmdBuyGriffin.Location = new System.Drawing.Point(8, 155);
            this.cmdBuyGriffin.Name = "cmdBuyGriffin";
            this.cmdBuyGriffin.Size = new System.Drawing.Size(388, 127);
            this.cmdBuyGriffin.TabIndex = 3;
            this.cmdBuyGriffin.UseVisualStyleBackColor = true;
            this.cmdBuyGriffin.Click += new System.EventHandler(this.cmdBuyPikemen_Click);
            // 
            // cmdBuyArcher
            // 
            this.cmdBuyArcher.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.archerTowerArcher;
            this.cmdBuyArcher.Location = new System.Drawing.Point(404, 23);
            this.cmdBuyArcher.Name = "cmdBuyArcher";
            this.cmdBuyArcher.Size = new System.Drawing.Size(388, 127);
            this.cmdBuyArcher.TabIndex = 2;
            this.cmdBuyArcher.UseVisualStyleBackColor = true;
            this.cmdBuyArcher.Click += new System.EventHandler(this.cmdBuyPikemen_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.cancel;
            this.cmdClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.cmdClose.Location = new System.Drawing.Point(746, 555);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(51, 44);
            this.cmdClose.TabIndex = 1;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdBuyPikemen
            // 
            this.cmdBuyPikemen.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.guardhousePikemen;
            this.cmdBuyPikemen.Location = new System.Drawing.Point(8, 23);
            this.cmdBuyPikemen.Name = "cmdBuyPikemen";
            this.cmdBuyPikemen.Size = new System.Drawing.Size(388, 127);
            this.cmdBuyPikemen.TabIndex = 0;
            this.cmdBuyPikemen.UseVisualStyleBackColor = true;
            this.cmdBuyPikemen.Click += new System.EventHandler(this.cmdBuyPikemen_Click);
            // 
            // cmdBuyMonks
            // 
            this.cmdBuyMonks.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.guardhousePikemen;
            this.cmdBuyMonks.Image = global::Heroes.Core.Castle.Properties.Resources.MonasteryMonk;
            this.cmdBuyMonks.Location = new System.Drawing.Point(8, 288);
            this.cmdBuyMonks.Name = "cmdBuyMonks";
            this.cmdBuyMonks.Size = new System.Drawing.Size(388, 127);
            this.cmdBuyMonks.TabIndex = 5;
            this.cmdBuyMonks.UseVisualStyleBackColor = true;
            this.cmdBuyMonks.Click += new System.EventHandler(this.cmdBuyPikemen_Click);
            // 
            // cmdBuyCavaliers
            // 
            this.cmdBuyCavaliers.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.guardhousePikemen;
            this.cmdBuyCavaliers.Image = global::Heroes.Core.Castle.Properties.Resources.TrainingGroundCavaliers;
            this.cmdBuyCavaliers.Location = new System.Drawing.Point(404, 288);
            this.cmdBuyCavaliers.Name = "cmdBuyCavaliers";
            this.cmdBuyCavaliers.Size = new System.Drawing.Size(388, 127);
            this.cmdBuyCavaliers.TabIndex = 6;
            this.cmdBuyCavaliers.UseVisualStyleBackColor = true;
            this.cmdBuyCavaliers.Click += new System.EventHandler(this.cmdBuyPikemen_Click);
            // 
            // cmdBuyAngels
            // 
            this.cmdBuyAngels.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.guardhousePikemen;
            this.cmdBuyAngels.Image = global::Heroes.Core.Castle.Properties.Resources.PortalofGloryAngels;
            this.cmdBuyAngels.Location = new System.Drawing.Point(206, 422);
            this.cmdBuyAngels.Name = "cmdBuyAngels";
            this.cmdBuyAngels.Size = new System.Drawing.Size(388, 127);
            this.cmdBuyAngels.TabIndex = 7;
            this.cmdBuyAngels.UseVisualStyleBackColor = true;
            this.cmdBuyAngels.Click += new System.EventHandler(this.cmdBuyPikemen_Click);
            // 
            // frmFort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.CastleBlock;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(803, 602);
            this.Controls.Add(this.cmdBuyAngels);
            this.Controls.Add(this.cmdBuyCavaliers);
            this.Controls.Add(this.cmdBuyMonks);
            this.Controls.Add(this.cmdBuySwordsmen);
            this.Controls.Add(this.cmdBuyGriffin);
            this.Controls.Add(this.cmdBuyArcher);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdBuyPikemen);
            this.Name = "frmFort";
            this.Text = "frmFort";
            this.Load += new System.EventHandler(this.frmFort_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdBuyPikemen;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdBuyArcher;
        private System.Windows.Forms.Button cmdBuyGriffin;
        private System.Windows.Forms.Button cmdBuySwordsmen;
        private System.Windows.Forms.Button cmdBuyMonks;
        private System.Windows.Forms.Button cmdBuyCavaliers;
        private System.Windows.Forms.Button cmdBuyAngels;
    }
}