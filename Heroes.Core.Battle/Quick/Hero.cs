using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Battle.Heros
{
    public class Hero : Heroes.Core.Hero
    {
        public Heroes.Core.Battle.Characters.ArmySideEnum _armySide;
        public bool _canCastSpell;

        public Hero()
        {
            _canCastSpell = false;
        }

    }
}
