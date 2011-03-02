using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Map.Armies
{
    public class Crusader : Heroes.Core.Army
    {
        public Crusader()
        {
            base._id = (int)Heroes.Core.ArmyIdEnum.Crusader;
            base._speed = 6;
        }

    }
}
