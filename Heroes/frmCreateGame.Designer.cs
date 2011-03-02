namespace Heroes
{
    partial class frmCreateGame
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
            this.cmdCreateGame = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtServerIp = new System.Windows.Forms.TextBox();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdCreateGame
            // 
            this.cmdCreateGame.Location = new System.Drawing.Point(120, 64);
            this.cmdCreateGame.Name = "cmdCreateGame";
            this.cmdCreateGame.Size = new System.Drawing.Size(72, 32);
            this.cmdCreateGame.TabIndex = 0;
            this.cmdCreateGame.Text = "Create";
            this.cmdCreateGame.UseVisualStyleBackColor = true;
            this.cmdCreateGame.Click += new System.EventHandler(this.cmdCreateGame_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server IP:";
            // 
            // txtServerIp
            // 
            this.txtServerIp.Location = new System.Drawing.Point(96, 8);
            this.txtServerIp.Name = "txtServerIp";
            this.txtServerIp.Size = new System.Drawing.Size(176, 20);
            this.txtServerIp.TabIndex = 2;
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(200, 64);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(72, 32);
            this.cmdCancel.TabIndex = 5;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // frmCreateGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 100);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.txtServerIp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCreateGame);
            this.Name = "frmCreateGame";
            this.Text = "Create Game";
            this.Load += new System.EventHandler(this.frmCreateGame_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCreateGame;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.Button cmdCancel;
    }
}