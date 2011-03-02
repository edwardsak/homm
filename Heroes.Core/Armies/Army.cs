using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core
{
    [Serializable]
    public class Army
    {
        public int _id;
        public int _raceId;
        public string _name;
        public int _playerId;
        public int _heroId;
        public int _monsterId;
        public string _description;

        public int _wood;
        public int _mercury;
        public int _ore;
        public int _sulfur;
        public int _crystal;
        public int _gem;
        public int _gold;

        public int _woodTotal;
        public int _mercuryTotal;
        public int _oreTotal;
        public int _sulfurTotal;
        public int _crystalTotal;
        public int _gemTotal;
        public int _goldTotal;

        public int _level;
        public int _qty;
        public int _qtyLeft;

        public int _basicHealth;
        public int _health;
        public int _healthRemain;

        public int _basicSpeed;
        public int _speed;

        public int _basicAttack;
        public int _basicDefense;
        public int _attack;     // basic attack + hero
        public int _defense;    // basic defense + hero
        public int _minDamage;
        public int _maxDamage;
        
        public int _noOfShot;
        public int _shotRemain;
        public bool _hasUnlimitedAmmo;
        public MoveTypeEnum _moveType;
        public bool _hasRangeAttack;
        public bool _handToHandPenalty;
        public int _experience;
        public int _noOfRetaliate;
        public int _retaliateRemain;
        public bool _hasUnlimitedRetaliate;
        public int _noOfAttack;

        public int _slotNo;     // which slot, from 0 to 6

        public bool _isBig;     // is big creature?
        public bool _isMachine;
        public bool _isBio;
        public bool _isUndead;

        public string _slotImgFileName;

        public Army()
        {
            _id = 0;
            _name = "";
            _playerId = 0;
            _heroId = 0;
            _monsterId = 0;
            _moveType = MoveTypeEnum.Ground;

            _basicHealth = 0;
            _health = 0;
            _healthRemain = 0;

            _basicSpeed = 0;
            _speed = 0;

            _qty = 0;
            _qtyLeft = 0;
            _slotNo = 0;

            _isBig = false;

            _slotImgFileName = "";
        }

        public void CopyFrom(Heroes.Core.Army army)
        {
            this._id = army._id;
            this._name = army._name;
            this._playerId = army._playerId;
            this._heroId = army._heroId;
            this._monsterId = army._monsterId;

            this._gold = army._gold;

            this._slotNo = army._slotNo;
            this._qty = army._qty;
            this._qtyLeft = army._qtyLeft;
            this._basicAttack = army._basicAttack;
            this._basicDefense = army._basicDefense;
            this._attack = army._attack;
            this._defense = army._defense;
            this._minDamage = army._minDamage;
            this._maxDamage = army._maxDamage;
            this._noOfShot = army._noOfShot;
            this._shotRemain = army._shotRemain;
            this._hasUnlimitedAmmo = army._hasUnlimitedAmmo;
            this._hasRangeAttack = army._hasRangeAttack;
            this._moveType = army._moveType;
            this._handToHandPenalty = army._handToHandPenalty;

            this._basicSpeed = army._basicSpeed;
            this._speed = army._speed;

            this._basicHealth = army._basicHealth;
            this._health = army._health;
            this._healthRemain = army._healthRemain;

            this._noOfRetaliate = army._noOfRetaliate;
            this._retaliateRemain = army._retaliateRemain;
            this._hasUnlimitedRetaliate = army._hasUnlimitedRetaliate;
            this._noOfAttack = army._noOfAttack;
            this._experience = army._experience;

            this._isBig = army._isBig;

            this._slotImgFileName = army._slotImgFileName;
        }

        public void AddAttribute(Heroes.Core.Hero hero)
        {
            this._attack = this._basicAttack + hero._attack;
            this._defense = this._basicDefense + hero._defense;
            this._speed = this._basicSpeed + hero._speed;
            this._health = this._basicHealth + hero._health;
        }

        public static int CalculateAmt(int qty, int cost)
        {
            return qty * cost;
        }

        public string GetArmySize()
        {
            return GetArmySize(this._qty);
        }

        public static string GetArmySize(int qty)
        {
            if (qty < 5) return "Few";
            else if (qty < 10) return "Several";
            else if (qty < 20) return "Pack";
            else if (qty < 50) return "Lots";
            else if (qty < 100) return "Horde";
            else if (qty < 250) return "Throng";
            else if (qty < 500) return "Swarm";
            else if (qty < 1000) return "Zounds";
            else return "Legion"; 
        }

        public void CalculateCost()
        {
            _woodTotal = this._wood * this._qty;
            _oreTotal = this._ore * this._qty;
            _gemTotal = this._gem * this._qty;
            _sulfurTotal = this._sulfur * this._qty;
            _crystalTotal = this._crystal * this._qty;
            _mercuryTotal = this._mercury * this._qty;
            _goldTotal = this._gold * this._qty;
        }

        public bool CanBuy(Player player)
        {
            if (player._wood < this._woodTotal) return false;
            if (player._ore < this._oreTotal) return false;
            if (player._gem < this._gemTotal) return false;
            if (player._sulfur < this._sulfurTotal) return false;
            if (player._crystal < this._crystalTotal) return false;
            if (player._mercury < this._mercuryTotal) return false;
            if (player._gold < this._goldTotal) return false;

            return true;
        }

        public void DeductResources(Player player)
        {
            player._wood -= this._woodTotal;
            player._ore -= this._oreTotal;
            player._gem -= this._gemTotal;
            player._sulfur -= this._sulfurTotal;
            player._crystal -= this._crystalTotal;
            player._mercury -= this._mercuryTotal;
            player._gold -= this._goldTotal;
        }
    }
}
