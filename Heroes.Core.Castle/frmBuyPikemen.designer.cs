namespace Heroes.Core.Castle
{
    partial class frmBuyPikemen
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
            ((System.ComponentModel.ISupportInitialize)(this.picArmy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picArmyLv2)).BeginInit();
            this.SuspendLayout();
            // 
            // picArmy
            // 
            this.picArmy.Image = global::Heroes.Core.Castle.Properties.Resources.pikemen;
            // 
            // pictureBox1
            // 
            this.picArmyLv2.Image = global::Heroes.Core.Castle.Properties.Resources.Halberdiers;
            // 
            // frmBuyPikemen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(480, 368);
            this.DoubleBuffered = true;
            this.Name = "frmBuyPikemen";
            this.Text = "frmPikemen";
            this.Load += new System.EventHandler(this.frmBuyPikemen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picArmy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picArmyLv2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}