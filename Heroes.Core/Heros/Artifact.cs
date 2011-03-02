using System;
using System.Collections.Generic;
using System.Text;

namespace Heroes.Core.Heros
{
    [Serializable]
    public class Artifact
    {
        public int _id;
        public string _name;
        public string _description;
        public string _infoImgFileName;

        public EquipPartEnum _equipPart;
        public ArtifactLevelEnum _level;

        public int _attack;
        public int _defense;
        public int _power;
        public int _knowledge;

        public int _skillId;
        public int _effect;

        public int _duration;
        public int _speed;
        public int _life;
        public int _morale;
        public int _luck;

        public int _wood;
        public int _mercury;
        public int _ore;
        public int _sulfur;
        public int _crystal;
        public int _gem;
        public int _gold;

        public bool _isEquiped;

        public Artifact()
        {
            _id = 0;
            _name = "";
            _description = "";
            _infoImgFileName = "";

            _isEquiped = false;
        }

        public void CopyFrom(Artifact a)
        {
            this._id = a._id;
            this._name = a._name;
            this._description = a._description;
            this._infoImgFileName = a._infoImgFileName;

            this._equipPart = a._equipPart;
            this._level = a._level;

            this._attack = a._attack;
            this._defense = a._defense;
            this._power = a._power;
            this._knowledge = a._knowledge;

            this._isEquiped = a._isEquiped;
        }

    }

    public enum ArtifactIdEnum
    { 
        ArmageddonsBlade = 1,
        BlackshardOfTheDeadKnight,
        CentaursAxe,
        GreaterGnollsFlail,
        OgresClubOfHavoc,
        RedDragonFlameTongue,
        SwordOfJudgement,
        SwordOfHellfire,
        TitansGladius,
        BucklerOfTheGnollKing,
        DragonScaleShield,
        LionsShieldOfCourage,
        SentinelsShield,
        ShieldOfTheDamned,
        ShieldOfTheDwarvenLords,
        ShieldOfTheYawningDead
    }

    public enum EquipPartEnum
    {
        RightHand = 1, 
        LeftHand,
        Torso,
        Head,
        Feet,
        Neck,
        Shoulder,
        Ring,
        Misc,
        BackPack
    }

    public enum ArtifactLevelEnum
    {
        None = 0,
        Treasure,
        Minor,
        Major,
        Relic,
        Unique
    }

}
