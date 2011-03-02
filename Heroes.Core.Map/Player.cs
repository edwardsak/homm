using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;   

namespace Heroes.Core.Map
{
    public class Player : Heroes.Core.Player
    {
        public Image _heroImage;
        public Image _heroHighlight;
        public Image _heroSelect;
        public Image _goldMine;

        public Player(int id)
            : base(id)
        { 
        }

    }
}
