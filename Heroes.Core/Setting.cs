using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.IO;

namespace Heroes.Core
{
    public class Setting
    {
        public static string _appStartupPath;

        public static Hashtable _races;
        public static Hashtable _buildings;

        public static Hashtable _armies;
        public static Dictionary<int, Hashtable> _armyKLevelKIds;
        
        public static Hashtable _heros;
        public static Hashtable _mineTypes;
        public static Hashtable _spells;

        public static Hashtable _artifacts;
        public static Dictionary<Heroes.Core.Heros.ArtifactLevelEnum, Hashtable> _artifactKLevelKIds;
        
        public static Hashtable _skills;
        public static Hashtable _heroExpKLvs;
        public static Dictionary<int, Hashtable> _armyAnimationKIdKPurs;
        public static Dictionary<int, Hashtable> _spellAnimationKIdKPurs;
        public static Dictionary<int, Dictionary<int, Hashtable>> _heroAnimationKIdKSexKPurs;

        public static bool GetSettings(string appStartupPath)
        {
            _appStartupPath = appStartupPath;

            if (!GetRaces(out _races))
            {
                return false;
            }

            if (!GetBuildings(out _buildings))
            {
                return false;
            }

            if (!GetHeros(out _heros))
            {
                return false;
            }

            if (!GetArmies(out _armies, out _armyKLevelKIds))
            {
                return false;
            }

            if (!GetMineTypes(out _mineTypes))
            {
                return false;
            }

            if (!GetSpells(out _spells))
            {
                return false;
            }

            if (!GetArtifacts(out _artifacts, out _artifactKLevelKIds))
            {
                return false;
            }

            if (!GetSkills(out _skills))
            {
                return false;
            }

            if (!GetHeroSkills())
            {
                return false;
            }

            if (!GetHeroLevelExps(out _heroExpKLvs))
            {
                return false;
            }

            if (!GetAnimations(string.Format(@"{0}\Data\ArmyAnimation.txt", _appStartupPath), out _armyAnimationKIdKPurs))
            {
                return false;
            }

            if (!GetSpellAnimations(string.Format(@"{0}\Data\SpellAnimation.txt", _appStartupPath), out _spellAnimationKIdKPurs))
            {
                return false;
            }

            if (!GetHeroAnimations(string.Format(@"{0}\Data\HeroAnimation.txt", _appStartupPath), out _heroAnimationKIdKSexKPurs))
            {
                return false;
            }

            return true;
        }

        private static bool GetRaces(out Hashtable lst)
        {
            lst = new Hashtable();

            using (StreamReader sr = new StreamReader(string.Format(@"{0}\Data\Race.txt", _appStartupPath)))
            {
                string strLine = "";
                strLine = sr.ReadLine();    // 1st line is column header

                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();

                    // id,name,desc
                    string[] datas = strLine.Split(new char[] { ',' }, 3);

                    Race dr = new Race();
                    dr._id = System.Convert.ToInt32(datas[0]);
                    dr._name = datas[1];
                    dr._description = datas[2];

                    lst.Add(dr._id, dr);
                }
            }

            return true;
        }

