using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes
{
    public partial class frmJoin : Form
    {
        public frmJoin()
        {
            InitializeComponent();

            this.txtYourIp.Text = "127.0.0.1";
            this.txtHostName.Text = "127.0.0.1";
        }

        private void frmJoin_Load(object sender, EventArgs e)
        {

        }

        private void cmdJoin_Click(object sender, EventArgs e)
        {
            int id = 0;
            if (!Join(txtHostName.Text, txtYourIp.Text, out id)) return;

            Remoting.GameSetting._serverHostName = txtHostName.Text;

            Player player = new Player();
            player._id = id;
            player._ip = txtYourIp.Text;

            Remoting.GameSetting._me = player;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public bool Join(string hostName, string yourIp, out int id)
        {
            id = 0;

            Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
            register._hostName = hostName;

            Heroes.Remoting.Game adp = null;
            adp = (Heroes.Remoting.Game)register.GetObject(
                typeof(Heroes.Remoting.Game),
                Heroes.Remoting.Game.CLASSNAME);

            if (adp == null)
            {
                MessageBox.Show("Error");
                return false;
            }

            try
            {
                adp.Join(yourIp, out id);
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
