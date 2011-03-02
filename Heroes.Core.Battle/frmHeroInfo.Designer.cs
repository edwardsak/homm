namespace Heroes.Core.Battle
{
    partial class frmHeroInfo
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblKnowledge = new System.Windows.Forms.Label();
            this.lblSpellPoint = new System.Windows.Forms.Label();
            this.lblPower = new System.Windows.Forms.Label();
            this.lblDefense = new System.Windows.Forms.Label();
            this.lblAttack = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Attack";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Defense";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Power";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(112, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Knowledge";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(112, 136);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Spell Points";
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(112, 16);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(75, 13);
            this.lblLevel.TabIndex = 5;
            this.lblLevel.Text = "Level 1 Knight";
            // 
            // lblKnowledge
            // 
            this.lblKnowledge.Location = new System.Drawing.Point(200, 112);
            this.lblKnowledge.Name = "lblKnowledge";
            this.lblKnowledge.Size = new System.Drawing.Size(72, 16);
            this.lblKnowledge.TabIndex = 6;
            this.lblKnowledge.Text = "0";
            this.lblKnowledge.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSpellPoint
            // 
            this.lblSpellPoint.Location = new System.Drawing.Point(200, 136);
            this.lblSpellPoint.Name = "lblSpellPoint";
            this.lblSpellPoint.Size = new System.Drawing.Size(72, 16);
            this.lblSpellPoint.TabIndex = 7;
            this.lblSpellPoint.Text = "10/10";
            this.lblSpellPoint.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPower
            // 
            this.lblPower.Location = new System.Drawing.Point(200, 88);
            this.lblPower.Name = "lblPower";
            this.lblPower.Size = new System.Drawing.Size(72, 16);
            this.lblPower.TabIndex = 8;
            this.lblPower.Text = "0";
            this.lblPower.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblDefense
            // 
            this.lblDefense.Location = new System.Drawing.Point(200, 64);
            this.lblDefense.Name = "lblDefense";
            this.lblDefense.Size = new System.Drawing.Size(72, 16);
            this.lblDefense.TabIndex = 9;
            this.lblDefense.Text = "0";
            this.lblDefense.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblAttack
            // 
            this.lblAttack.Location = new System.Drawing.Point(200, 40);
            this.lblAttack.Name = "lblAttack";
            this.lblAttack.Size = new System.Drawing.Size(72, 16);
            this.lblAttack.TabIndex = 10;
            this.lblAttack.Text = "0";
            this.lblAttack.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(8, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(88, 96);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // frmHeroInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 164);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblAttack);
            this.Controls.Add(this.lblDefense);
            this.Controls.Add(this.lblPower);
            this.Controls.Add(this.lblSpellPoint);
            this.Controls.Add(this.lblKnowledge);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmHeroInfo";
            this.Text = "0";
            this.Load += new System.EventHandler(this.frmHeroInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblKnowledge;
        private System.Windows.Forms.Label lblSpellPoint;
        private System.Windows.Forms.Label lblPower;
        private System.Windows.Forms.Label lblDefense;
        private System.Windows.Forms.Label lblAttack;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}