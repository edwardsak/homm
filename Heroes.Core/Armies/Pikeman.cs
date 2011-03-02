using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Armies
{
    public class Pikeman : Heroes.Core.Army
    {
        public Pikeman()
        {
            _id = (int)Heroes.Core.ArmyIdEnum.Pikeman;
            _minDamage = 1;
            _maxDamage = 3;
            _basicAttack = 4;
            _basicDefense = 5;
            _attack = _basicAttack;
            _defense = _basicDefense;
            _health = 10;
            _cost = 60;
            _noOfShot = 0;
            _moveType = MoveTypeEnum.Ground;
            _speed = 4;
            _hasRangeAttack = false;
            _handToHandPenalty = false;
            _noOfRetaliate = 1;
            _noOfAttack = 1;
            _exp = 10;
        }

    }
}
