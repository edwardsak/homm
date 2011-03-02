using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Map.Armies
{
    public class Angel : Heroes.Core.Army
    {
        public Angel()
        {
            _id = (int)Heroes.Core.ArmyIdEnum.Angel;
            _speed = 10;
        }
    }
}
