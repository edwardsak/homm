﻿namespace Heroes.Core.Castle
{
    partial class frmBuyGriffin
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
            this.picArmy.Image = global::Heroes.Core.Castle.Properties.Resources.Griffin;
            // 
            // label1
            // 
            this.label1.Text = "Recruit Griffin";
            // 
            // pictureBox1
            // 
            this.picArmyLv2.Image = global::Heroes.Core.Castle.Properties.Resources.RoyalGriffins;
            // 
            // frmBuyGriffin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 371);
            this.Name = "frmBuyGriffin";
            this.Text = "frmBuyGriffin";
            ((System.ComponentModel.ISupportInitialize)(this.picArmy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picArmyLv2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}