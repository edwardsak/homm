using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Castle.Armies
{
    public class Swordsmen : Heroes.Core.Army
    {
        public const int ID = 4;

        public Swordsmen()
        {
            _id = (int)Heroes.Core.ArmyIdEnum.Swordman;
            _name = "Swordsmen";
        }
    }
}
