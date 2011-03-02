using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Heroes.Core.Map.Monsters
{
    public class Monster : Heroes.Core.Monster
    {
        public Image _image;
        public Cell _cell;

        public Monster()
        {
            _image = null;
            _cell = null;
        }

    }
}
