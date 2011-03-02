using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Remoting
{
    [Serializable]
    public class BattleCommand
    {
        public int _x;
        public int _y;
        public int _button;
        public bool _doubleClick;
        public int _cmdType;
        public Spell _spell;

        public BattleCommand(int x, int y, int button, bool doubleClick, int cmdType, Spell spell)
        {
            _x = x;
            _y = y;
            _button = button;
            _doubleClick = doubleClick;
            _cmdType = cmdType;
            _spell = spell;
        }

    }

}
