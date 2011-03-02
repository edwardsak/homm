namespace Heroes.Core.Castle
{
    partial class frmCommonBuy
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
            this.lblAv = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.picArmy = new System.Windows.Forms.PictureBox();
            this.cmdBuy = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.lblCost = new System.Windows.Forms.Label();
            this.lblAmt = new System.Windows.Forms.Label();
            this.picArmyLv2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picArmy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picArmyLv2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblAv
            // 
            this.lblAv.BackColor = System.Drawing.Color.Transparent;
            this.lblAv.ForeColor = System.Drawing.Color.White;
            this.lblAv.Location = new System.Drawing.Point(172, 244);
            this.lblAv.Name = "lblAv";
            this.lblAv.Size = new System.Drawing.Size(64, 17);
            this.lblAv.TabIndex = 10;
            this.lblAv.Text = "100";
            this.lblAv.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(168, 276);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(144, 20);
            this.txtQty.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(152, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "Recruit Pikeman";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // picArmy
            // 
            this.picArmy.Location = new System.Drawing.Point(252, 64);
            this.picArmy.Name = "picArmy";
            this.picArmy.Size = new System.Drawing.Size(100, 128);
            this.picArmy.TabIndex = 11;
            this.picArmy.TabStop = false;
            this.picArmy.Click += new System.EventHandler(this.picArmy_Click);
            // 
            // cmdBuy
            // 
            this.cmdBuy.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.purchase1;
            this.cmdBuy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cmdBuy.Location = new System.Drawing.Point(168, 312);
            this.cmdBuy.Name = "cmdBuy";
            this.cmdBuy.Size = new System.Drawing.Size(66, 35);
            this.cmdBuy.TabIndex = 7;
            this.cmdBuy.UseVisualStyleBackColor = true;
            this.cmdBuy.Click += new System.EventHandler(this.cmdBuy_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.close;
            this.cmdClose.Location = new System.Drawing.Point(248, 312);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(66, 33);
            this.cmdClose.TabIndex = 8;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // lblCost
            // 
            this.lblCost.BackColor = System.Drawing.Color.Transparent;
            this.lblCost.ForeColor = System.Drawing.Color.White;
            this.lblCost.Location = new System.Drawing.Point(64, 280);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(96, 16);
            this.lblCost.TabIndex = 13;
            this.lblCost.Text = "label2";
            // 
            // lblAmt
            // 
            this.lblAmt.BackColor = System.Drawing.Color.Transparent;
            this.lblAmt.ForeColor = System.Drawing.Color.White;
            this.lblAmt.Location = new System.Drawing.Point(320, 280);
            this.lblAmt.Name = "lblAmt";
            this.lblAmt.Size = new System.Drawing.Size(96, 16);
            this.lblAmt.TabIndex = 14;
            this.lblAmt.Text = "label2";
            // 
            // picArmyLv2
            // 
            this.picArmyLv2.Location = new System.Drawing.Point(128, 64);
            this.picArmyLv2.Name = "picArmyLv2";
            this.picArmyLv2.Size = new System.Drawing.Size(100, 128);
            this.picArmyLv2.TabIndex = 15;
            this.picArmyLv2.TabStop = false;
            this.picArmyLv2.Click += new System.EventHandler(this.picArmyLv2_Click);
            // 
            // frmCommonBuy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.buyArmy1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(481, 371);
            this.Controls.Add(this.picArmyLv2);
            this.Controls.Add(this.lblAmt);
            this.Controls.Add(this.lblCost);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picArmy);
            this.Controls.Add(this.lblAv);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.cmdBuy);
            this.Controls.Add(this.cmdClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCommonBuy";
            this.Text = "Recruit";
            this.Load += new System.EventHandler(this.frmCommonBuy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picArmy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picArmyLv2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.Label lblAv;
        protected System.Windows.Forms.TextBox txtQty;
        protected System.Windows.Forms.Button cmdBuy;
        protected System.Windows.Forms.Button cmdClose;
        protected System.Windows.Forms.PictureBox picArmy;
        protected System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCost;
        private System.Windows.Forms.Label lblAmt;
        protected System.Windows.Forms.PictureBox picArmyLv2;
    }
}