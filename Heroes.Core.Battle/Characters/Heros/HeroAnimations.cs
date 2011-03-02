using System;
using System.Collections.Generic;
using System.Text;

using Heroes.Core.Battle.Characters.Graphics;

namespace Heroes.Core.Battle.Characters.Heros
{
    public class HeroAnimations : Animations
    {
        public Animation _standingRightMale;
        public Animation _standingLeftMale;

        public Animation _standingRightFemale;
        public Animation _standingLeftFemale;

        public Animation _startCastSpellRightMale;
        public Animation _startCastSpellLeftMale;
        public Animation _stopCastSpellRightMale;
        public Animation _stopCastSpellLeftMale;

        public Animation _startCastSpellRightFemale;
        public Animation _startCastSpellLeftFemale;
        public Animation _stopCastSpellRightFemale;
        public Animation _stopCastSpellLeftFemale;

        public HeroAnimations()
        {
        }

    }
}
