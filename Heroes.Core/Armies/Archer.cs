using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Armies
{
    public class Archer : Heroes.Core.Army
    {
        public Archer()
        {
            _id = (int)Heroes.Core.ArmyIdEnum.Archer;
            _minDamage = 2;
            _maxDamage = 3;
            _basicAttack = 6;
            _basicDefense = 3;
            _attack = _basicAttack;
            _defense = _basicDefense;
            _health = 10;
            _cost = 100;
            _noOfShot = 12;
            _moveType = MoveTypeEnum.Ground;
            _speed = 4;
            _hasRangeAttack = true;
            _handToHandPenalty = true;
            _noOfRetaliate = 1;
            _noOfAttack = 1;
            _exp = 15;
        }

    }
}
