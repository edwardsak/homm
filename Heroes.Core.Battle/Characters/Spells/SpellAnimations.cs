using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Heroes.Core.Battle.Characters.Graphics;
using Heroes.Core.Battle.Characters.Commands;
using Heroes.Core.Battle.Characters.Heros;

namespace Heroes.Core.Battle.Characters.Spells
{
    public class SpellAnimations : Animations
    {
        public Animation _onArmy;
        public Animation _missileRight;
        public Animation _missileLeft;
        public Animation _hitRight;
        public Animation _hitLeft;

        public SpellAnimations()
        {
        }

    }
}
