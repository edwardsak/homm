using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Armies
{
    public class Griffin : Heroes.Core.Army
    {
        public Griffin()
        {
            _id = (int)ArmyIdEnum.Griffin;
            _minDamage = 3;
            _maxDamage = 6;
            _basicAttack = 8;
            _basicDefense = 8;
            _attack = _basicAttack;
            _defense = _basicDefense;
            _health = 25;
            _cost = 200;
            _noOfShot = 0;
            _moveType = MoveTypeEnum.Flying;
            _speed = 6;
            _hasRangeAttack = false;
            _handToHandPenalty = false;
            _noOfRetaliate = 2;
            _noOfAttack = 1;
            _exp = 20;

            _isBig = true;
        }

    }
}
