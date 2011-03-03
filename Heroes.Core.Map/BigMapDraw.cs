using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Map
{
    public partial class BigMapDraw : UserControl
    {
        public Image _bigMap;
        public Terrain.MapTerrain _mapTerrain;

        public BigMapDraw()
        {
            InitializeComponent();
        }

        private void BigMapController_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Draw();

            base.OnPaint(e);
        }

        public void Draw()
        {
            if (_bigMap == null) return;

            try
            {
                Bitmap bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    g.DrawImage(_bigMap, -gv._bigMapPtX, -gv._bigMapPtY, _bigMap.Width, _bigMap.Height);

                    _mapTerrain.Draw2(g);


                }


                using (Graphics g = this.CreateGraphics())
                {
                    g.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);
                }

            }
            catch
            {

            }
        }
    }
}