        private static bool GetBuildings(out Hashtable lst)
        {
            lst = new Hashtable();

            using (StreamReader sr = new StreamReader(string.Format(@"{0}\Data\Building.txt", _appStartupPath)))
            {
                string strLine = "";
                strLine = sr.ReadLine();    // 1st line is column header

                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();

                    // Id,raceId,name,w,m,o,s,c,g,gold,isdwelling,growth,hordeId,gw,gm,go,gs,gc,gg,ggold,imgFileName,x,y,orderSeq,desc
                    string[] datas = strLine.Split(new char[] { ',' }, 25);

                    Building dr = new Building();
                    dr._id = System.Convert.ToInt32(datas[0]);
                    dr._raceId = System.Convert.ToInt32(datas[1]);
                    dr._name = datas[2];
                    
                    dr._wood = System.Convert.ToInt32(datas[3]);
                    dr._mercury = System.Convert.ToInt32(datas[4]);
                    dr._ore = System.Convert.ToInt32(datas[5]);
                    dr._sulfur = System.Convert.ToInt32(datas[6]);
                    dr._crystal = System.Convert.ToInt32(datas[7]);
                    dr._gem = System.Convert.ToInt32(datas[8]);
                    dr._gold = System.Convert.ToInt32(datas[9]);

                    dr._isDwelling = System.Convert.ToBoolean(System.Convert.ToInt32(datas[10]));
                    dr._growth = System.Convert.ToInt32(datas[11]);
                    dr._hordeId = System.Convert.ToInt32(datas[12]);

                    dr._woodGen = System.Convert.ToInt32(datas[13]);
                    dr._mercuryGen = System.Convert.ToInt32(datas[14]);
                    dr._oreGen = System.Convert.ToInt32(datas[15]);
                    dr._sulfurGen = System.Convert.ToInt32(datas[16]);
                    dr._crystalGen = System.Convert.ToInt32(datas[17]);
                    dr._gemGen = System.Convert.ToInt32(datas[18]);
                    dr._goldGen = System.Convert.ToInt32(datas[19]);

                    dr._imgFileName = string.Format(@"{0}\Images\Castle\Buildings\{1}", _appStartupPath, datas[20]);
                    dr._point.X = System.Convert.ToInt32(datas[21]);
                    dr._point.Y = System.Convert.ToInt32(datas[22]);
                    dr._orderSeq = System.Convert.ToInt32(datas[23]);

                    dr._description = datas[24];

                    lst.Add(dr._id, dr);
                }
            }
            return true;
        }

        private static bool GetHeros(out Hashtable lst)
        {
            lst = new Hashtable();

            using (StreamReader sr = new StreamReader(string.Format(@"{0}\Data\Hero.txt", _appStartupPath)))
            {
                string strLine = "";
                strLine = sr.ReadLine();    // 1st line is column header
                
                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();
                    
                    // id,race,type,name,sex,specialId,a,d,p,k,img,desc
                    string[] datas = strLine.Split(new char[] { ',' }, 12);

                    Hero dr = new Hero();
                    dr._id = System.Convert.ToInt32(datas[0]);
                    dr._raceId = System.Convert.ToInt32(datas[1]);
                    dr._heroType = (HeroTypeEnum)System.Convert.ToInt32(datas[2]);
                    dr._name = datas[3];
                    dr._sex = (SexEnum)System.Convert.ToInt32(datas[4]);
                    //dr._specialty = (SpecialtyIdEnum)System.Convert.ToInt32(datas[5]);
                    
                    dr._basicAttack = System.Convert.ToInt32(datas[6]);
                    dr._basicDefense = System.Convert.ToInt32(datas[7]);
                    dr._basicPower = System.Convert.ToInt32(datas[8]);
                    dr._basicKnowledge = System.Convert.ToInt32(datas[9]);

                    dr._attack = dr._basicAttack;
                    dr._defense = dr._basicDefense;
                    dr._power = dr._basicPower;
                    dr._knowledge = dr._basicKnowledge;

                    dr._infoImgFileName = string.Format(@"{0}\Images\Core\Heros\Info\{1}", _appStartupPath, datas[10]);
                    dr._description = datas[11];

                    lst.Add(dr._id, dr);
                }
            }

            return true;
        }

        private static bool GetArmies(out Hashtable lst, out Dictionary<int, Hashtable> armyKLevelKIds)
        {
            lst = new Hashtable();
            armyKLevelKIds = new Dictionary<int, Hashtable>();

            using (StreamReader sr = new StreamReader(string.Format(@"{0}\Data\Army.txt", _appStartupPath)))
            {
                string strLine = "";
                strLine = sr.ReadLine();    // 1st line is column header

                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();

                    // id,race,name,w,m,o,s,c,g,gold,lv,exp,a,d,minDmg,maxDmg,h,speed,canShot,NoofShot,IsUnlimitedShot,NoOfRetailate,IsUnlimitRetaliate,noOfAttack,moveType,handToHandPenalty,isbig,slotImg,desc
                    string[] datas = strLine.Split(new char[] { ',' }, 29);

                    Army dr = new Army();
                    dr._id = System.Convert.ToInt32(datas[0]);
                    dr._raceId = System.Convert.ToInt32(datas[1]);
                    dr._name = datas[2];
                    
                    dr._wood = System.Convert.ToInt32(datas[3]);
                    dr._mercury = System.Convert.ToInt32(datas[4]);
                    dr._ore = System.Convert.ToInt32(datas[5]);
                    dr._sulfur = System.Convert.ToInt32(datas[6]);
                    dr._crystal = System.Convert.ToInt32(datas[7]);
                    dr._gem = System.Convert.ToInt32(datas[8]);
                    dr._gold = System.Convert.ToInt32(datas[9]);

                    dr._level = System.Convert.ToInt32(datas[10]);
                    dr._experience = System.Convert.ToInt32(datas[11]);

                    dr._basicAttack = System.Convert.ToInt32(datas[12]);
                    dr._basicDefense = System.Convert.ToInt32(datas[13]);
                    dr._attack = dr._basicAttack;
                    dr._defense = dr._basicDefense;

                    dr._minDamage = System.Convert.ToInt32(datas[14]);
                    dr._maxDamage = System.Convert.ToInt32(datas[15]);
                    
                    dr._basicHealth = System.Convert.ToInt32(datas[16]);
                    dr._health = dr._basicHealth;
                    
                    dr._basicSpeed = System.Convert.ToInt32(datas[17]);
                    dr._speed = dr._basicSpeed;

                    dr._hasRangeAttack = System.Convert.ToBoolean(System.Convert.ToInt32(datas[18]));
                    dr._noOfShot = System.Convert.ToInt32(datas[19]);
                    dr._hasUnlimitedAmmo = System.Convert.ToBoolean(System.Convert.ToInt32(datas[20]));
                    dr._noOfRetaliate = System.Convert.ToInt32(datas[21]);
                    dr._hasUnlimitedRetaliate = System.Convert.ToBoolean(System.Convert.ToInt32(datas[22]));
                    dr._noOfAttack = System.Convert.ToInt32(datas[23]);
                    dr._moveType = (MoveTypeEnum)System.Convert.ToInt32(datas[24]);
                    dr._handToHandPenalty = System.Convert.ToBoolean(System.Convert.ToInt32(datas[25]));
                    dr._isBig = System.Convert.ToBoolean(System.Convert.ToInt32(datas[26]));

                    dr._slotImgFileName = string.Format(@"{0}\Images\Core\Armies\Slot\{1}", _appStartupPath, datas[27]);

                    dr._description = datas[28];

                    lst.Add(dr._id, dr);

                    // group by level
                    Hashtable armyKIds = null;
                    if (armyKLevelKIds.ContainsKey(dr._level))
                        armyKIds = armyKLevelKIds[dr._level];
                    else
                    {
                        armyKIds = new Hashtable();
                        armyKLevelKIds.Add(dr._level, armyKIds);
                    }

                    armyKIds.Add(dr._id, dr);
                }
            }

            return true;
        }

        private static bool GetMineTypes(out Hashtable lst)
        {
            lst = new Hashtable();

            using (StreamReader sr = new StreamReader(string.Format(@"{0}\Data\MineType.txt", _appStartupPath)))
            {
                string strLine = "";
                strLine = sr.ReadLine();    // 1st line is column header

                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();

                    // id,name,w,m,o,s,c,g,gold,desc
                    string[] datas = strLine.Split(new char[] { ',' }, 10);

                    Mine dr = new Mine();
                    dr._id = System.Convert.ToInt32(datas[0]);
                    dr._name = datas[1];
                    dr._wood = System.Convert.ToInt32(datas[2]);
                    dr._mercury = System.Convert.ToInt32(datas[3]);
                    dr._ore = System.Convert.ToInt32(datas[4]);
                    dr._sulfur = System.Convert.ToInt32(datas[5]);
                    dr._crystal = System.Convert.ToInt32(datas[6]);
                    dr._gem = System.Convert.ToInt32(datas[7]);
                    dr._gold = System.Convert.ToInt32(datas[8]);
                    dr._description = datas[9];

                    lst.Add(dr._id, dr);
                }
            }

            return true;
        }

        private static bool GetSpells(out Hashtable lst)
        {
            lst = new Hashtable();

            using (StreamReader sr = new StreamReader(string.Format(@"{0}\Data\Spell.txt", _appStartupPath)))
            {
                string strLine = "";
                strLine = sr.ReadLine();    // 1st line is column header

                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();

                    // id,name,eleType,level,SpellType,cost,duration,baseDamage,Dmg1,Dmg2,Dmg3,all,summon,isMissile,target,isDamage,bookImgFileName,desc
                    string[] datas = strLine.Split(new char[] { ',' }, 19);

                    Spell dr = new Spell();
                    dr._id = System.Convert.ToInt32(datas[0]);
                    dr._name = datas[1];
                    dr._elementType = (ElementTypeEnum)System.Convert.ToInt32(datas[2]);
                    dr._level = System.Convert.ToInt32(datas[3]);
                    dr._spellType = (SpellTypeEnum)System.Convert.ToInt32(datas[4]);

                    dr._basicCost = System.Convert.ToInt32(datas[5]);
                    dr._cost = dr._basicCost;
                    
                    dr._duration = System.Convert.ToInt32(datas[6]);
                    dr._basicDamage = System.Convert.ToInt32(datas[7]);
                    dr._damageLevels[0] = System.Convert.ToInt32(datas[8]);
                    dr._damageLevels[1] = System.Convert.ToInt32(datas[9]);
                    dr._damageLevels[2] = System.Convert.ToInt32(datas[10]);
                    dr._isAll = System.Convert.ToBoolean(System.Convert.ToInt32(datas[11]));
                    dr._isSummon = System.Convert.ToBoolean(System.Convert.ToInt32(datas[12]));
                    dr._isMissile = System.Convert.ToBoolean(System.Convert.ToInt32(datas[13]));
                    dr._isHit = System.Convert.ToBoolean(System.Convert.ToInt32(datas[14]));
                    dr._targetType = (SpellTargetTypeEnum)System.Convert.ToInt32(datas[15]);
                    dr._isDamage = System.Convert.ToBoolean(System.Convert.ToInt32(datas[16]));
                    dr._bookImgFileName = string.Format(@"{0}\Images\Core\Spells\Book\{1}", _appStartupPath, datas[17]);

                    dr._description = datas[18];

                    lst.Add(dr._id, dr);
                }
            }

            return true;
        }

        private static bool GetArtifacts(out Hashtable lst, out Dictionary<Heros.ArtifactLevelEnum, Hashtable> artifactKLevelKIds)
        {
            lst = new Hashtable();
            artifactKLevelKIds = new Dictionary<Heroes.Core.Heros.ArtifactLevelEnum, Hashtable>();

            using (StreamReader sr = new StreamReader(string.Format(@"{0}\Data\Artifact.txt", _appStartupPath)))
            {
                string strLine = "";
                strLine = sr.ReadLine();    // 1st line is column header

                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();

                    // id,name,part,rank,a,d,p,k,skillId,effect,dur,speed,morale,luck,life,w,m,o,s,c,g,gold,img,desc
                    string[] datas = strLine.Split(new char[] { ',' }, 24);

                    Heros.Artifact dr = new Heros.Artifact();
                    dr._id = System.Convert.ToInt32(datas[0]);
                    dr._name = datas[1];
                    dr._equipPart = (Heroes.Core.Heros.EquipPartEnum)System.Convert.ToInt32(datas[2]);
                    dr._level = (Heroes.Core.Heros.ArtifactLevelEnum)System.Convert.ToInt32(datas[3]);

                    dr._attack = System.Convert.ToInt32(datas[4]);
                    dr._defense = System.Convert.ToInt32(datas[5]);
                    dr._power = System.Convert.ToInt32(datas[6]);
                    dr._knowledge = System.Convert.ToInt32(datas[7]);

                    dr._skillId = System.Convert.ToInt32(datas[8]);
                    dr._effect = System.Convert.ToInt32(datas[9]);
                    dr._duration = System.Convert.ToInt32(datas[10]);
                    dr._speed = System.Convert.ToInt32(datas[11]);
                    dr._morale = System.Convert.ToInt32(datas[12]);
                    dr._luck = System.Convert.ToInt32(datas[13]);
                    dr._life = System.Convert.ToInt32(datas[14]);

                    dr._wood = System.Convert.ToInt32(datas[15]);
                    dr._mercury = System.Convert.ToInt32(datas[16]);
                    dr._ore = System.Convert.ToInt32(datas[17]);
                    dr._sulfur = System.Convert.ToInt32(datas[18]);
                    dr._crystal = System.Convert.ToInt32(datas[19]);
                    dr._gem = System.Convert.ToInt32(datas[20]);
                    dr._gold = System.Convert.ToInt32(datas[21]);

                    //if (datas[22] == "0")
                    //{
                    //    System.Diagnostics.Debug.WriteLine("");
                    //}

                    dr._infoImgFileName = string.Format(@"{0}\Images\Core\Artifacts\Info\{1}", _appStartupPath, datas[22]);

                    dr._description = datas[23];

                    lst.Add(dr._id, dr);

                    // group by level
                    Hashtable artifactKIds = null;
                    if (artifactKLevelKIds.ContainsKey(dr._level))
                        artifactKIds = artifactKLevelKIds[dr._level];
                    else
                    {
                        artifactKIds = new Hashtable();
                        artifactKLevelKIds.Add(dr._level, artifactKIds);
                    }

                    artifactKIds.Add(dr._id, dr);
                }
            }

            return true;
        }

        private static bool GetSkills(out Hashtable lst)
        {
            lst = new Hashtable();

            using (StreamReader sr = new StreamReader(string.Format(@"{0}\Data\Skill.txt", _appStartupPath)))
            {
                string strLine = "";
                strLine = sr.ReadLine();    // 1st line is column header

                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();

                    // id,name,b,a,e,fileb,filea,filee,desc
                    string[] datas = strLine.Split(new char[] { ',' }, 9);

                    Skill dr = new Skill();
                    dr._id = System.Convert.ToInt32(datas[0]);
                    dr._name = datas[1];
                    dr._effects[0] = System.Convert.ToInt32(datas[2]);
                    dr._effects[1] = System.Convert.ToInt32(datas[3]);
                    dr._effects[2] = System.Convert.ToInt32(datas[4]);
                    dr._infoImgFileNames[0] = string.Format(@"{0}\Images\Core\Skills\Info\{1}", _appStartupPath, datas[5]);
                    dr._infoImgFileNames[1] = string.Format(@"{0}\Images\Core\Skills\Info\{1}", _appStartupPath, datas[6]);
                    dr._infoImgFileNames[2] = string.Format(@"{0}\Images\Core\Skills\Info\{1}", _appStartupPath, datas[7]);

                    dr._description = datas[8];

                    lst.Add(dr._id, dr);
                }
            }

            return true;
        }

        private static bool GetHeroSkills()
        {
            using (StreamReader sr = new StreamReader(string.Format(@"{0}\Data\HeroSkill.txt", _appStartupPath)))
            {
                string strLine = "";
                strLine = sr.ReadLine();    // 1st line is column header

                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();

                    // heroId,skillId,level
                    string[] datas = strLine.Split(new char[] { ',' }, 3);

                    int heroId = System.Convert.ToInt32(datas[0]);
                    int skillId = System.Convert.ToInt32(datas[1]);
                    int level = System.Convert.ToInt32(datas[2]);

                    Heroes.Core.Hero hero = null;
                    if (_heros.ContainsKey(heroId))
                        hero = (Heroes.Core.Hero)_heros[heroId];
                    else
                        continue;

                    Heroes.Core.Skill skill = null;
                    if (_skills.ContainsKey(skillId))
                        skill = (Heroes.Core.Skill)_skills[skillId];
                    else
                        continue;

                    hero._skills.Add(skill._id, skill);
                }
            }

            return true;
        }

        private static bool GetHeroLevelExps(out Hashtable lst)
        {
            lst = new Hashtable();

            using (StreamReader sr = new StreamReader(string.Format(@"{0}\Data\HeroLevelExp.txt", _appStartupPath)))
            {
                string strLine = "";
                strLine = sr.ReadLine();    // 1st line is column header

                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();

                    // level,exp
                    string[] datas = strLine.Split(new char[] { ',' });

                    int level = System.Convert.ToInt32(datas[0]);
                    int exp = System.Convert.ToInt32(datas[1]);

                    lst.Add(level, exp);
                }
            }

            return true;
        }

        private static bool GetAnimations(string fileName, out Dictionary<int, Hashtable> animationKIdKPurs)
        {
            animationKIdKPurs = new Dictionary<int, Hashtable>();

            using (StreamReader sr = new StreamReader(fileName))
            {
                string strLine = "";
                strLine = sr.ReadLine();    // 1st line is column header

                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();

                    // armyId,action,folder,prefix,ext,no1,no2,....
                    string[] datas = strLine.Split(new char[] { ',' });

                    AnimationData dr = new AnimationData();
                    dr._id = System.Convert.ToInt32(datas[0]);
                    dr._purpose = datas[1];
                    dr._folder = datas[2];
                    dr._prefix = datas[3];
                    dr._ext = datas[4];
                    dr._moveSpeed = System.Convert.ToInt32(datas[5]);
                    dr._turnPerFrame = System.Convert.ToInt32(datas[6]);

                    dr._fileNos = new string[datas.Length - 7];
                    for (int i = 7; i < datas.Length; i++)
                    {
                        dr._fileNos[i - 7] = datas[i];
                    }

                    Hashtable lst = null;
                    if (animationKIdKPurs.ContainsKey(dr._id))
                        lst = animationKIdKPurs[dr._id];
                    else
                    {
                        lst = new Hashtable();
                        animationKIdKPurs.Add(dr._id, lst);
                    }

                    lst.Add(dr._purpose, dr);
                }
            }

            return true;
        }

        private static bool GetSpellAnimations(string fileName, out Dictionary<int, Hashtable> animationKIdKPurs)
        {
            animationKIdKPurs = new Dictionary<int, Hashtable>();

            using (StreamReader sr = new StreamReader(fileName))
            {
                string strLine = "";
                strLine = sr.ReadLine();    // 1st line is column header

                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();

                    // armyId,action,folder,prefix,ext,no1,no2,....
                    string[] datas = strLine.Split(new char[] { ',' });

                    Heroes.Core.Heros.SpellAnimationData dr = new Heroes.Core.Heros.SpellAnimationData();
                    dr._id = System.Convert.ToInt32(datas[0]);
                    dr._purpose = datas[1];
                    dr._folder = datas[2];
                    dr._prefix = datas[3];
                    dr._ext = datas[4];
                    dr._moveSpeed = System.Convert.ToInt32(datas[5]);
                    dr._turnPerFrame = System.Convert.ToInt32(datas[6]);

                    dr._point = new System.Drawing.Point(System.Convert.ToInt32(datas[7]), System.Convert.ToInt32(datas[8]));
                    dr._size = new System.Drawing.Size(System.Convert.ToInt32(datas[9]), System.Convert.ToInt32(datas[10]));

                    dr._fileNos = new string[datas.Length - 11];
                    for (int i = 11; i < datas.Length; i++)
                    {
                        dr._fileNos[i - 11] = datas[i];
                    }

                    Hashtable lst = null;
                    if (animationKIdKPurs.ContainsKey(dr._id))
                        lst = animationKIdKPurs[dr._id];
                    else
                    {
                        lst = new Hashtable();
                        animationKIdKPurs.Add(dr._id, lst);
                    }

                    lst.Add(dr._purpose, dr);
                }
            }

            return true;
        }

        private static bool GetHeroAnimations(string fileName, 
            out Dictionary<int, Dictionary<int, Hashtable>> animationKIdKSexKPurs)
        {
            animationKIdKSexKPurs = new Dictionary<int, Dictionary<int, Hashtable>>();

            using (StreamReader sr = new StreamReader(fileName))
            {
                string strLine = "";
                strLine = sr.ReadLine();    // 1st line is column header

                while (!sr.EndOfStream)
                {
                    strLine = sr.ReadLine();

                    // armyId,action,folder,prefix,ext,no1,no2,....
                    string[] datas = strLine.Split(new char[] { ',' });

                    Heros.HeroAnimationData dr = new Heros.HeroAnimationData();
                    dr._id = System.Convert.ToInt32(datas[0]);
                    dr._sex = System.Convert.ToInt32(datas[1]);
                    dr._purpose = datas[2];
                    dr._folder = datas[3];
                    dr._prefix = datas[4];
                    dr._ext = datas[5];
                    dr._turnPerFrame = System.Convert.ToInt32(datas[6]);

                    dr._fileNos = new string[datas.Length - 7];
                    for (int i = 7; i < datas.Length; i++)
                    {
                        dr._fileNos[i - 7] = datas[i];
                    }

                    Dictionary<int, Hashtable> animationKSexKPurs = null;
                    Hashtable lst = null;
                    if (animationKIdKSexKPurs.ContainsKey(dr._id))
                        animationKSexKPurs = animationKIdKSexKPurs[dr._id];
                    else
                    {
                        animationKSexKPurs = new Dictionary<int, Hashtable>();
                        animationKIdKSexKPurs.Add(dr._id, animationKSexKPurs);
                    }

                    if (animationKSexKPurs.ContainsKey(dr._sex))
                        lst = animationKSexKPurs[dr._sex];
                    else
                    {
                        lst = new Hashtable();
                        animationKSexKPurs.Add(dr._sex, lst);
                    }

                    lst.Add(dr._purpose, dr);
                }
            }

            return true;
        }

    }
}
