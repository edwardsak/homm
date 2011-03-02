using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Heroes.Core.Map
{
    public class Hero : Heroes.Core.Hero
    {
        public Image _image;
        public Cell _cell;

        public Hero()
        {
            Init(0, null);
        }

        public Hero(int id, Player player)
            : base()
        {
            Init(id, player);
        }

        private void Init(int id, Player player)
        {
            _id = id;
            _player = player;

            this._movementPoint = 2;
            this._movementPointLeft = this._movementPoint;
        }

    }

}
