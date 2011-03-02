using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Heroes.Core
{
    [Serializable]
    public class Spell
    {
        public int _id;
        public string _name;
        public string _description;
        public int _level;
        public int _basicCost;
        public int _cost;
        public int _duration;
        public int _basicDamage;
        public int [] _damageLevels;
        public int _damage;
        public bool _isAll;
        public bool _isSummon;
        public SpellTypeEnum _spellType;
        public ElementTypeEnum _elementType;
        public bool _isMissile;
        public bool _isHit;
        public SpellTargetTypeEnum _targetType;
        public bool _isDamage;
        public string _bookImgFileName;

        public Spell()
        {
            _id = 0;
            _basicCost = 0;

            _basicCost = 0;
            _cost = 0;

            _damage = 0;
            _damageLevels = new int[3];
            _elementType = ElementTypeEnum.None;
            _isMissile = false;
            _isHit = false;
            _isDamage = false;
            _bookImgFileName = "";
        }

        public void CopyFrom(Spell spell)
        {
            this._id = spell._id;
            this._name = spell._name;
            this._elementType = spell._elementType;
            this._spellType = spell._spellType;
            this._level = spell._level;

            this._basicCost = spell._basicCost;
            this._cost = spell._cost;
            
            this._duration = spell._duration;
            this._basicDamage = spell._basicDamage;
            this._damageLevels[0] = spell._damageLevels[0];
            this._damageLevels[1] = spell._damageLevels[1];
            this._damageLevels[2] = spell._damageLevels[2];
            this._damage = spell._damage;
            this._isAll = spell._isAll;
            this._isSummon = spell._isSummon;
            this._description = spell._description;
            this._isMissile = spell._isMissile;
            this._isHit = spell._isHit;
            this._targetType = spell._targetType;
            this._isDamage = spell._isDamage;
            this._bookImgFileName = spell._bookImgFileName;
        }

        public void CalculateDamage(Hero hero)
        {
            int level = GetSkillLevel(hero);
            if (level == 0) level = 1;  // at least has basic damage

            decimal sorceryBonus = 0m;
            if (hero._skills.ContainsKey((int)SkillIdEnum.Sorcery))
            {
                Skill skill = (Skill)hero._skills[(int)SkillIdEnum.Sorcery];
                sorceryBonus = (decimal)skill._effects[skill._level - 1] / 100m;
            }

            decimal artifactBonus = 0m;
            if (hero._spellDmgKEles.ContainsKey(this._elementType))
                artifactBonus = (decimal)hero._spellDmgKEles[this._elementType] / 100m;

            decimal dmg = ((decimal)hero._power * (decimal)this._basicDamage + (decimal)this._damageLevels[level - 1]) 
                * (1m + sorceryBonus) * (1m + artifactBonus);
            this._damage = (int)decimal.Truncate(dmg);
        }

        public void CalculateCost(Hero hero)
        {
            int level = GetSkillLevel(hero);

            decimal reductionBonus = 0m;
            if (level > 0) reductionBonus = 0.2m;

            decimal cost = (decimal)_basicCost * (1m - reductionBonus);
            this._cost = (int)decimal.Truncate(cost);
        }

        private int GetSkillLevel(Hero hero)
        {
            int level = 0;
            if (_elementType == ElementTypeEnum.Air)
            {
                if (hero._skills.ContainsKey((int)SkillIdEnum.AirMagic))
                {
                    Skill skill = (Skill)hero._skills[(int)SkillIdEnum.AirMagic];
                    level = skill._level;
                }
            }
            else if (_elementType == ElementTypeEnum.Fire)
            {
                if (hero._skills.ContainsKey((int)SkillIdEnum.FireMagic))
                {
                    Skill skill = (Skill)hero._skills[(int)SkillIdEnum.FireMagic];
                    level = skill._level;
                }
            }
            else if (_elementType == ElementTypeEnum.Earth)
            {
                if (hero._skills.ContainsKey((int)SkillIdEnum.EarthMagic))
                {
                    Skill skill = (Skill)hero._skills[(int)SkillIdEnum.EarthMagic];
                    level = skill._level;
                }
            }
            else if (_elementType == ElementTypeEnum.Water)
            {
                if (hero._skills.ContainsKey((int)SkillIdEnum.WaterMagic))
                {
                    Skill skill = (Skill)hero._skills[(int)SkillIdEnum.WaterMagic];
                    level = skill._level;
                }
            }

            return level;
        }

    }

    public enum SpellIdEnum
    {
        Haste = 1,
        ViewAir,
        MagicArrow,
        Disguise,
        DisruptingRay,
        Fortune,
        LightningBolt,
        Precision,
        Vision,
        AirShield,
        DestroyUndead,
        ProtectionfromAir,
        Hypnotize,
        ChainLightning,
        Counterstrike,
        DimensionDoor,
        Fly,
        MagicMirror,
        SummonAirElemental,
        Bloodlust,
        Curse,
        Blind,
        FireWall,
        FireBall,
        LandMine,
        Misfortune,
        Armageddon,
        Berzerk,
        FireShield,
        Frenzy,
        Inferno,
        Slayer,
        Sacrifice,
        SummonFireElemental,
        Shield,
        Slow,
        Stoneskin,
        ViewEarth,
        DeathRipple,
        ProtectionFromEarth,
        Quicksand,
        AnimateDead,
        AntiMagic,
        Earthquake,
        ForeField,
        MeteorShower,
        Resurrection,
        Sorrow,
        TownPortal,
        Implosion,
        SummonEarthElemental,
        Bless,
        Cure,
        Dispel,
        ProtectionFromWater,
        SummonBoat,
        IceBolt,
        RemoveObstacle,
        ScuttleBoat,
        Weakness,
        Forgetfulness,
        FrostRing,
        Mirth,
        Teleport,
        Clone,
        Prayer,
        WaterWalk,
        SummonWaterElemental
    }

    public enum ElementTypeEnum
    {
        None = 0,
        Air,
        Fire,
        Water,
        Earth,
        Light,
        Dark,
    }

    public enum SpellTypeEnum
    {
        Combat = 1,
        Adventure
    }

    public enum SpellTargetTypeEnum
    {
        None = 0,
        Ally,
        Enermy,
        All
    }

}
