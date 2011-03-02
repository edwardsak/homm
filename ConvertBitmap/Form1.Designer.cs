namespace ConvertBitmap
{
    partial class Form1
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
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.txtFile = new System.Windows.Forms.TextBox();
            this.cmdSave = new System.Windows.Forms.Button();
            this.chkFlip = new System.Windows.Forms.CheckBox();
            this.chkSel = new System.Windows.Forms.CheckBox();
            this.cmdSaveAll = new System.Windows.Forms.Button();
            this.optGif = new System.Windows.Forms.RadioButton();
            this.optPng = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.Location = new System.Drawing.Point(8, 40);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.Size = new System.Drawing.Size(75, 23);
            this.cmdBrowse.TabIndex = 0;
            this.cmdBrowse.Text = "Browse";
            this.cmdBrowse.UseVisualStyleBackColor = true;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // txtFile
            // 
            this.txtFile.Location = new System.Drawing.Point(8, 16);
            this.txtFile.Name = "txtFile";
            this.txtFile.Size = new System.Drawing.Size(608, 20);
            this.txtFile.TabIndex = 1;
            // 
            // cmdSave
            // 
            this.cmdSave.Location = new System.Drawing.Point(8, 112);
            this.cmdSave.Name = "cmdSave";
            this.cmdSave.Size = new System.Drawing.Size(75, 23);
            this.cmdSave.TabIndex = 2;
            this.cmdSave.Text = "Save";
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // chkFlip
            // 
            this.chkFlip.AutoSize = true;
            this.chkFlip.Location = new System.Drawing.Point(8, 88);
            this.chkFlip.Name = "chkFlip";
            this.chkFlip.Size = new System.Drawing.Size(42, 17);
            this.chkFlip.TabIndex = 3;
            this.chkFlip.Text = "Flip";
            this.chkFlip.UseVisualStyleBackColor = true;
            // 
            // chkSel
            // 
            this.chkSel.AutoSize = true;
            this.chkSel.Location = new System.Drawing.Point(64, 88);
            this.chkSel.Name = "chkSel";
            this.chkSel.Size = new System.Drawing.Size(70, 17);
            this.chkSel.TabIndex = 4;
            this.chkSel.Text = "Selection";
            this.chkSel.UseVisualStyleBackColor = true;
            // 
            // cmdSaveAll
            // 
            this.cmdSaveAll.Location = new System.Drawing.Point(144, 112);
            this.cmdSaveAll.Name = "cmdSaveAll";
            this.cmdSaveAll.Size = new System.Drawing.Size(75, 23);
            this.cmdSaveAll.TabIndex = 5;
            this.cmdSaveAll.Text = "Save All";
            this.cmdSaveAll.UseVisualStyleBackColor = true;
            this.cmdSaveAll.Click += new System.EventHandler(this.cmdSaveAll_Click);
            // 
            // optGif
            // 
            this.optGif.AutoSize = true;
            this.optGif.Location = new System.Drawing.Point(256, 48);
            this.optGif.Name = "optGif";
            this.optGif.Size = new System.Drawing.Size(36, 17);
            this.optGif.TabIndex = 6;
            this.optGif.Text = "gif";
            this.optGif.UseVisualStyleBackColor = true;
            // 
            // optPng
            // 
            this.optPng.AutoSize = true;
            this.optPng.Checked = true;
            this.optPng.Location = new System.Drawing.Point(200, 48);
            this.optPng.Name = "optPng";
            this.optPng.Size = new System.Drawing.Size(43, 17);
            this.optPng.TabIndex = 7;
            this.optPng.TabStop = true;
            this.optPng.Text = "png";
            this.optPng.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 262);
            this.Controls.Add(this.optPng);
            this.Controls.Add(this.optGif);
            this.Controls.Add(this.cmdSaveAll);
            this.Controls.Add(this.chkSel);
            this.Controls.Add(this.chkFlip);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.txtFile);
            this.Controls.Add(this.cmdBrowse);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdBrowse;
        private System.Windows.Forms.TextBox txtFile;
        private System.Windows.Forms.Button cmdSave;
        private System.Windows.Forms.CheckBox chkFlip;
        private System.Windows.Forms.CheckBox chkSel;
        private System.Windows.Forms.Button cmdSaveAll;
        private System.Windows.Forms.RadioButton optGif;
        private System.Windows.Forms.RadioButton optPng;
    }
}

