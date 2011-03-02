using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Armies
{
    public class Swordsman : Heroes.Core.Army
    {
        public Swordsman()
        {
            _id = (int)ArmyIdEnum.Swordman;
            _minDamage = 6;
            _maxDamage = 9;
            _basicAttack = 10;
            _basicDefense = 12;
            _attack = _basicAttack;
            _defense = _basicDefense;
            _health = 35;
            _cost = 300;
            _noOfShot = 0;
            _moveType = MoveTypeEnum.Ground;
            _speed = 5;
            _hasRangeAttack = false;
            _handToHandPenalty = false;
            _noOfRetaliate = 1;
            _noOfAttack = 1;
            _exp = 25;

            _isBig = false;
        }

    }
}
