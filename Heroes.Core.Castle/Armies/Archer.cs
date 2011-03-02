using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Castle.Armies
{
    public class Archer : Heroes.Core.Army
    {
        public const int ID = 2;

        public Archer()
        {
            _id = (int)Heroes.Core.ArmyIdEnum.Archer;
            _name = "Archer";
        }
    }
}
