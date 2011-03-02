using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes
{
    public partial class frmWaiting : Form
    {
        bool _isInitialized;

        Timer _timer1;

        public frmWaiting()
        {
            InitializeComponent();

            _isInitialized = false;

            _timer1 = new Timer();
            _timer1.Interval = 500;
            _timer1.Tick += new EventHandler(_timer1_Tick);
            _timer1.Start();
        }

        private void frmWaiting_Load(object sender, EventArgs e)
        {
            _timer1.Start();
        }

        void _timer1_Tick(object sender, EventArgs e)
        {
            _timer1.Stop();

            if (Remoting.GameSetting._isServer)
            {
                if (Remoting.GameSetting.IsAllPlayerInitialized())
                {
                    Remoting.GameSetting._frmMap.ReadOnly = false;

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                if (!_isInitialized)
                {
                    //ArrayList players = null;
                    //Hashtable heroCells = null;

                    //if (!frmGame.GetGameSettings(out players, out heroCells))
                    //{
                    //    // error
                    //}
                    //else
                    //{
                    //    // update game settings
                    //    Remoting.GameSetting.UpdateGameSettings(players, heroCells);
                    //    Remoting.GameSetting._frmMap.Draw();

                    //    _isInitialized = true;
                    //}
                }
                
                if (_isInitialized)
                {
                    bool b = false;
                    if (!IsAllPlayerInitialized(out b))
                    {
                        // error
                    }

                    if (b)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }

            _timer1.Start();
        }

        private bool IsAllPlayerInitialized(out bool b)
        {
            b = false;

            Heroes.Remoting.RegisterServer register = new Heroes.Remoting.RegisterServer();
            register._hostName = Remoting.GameSetting._serverHostName;

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
                adp.IsAllPlayerInitialized(out b);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }
        

    }
}
