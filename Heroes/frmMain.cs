using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Heroes
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string appStartupPath = Application.StartupPath;

            if (!Heroes.Core.Setting.GetSettings(appStartupPath))
            {
                MessageBox.Show("Get Settings fail.");
                this.Close();
                return;
            }

            Heroes.Core.Battle.Setting._appStartupPath = appStartupPath;
        }

        private void ConvertBitmap()
        {
            //{
            //        Heroes.Core.Resource rs = new Heroes.Core.Resource("Heroes.Core.Map.Images.ccrusd30.bmp", "Heroes.Core.Map", Heroes.Core.Map.Properties.Resources.ResourceManager, Heroes.Core.Map.Properties.Resources.Culture);
            //        Image img = Image.FromStream(rs.GetStream());
            //        img.RotateFlip(RotateFlipType.RotateNoneFlipX);
            //        Bitmap bmp = new Bitmap(img.Width, img.Height);
            //        using (Graphics g = Graphics.FromImage(bmp))
            //        {
            //            g.Clear(Color.Transparent);
            //            g.DrawImage(img, 0, 0, img.Width, img.Height);
            //            for (int y = 0; y < bmp.Height; y++)
            //            {
            //                for (int x = 0; x < bmp.Width; x++)
            //                {
            //                    int c = bmp.GetPixel(x, y).ToArgb();
            //                    if (c == Color.Cyan.ToArgb())
            //                    {
            //                        bmp.SetPixel(x, y, Color.Transparent);
            //                    }
            //                    else if (c == Color.Magenta.ToArgb())
            //                    {
            //                        bmp.SetPixel(x, y, Color.FromArgb(128, 0, 0, 0));
            //                    }
            //                }
            //            }
            //        }

            //        using (Graphics g = Graphics.FromImage(this.BackgroundImage))
            //        {
            //            g.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);
            //        }
            //    }
        }

        private void cmdCreateGame_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();

                using (frmCreateGame f = new frmCreateGame())
                {
                    if (f.ShowDialog() == DialogResult.Cancel) return;

                    using (frmGame2 f2 = new frmGame2())
                    {
                        f2.ShowDialog(f._player, true);
                    }
                }

                //Remoting.GameSetting.ResetAll();

                //frmGame f = new frmGame();
                //f.FormTerminated += new frmGame.FormTerminatedEventHandler(frmGame_FormTerminated);
                //f.ShowDialog(true);


            }
            finally
            {
                this.Show();
            }
        }

        private void cmdJoinGame_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();

                using (frmJoinGame f = new frmJoinGame())
                {
                    if (f.ShowDialog() == DialogResult.Cancel) return;

                    using (frmGame2 f2 = new frmGame2())
                    {
                        f2.ShowDialog(f._player, false);
                    }
                }

                //frmJoin f = new frmJoin();
                //if (f.ShowDialog() != DialogResult.OK)
                //{
                //    this.Show();
                //    return;
                //}

                //frmGame f2 = new frmGame();
                //f2.FormTerminated += new frmGame.FormTerminatedEventHandler(frmGame_FormTerminated);
                //f2.ShowDialog(false);
            }
            finally
            {
                this.Show();
            }
        }

        void frmGame_FormTerminated()
        {
            this.Show();
        }

        #region Single Player
        private void cmdSinglePlayer_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();

                using (Heroes.Core.Map.frmMap f = new Heroes.Core.Map.frmMap())
                {
                    f.IsMultiPlayer = false;
                    f.ReadOnly = false;
                    f.Initialize(4);

                    f.VisitingCastle += new Heroes.Core.Map.frmMap.VisitingCastleEventHandler(f_VisitingCastle);
                    f.StartingBattle += new Heroes.Core.Map.frmMap.StartingBattleEventHandler(f_StartingBattle);
                    f.ShowDialog();
                }
            }
            finally
            {
                this.Show();
            }
        }

        void f_VisitingCastle(Heroes.Core.Map.frmMap.VisitingCastleEventArg e)
        {
            using (Heroes.Core.Castle.frmCastle f = new Heroes.Core.Castle.frmCastle())
            {
                f.ShowDialog(e._castle);
            }
        }

        void f_StartingBattle(Heroes.Core.Map.frmMap.StartingBattleEventArg e)
        {
            using (frmBattle f = new frmBattle())
            {
                if (f.ShowDialog(e._attackHero, e._defendHero, e._monster, e._defendCastle) != DialogResult.OK)
                {
                    return;
                }

                e._victory = (int)f._victory;
            }
            
        }
        #endregion

        private void cmd_SinglePlayerNewMap_Click(object sender, EventArgs e)
        {
            try
            {
                using (Heroes.Core.Map.frmMap3 f = new Heroes.Core.Map.frmMap3())
                {
                    this.Hide();
                    f.ShowDialog();
                }
            }
            finally
            {
                this.Show();
            }
        }

        private void cmdDuel_Click(object sender, EventArgs e)
        {
            this.Hide();

            frmDuel f = new frmDuel();
            f.ShowDialog();

            this.Show();
        }

        private void button_QuitGame_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
