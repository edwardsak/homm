using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Castle.Armies
{
    public class PikeMan : Heroes.Core.Army
    {
        public const int ID = 1;

        public PikeMan()
        {
            _id = (int)Heroes.Core.ArmyIdEnum.Pikeman;
            _name = "PikeMan";
        }

    }
}
