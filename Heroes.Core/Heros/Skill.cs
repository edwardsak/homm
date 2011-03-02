using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core
{
    [Serializable]
    public class Skill
    {
        public int _id;
        public string _name;
        public int _level;
        public int[] _effects;
        public string[] _infoImgFileNames;
        public string _description;

        public Skill()
        {
            Init(0);
        }

        public Skill(int id)
        {
            Init(id);
        }

        private void Init(int id)
        {
            _id = id;
            _name = "";
            _level = 1;
            _effects = new int[3];
            _infoImgFileNames = new string[3];
            _description = "";
        }

        public void CopyFrom(Skill skill)
        {
            this._id = skill._id;
            this._name = skill._name;
            this._description = skill._description;
            this._level = skill._level;
            
            this._effects = skill._effects;
            this._infoImgFileNames = skill._infoImgFileNames;
        }

        public string GetLevelName()
        {
            if (_level == 1) return "Basic";
            else if (_level == 2) return "Advance";
            else if (_level == 3) return "Expert";
            else return "";
        }

    }

    public enum SkillIdEnum
    {
        Archery = 1,
        Offence,
        Armourer,
        Resistance,
        Leadership,
        Luck,
        Tactics,
        Artillery,
        Ballistics,
        FirstAid,

        Diplomacy,
        Estates,
        Learning,
        Logistics,
        Pathfinding,
        Scouting,
        Navigation,

        EagleEye,
        Intelligence,
        Mysticism,
        Scholar,
        Sorcery,
        Wisdom,
        AirMagic,
        EarthMagic,
        FireMagic,
        WaterMagic,
        LightMagic,
        DarkMagic
    }

    public enum RaceSkillIdEnum
    {
        Counterstrike = 1,
        Avenger,
        Artificier,
        Gating,
        Necromancy,
        IrresistibleMagic,
        Rage,
        Vitality
    }

    public enum AbilityIdEnum
    {
        SoldierLuck,
        Recruitment,
        Evasion,
        ChillingBones,
        CorroptedSoil,
        ColdSteel,
        Retribution,
        AuraOfSwiftness,
        StandYourGround,
        HellFire,
        GuardianAngle,
        OffensiveFormation,
        DefensiveFormation,
        Preparation,
        FlamingArrows,
        PointBlankShoot
    }

}
