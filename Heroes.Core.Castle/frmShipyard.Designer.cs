namespace Heroes.Core.Castle
{
    partial class frmShipyard
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
            this.cmdBuy = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdBuy
            // 
            this.cmdBuy.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.purchase1;
            this.cmdBuy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cmdBuy.Location = new System.Drawing.Point(40, 310);
            this.cmdBuy.Name = "cmdBuy";
            this.cmdBuy.Size = new System.Drawing.Size(65, 35);
            this.cmdBuy.TabIndex = 0;
            this.cmdBuy.UseVisualStyleBackColor = true;
            this.cmdBuy.Click += new System.EventHandler(this.cmdBuy_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.close1;
            this.cmdClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.cmdClose.Location = new System.Drawing.Point(224, 310);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(65, 35);
            this.cmdClose.TabIndex = 1;
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // frmShipyard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Heroes.Core.Castle.Properties.Resources.BuildNewShip1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(328, 361);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdBuy);
            this.DoubleBuffered = true;
            this.Name = "frmShipyard";
            this.Text = "frmShipyard";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdBuy;
        private System.Windows.Forms.Button cmdClose;
    }
}