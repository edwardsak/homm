using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Heroes.Core.Map
{
    public class Cell : PictureBox
    {
        public int _row;
        public int _col;
        public Hero _hero;
        public Heroes.Core.Map.Monsters.Monster _monster;
        public Town _castle;
        public Mine _mine;

        public Cell()
        {

        }

        public void Draw()
        {
            if (_hero != null)
                this.Image = _hero._image;
            else if (_monster != null)
                this.Image = _monster._image;
            else if (_mine != null)
                this.Image = _mine._image;
            else if (_castle != null)
                this.Image = _castle._image;
            else
                this.Image = null;

            //this.BorderStyle = BorderStyle.FixedSingle;
        }

    }
}
