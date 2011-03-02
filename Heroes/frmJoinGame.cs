using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Heroes
{
    public partial class frmJoinGame : Form
    {
        TextReader tr;
        public Heroes.Core.Player _player;

        public frmJoinGame()
        {
            InitializeComponent();

            _player = null;

            this.txtServerIp.Text = "127.0.0.1";

            this.listBox1.SelectedIndexChanged += new EventHandler(listBox1_SelectedIndexChanged);
        }

        private void frmJoinGame_Load(object sender, EventArgs e)
        {
            this.button_deleteIP.Visible = false;
            loadIPFromTxt();
            if (this.listBox1.Items.Count > 0)
            {
                this.txtServerIp.Text = (string)this.listBox1.Items[0];
            }
        }

        private void cmdJoin_Click(object sender, EventArgs e)
        {
            if (this.txtPlayerName.Text.Length < 1)
            {
                MessageBox.Show("Please enter a Player Name");
                this.txtPlayerName.Focus();
                return;
            }

            if (this.txtServerIp.Text == "")
            {
                MessageBox.Show("Please enter the server's IP");
                this.txtServerIp.Focus();
            }
            else
            {
                Setting._remoteHostName = txtServerIp.Text;

                if (!RemoteJoinGame(out _player))
                {
                    Debug.WriteLine("RemoteJoinGame failed.");
                    return;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool RemoteJoinGame(out Heroes.Core.Player player)
        {
            player = null;

            Heroes.Core.Remoting.RegisterServer register = new Heroes.Core.Remoting.RegisterServer();
            register._hostName = this.txtServerIp.Text;

            Heroes.Core.Remoting.Game adp = null;
            adp = (Heroes.Core.Remoting.Game)register.GetObject(
                typeof(Heroes.Core.Remoting.Game),
                Heroes.Core.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                adp.JoinGame(out player);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button_saveIP_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Add(this.txtServerIp.Text);
            saveIP();
        }

        void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.txtServerIp.Text = (string)this.listBox1.Items[this.listBox1.SelectedIndex];
                this.button_deleteIP.Text = string.Format("Delete this IP:\r\n" + (string)this.listBox1.Items[this.listBox1.SelectedIndex]);
                this.button_deleteIP.Visible = true;
            }
            catch
            { }
        }

        private void button_deleteIP_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Remove(this.txtServerIp.Text);
            saveIP();
            this.button_deleteIP.Visible = false;
            if (this.listBox1.Items.Count != 0)
                this.listBox1.SelectedIndex = 0;
            else
                this.txtServerIp.Text = "";
        }

        void saveIP()
        {
            TextWriter tw = new StreamWriter(Application.StartupPath + @"\saveip.txt");
            foreach (string s in this.listBox1.Items)
            {
                tw.WriteLine(s);
            }
            tw.Close();
        }

        void loadIPFromTxt()
        {
            try
            {
                tr = new StreamReader(Application.StartupPath + @"\saveip.txt");
                string s = tr.ReadLine();
                while (s != "")
                {
                    this.listBox1.Items.Add(s);
                    s = tr.ReadLine();
                }
            }
            catch
            {
            }
            finally
            {
                if (tr != null)
                    tr.Close();
            }
        }

    }
}
