using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Map.Armies
{
    public class Swordman : Heroes.Core.Army
    {
        public Swordman()
        {
            _id = (int)Heroes.Core.ArmyIdEnum.Swordman;
            _speed = 5;
        }
    }
}
