using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Heroes.Core.Heros
{
    public class SpellAnimationData : AnimationData
    {
        public const string PURPOSE_ON_ARMY = "O";
        public const string PURPOSE_MISSILE = "M";
        public const string PURPOSE_HIT = "H";

        public Point _point;
        public Size _size;

        public SpellAnimationData()
        {
            _point = new Point(0, 0);
            _size = new Size(0, 0);
        }

    }
}
