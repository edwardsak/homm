using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Heroes.Core
{
    [Serializable]
    public class Hero
    {
        public int _id;
        public int _raceId;
        public int _playerId;
        public string _name;
        public string _description;
        public SexEnum _sex;
        public HeroTypeEnum _heroType;
        public SpecialtyIdEnum _specialty;
        public int _experience;
        public int _level;

        public int _basicAttack;
        public int _basicDefense;
        public int _basicPower;
        public int _basicKnowledge;

        public int _attack;
        public int _defense;
        public int _power;
        public int _knowledge;
        public int _speed;      // army speed modifier
        public int _health;     // army health modifier
        
        public int _maxSpellPoint;
        public int _spellPointLeft;
        public string _infoImgFileName;

        public int _movementPoint;
        public int _movementPointLeft;

        public Hashtable _skills;

        public Hashtable _spells;
        public Hashtable _spellDmgKEles;    // spell damage modifier, key = element, value = int

        public Hashtable _artifacts;
        public Hashtable _artifactKEquips;  // key = EquipPart, value = Artifact[]
        public ArrayList _artifactUnequips;

        public Hashtable _armyKSlots;   // key = SlotNo
        public Player _player;

        public Hero()
        {
            Init(0);
        }

        public Hero(int id)
        {
            Init(id);
        }

        private void Init(int id)
        {
            _id = id;
            _name = "";
            _playerId = 0;
            _sex = SexEnum.Male;
            _level = 1;
            _experience = 0;

            _basicAttack = 0;
            _basicDefense = 0;
            _basicPower = 0;
            _basicKnowledge = 0;

            _attack = 0;
            _defense = 0;
            _power = 0;
            _knowledge = 0;
            _speed = 0;
            _health = 0;
            
            _maxSpellPoint = 0;
            _spellPointLeft = 0;

            _movementPoint = 0;
            _movementPointLeft = 0;

            _skills = new Hashtable();
            
            _spells = new Hashtable();
            _spellDmgKEles = new Hashtable();

            _artifacts = new Hashtable();
            _artifactKEquips = new Hashtable();
            _artifactKEquips.Add(Heroes.Core.Heros.EquipPartEnum.LeftHand, new Heroes.Core.Heros.Artifact[1]);
            _artifactKEquips.Add(Heroes.Core.Heros.EquipPartEnum.RightHand, new Heroes.Core.Heros.Artifact[1]);
            _artifactKEquips.Add(Heroes.Core.Heros.EquipPartEnum.Head, new Heroes.Core.Heros.Artifact[1]);
            _artifactKEquips.Add(Heroes.Core.Heros.EquipPartEnum.Torso, new Heroes.Core.Heros.Artifact[1]);
            _artifactKEquips.Add(Heroes.Core.Heros.EquipPartEnum.Ring, new Heroes.Core.Heros.Artifact[2]);
            _artifactKEquips.Add(Heroes.Core.Heros.EquipPartEnum.Neck, new Heroes.Core.Heros.Artifact[1]);
            _artifactKEquips.Add(Heroes.Core.Heros.EquipPartEnum.Shoulder, new Heroes.Core.Heros.Artifact[1]);
            _artifactKEquips.Add(Heroes.Core.Heros.EquipPartEnum.Feet, new Heroes.Core.Heros.Artifact[1]);
            _artifactKEquips.Add(Heroes.Core.Heros.EquipPartEnum.Misc, new Heroes.Core.Heros.Artifact[6]);
            _artifactUnequips = new ArrayList();

            _armyKSlots = new Hashtable();
            _player = null;
        }

        public void CopyFrom(Hero hero)
        {
            this._id = hero._id;
            this._playerId = hero._playerId;
            this._name = hero._name;
            this._description = hero._description;
            this._sex = hero._sex;
            this._heroType = hero._heroType;

            this._basicAttack = hero._basicAttack;
            this._basicDefense = hero._basicDefense;
            this._basicPower = hero._basicPower;
            this._basicKnowledge = hero._basicKnowledge;

            this._attack = hero._attack;
            this._defense = hero._defense;
            this._power = hero._power;
            this._knowledge = hero._knowledge;
            this._speed = hero._speed;
            this._health = hero._health;
            
            this._experience = hero._experience;
            this._level = hero._level;
            this._maxSpellPoint = hero._maxSpellPoint;
            this._spellPointLeft = hero._spellPointLeft;
            this._infoImgFileName = hero._infoImgFileName;

            this._movementPoint = hero._movementPoint;
            this._movementPointLeft = hero._movementPointLeft;
        }

        public void CopyAllFrom(Hero hero)
        {
            CopyFrom(hero);
            
            this._player = new Player();
            this._player.CopyFrom(hero._player);
            
            foreach (Army army in hero._armyKSlots.Values)
            {
                this._armyKSlots.Add(army._slotNo, army);
            }

            foreach (Spell spell in hero._spells.Values)
            {
                Spell spell2 = new Spell();
                spell2.CopyFrom(spell);
                this._spells.Add(spell2._id, spell2);
            }

            foreach (Skill skill in hero._skills.Values)
            {
                Skill skill2 = new Skill();
                skill2.CopyFrom(skill);
                this._skills.Add(skill2._id, skill2);
            }
        }

        public string GetHeroTypeName()
        {
            return GetHeroTypeName(this._heroType);
        }

        public static string GetHeroTypeName(HeroTypeEnum heroType)
        {
            switch (heroType)
            {
                case HeroTypeEnum.Knight:
                    return "Knight";
                case HeroTypeEnum.Cleric:
                    return "Cleric";
                default:
                    return "";
            }
        }

        public bool IsLevelUp()
        {
            if (!Setting._heroExpKLvs.ContainsKey(this._level + 1)) return false;

            int expReq = (int)Setting._heroExpKLvs[this._level + 1];
            if (this._experience > expReq) return true;
            else return false;
        }

        public void CalculateMaxSpellPoint()
        {
            decimal intelligentBonus = 0m;
            if (_skills.ContainsKey((int)SkillIdEnum.Intelligence))
            {
                Skill skill = (Skill)_skills[(int)SkillIdEnum.Intelligence];
                intelligentBonus = (decimal)skill._effects[skill._level] / 100m;
            }

            decimal sp = (decimal)this._knowledge * 10m * (1m + intelligentBonus);
            this._maxSpellPoint = (int)decimal.Truncate(sp);
        }

        public void AddAttributeToArmies()
        {
            foreach (Army army in _armyKSlots.Values)
            {
                army.AddAttribute(this);
            }
        }

        public void CalculateSpellDamage()
        {
            foreach (Spell spell in _spells.Values)
            {
                spell.CalculateDamage(this);
            }
        }

        public void CalculateSpellCost()
        {
            foreach (Spell spell in _spells.Values)
            {
                spell.CalculateCost(this);
            }
        }

        public void AddArtifacts(Heroes.Core.Heros.Artifact a)
        {
            this._artifacts.Add(a._id, a);

            Heroes.Core.Heros.Artifact[] artifacts = (Heroes.Core.Heros.Artifact[])this._artifactKEquips[a._equipPart];

            for (int i = 0; i < artifacts.Length; i++)
            {
                if (artifacts[i] == null)
                {
                    artifacts[i] = a;
                    return;
                }
            }

            this._artifactUnequips.Add(a);
        }

        public void CalculateAttribute()
        {
            this._attack = this._basicAttack;
            this._defense = this._basicDefense;
            this._power = this._basicPower;
            this._knowledge = this._basicKnowledge;
            this._speed = 0;
            this._health = 0;

            this._spellDmgKEles.Clear();

            foreach (Heroes.Core.Heros.Artifact[] lst in _artifactKEquips.Values)
            {
                foreach (Heroes.Core.Heros.Artifact a in lst)
                {
                    if (a == null) continue;

                    this._attack += a._attack;
                    this._defense += a._defense;
                    this._power += a._power;
                    this._knowledge += a._knowledge;
                    this._speed += a._speed;
                    this._health += a._life;

                    if (a._skillId == (int)SkillIdEnum.AirMagic)
                        this._spellDmgKEles.Add(ElementTypeEnum.Air, a._effect);
                    else if (a._skillId == (int)SkillIdEnum.FireMagic)
                        this._spellDmgKEles.Add(ElementTypeEnum.Fire, a._effect);
                    else if (a._skillId == (int)SkillIdEnum.EarthMagic)
                        this._spellDmgKEles.Add(ElementTypeEnum.Earth, a._effect);
                    else if (a._skillId == (int)SkillIdEnum.WaterMagic)
                        this._spellDmgKEles.Add(ElementTypeEnum.Water, a._effect);
                }
            }
        }

        public void CalculateAll()
        {
            CalculateAttribute();
            CalculateMaxSpellPoint();
            AddAttributeToArmies();
            CalculateSpellDamage();
            CalculateSpellCost();
        }
    }

    public enum SexEnum
    {
        Male = 1,
        Female
    }

    public enum SpecialtyIdEnum
    {
        Griffin = 1
    }

}
