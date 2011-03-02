using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes
{
    public partial class frmCreateGame : Form
    {
        public Heroes.Core.Player _player;

        public frmCreateGame()
        {
            InitializeComponent();

            this.txtServerIp.Text = "127.0.0.1";
        }

        private void frmCreateGame_Load(object sender, EventArgs e)
        {

        }

        private void cmdCreateGame_Click(object sender, EventArgs e)
        {
            Setting._remoteHostName = txtServerIp.Text;

            if (!RemoteCreateGame(out _player)) return;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool RemoteCreateGame(out Heroes.Core.Player player)
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
                adp.CreateGame(out player);
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

    }
}
