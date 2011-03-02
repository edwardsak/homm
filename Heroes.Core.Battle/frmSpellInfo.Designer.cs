namespace Heroes.Core.Battle
{
    partial class frmSpellInfo
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
            this.lblName = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblNameSmall = new System.Windows.Forms.Label();
            this.picSpell = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picSpell)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.Gold;
            this.lblName.Location = new System.Drawing.Point(12, 52);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(296, 20);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Magic Arrow";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.ForeColor = System.Drawing.Color.White;
            this.lblDesc.Location = new System.Drawing.Point(12, 100);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(296, 76);
            this.lblDesc.TabIndex = 1;
            this.lblDesc.Text = "Descriptions";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblNameSmall
            // 
            this.lblNameSmall.BackColor = System.Drawing.Color.Transparent;
            this.lblNameSmall.ForeColor = System.Drawing.Color.White;
            this.lblNameSmall.Location = new System.Drawing.Point(12, 276);
            this.lblNameSmall.Name = "lblNameSmall";
            this.lblNameSmall.Size = new System.Drawing.Size(296, 13);
            this.lblNameSmall.TabIndex = 4;
            this.lblNameSmall.Text = "Name";
            this.lblNameSmall.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // picSpell
            // 
            this.picSpell.BackColor = System.Drawing.Color.Transparent;
            this.picSpell.Location = new System.Drawing.Point(120, 208);
            this.picSpell.Name = "picSpell";
            this.picSpell.Size = new System.Drawing.Size(78, 65);
            this.picSpell.TabIndex = 3;
            this.picSpell.TabStop = false;
            // 
            // frmSpellInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Heroes.Core.Battle.Properties.Resources.SpellInfoDlg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(322, 322);
            this.Controls.Add(this.lblNameSmall);
            this.Controls.Add(this.picSpell);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSpellInfo";
            this.Text = "Spell Info";
            this.Load += new System.EventHandler(this.frmSpellInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picSpell)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblDesc;
        private System.Windows.Forms.PictureBox picSpell;
        private System.Windows.Forms.Label lblNameSmall;
    }
}