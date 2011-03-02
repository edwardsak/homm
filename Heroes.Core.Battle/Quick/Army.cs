using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Battle.Armies
{
    public class Army : Heroes.Core.Army
    {
        public Heroes.Core.Battle.Characters.ArmySideEnum _armySide;

        public bool _isBeginTurn;
        public bool _isEndTurn;
        public bool _isDead;
        public bool _isWait;
        public bool _isDefend;

        public bool _isHover;

        // character is standing on this cell
        public Heroes.Core.Battle.Terrains.Cell _cell;

        public Army()
        {
            _isDead = false;
            _isWait = false;
            _isDefend = false;

            _cell = null;
        }

    }
}
