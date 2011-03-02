using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Castle
{
    public partial class CastleView : UserControl
    {
        public Heroes.Core.Town _town;

        public CastleView()
        {
            InitializeComponent();

            _town = null;
        }

        private void CastleView_Load(object sender, EventArgs e)
        {

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Draw();

            base.OnPaint(e);
        }

        public void Draw()
        {
            if (_town == null) return;

            Bitmap bmp = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // clear
                g.Clear(Color.White);

                Image bg = Heroes.Core.Castle.Properties.Resources.TBcsback;
                g.DrawImage(bg, 0, 0, bg.Width, bg.Height);

                // sort by orderSeq
                ArrayList buildings = new ArrayList();
                foreach (Building building in _town._buildingKIds.Values)
                {
                    buildings.Add(building);
                }
                buildings.Sort(new Heroes.Core.CompareOrderSeq());

                // draw buildings
                foreach (Building building in buildings)
                {
                    if (!System.IO.File.Exists(building._imgFileName)) continue;

                    Image img = Image.FromFile(building._imgFileName);
                    g.DrawImage(img, building._point.X, building._point.Y, img.Width, img.Height);
                }
            }

            using (Graphics g = this.CreateGraphics())
            {
                g.DrawImage(bmp, 0, 0, bmp.Width, bmp.Height);
            }

            bmp.Dispose();
        }

    }
}
