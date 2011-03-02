namespace Heroes
{
    partial class frmMain
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
            this.cmdJoinGame = new System.Windows.Forms.Button();
            this.cmdSinglePlayer = new System.Windows.Forms.Button();
            this.cmd_SinglePlayerNewMap = new System.Windows.Forms.Button();
            this.cmdDuel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_QuitGame = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdCreateGame
            // 
            this.cmdCreateGame.Location = new System.Drawing.Point(144, 24);
            this.cmdCreateGame.Name = "cmdCreateGame";
            this.cmdCreateGame.Size = new System.Drawing.Size(96, 32);
            this.cmdCreateGame.TabIndex = 1;
            this.cmdCreateGame.Text = "Create Game";
            this.cmdCreateGame.UseVisualStyleBackColor = true;
            this.cmdCreateGame.Click += new System.EventHandler(this.cmdCreateGame_Click);
            // 
            // cmdJoinGame
            // 
            this.cmdJoinGame.Location = new System.Drawing.Point(144, 64);
            this.cmdJoinGame.Name = "cmdJoinGame";
            this.cmdJoinGame.Size = new System.Drawing.Size(96, 32);
            this.cmdJoinGame.TabIndex = 2;
            this.cmdJoinGame.Text = "Join Game";
            this.cmdJoinGame.UseVisualStyleBackColor = true;
            this.cmdJoinGame.Click += new System.EventHandler(this.cmdJoinGame_Click);
            // 
            // cmdSinglePlayer
            // 
            this.cmdSinglePlayer.Location = new System.Drawing.Point(8, 24);
            this.cmdSinglePlayer.Name = "cmdSinglePlayer";
            this.cmdSinglePlayer.Size = new System.Drawing.Size(96, 32);
            this.cmdSinglePlayer.TabIndex = 3;
            this.cmdSinglePlayer.Text = "Single Player";
            this.cmdSinglePlayer.UseVisualStyleBackColor = true;
            this.cmdSinglePlayer.Click += new System.EventHandler(this.cmdSinglePlayer_Click);
            // 
            // cmd_SinglePlayerNewMap
            // 
            this.cmd_SinglePlayerNewMap.Location = new System.Drawing.Point(8, 64);
            this.cmd_SinglePlayerNewMap.Name = "cmd_SinglePlayerNewMap";
            this.cmd_SinglePlayerNewMap.Size = new System.Drawing.Size(96, 48);
            this.cmd_SinglePlayerNewMap.TabIndex = 4;
            this.cmd_SinglePlayerNewMap.Text = "Single Player (new map)";
            this.cmd_SinglePlayerNewMap.UseVisualStyleBackColor = true;
            this.cmd_SinglePlayerNewMap.Click += new System.EventHandler(this.cmd_SinglePlayerNewMap_Click);
            // 
            // cmdDuel
            // 
            this.cmdDuel.Location = new System.Drawing.Point(8, 120);
            this.cmdDuel.Name = "cmdDuel";
            this.cmdDuel.Size = new System.Drawing.Size(96, 32);
            this.cmdDuel.TabIndex = 5;
            this.cmdDuel.Text = "Duel";
            this.cmdDuel.UseVisualStyleBackColor = true;
            this.cmdDuel.Click += new System.EventHandler(this.cmdDuel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmdSinglePlayer);
            this.groupBox1.Controls.Add(this.cmdDuel);
            this.groupBox1.Controls.Add(this.cmd_SinglePlayerNewMap);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(112, 160);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Single Player";
            // 
            // button_QuitGame
            // 
            this.button_QuitGame.Location = new System.Drawing.Point(144, 106);
            this.button_QuitGame.Name = "button_QuitGame";
            this.button_QuitGame.Size = new System.Drawing.Size(96, 32);
            this.button_QuitGame.TabIndex = 7;
            this.button_QuitGame.Text = "Quit Game";
            this.button_QuitGame.UseVisualStyleBackColor = true;
            this.button_QuitGame.Click += new System.EventHandler(this.button_QuitGame_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 262);
            this.Controls.Add(this.button_QuitGame);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmdJoinGame);
            this.Controls.Add(this.cmdCreateGame);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdCreateGame;
        private System.Windows.Forms.Button cmdJoinGame;
        private System.Windows.Forms.Button cmdSinglePlayer;
        private System.Windows.Forms.Button cmd_SinglePlayerNewMap;
        private System.Windows.Forms.Button cmdDuel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_QuitGame;

    }
}