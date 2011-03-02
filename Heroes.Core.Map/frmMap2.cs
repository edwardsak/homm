using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Heroes.Core.Map
{
    public partial class frmMap2 : Form
    {
        public frmMap2()
        {
            InitializeComponent();
        }

        private void frmMap2_Load(object sender, EventArgs e)
        {

        }

        void Draw()
        {
            Bitmap bmpbg = new Bitmap(Application.StartupPath + @"\Image\Map\2.bmp");
            //using (Graphics g = new Graphics
        }
    }
}
