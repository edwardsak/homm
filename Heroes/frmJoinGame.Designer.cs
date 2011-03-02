namespace Heroes
{
    partial class frmJoinGame
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
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdJoin = new System.Windows.Forms.Button();
            this.txtServerIp = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_saveIP = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button_deleteIP = new System.Windows.Forms.Button();
            this.txtPlayerName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(177, 154);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(72, 32);
            this.cmdCancel.TabIndex = 7;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdJoin
            // 
            this.cmdJoin.Location = new System.Drawing.Point(97, 154);
            this.cmdJoin.Name = "cmdJoin";
            this.cmdJoin.Size = new System.Drawing.Size(72, 32);
            this.cmdJoin.TabIndex = 6;
            this.cmdJoin.Text = "Join";
            this.cmdJoin.UseVisualStyleBackColor = true;
            this.cmdJoin.Click += new System.EventHandler(this.cmdJoin_Click);
            // 
            // txtServerIp
            // 
            this.txtServerIp.Location = new System.Drawing.Point(88, 34);
            this.txtServerIp.Name = "txtServerIp";
            this.txtServerIp.Size = new System.Drawing.Size(176, 20);
            this.txtServerIp.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Server IP:";
            // 
            // button_saveIP
            // 
            this.button_saveIP.Location = new System.Drawing.Point(275, 31);
            this.button_saveIP.Name = "button_saveIP";
            this.button_saveIP.Size = new System.Drawing.Size(75, 23);
            this.button_saveIP.TabIndex = 8;
            this.button_saveIP.Text = "Save this IP";
            this.button_saveIP.UseVisualStyleBackColor = true;
            this.button_saveIP.Click += new System.EventHandler(this.button_saveIP_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(88, 80);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(176, 56);
            this.listBox1.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(85, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Available Saved IP:";
            // 
            // button_deleteIP
            // 
            this.button_deleteIP.Location = new System.Drawing.Point(275, 80);
            this.button_deleteIP.Name = "button_deleteIP";
            this.button_deleteIP.Size = new System.Drawing.Size(109, 43);
            this.button_deleteIP.TabIndex = 11;
            this.button_deleteIP.Text = "button1";
            this.button_deleteIP.UseVisualStyleBackColor = true;
            this.button_deleteIP.Click += new System.EventHandler(this.button_deleteIP_Click);
            // 
            // txtPlayerName
            // 
            this.txtPlayerName.Location = new System.Drawing.Point(88, 8);
            this.txtPlayerName.Name = "txtPlayerName";
            this.txtPlayerName.Size = new System.Drawing.Size(176, 20);
            this.txtPlayerName.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Player Name:";
            // 
            // frmJoinGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 251);
            this.Controls.Add(this.txtPlayerName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button_deleteIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button_saveIP);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdJoin);
            this.Controls.Add(this.txtServerIp);
            this.Controls.Add(this.label1);
            this.Name = "frmJoinGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Join Game";
            this.Load += new System.EventHandler(this.frmJoinGame_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.Button cmdJoin;
        private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_saveIP;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_deleteIP;
        private System.Windows.Forms.TextBox txtPlayerName;
        private System.Windows.Forms.Label label3;
    }
}