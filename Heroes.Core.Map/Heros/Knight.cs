using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Map.Heros
{
    public class Knight : Heroes.Core.Map.Hero
    {
        public Knight()
        {
            Init(0, null);
        }

        public Knight(int id, Player player)
            : base(id, player)
        {
            Init(id, player);
        }

        private void Init(int id, Player player)
        {
            this._id = id;
            this._player = player;
            this._heroType = HeroTypeEnum.Knight;
        }

    }
}
