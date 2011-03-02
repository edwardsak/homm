using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Heros
{
    public class Knight : Hero
    {
        public Knight()
        {
            this._heroType = HeroTypeEnum.Knight;
            this._attack = 2;
            this._defense = 2;
            this._power = 1;
            this._knowledge = 1;
            this._maxSpellPoint = 10;
            this._spellPointLeft = this._maxSpellPoint;
            this._experience = 90;
            this._level = 1;
        }

    }
}
