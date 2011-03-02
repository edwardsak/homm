using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Castle.Armies
{
    public class Griffin : Heroes.Core.Army
    {
        public const int ID = 3;

        public Griffin()
        {
            _id = (int)Heroes.Core.ArmyIdEnum.Griffin;
            _name = "Griffin";
        }
    }
}
