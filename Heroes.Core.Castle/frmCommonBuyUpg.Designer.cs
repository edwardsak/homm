namespace Heroes.Core.Castle
{
    partial class frmCommonBuyUpg
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblAmt = new System.Windows.Forms.Label();
            this.lblCost = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAv = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.cmdBuy = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(192, 64);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(104, 133);
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // lblAmt
            // 
            this.lblAmt.BackColor = System.Drawing.Color.Transparent;
            this.lblAmt.ForeColor = System.Drawing.Color.White;
            this.lblAmt.Location = new System.Drawing.Point(321, 280);
            this.lblAmt.Name = "lblAmt";
            this.lblAmt.Size = new System.Drawing.Size(96, 16);
            this.lblAmt.TabIndex = 23;
            this.lblAmt.Text = "label2";
            // 
            // lblCost
            // 
            this.lblCost.BackColor = System.Drawing.Color.Transparent;
            this.lblCost.ForeColor = System.Drawing.Color.White;
            this.lblCost.Location = new System.Drawing.Point(65, 280);
            this.lblCost.Name = "lblCost";
            this.lblCost.Size = new System.Drawing.Size(96, 16);
            this.lblCost.TabIndex = 22;
            this.lblCost.Text = "label2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Gold;
            this.label1.Location = new System.Drawing.Point(153, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(176, 23);
            this.label1.TabIndex = 21;
            this.label1.Text = "Recruit Pikeman";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblAv
            // 
            this.lblAv.BackColor = System.Drawing.Color.Transparent;
            this.lblAv.ForeColor = System.Drawing.Color.White;
            this.lblAv.Location = new System.Drawing.Point(173, 244);
            this.lblAv.Name = "lblAv";
            this.lblAv.Size = new System.Drawing.Size(64, 17);
            this.lblAv.TabIndex = 19;
            this.lblAv.Text = "100";
            this.lblAv.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(169, 276);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(144, 20);
            this.txtQty.TabIndex = 16;
            // 
            // cmdBuy
            // 
            this.cmdBuy.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.purchase1;
            this.cmdBuy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cmdBuy.Location = new System.Drawing.Point(169, 312);
            this.cmdBuy.Name = "cmdBuy";
            this.cmdBuy.Size = new System.Drawing.Size(66, 35);
            this.cmdBuy.TabIndex = 17;
            this.cmdBuy.UseVisualStyleBackColor = true;
            this.cmdBuy.Click += new System.EventHandler(this.cmdBuy_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.close;
            this.cmdClose.Location = new System.Drawing.Point(249, 312);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(66, 33);
            this.cmdClose.TabIndex = 18;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmCommonBuyUpg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.buyArmy1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(482, 371);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblAmt);
            this.Controls.Add(this.lblCost);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAv);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.cmdBuy);
            this.Controls.Add(this.cmdClose);
            this.DoubleBuffered = true;
            this.Name = "frmCommonBuyUpg";
            this.Text = "frmCommonBuyUpg";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblAmt;
        private System.Windows.Forms.Label lblCost;
        protected System.Windows.Forms.Label label1;
        protected System.Windows.Forms.Label lblAv;
        protected System.Windows.Forms.TextBox txtQty;
        protected System.Windows.Forms.Button cmdBuy;
        protected System.Windows.Forms.Button cmdClose;

    }
}